namespace tts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Android.Content;
    using Android.Views;
    using Android.Widget;

    public class FileListAdapter : ArrayAdapter<FileSystemInfo>
    {
        private readonly Context _context;

        public FileListAdapter(Context context, IList<FileSystemInfo> fsi)
            : base(context, Resource.Layout.file_picker_list_item, Android.Resource.Id.Text1, fsi)
        {
            _context = context;
        }

        public void AddDirectoryContents(IEnumerable<FileSystemInfo> directoryContents)
        {
            Clear();
            if (directoryContents.Any())
            {
#if __ANDROID_11__
                AddAll(directoryContents.ToArray());
#else

                lock (this)
                    foreach (var fsi in directoryContents)
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

            var fileSystemEntry = GetItem(position);

            FileListRowViewHolder viewHolder;
            View row;
            if (convertView == null)
            {
                row = _context.GetLayoutInflater().Inflate(Resource.Layout.file_picker_list_item, parent, false);
                viewHolder = new FileListRowViewHolder(row.FindViewById<TextView>(Resource.Id.file_picker_text), row.FindViewById<ImageView>(Resource.Id.file_picker_image));
                row.Tag = viewHolder;
            }
            else
            {
                row = convertView;
                viewHolder = (FileListRowViewHolder)row.Tag;
            }
            if (fileSystemEntry.Name == "/")
                viewHolder.Update("...", fileSystemEntry.IsDirectory() ? Resource.Drawable.folder : Resource.Drawable.file);
            else
                viewHolder.Update(fileSystemEntry.Name, fileSystemEntry.IsDirectory() ? Resource.Drawable.folder : Resource.Drawable.file);

            return row;
        }
    }
}
