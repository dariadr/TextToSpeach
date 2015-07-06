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
    class elemTts
    {
        private string _text;
        public float tts;
        public float ttt;
        public elemTts(string t, float ts, float tt)
        {
            if (t != "") _text = t; else _text = "Пример";
            tts = ts;
            ttt = tt;
        }
        public string text
        {
            get { return _text; }
            set { if (value != "") _text = value; }
        }
    }
}