using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : Bolt.GlobalEventListener {
	public GameObject mainMenu;
	public GameObject joinMenu;
	public InputField ip;
	public string servPublicIP;
	public ushort port = 27000;

	void Start () {}

	void Update () {}

	public static string GetPublicIP()
	{ 
		return (new System.Net.WebClient().DownloadString("https://api.ipify.org"));
	}

	public void LaunchServerButton()
	{
		BoltLauncher.StartServer(new UdpKit.UdpEndPoint(UdpKit.UdpIPv4Address.Any, port));
		//DISPLAY THE PUBLIC IP IN UI
		servPublicIP = GetPublicIP();
		Debug.Log(servPublicIP + ":" + port);
	}

	public void JoinServerButton()
	{
		mainMenu.SetActive(false);
		joinMenu.SetActive(true);

	}

	public void ExitButton()
	{
		BoltNetwork.ClosePortUPnP(port);
		BoltLauncher.Shutdown ();
		Application.Quit ();
	}
		
	public void ConnectButton()
	{
		Debug.Log (ip.text);
		BoltLauncher.StartClient();
	}

	public void backButton()
	{
		joinMenu.SetActive (false);
		mainMenu.SetActive (true);
	}

	public override void BoltStartDone ()
	{
		if (BoltNetwork.isServer) 
		{
			BoltNetwork.LoadScene ("Term3D");
			//BOLT UPNP PORT FORWARDING
			BoltNetwork.EnableUPnP ();
			BoltNetwork.OpenPortUPnP (port);
		} 
		else 
		{
			BoltNetwork.Connect (UdpKit.UdpEndPoint.Parse (ip.text));
		}
	}
}
