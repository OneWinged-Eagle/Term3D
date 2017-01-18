///<summary>
///VideoObject handlers
///</summary>

using UnityEngine;
using System.Collections;
using System.IO;


[System.Serializable]
public class VideoObject : Bolt.EntityBehaviour<IVideoObjectState>
{
	public FileUtils.File Video;
	private GameObject player;
	private PlayVLC vlc;
	private string url;

	public void OpenCanvas()
	{
		player = Instantiate(Resources.Load ("Player")) as GameObject;
		GameObject panel = player.transform.FindChild ("Video_On_Panel").gameObject;
		vlc = panel.GetComponent<PlayVLC> ();
		vlc.VideoPath = Video.RealPath;
		if (BoltNetwork.isServer) 
		{
			//Lancer le serveur VLC
			// vlc -Idummy [--dummy-quiet] <video_input> --sout "#transcode{vcodec=h264,acodec=mp3,ab=128,channels=2,samplerate=44100}:http{mux=ffmpeg{mux=flv},dst=:4242}"

			/*
			 * Envoyer l'url
			byte[] data = File.ReadAllBytes (fileSend.RealPath);
			BoltLog.Info ("data test");
			foreach (var connection in BoltNetwork.connections) {
				Debug.Log (connection);
				connection.StreamBytes (fileChannel, data);
			}
			*/
		}
	}

	/*
	 * Recevoir le lien
	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		if (BoltNetwork.isClient) {
			url = data.Data.toString();
			gameObject.name = url;
		}
	}
	*/

	public void CloseCanvas()
	{
		Destroy (transform.root.gameObject);
	}
}
