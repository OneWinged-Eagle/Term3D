using System.Collections;

﻿using UnityEngine;
using UnityEngine.UI;

///<summary>
///Handle MainMenu actions (launch server, join server, options, exit)
///</summary>
[BoltGlobalBehaviour()]
public class MainMenu : Bolt.GlobalEventListener // TODO: à vérif' (@guillaume)
{
	private const int PORT = 27000;

	public GameObject Menu;
	public GameObject LaunchMenu;
	public GameObject LoadMenu;
	public GameObject JoinMenu;
	public InputField ChooseFolderTxt;
	public InputField IP;
	public Toggle EmptyRoomToggle;
	public Toggle ForestRoomToggle;
	public Toggle SpaceRoomToggle;

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

	public void LoadMenuBtn()
	{
		LaunchMenu.SetActive(false);
		LoadMenu.SetActive(true);
	}

	public void JoinMenuBtn()
	{
		Menu.SetActive(false);
		JoinMenu.SetActive(true);
	}

	private Bolt.PrefabId GetRoom()
	{
		if (EmptyRoomToggle.isOn)
			return BoltPrefabs.Room;
		else if (ForestRoomToggle.isOn)
			return BoltPrefabs.Forest;
		else if (SpaceRoomToggle.isOn)
			return BoltPrefabs.Space;
		else
			return BoltPrefabs.Room;
	}

	public void LaunchBtn()
	{
		if (string.IsNullOrEmpty(ChooseFolderTxt.text))
			ChooseFolderTxt.text = "/";
		string root = PathUtils.GetPathFrom(ChooseFolderTxt.text);

		if (!PathUtils.IsValidPath(root))
		{
			ChooseFolderTxt.text = "Dossier non trouvé";
			return;
		}

		PathUtils.RootPath = root;
		PathUtils.CurrPath = root;
		RoomUtils.Room = GetRoom();

		BoltLauncher.StartServer(new UdpKit.UdpEndPoint(UdpKit.UdpIPv4Address.Any, PORT));
		// DISPLAY THE PUBLIC IP IN UI
		string ServPublicIP = GetPublicIP();
		Debug.Log("IP: " + ServPublicIP + "(PORT: " + PORT + ")");
	}

	public void LoadBtn()
	{
		string root = PathUtils.GetPathFrom("/");

		if (!PathUtils.IsValidPath(root))
		{
			ChooseFolderTxt.text = "Dossier non trouvé";
			return;
		}

		PathUtils.RootPath = root;
		PathUtils.CurrPath = root;
		RoomUtils.Room = GetRoom();

		BoltLauncher.StartServer(new UdpKit.UdpEndPoint(UdpKit.UdpIPv4Address.Any, PORT));
		SavesHandler.ToLoad = true;
		// DISPLAY THE PUBLIC IP IN UI
		string ServPublicIP = GetPublicIP();
		Debug.Log("IP: " + ServPublicIP + "(PORT: " + PORT + ")");
	}

	public void JoinBtn()
	{
		Debug.Log(IP.text);
		BoltLauncher.StartClient();
	}

	public void BackBtn1()
	{
		Menu.SetActive(true);
		LaunchMenu.SetActive(false);
		JoinMenu.SetActive(false);
	}

	public void BackBtn2()
	{
		LaunchMenu.SetActive(true);
		LoadMenu.SetActive(false);
	}

	public void ExitBtn()
	{
		if (BoltNetwork.isRunning)
		{
			BoltNetwork.ClosePortUPnP(PORT);
			BoltLauncher.Shutdown();
		}
		Application.Quit();
	}

	public override void BoltStartDone()
	{
		if (BoltNetwork.isServer)
		{
			BoltNetwork.LoadScene("Term3D");
			// BOLT UPNP PORT FORWARDING
			BoltNetwork.EnableUPnP();
			BoltNetwork.OpenPortUPnP(PORT);
		}
		else
			BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(IP.text));
	}
}
