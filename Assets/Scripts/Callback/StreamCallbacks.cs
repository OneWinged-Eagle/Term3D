using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using UdpKit;
using Bolt;

[BoltGlobalBehaviour()]
public class StreamCallbacks : Bolt.GlobalEventListener
{
	public static UdpKit.UdpChannelName audioChannel;
	public override void BoltStartBegin()
	{
		audioChannel = BoltNetwork.CreateStreamChannel("audio", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public override void SceneLoadRemoteDone(BoltConnection c)
	{
		if (BoltNetwork.isServer)
		{
            //byte[] data = Encoding.ASCII.GetBytes("string"); pour encoder les string
			byte[] data = File.ReadAllBytes(@"c:\testin.mp3"); // pour tout autre type de data
			BoltLog.Info("DATA TEST");
			c.StreamBytes(audioChannel, data);
		}
	}

	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		BoltLog.Info("CLIENT GOT DATA ! :)");
		System.IO.File.WriteAllBytes(@"c:\testout.mp3", data.Data);
        //string text = Encoding.ASCII.GetString(data.Data); pour lire les string
	} 
}