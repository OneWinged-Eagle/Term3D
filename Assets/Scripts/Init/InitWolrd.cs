using System;
using System.Collections;
using System.IO;
﻿using UnityEngine;

[BoltGlobalBehaviour]
public class testCallbacks : Bolt.GlobalEventListener // TODO: pourquoi la classe n'a pas le même nom que le fichier ?
{
	public override void SceneLoadLocalDone(string map) // TODO: à quoi sert le string map ?
	{
		BoltNetwork.Instantiate(BoltPrefabs.CubePlayer, new Vector3(30, 1, 30), Quaternion.identity);

		if (BoltNetwork.isServer)
		{
			//PlayerObjectRegistry.CreateServerPlayer();
			instantiateWorld();
		}
		if (BoltNetwork.isClient)
		{
			//PlayerObjectRegistry.CreateClientPlayer();
		}
	}

	private void instantiateWorld()
	{
		RoomUtils.Reset();
		RoomUtils.CreateNewRoom();
	}
}
