using System;
using System.Collections;
using System.IO;
﻿using UnityEngine;

[BoltGlobalBehaviour]
public class testCallbacks : Bolt.GlobalEventListener // TODO: pourquoi la classe n'a pas le même nom que le fichier ?
{
	public override void SceneLoadLocalDone(string map) // TODO: à quoi sert le string map ?
	{
		BoltNetwork.Instantiate(BoltPrefabs.unitychan, new Vector3(30, 1, 30), Quaternion.identity);

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

	private bool init(string root)
	{
		if (!Directory.Exists(root))
			return false;

		PathUtils.RootPath = root;
		PathUtils.CurrPath = root;

		RoomUtils.CreateNewRoom();

		/*DirectoryUtils.Directory rootDir = new DirectoryUtils.Directory(root);

    FileUtils.File[] files = rootDir.GetFiles();
		for (uint i = 0; i < files.Length; ++i)
			BoltNetwork.Instantiate(BoltPrefabs.Cube_vert, new Vector3 (20f, 0.5f, 5.0f * (2 + i)), Quaternion.identity); // TODO: à gérer mieux que ça !

		DirectoryUtils.Directory[] directories = rootDir.GetDirectories();
		for (uint i = 0; i < directories.Length; ++i)
			BoltNetwork.Instantiate(BoltPrefabs.Cube_rouge, new Vector3 (25f, 0.5f, 25f + (2 * i)), Quaternion.identity); // TODO: à gérer mieux que ça !*/

		return true;
	}

	private void instantiateWorld()
	{
		if (init(Directory.GetCurrentDirectory() + "\\beta_root")) // TODO: à gérer via GUI (après bêta)
			Debug.Log("Everything OK!"); // TODO: à enlever ?
		else
			Debug.Log("init failed..."); // TODO: faire pop une erreur "relancez plz"
	}
}
