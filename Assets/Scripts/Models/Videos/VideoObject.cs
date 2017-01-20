using UnityEngine;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Net;
using UdpKit;


[System.Serializable]
public class VideoObject : Bolt.EntityBehaviour<IVideoObjectState>
{
	public FileUtils.File Video;
	private GameObject player;
	private PlayVLC vlc;
	private string url;
	public Process vlcserver;
	public string ip;

	public void OpenCanvas()
	{
		player = Instantiate(Resources.Load ("Player")) as GameObject;
		GameObject panel = player.transform.FindChild ("Video_On_Panel").gameObject;
		vlc = panel.GetComponent<PlayVLC> ();
		if (BoltNetwork.isServer)
			vlc.VideoPath = "http://127.0.0.1:4242";
		else
			vlc.VideoPath = ip;
		if (BoltNetwork.isServer) 
		{
			vlcserver = Process.Start(Application.dataPath + @"/StreamingAssets/vlc/vlc.exe","-Idummy --dummy-quiet "+Video.RealPath +
				" --sout \"#transcode{vcodec=h264,acodec=mp3,ab=128,channels=2,samplerate=44100}:http{mux=ffmpeg{mux=flv},dst=:4242}\"");
			gameObject.GetComponent<SendIP> ().sendIP ();
		}

	}

	public void CloseCanvas()
	{
		foreach (Process vlcOverlay in Process.GetProcesses()) 
		{
			if (vlcOverlay.ProcessName == "vlc")
				vlcOverlay.Kill ();
		}
		Destroy (transform.root.gameObject);
	}
}
