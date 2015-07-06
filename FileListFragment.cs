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


    public class FileListFragment : ListFragment
    {
        public static readonly string DefaultInitialDirectory = "/";
        private FileListAdapter _adapter;
        private DirectoryInfo _directory;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _adapter = new FileListAdapter(Activity, new FileSystemInfo[0]);
            ListAdapter = _adapter;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var fileSystemInfo = _adapter.GetItem(position);

            if (fileSystemInfo.IsFile())
            {
                Context packageContext = Activity;
                Intent i = new Intent(packageContext, typeof(MainActivity));
                i.PutExtra("1", fileSystemInfo.FullName);
                Activity.SetResult(Result.Ok, i);
                Activity.Finish();
            }
            else
            {
                RefreshFilesList(fileSystemInfo.FullName);
            }
            base.OnListItemClick(l, v, position, id);
        }

        public override void OnResume()
        {
            base.OnResume();
            RefreshFilesList(DefaultInitialDirectory);
        }

        public bool txt(String filename)
        {
            int lastdot = filename.LastIndexOf(".");
            if (lastdot > 0)
            {
                return (filename.Substring(lastdot + 1) == "txt");
            }
            return false;
        }
        public void RefreshFilesList(string directory)
        {
            IList<FileSystemInfo> visibleThings = new List<FileSystemInfo>();
            var dir = new DirectoryInfo(directory);

            try
            {
                if (dir.ToString()!="/") 
                    visibleThings.Add(dir.Parent);
                foreach (var item in dir.GetFileSystemInfos().Where(item => item.IsVisible()))
                {
                    bool b;
                    if (item.IsDirectory()) b = true; else b = txt(item.Name);
                    if (b) visibleThings.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.Error("FileListFragment", "Нет доступа к директории " + _directory.FullName + "; " + ex);
                Toast.MakeText(Activity, "Проблема доступа к директории " + directory, ToastLength.Long).Show();
                return;
            }

            _directory = dir;

            _adapter.AddDirectoryContents(visibleThings);

            ListView.RefreshDrawableState();

            Log.Verbose("FileListFragment", "Показан контент директории {0}.", directory);
        }
    }
}
