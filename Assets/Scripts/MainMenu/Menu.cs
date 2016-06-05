using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : Bolt.GlobalEventListener {
	public GameObject mainMenu;
	public GameObject joinMenu;
	public InputField ip;
	public string servPublicIP;
	public ushort port = 27000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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
		mainMenu.SetActive (false);
		joinMenu.SetActive(true);

	}

	public void ExitButton()
	{
		Application.Quit ();
	}
		
	public void ConnectButton()
	{
		Debug.Log (ip.text);
		BoltLauncher.StartClient ();
	}

	public void backButton()
	{
		joinMenu.SetActive (false);
		mainMenu.SetActive (true);
	}

	public override void BoltStartDone ()
	{
		if (BoltNetwork.isServer) {
			BoltNetwork.LoadScene ("Term3D");
			//MAKE UPNP WORK
			/*BoltNetwork.EnableUPnP();
			BoltNetwork.OpenPortUPnP(27000);*/
		}
		else
			BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(ip.text));
	}
}
