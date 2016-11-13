using Bolt;

using System.Collections;
using System.IO;
using System.Text;

using UdpKit;

using UnityEngine;

///<summary>
///Handle file transfer
///</summary>
[BoltGlobalBehaviour()]
public class StreamCallback : Bolt.GlobalEventListener // TODO: à update ou virer
{
	/*
	public static UdpKit.UdpChannelName AudioChannel;

	public override void BoltStartBegin()
	{
		AudioChannel = BoltNetwork.CreateStreamChannel("audio", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public override void SceneLoadRemoteDone(BoltConnection c)
	{
		if (BoltNetwork.isServer)
		{
			byte[] data = File.ReadAllBytes(@"c:\testin.mp3");
			BoltLog.Info("DATA TEST");
			c.StreamBytes(AudioChannel, data);
		}
	}

	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		BoltLog.Info("CLIENT GOT DATA! :)");
		System.IO.File.WriteAllBytes(@"c:\testout.mp3", data.Data);
	}
	*/
}
