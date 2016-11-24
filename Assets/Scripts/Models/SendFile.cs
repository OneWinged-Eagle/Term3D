using UnityEngine;
using System.Collections;
using System.IO;
using UdpKit;

[BoltGlobalBehaviour()]
public class SendFile : Bolt.GlobalEventListener {
	public static UdpKit.UdpChannelName fileChannel;
	public FileUtils.File file;

	public override void BoltStartBegin()
	{
		fileChannel = BoltNetwork.CreateStreamChannel ("file", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public void sendFile(FileUtils.File fileSend)
	{
		file = fileSend;
		Debug.Log ("cooucou");
		if (BoltNetwork.isServer) {
			byte[] data = File.ReadAllBytes (fileSend.RealPath);
			BoltLog.Info ("data test");
			foreach (var connection in BoltNetwork.connections) {
				Debug.Log (connection);
				connection.StreamBytes (fileChannel, data);
			}
		} else if (BoltNetwork.isClient) {
		
		}
	}

	/*public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		Debug.Log ("cooucoukekekeeke");
		Debug.Log (Application.persistentDataPath);
		System.IO.File.WriteAllBytes(Application.persistentDataPath + file.ProjectPath, data.Data);
	}*/

	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		Debug.Log (Application.persistentDataPath);
		Debug.Log (data.Channel.ToString());

		System.IO.File.WriteAllBytes(Application.persistentDataPath + @"\poueté.ogg", data.Data);
		//WWW testwww = new WWW ("file:///" + Application.persistentDataPath + "/testout.ogg");

		/*
		System.IO.File.WriteAllBytes(@"d:\testout.ogg", data.Data);
		WWW testwww = new WWW ("file://" + @"d:\testout.ogg");



		AudioClip monSon = testwww.audioClip;
		audioSrc.clip = monSon;
		audioSrc.Play ();

		Debug.Log ("testeaejdqhdjsqdhsjkhdjkqlololololo");


		/*var startAudio = PlayAudio.Create ();
		startAudio.isPlayed = true;
		startAudio.Send ();*/
	}
}
