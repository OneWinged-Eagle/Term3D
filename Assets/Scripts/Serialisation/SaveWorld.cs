using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class SaveWorld : MonoBehaviour
{
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
		GameObject[] all = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
		SerializableObj[] objs = new SerializableObj[all.Length];

	    for (int i = 0; i < all.Length; i++)
		{
	     	objs[i] = new SerializableObj();

	      	objs[i].x = all[i].transform.position.x;
			objs[i].y = all[i].transform.position.y;
			objs[i].z = all[i].transform.position.z;

			objs[i].xRotate = all[i].transform.rotation.eulerAngles.x;
			objs[i].yRotate = all[i].transform.rotation.eulerAngles.y;
			objs[i].zRotate = all[i].transform.rotation.eulerAngles.z;

			objs[i].objName = all[i].name;

	      	objs[i].audio = all[i].GetComponent<AudioObject>();
	      	objs[i].link = all[i].GetComponent<LinkObject>();
	      	objs[i].image = all[i].GetComponent<ImageObject>();
	      	objs[i].text = all[i].GetComponent<TextObject>();
	      	objs[i].video = all[i].GetComponent<VideoObject>();
	    }

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/roomInfo.dat", FileMode.OpenOrCreate);

		bf.Serialize(file, objs);
		file.Close();
	}
}
