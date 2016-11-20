using UnityEngine;
using System.Collections;
using System.IO;
using UdpKit;

[BoltGlobalBehaviour()]
public class SendFile : Bolt.GlobalEventListener {
	/*public static UdpKit.UdpChannelName fileChannel;
	public FileUtils.File fileToSend;


	public override void BoltStartBegin()
	{
		fileChannel = BoltNetwork.CreateStreamChannel ("file", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public void sendFile()
	{
		if (BoltNetwork.isServer) {
			byte[] data = File.ReadAllBytes (fileToSend); //path to fiile ?
			BoltLog.Info ("data test");
			foreach (var connection in BoltNetwork.connections) {
				connection.StreamBytes (fileChannel, data);
			}


		} else if (BoltNetwork.isClient) {
		
		}
	}

	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data.HostData)
	{
		Debug.Log (Application.persistentDataPath);
		System.IO.File.WriteAllBytes(Application.persistentDataPath + @"\testout.ogg", data.Data);

	}*/
}
