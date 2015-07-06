namespace tts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Android.Content;
    using Android.Views;
    using Android.Widget;
    using Android.Database;

    class ttsListAdapter : ArrayAdapter<elemTts>
    {
        private readonly Context _context;

        public ttsListAdapter(Context context, IList<elemTts> lt)
            : base(context, Resource.Layout.tts_list_item, Android.Resource.Id.Text1, lt)
        {
            _context = context;
        }

        public void AddDirectoryContents(IEnumerable<elemTts> list)
        {
            Clear();
            if (list.Any())
            {
#if __ANDROID_11__
                AddAll(list.ToArray());
#else

                lock (this)
                    foreach (var fsi in list)
                    {
                        Add(fsi);
                    }
#endif

                NotifyDataSetChanged();
            }
            else
            {
                NotifyDataSetInvalidated();
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var element = GetItem(position);
            ttsRow viewHolder;
            View row = _context.GetLayoutInflater().Inflate(Resource.Layout.tts_list_item, parent, false);
            viewHolder = new ttsRow(row.FindViewById<TextView>(Resource.Id.ttstext), row.FindViewById<TextView>(Resource.Id.ttsspeed), row.FindViewById<TextView>(Resource.Id.ttston));
            viewHolder.Update(element.text, ((element.tts - 0.1f)).ToString("F2"), ((element.ttt - 0.5f) * 170f / 255f).ToString("F2"));
            return row;
        }
    }
}