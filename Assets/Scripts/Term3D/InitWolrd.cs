using UnityEngine;
using System;
using System.Collections;
using System.IO;

[BoltGlobalBehaviour]
public class testCallbacks : Bolt.GlobalEventListener
{
	public override void SceneLoadLocalDone(string map)
	{
		// randomize a position
		//Vector3 pos = new Vector3(UnityEngine.Random.Range(0, 10), 0, UnityEngine.Random.Range(0, 10));

		//BoltNetwork.Instantiate(BoltPrefabs.Robot, pos, Quaternion.identity);
		//BoltNetwork.Instantiate(BoltPrefabs.Player, pos, Quaternion.identity);



		BoltNetwork.Instantiate(BoltPrefabs.Player, new Vector3(30, 0, 30), Quaternion.identity);


		if (BoltNetwork.isServer) {
			//PlayerObjectRegistry.CreateServerPlayer ();
			instantiateWorld ();
		}
		if (BoltNetwork.isClient) 
		{
			//PlayerObjectRegistry.CreateClientPlayer ();
		}
	}

	private bool init(string root)
	{
		if (!Directory.Exists(root))
			return false;

		PathUtils.setRootPath(root);
		PathUtils.setCurrPath(root);
		DirectoryUtils.Directory rootDir = new DirectoryUtils.Directory(root);

		FileUtils.File[] files = rootDir.getFiles();
		for (uint i = 0; i < files.Length; ++i)
			BoltNetwork.Instantiate(BoltPrefabs.Cube_vert, new Vector3 (20f, 0.5f, 5.0f * (2 + i)), Quaternion.identity);

		DirectoryUtils.Directory[] directories = rootDir.getDirectories();
		for (uint i = 0; i < directories.Length; ++i)
			BoltNetwork.Instantiate(BoltPrefabs.Cube_rouge, new Vector3 (25f, 0.5f, 25f + (2 * i)), Quaternion.identity);

		return true;
	}

	public void instantiateWorld()
	{
		//BoltNetwork.Instantiate(BoltPrefabs.Terrain, new Vector3 (0, 0, 0), Quaternion.identity);
		/*BoltNetwork.Instantiate(BoltPrefabs.Cube_rouge, new Vector3 (20f, 0.5f, 20f), Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Cube_vert, new Vector3 (20f, 0.5f, 15f), Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Sphere, new Vector3 (25f, 0.5f, 25f), Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Cylindre, new Vector3 (20f, 0.5f, 30f), Quaternion.identity);*/

		if (init(Directory.GetCurrentDirectory() + "\\beta_root"))
			Debug.Log("Everything OK!");
		else
			Debug.Log("init failed...");
	}
}
