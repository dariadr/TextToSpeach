using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace tts
{
    [Activity(Label = "Dialog")]
    public class Dialog : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.set_params);
            EditText text=FindViewById<EditText>(Resource.Id.text);
            SeekBar ttssp = FindViewById<SeekBar>(Resource.Id.speed);
            SeekBar ttston = FindViewById<SeekBar>(Resource.Id.ton);
            Button ok = FindViewById<Button>(Resource.Id.ok);
            Button cancel = FindViewById<Button>(Resource.Id.cancel);
            TextView sst = FindViewById<TextView>(Resource.Id.textSpeed);
            TextView spt = FindViewById<TextView>(Resource.Id.textton);

            text.Text = Intent.GetStringExtra("1");
            ttssp.Progress = Convert.ToInt32((Intent.GetFloatExtra("2", 0f) - 0.1f)*255f);
            ttston.Progress = Convert.ToInt32((Intent.GetFloatExtra("3", 0f) - 0.5f) * 170f);
            sst.Text = (ttssp.Progress / 255f).ToString("F2");
            spt.Text = (ttston.Progress / 255f).ToString("F2");
            ttston.StopTrackingTouch += (object sender, SeekBar.StopTrackingTouchEventArgs e) =>
            {
                var seek = sender as SeekBar;
                var progress = seek.Progress / 255f;
                spt.Text = progress.ToString("F2");
            };
            ttssp.StopTrackingTouch += (object sender, SeekBar.StopTrackingTouchEventArgs e) =>
            {
                var seek = sender as SeekBar;
                var progress = seek.Progress / 255f;
                sst.Text = progress.ToString("F2");
            };
            ok.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));

                i.PutExtra("1", text.Text);
                i.PutExtra("2", ttssp.Progress);
                i.PutExtra("3", ttston.Progress);
                i.PutExtra("4", Intent.GetIntExtra("4",0));
                SetResult(Result.Ok, i);
                Finish();
            };

            cancel.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                SetResult(Result.Canceled, i);
                Finish();
            };
        }
    }
}