using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		//Debug.Log("test");

		foreach (GameObject gameObj in gameObjs)
		{
			objs[i++] = new SerializableObj(gameObj.name,
					gameObj.GetComponent<BoltEntity>().prefabId, gameObj.transform.position, gameObj.transform.rotation.eulerAngles,
					gameObj.GetComponent<BaseObject>().Path);
		}
		FileStream file = File.Open(Path.Combine(Application.persistentDataPath, (String.IsNullOrEmpty(SaveName) ? "save" : SaveName) + ".dat"), FileMode.OpenOrCreate);
		formatter.Serialize(file, PathUtils.RootPath);
		formatter.Serialize(file, objs);
		file.Close();
	}

	public static void Load()
	{
		if (ToLoad)
		{
			FileStream file = File.Open(Path.Combine(Application.persistentDataPath, (String.IsNullOrEmpty(SaveName) ? "save" : SaveName) + ".dat"), FileMode.Open);
			formatter.Deserialize(file);
			SerializableObj[] objs = (SerializableObj[])formatter.Deserialize(file);
			file.Close();

			foreach (SerializableObj obj in objs)
			{
				GameObject newObj = BoltNetwork.Instantiate(obj.ID, new Vector3(obj.PosX, obj.PosY, obj.PosZ), Quaternion.Euler(obj.RotX, obj.RotY, obj.RotZ));
				newObj.name = obj.Name;

				if (!String.IsNullOrEmpty(obj.Path.ProjectPath) && !String.IsNullOrEmpty(obj.Path.RealPath))
				{
					newObj.GetComponent<BaseObject>().state.Name = obj.Path.ProjectPath;
					newObj.GetComponent<BaseObject>().Path = obj.Path;
					switch (newObj.tag)
					{
					case "ImageObject":
						ImageObject imageObject = newObj.GetComponent<ImageObject>();
						imageObject.Image = new FileUtils.File(obj.Path.RealPath);
						imageObject.Apply();
						break;
					case "AudioObject":
						newObj.GetComponent<AudioObject>().Audio = new FileUtils.File(obj.Path.RealPath);
						break;
					case "VideoObject":
						newObj.GetComponent<VideoObject>().Video = new FileUtils.File(obj.Path.RealPath);
						break;
					case "TextObject":
						newObj.GetComponent<TextObject>().Text = new FileUtils.File(obj.Path.RealPath);
						break;
					case "LinkObject":
						LinkObject linkObject = newObj.GetComponent<LinkObject>();
						linkObject.Link = new DirectoryUtils.Directory(obj.Path.RealPath);
						linkObject.Apply();
						break;
					}
				}
			}

			ToLoad = false;
		}
	}

	public static string GetRoot(string save)
	{
		FileStream file = File.Open(save, FileMode.Open);
		string root = (string)formatter.Deserialize(file);
		file.Close();

		return root;
	}
}
