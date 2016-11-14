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
		List<GameObject> objectList;
		GameObject[] tmp;

		String[] tagString = {"ImageObject", "AudioObject", "VideoObject", "TextObject", "LinkObject", "OtherObject"};

		int y = 0;
		while (y <= 5)
		{
			tmp = GameObject.FindGameObjectsWithTag (tagString [y]);
			for (int i = 0; i < tmp.Length; i++)
				objectList.Add(tmp[i]);
			y++;
		}

		SerializableObj[] objs = new SerializableObj[objectList.Count];

		for (int i = 0; i < objectList.Count; i++)
		{
	    objs[i] = new SerializableObj();


			Debug.Log (objectList [i].GetComponent<BoltEntity> ().prefabId.GetType());

			objs[i].x = objectList[i].transform.position.x;
			objs[i].y = objectList[i].transform.position.y;
			objs[i].z = objectList[i].transform.position.z;

			objs[i].xRotate = objectList[i].transform.rotation.eulerAngles.x;
			objs[i].yRotate = objectList[i].transform.rotation.eulerAngles.y;
			objs[i].zRotate = objectList[i].transform.rotation.eulerAngles.z;

			objs[i].objName = objectList[i].name;  //still usefull ?
			objs [i].objId = objectList [i].GetComponent<BoltEntity> ().prefabId;

			objs[i].audio = objectList[i].GetComponent<AudioObject>();
			objs[i].link = objectList[i].GetComponent<LinkObject>();
			objs[i].image = objectList[i].GetComponent<ImageObject>();
			objs[i].text = objectList[i].GetComponent<TextObject>();
			objs[i].video = objectList[i].GetComponent<VideoObject>();
	  }

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/roomInfo.dat", FileMode.OpenOrCreate);

		bf.Serialize(file, objs);
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
			{
				Quaternion rotate = Quaternion.Euler(obj.xRotate, obj.yRotate, obj.zRotate);
				BoltNetwork.Instantiate(obj.objId, new Vector3(obj.x, obj.y, obj.z), rotate);
			}

			ToLoad = false;
		}
	}
}
