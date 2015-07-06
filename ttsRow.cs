namespace tts
{
    using Android.Widget;
    using Java.Lang;

    class ttsRow : Object
    {
        public ttsRow(TextView textViewtts, TextView textViews, TextView textViewt)
        {
            _TextViewtts = textViewtts;
            _TextViews = textViews;
            _TextViewt = textViewt; 
        }

        public TextView _TextViewtts { get; private set; }
        public TextView _TextViews { get; private set; }
        public TextView _TextViewt { get; private set; }
                
        public void Update(string text, string speed, string ton)
        {
            _TextViewtts.Text = text;
            _TextViews.Text = speed;
            _TextViewt.Text =ton;
        }
    }
}