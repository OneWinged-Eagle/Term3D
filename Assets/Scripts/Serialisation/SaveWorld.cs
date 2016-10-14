using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class SaveWorld : MonoBehaviour
{
	public List<GameObject> objectList;
	//public List<SerializableObj> serializableList;

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.P))
		{
			Debug.Log("j'appuie sur P");
			save();
		}
	}

	public void save()
	{
		//GameObject[] all = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
		//SerializableObj[] objs = new SerializableObj[all.Length];

	
		GameObject[] tmp;

		String[] tagString = {"OtherObject","AudioObject", "LinkObject","ImageObject","VideoObject","TextObject"};

		Debug.Log (tagString);

		objectList.Clear ();
		int y = 0;
		while (y <= 5)
		{
			tmp = GameObject.FindGameObjectsWithTag (tagString [y]);
			for (int i = 0; i < tmp.Length; i++)
				objectList.Add (tmp[i]);
			y++;
		}

		/*
		tmp  = GameObject.FindGameObjectsWithTag ("OtherObject");
		for (int i = 0; i < tmp.Length; i++)
			objectList.Add (tmp[i]);
		tmp  = GameObject.FindGameObjectsWithTag ("AudioObject");
		for (int i = 0; i < tmp.Length; i++)
			objectList.Add (tmp[i]);
		tmp  = GameObject.FindGameObjectsWithTag ("LinkObject");
		for (int i = 0; i < tmp.Length; i++)
			objectList.Add (tmp[i]);
		tmp  = GameObject.FindGameObjectsWithTag ("ImageObject");
		for (int i = 0; i < tmp.Length; i++)
			objectList.Add (tmp[i]);
		tmp  = GameObject.FindGameObjectsWithTag ("VideoObject");
		for (int i = 0; i < tmp.Length; i++)
			objectList.Add (tmp[i]);
		tmp  = GameObject.FindGameObjectsWithTag ("TextObject");
		for (int i = 0; i < tmp.Length; i++)
			objectList.Add (tmp[i]);
		/*

		for (int i = 0; i < objectList.Count; i++) {
			serializableList [i].x = objectList [i].transform.position.x;
			serializableList [i].y = objectList [i].transform.position.y;
			serializableList [i].z = objectList [i].transform.position.z;

			serializableList [i].xRotate = objectList [i].transform.rotation.eulerAngles.x;
			serializableList [i].yRotate = objectList [i].transform.rotation.eulerAngles.y;
			serializableList [i].zRotate = objectList [i].transform.rotation.eulerAngles.z;

			serializableList [i].objName = objectList [i].name;

			serializableList [i].audio = objectList [i].GetComponent<AudioObject>();
			serializableList [i].link = objectList [i].GetComponent<LinkObject>();
			serializableList [i].image = objectList [i].GetComponent<ImageObject>();
			serializableList [i].text = objectList [i].GetComponent<TextObject>();
			serializableList [i].video = objectList [i].GetComponent<VideoObject>();
		}

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/roomInfo.dat", FileMode.OpenOrCreate);

		bf.Serialize(file, serializableList);
		file.Close();*/

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
}
