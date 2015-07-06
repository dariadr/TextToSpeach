namespace tts
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    [Activity(Label = "@string/app_name", Icon = "@drawable/ic_launcher")]
    public class FilePickerActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.listfragment);
        }
    }
}
