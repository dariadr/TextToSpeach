package md5832a8baa19d67d30faaee0f1fc515454;


public class FileListRowViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("tts.FileListRowViewHolder, tts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FileListRowViewHolder.class, __md_methods);
	}


	public FileListRowViewHolder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FileListRowViewHolder.class)
			mono.android.TypeManager.Activate ("tts.FileListRowViewHolder, tts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public FileListRowViewHolder (android.widget.TextView p0, android.widget.ImageView p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == FileListRowViewHolder.class)
			mono.android.TypeManager.Activate ("tts.FileListRowViewHolder, tts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Widget.TextView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Widget.ImageView, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
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
