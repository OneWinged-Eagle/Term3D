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
			gameObjs.AddRange(GameObject.FindGameObjectsWithTag(tag));

		SerializableObj[] objs = new SerializableObj[gameObjs.Count];
		uint i = 0;

		foreach (GameObject gameObj in gameObjs)
		{
			objs[i++] = new SerializableObj(gameObj.name,
					gameObj.GetComponent<BoltEntity>().prefabId, gameObj.transform.position, gameObj.transform.rotation.eulerAngles,
					gameObj.GetComponent<ImageObject>(), gameObj.GetComponent<AudioObject>(), gameObj.GetComponent<VideoObject>(), gameObj.GetComponent<TextObject>(), gameObj.GetComponent<LinkObject>());
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
			{
				GameObject newObj = BoltNetwork.Instantiate(obj.ID, new Vector3(obj.PosX, obj.PosY, obj.PosZ), Quaternion.Euler(obj.RotX, obj.RotY, obj.RotZ));
				newObj.name = obj.Name;
			}

			ToLoad = false;
		}
	}
}
