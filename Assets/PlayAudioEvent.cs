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
		test.Played = true;

		if (BoltNetwork.isServer) {
			byte[] data = File.ReadAllBytes(@"c:\testin.mp3");
			BoltLog.Info("DATA TEST");
			foreach (var connection in BoltNetwork.connections) {
				connection.StreamBytes(audioChannel, data);
		}


	}
	}

	public override void OnEvent(PlayAudio e)
	{

	}


	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		BoltLog.Info("CLIENT GOT DATA ! :)");
		System.IO.File.WriteAllBytes(@"c:\testout.mp3", data.Data);
	}

}
