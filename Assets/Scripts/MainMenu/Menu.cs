using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuTemp : Bolt.GlobalEventListener {
	public GameObject mainMenu;
	public GameObject joinMenu;
	public InputField ip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LaunchServerButton()
	{
		BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));
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
		if (BoltNetwork.isServer)
			BoltNetwork.LoadScene ("Term3D");
		else
			BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(ip.text));
	}
}
