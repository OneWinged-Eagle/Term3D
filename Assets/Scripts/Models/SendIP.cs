using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UdpKit;

[BoltGlobalBehaviour()]
public class SendIP : Bolt.GlobalEventListener {
	public static UdpKit.UdpChannelName fileChannel;
	public FileUtils.File file;

	public string LocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
				break;
			}
		}
		return localIP;
	}

	public void sendIP()
	{
		var sendIp = sendIpEvent.Create ();

		//string ip = new System.Net.WebClient().DownloadString("http://api.ipify.org");
		string send = "http://" + LocalIPAddress() + ":4242";

		sendIp.ip = send;
		sendIp.Send ();
	}

	public override void OnEvent(sendIpEvent e)
	{
		if (BoltNetwork.isClient)
			gameObject.GetComponent<VideoObject> ().ip = e.ip;	
	}
}
