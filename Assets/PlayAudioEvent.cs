using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using UdpKit;
using Bolt;


[BoltGlobalBehaviour]
public class PlayAudioEvent : Bolt.GlobalEventListener {

	public static UdpKit.UdpChannelName audioChannel;



	public override void BoltStartBegin()
	{
		audioChannel = BoltNetwork.CreateStreamChannel("audio", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public void sendPlayPauseSignal()
	{
	//	audioChannel = BoltNetwork.CreateStreamChannel("audio", UdpKit.UdpChannelMode.Reliable, 1);

		var test = PlayAudio.Create ();
		test.isPlayed = true;

		if (BoltNetwork.isServer) {
			byte[] data = File.ReadAllBytes (@"d:\testin.ogg");
			BoltLog.Info ("DATA TEST");
			foreach (var connection in BoltNetwork.connections) {
				connection.StreamBytes (audioChannel, data);
			}
		}
			else if (BoltNetwork.isClient)
			{
				var askServer = PlayAudio.Create();
				askServer.Send ();
			}


	}

	public override void OnEvent(PlayAudio e)
	{
		Debug.Log ("le client demande du son au serveur");
	}


	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		BoltLog.Info("CLIENT GOT DATA ! :)");
		Debug.Log("CLIENT GOT DATA ! :)");
		System.IO.File.WriteAllBytes(@"d:\testout.ogg", data.Data);
		BoltLog.Info("C'est copié");


		WWW testwww = new WWW ("file://" + "d:\\testout.ogg");
		Debug.Log (testwww);
		BoltLog.Info (testwww);


		AudioClip monSon = testwww.audioClip;
		//while (!monSon.isReadyToPlay)
		//	yield return testwww;
		Debug.Log ("avant" + this.GetComponent<AudioSource> ().clip);
		this.GetComponent<AudioSource> ().clip = monSon;
		Debug.Log ("apres" + this.GetComponent<AudioSource> ().clip);
		this.GetComponent<AudioSource> ().Play ();
		Debug.Log ("testeaejdqhdjsqdhsjkhdjkqlololololo");
	}


}
