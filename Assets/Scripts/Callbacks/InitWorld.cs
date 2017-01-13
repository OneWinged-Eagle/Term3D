using System;
using System.Collections;
using System.IO;
﻿using UnityEngine;

///<summary>
///Initialize all bolt objects
///</summary>
[BoltGlobalBehaviour("Term3D")]
public class InitWorld : Bolt.GlobalEventListener // TODO: à update les comm' (via les autres callbacks)
{
	public override void SceneLoadLocalDone(string map)
	{
		SavesHandler.Load();

		GameObject room = RoomUtils.GetRoom("\\");

		if (BoltNetwork.isServer)
		{
			RoomUtils.Reset();
			if (!room)
				room = RoomUtils.CreateNewRoom("\\");
			if (RoomUtils.Room != BoltPrefabs.Space)
				RenderSettings.skybox = null;
		}
		else if (BoltNetwork.isClient)
		{
			PathUtils.RootPath = PathUtils.GetPathFrom(Application.persistentDataPath) + "\\tmp";
			PathUtils.CurrPath = PathUtils.RootPath;
		}

		string name = "un gros nom à modifier par un genre de pseudo";

		GameObject player = BoltNetwork.Instantiate(BoltPrefabs.CubePlayer, room.transform.Find("Spawn").transform.position, Quaternion.identity);
		player.name = name;
		player.transform.parent = room.transform;
	}
}
