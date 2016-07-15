using System.Collections;
﻿using UnityEngine;
using UnityEngine.UI;

public class MenuScript : Bolt.GlobalEventListener
{
	public GameObject _mainMenu;
	public GameObject _joinMenu;
	public InputField _ip;
	public string _servPublicIP; // TODO: variable utilisée uniquement dans LaunchServerButton : à passer en variable locale ?
	public ushort _port = 27000;
	// TODO: toutes les variables ci-dessus sont publiques, normal ? Peut-être à passer en properties ?

	private void Start() {}

	private void Update() {}

	public static string GetPublicIP()
	{
		return new System.Net.WebClient().DownloadString("https://api.ipify.org");
	}

	public void LaunchServerButton()
	{
		BoltLauncher.StartServer(new UdpKit.UdpEndPoint(UdpKit.UdpIPv4Address.Any, _port));
		// DISPLAY THE PUBLIC IP IN UI
		_servPublicIP = GetPublicIP();
		Debug.Log(_servPublicIP + ": " + _port);
	}

	public void JoinServerButton()
	{
		_mainMenu.SetActive(false);
		_joinMenu.SetActive(true);
	}

	public void ExitButton()
	{
		BoltNetwork.ClosePortUPnP(_port);
		BoltLauncher.Shutdown();
		Application.Quit();
	}

	public void ConnectButton()
	{
		Debug.Log(_ip.text);
		BoltLauncher.StartClient();
	}

	public void BackButton()
	{
		_mainMenu.SetActive(true);
		_joinMenu.SetActive(false);
	}

	public override void BoltStartDone()
	{
		if (BoltNetwork.isServer)
		{
			BoltNetwork.LoadScene("Term3D");
			// BOLT UPNP PORT FORWARDING
			BoltNetwork.EnableUPnP();
			BoltNetwork.OpenPortUPnP(_port);
		}
		else
			BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(_ip.text));
	}
}
