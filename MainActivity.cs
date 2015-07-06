using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech.Tts;
using Android.Util;
using System.Text.RegularExpressions;

namespace tts
{
    [Activity(Label = "@string/main_app_name", MainLauncher = true, Icon = "@drawable/icon")]
    class MainActivity : FragmentActivity, TextToSpeech.IOnInitListener, TextToSpeech.IOnUtteranceCompletedListener
    {
        public List<elemTts> ttslist;
        private readonly int tkey = 1;
        bool key;

        void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
        {
            if (status == OperationResult.Success)
            {
                tt.SetEngineByPackageName("com.svox.pico");
                tt.SetLanguage(Java.Util.Locale.Uk);
                tt.SetOnUtteranceCompletedListener(this);
            }
        }
        void TextToSpeech.IOnUtteranceCompletedListener.OnUtteranceCompleted(string utteranceId)
        {
            if (ttslist.Count != 0 && key)
            {
                int c = Convert.ToInt32(utteranceId);
                Dictionary<string, string> param = new Dictionary<string, string> { { TextToSpeech.Engine.KeyParamUtteranceId, Convert.ToString(c++) } };
                elemTts temp = ttslist[0];
                ttslist.RemoveAt(0);
                ttsListFragment frag = SupportFragmentManager.FindFragmentById(Resource.Id.tts_list_fragment) as ttsListFragment;
                frag.OnResume();
                tt.SetSpeechRate(temp.tts);
                tt.SetPitch(temp.ttt);
                tt.Speak(temp.text, QueueMode.Add, param);
                Log.Verbose("TextToSpeach", "TextToSpeach начал чтение новой фразы");
            }
        }
        TextToSpeech tt;
        Context context;
        protected override void OnPause()
        {
            base.OnPause();
            key = false;
        }
        protected override void OnResume()
        {
            base.OnResume();
            key = true;
            if (ttslist.Count != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string> { { TextToSpeech.Engine.KeyParamUtteranceId, "1" } };
                tt.PlaySilence(10, QueueMode.Add, param);
            }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.main);
            key = true;
            EditText tb1 = FindViewById<EditText>(Resource.Id.text);
            SeekBar ss = FindViewById<SeekBar>(Resource.Id.speed);
            TextView sst = FindViewById<TextView>(Resource.Id.textSpeed);
            SeekBar sp = FindViewById<SeekBar>(Resource.Id.ton);
            TextView spt = FindViewById<TextView>(Resource.Id.textton);
            Button button1 = FindViewById<Button>(Resource.Id.buttonread);
            ttsListFragment frag = SupportFragmentManager.FindFragmentById(Resource.Id.tts_list_fragment) as ttsListFragment;
            ttslist = new List<elemTts>();
            ss.Progress = sp.Progress = 127;
            sst.Text = spt.Text = "0,5";
            context = button1.Context;

            tt = new TextToSpeech(this, this);
            tt.SetPitch(1.25f);
            tt.SetSpeechRate(1f);
            sp.StopTrackingTouch += (object sender, SeekBar.StopTrackingTouchEventArgs e) =>
            {
                var seek = sender as SeekBar;
                var progress = seek.Progress / 255f;
                tt.SetPitch(seek.Progress / 170f + 0.5f);
                spt.Text = progress.ToString("F2");
            };
            ss.StopTrackingTouch += (object sender, SeekBar.StopTrackingTouchEventArgs e) =>
            {
                var seek = sender as SeekBar;
                var progress = seek.Progress / 255f;
                tt.SetSpeechRate(seek.Progress / 255f + 0.1f);
                sst.Text = progress.ToString("F2");
            };
            button1.Click += delegate
            {
                try
                {
                    if (new Regex(@"[А-Яа-я]").Match(tb1.Text).Success)
                    {
                        tb1.FindFocus();
                        throw new Exception("возможно чтение только символов английского алфавита");
                    }

                    ttslist.Add(new elemTts(tb1.Text, ss.Progress / 255f + 0.1f, sp.Progress / 170f + 0.5f));
                    frag.OnResume();
                    if (ttslist.Count == 1)
                    {
                        Dictionary<string, string> param = new Dictionary<string, string> { { TextToSpeech.Engine.KeyParamUtteranceId, "1" } };
                        tt.PlaySilence(10, QueueMode.Add, param);
                    }
                }
                catch (Exception E)
                {
                    Toast.MakeText(this, E.Message, ToastLength.Short).Show();
                }
            };
            Button button2 = FindViewById<Button>(Resource.Id.buttonload);
            button2.Click += delegate
            {
                StartActivityForResult(new Intent(this,typeof(FilePickerActivity)),tkey);   
            };
        }
       
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
                if (requestCode==tkey)
                {
                    EditText tb1 = FindViewById<EditText>(Resource.Id.text);
                    tb1.Text = File.ReadAllText(data.GetStringExtra("1"));
                    Log.Verbose("FileListFragment", "файл {0} был открыт.", data.GetStringExtra("1"));
                    Toast.MakeText(this, "файл выбран" + data.GetStringExtra("1"), ToastLength.Short).Show();
                }
                else
                {
                    ttslist[data.GetIntExtra("4",0)] = new elemTts(data.GetStringExtra("1"), data.GetIntExtra("2",0) / 255f + 0.1f,data.GetIntExtra("3",0) / 170f + 0.5f);
                }
        }
    }
}