package tts;


public class ttsRow
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("tts.ttsRow, tts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ttsRow.class, __md_methods);
	}


	public ttsRow () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ttsRow.class)
			mono.android.TypeManager.Activate ("tts.ttsRow, tts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public ttsRow (android.widget.TextView p0, android.widget.TextView p1, android.widget.TextView p2) throws java.lang.Throwable
	{
		super ();
		if (getClass () == ttsRow.class)
			mono.android.TypeManager.Activate ("tts.ttsRow, tts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Widget.TextView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Widget.TextView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Widget.TextView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1, p2 });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
