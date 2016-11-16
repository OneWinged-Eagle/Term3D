using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

///<summary>
///Saves handler: creates, saves and loads saves files
///</summary>
public class SavesHandler
{
	public static bool ToLoad	{ get; set; }
	public static string SaveName { get; set; }

	private static BinaryFormatter formatter = new BinaryFormatter();

	public static void Save()
	{
		List<GameObject> gameObjs = new List<GameObject>();

		foreach (string tag in ModelsUtils.Tags)
			foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag(tag))
				gameObjs.Add(gameObj);

		SerializableObj[] objs = new SerializableObj[gameObjs.Count];

		for (int i = 0; i < gameObjs.Count; i++)
		{
	    objs[i] = new SerializableObj();

			objs[i].x = gameObjs[i].transform.position.x;
			objs[i].y = gameObjs[i].transform.position.y;
			objs[i].z = gameObjs[i].transform.position.z;

			objs[i].xRotate = gameObjs[i].transform.rotation.eulerAngles.x;
			objs[i].yRotate = gameObjs[i].transform.rotation.eulerAngles.y;
			objs[i].zRotate = gameObjs[i].transform.rotation.eulerAngles.z;

			objs[i].objName = gameObjs[i].name;
			objs[i].objId = gameObjs [i].GetComponent<BoltEntity>().prefabId;

			objs[i].audio = gameObjs[i].GetComponent<AudioObject>();
			objs[i].link = gameObjs[i].GetComponent<LinkObject>();
			objs[i].image = gameObjs[i].GetComponent<ImageObject>();
			objs[i].text = gameObjs[i].GetComponent<TextObject>();
			objs[i].video = gameObjs[i].GetComponent<VideoObject>();
	  }

		FileStream file = File.Open(Application.persistentDataPath + "/" + (String.IsNullOrEmpty(SaveName) ? "save" : SaveName) + ".dat", FileMode.OpenOrCreate);
		formatter.Serialize(file, objs);
		file.Close();
	}

	public static void Load()
	{
		if (ToLoad)
		{
			FileStream file = File.Open(Application.persistentDataPath + "/" + (String.IsNullOrEmpty(SaveName) ? "save" : SaveName) + ".dat", FileMode.Open);
			SerializableObj[] objs = (SerializableObj[])formatter.Deserialize(file);
			file.Close();

			foreach (SerializableObj obj in objs)
				BoltNetwork.Instantiate(obj.objId, new Vector3(obj.x, obj.y, obj.z), Quaternion.Euler(obj.xRotate, obj.yRotate, obj.zRotate));

			ToLoad = false;
		}
	}
}
