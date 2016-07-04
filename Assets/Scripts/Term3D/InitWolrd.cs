using UnityEngine;
using System;
using System.Collections;
using System.IO;

[BoltGlobalBehaviour]
public class testCallbacks : Bolt.GlobalEventListener
{

	//public GameObject[] objs;
	//public GameObject test;


	public BoltEntity[] lelele;
	public BoltEntity test;


	public override void SceneLoadLocalDone(string map)
	{
		// randomize a position
		Vector3 pos = new Vector3(UnityEngine.Random.Range(-16, 16), 0, UnityEngine.Random.Range(-16, 16));

		// instantiate cube
		//BoltNetwork.Instantiate(BoltPrefabs.Robot, pos, Quaternion.identity);

		if (BoltNetwork.isServer)
			instantiateWorld ();

	}

	private bool init(string root)
	{
		if (!Directory.Exists(root))
			return false;

		PathUtils.setRootPath(root);
		PathUtils.setCurrPath(root);
		DirectoryUtils.Directory rootDir = new DirectoryUtils.Directory(root);
		//Debug.Log(string.Format("Directory name is {0}", rootDir.getDirName()));
		FileUtils.File[] files = rootDir.getFiles();
		for (uint i = 0; i < files.Length; ++i)
			//Debug.Log(string.Format("File name is {0} with extension {1}\nFile content: {2}", file.getFileNameWithoutExtension(), file.getExtension(), file.getContent()));
			BoltNetwork.Instantiate(BoltPrefabs.Document, new Vector3 (20f, 0.5f, 5.0f * (2 + i)), Quaternion.identity);
		foreach (DirectoryUtils.Directory directory in rootDir.getDirectories())
			//Debug.Log(string.Format("Directory name is {0}", directory.getDirName()));
			BoltNetwork.Instantiate(BoltPrefabs.Cube_rouge, new Vector3 (25f, 0.5f, 25f), Quaternion.identity);
		return true;
	}

	public void instantiateWorld()
	{
		BoltNetwork.Instantiate(BoltPrefabs.Terrain, new Vector3 (0, 0, 0), Quaternion.identity);
		/*BoltNetwork.Instantiate(BoltPrefabs.Cube_rouge, new Vector3 (20f, 0.5f, 20f), Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Cube_vert, new Vector3 (20f, 0.5f, 15f), Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Sphere, new Vector3 (25f, 0.5f, 25f), Quaternion.identity);
		BoltNetwork.Instantiate(BoltPrefabs.Cylindre, new Vector3 (20f, 0.5f, 30f), Quaternion.identity);*/



		if (init("C:\\development\\test"))
			Debug.Log("Everything ok!");
		else
			Debug.Log("init(\"C:\\development\\test\") failed...");



		//BoltEntity test = BoltNetwork.Instantiate (BoltPrefabs.Cube_rouge, new Vector3 (10f, 0.5f, 10f), Quaternion.identity);







/*		test = GameObject.Find("Terrain(Clone)");    //pourquoi ça marche ?

		lelele[0] = GameObject.Find ("Terrain(Clone)"); // et la putain de non ?


/*

	


		objs [0] = GameObject.Find("Cube vert");
		objs [1] = GameObject.Find ("Cube vert(clone)");
		objs [2] = GameObject.Find ("Cube rouge(clone)");
		objs [3] = GameObject.Find ("Sphere(clone)");
		objs [4] = GameObject.Find ("Cylindre(clone)");

			/*
			Debug.Log (Application.persistentDataPath);

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/roomInfo.dat", FileMode.Open);

		bf.Serialize (file, obj);
		file.Close();*/
	}
}
