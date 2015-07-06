namespace tts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using Android.Content;
    using Android.Runtime;
    using Android.Database;
    class ttsListFragment : ListFragment
    {
        private ttsListAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _adapter = new ttsListAdapter(Activity, new List<elemTts>());
            ListAdapter = _adapter;

        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var item = _adapter.GetItem(position);
            Intent i = new Intent(Activity, typeof(Dialog));
            i.PutExtra("1", item.text);
            i.PutExtra("2", item.tts);
            i.PutExtra("3", item.ttt);
            i.PutExtra("4", position);
            StartActivityForResult(i, 2);
            base.OnListItemClick(l, v, position, id);
        }

        public override void OnResume()
        {
            base.OnResume();
            RefreshFilesList((Activity as MainActivity).ttslist);
        }

        public void RefreshFilesList(List<elemTts> tts)
        {
            IList<elemTts> visibleThings = new List<elemTts>();
            try
            {
                foreach (var item in tts)
                {
                    visibleThings.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ttsListFragment", "Проблема доступа к данным массива tts; " + ex);
                Toast.MakeText(Activity, "Проблема доступа к данным массива tts;", ToastLength.Long).Show();
                return;
            }

            _adapter.Clear();
            foreach (var fsi in visibleThings)
            {
                _adapter.Add(fsi);
            }
            _adapter.NotifyDataSetChanged();

            _adapter.AddDirectoryContents(visibleThings);
            ListView.RefreshDrawableState();

            Log.Verbose("ttsListFragment", "Показано содержимое массива tts");
        }
    }
}