using System.Collections;

using UnityEditor;
﻿using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Bolt.GlobalEventListener
{
	public GameObject Menu;
	public GameObject LaunchMenu;
	public GameObject JoinMenu;
	public Text ChooseFolderTxt;
	public InputField IP;
	public string ServPublicIP; // TODO: variable utilisée uniquement dans LaunchServerButton : à passer en variable locale ?
	public ushort Port = 27000;
	// TODO: toutes les variables ci-dessus sont publiques, normal ? Peut-être à passer en properties ?

	private void Start() {}

	private void Update() {}

	public static string GetPublicIP()
	{
		return new System.Net.WebClient().DownloadString("https://api.ipify.org");
	}

	public void LaunchMenuBtn()
	{
		Menu.SetActive(false);
		LaunchMenu.SetActive(true);
	}

	public void JoinMenuBtn()
	{
		Menu.SetActive(false);
		JoinMenu.SetActive(true);
	}

	public void BrowseBtn()
	{
		ChooseFolderTxt.text = EditorUtility.OpenFolderPanel( "Choisissez votre dossier racine", "", "" );
	}

	public void LaunchBtn()
	{
		string root = PathUtils.GetPathFrom(ChooseFolderTxt.text);

		if (!PathUtils.IsValidPath(root))
		{
			ChooseFolderTxt.text = "Dossier non trouvé";
			return;
		}

		PathUtils.RootPath = root;
		PathUtils.CurrPath = root;

		BoltLauncher.StartServer(new UdpKit.UdpEndPoint(UdpKit.UdpIPv4Address.Any, Port));
		// DISPLAY THE PUBLIC IP IN UI
		ServPublicIP = GetPublicIP();
		Debug.Log(ServPublicIP + ": " + Port);
	}

	public void JoinBtn()
	{
		Debug.Log(IP.text);
		BoltLauncher.StartClient();
	}

	public void BackBtn()
	{
		Menu.SetActive(true);
		LaunchMenu.SetActive(false);
		JoinMenu.SetActive(false);
	}

	public void ExitBtn()
	{
		BoltNetwork.ClosePortUPnP(Port);
		BoltLauncher.Shutdown();
		Application.Quit();
	}

	public override void BoltStartDone()
	{
		if (BoltNetwork.isServer)
		{
			BoltNetwork.LoadScene("Term3D");
			// BOLT UPNP PORT FORWARDING
			BoltNetwork.EnableUPnP();
			BoltNetwork.OpenPortUPnP(Port);
		}
		else
			BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(IP.text));
	}
}
