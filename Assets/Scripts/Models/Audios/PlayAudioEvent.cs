using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using UdpKit;
using Bolt;


[BoltGlobalBehaviour]
public class PlayAudioEvent : Bolt.GlobalEventListener {

	public static UdpKit.UdpChannelName audioChannel;

	AudioSource audioSrc;


	public override void BoltStartBegin()
	{
		audioChannel = BoltNetwork.CreateStreamChannel("audio", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public void sendPlayPauseSignal()
	{
	//	audioChannel = BoltNetwork.CreateStreamChannel("audio", UdpKit.UdpChannelMode.Reliable, 1);

		//var test = PlayAudio.Create ();
	//	test.isPlayed = true;

		if (BoltNetwork.isServer) {
			byte[] data = File.ReadAllBytes (@"d:\testin.ogg");
			BoltLog.Info ("DATA TEST");
			foreach (var connection in BoltNetwork.connections) {
				connection.StreamBytes (audioChannel, data);
			}
		}
			else if (BoltNetwork.isClient)
			{
				//var askServer = PlayAudio.Create();
				//askServer.Send ();
			}


	}

	public override void OnEvent(PlayAudio e)
	{
	/*	Debug.Log ("je recoit le message");
		WWW serverWww = new WWW ("file://" + @"d:\testin.ogg");
		AudioClip sonServeur = serverWww.audioClip;
		gameObject.GetComponent<AudioSource> ().clip = sonServeur;
		gameObject.GetComponent<AudioSource> ().Play ();*/





	Debug.Log ("coucou je recoit un event");
//	Debug.Log ("le client demande du son au serveur");
	}



	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		audioSrc = gameObject.GetComponent<AudioSource> ();

		System.IO.File.WriteAllBytes(Application.persistentDataPath + @"\testout.ogg", data.Data);
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
