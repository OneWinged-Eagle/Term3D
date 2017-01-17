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
		bool loaded = SavesHandler.ToLoad;
		SavesHandler.Load();

		if (BoltNetwork.isServer)
		{
			RoomUtils.Reset();

			if (!loaded)
				RoomUtils.CreateNewRoom(Path.DirectorySeparatorChar.ToString());

			if (RoomUtils.Room == BoltPrefabs.Space)
				Destroy(GameObject.Find("Directional Light"));
			else
				RenderSettings.skybox = null;
		}
		else if (BoltNetwork.isClient)
		{
			PathUtils.RootPath = PathUtils.GetPathFromRelative("tmp", Application.persistentDataPath);
			PathUtils.CurrPath = PathUtils.RootPath;
		}

		string name = "un gros nom à modifier par un genre de pseudo";

		GameObject player = BoltNetwork.Instantiate(BoltPrefabs.CubePlayer, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
		player.GetComponent<Movement>().state.Name = name;
	}
}
