using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

///<summary>
///Saves handler: creates, saves and loads saves files
///</summary>
public class SavesHandler
{
	public static bool ToLoad	{ get; set; } = false;
	public static string SaveName { get; set; } = "save";

	private static BinaryFormatter formatter = new BinaryFormatter();

	public static void Save()
	{
		List<GameObject> gameObjs;

		foreach (string tag in ModelsUtils.Tags)
			foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag(tag))
				gameObjs.Add(gameObj);

		SerializableObj[] objs = new SerializableObj[objectList.Count];

		for (int i = 0; i < gameObjs.Count; i++)
		{
	    objs[i] = new SerializableObj();

			objs[i].x = objectList[i].transform.position.x;
			objs[i].y = objectList[i].transform.position.y;
			objs[i].z = objectList[i].transform.position.z;

			objs[i].xRotate = objectList[i].transform.rotation.eulerAngles.x;
			objs[i].yRotate = objectList[i].transform.rotation.eulerAngles.y;
			objs[i].zRotate = objectList[i].transform.rotation.eulerAngles.z;

			objs[i].objName = objectList[i].name;
			objs[i].objId = objectList [i].GetComponent<BoltEntity>().prefabId;

			objs[i].audio = objectList[i].GetComponent<AudioObject>();
			objs[i].link = objectList[i].GetComponent<LinkObject>();
			objs[i].image = objectList[i].GetComponent<ImageObject>();
			objs[i].text = objectList[i].GetComponent<TextObject>();
			objs[i].video = objectList[i].GetComponent<VideoObject>();
	  }

		FileStream file = File.Open(Application.persistentDataPath + "/" + SaveName + ".dat", FileMode.OpenOrCreate);
		formatter.Serialize(file, objs);
		file.Close();
	}

	public static void Load()
	{
		if (ToLoad)
		{
			FileStream file = File.Open(Application.persistentDataPath + "/" + SaveName + ".dat", FileMode.Open);
			SerializableObj[] objs = (SerializableObj[])formatter.Deserialize(file);
			file.Close();

			foreach (SerializableObj obj in objs)
				BoltNetwork.Instantiate(obj.objId, new Vector3(obj.x, obj.y, obj.z), Quaternion.Euler(obj.xRotate, obj.yRotate, obj.zRotate));

			ToLoad = false;
		}
	}
}
