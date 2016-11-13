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
		BoltNetwork.Instantiate(BoltPrefabs.CubePlayer, new Vector3(30, 1, 30), Quaternion.identity);

		if (BoltNetwork.isServer)
		{
			//PlayerObjectRegistry.CreateServerPlayer();
			instantiateWorld();
			if (LoadWorld.ToLoad)
				LoadWorld.Load();
		}
		else if (BoltNetwork.isClient)
			; //PlayerObjectRegistry.CreateClientPlayer();
		else
			; //WUT?
	}

	private void instantiateWorld()
	{
		RoomUtils.Reset();
		RoomUtils.CreateNewRoom();
	}
}
