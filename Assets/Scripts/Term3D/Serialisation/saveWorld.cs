using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization; 

public class saveWorld : MonoBehaviour {

	public GameObject[] all;
	//public serializableObj[] objs;


	public serializableObj[] objs = new serializableObj[10];



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.P))
			{
				Debug.Log ("j'appuie sur P");
				save();	
			}
	}

	public void save()
	{
		var other = GameObject.FindGameObjectsWithTag("OtherObject");
		var audio = GameObject.FindGameObjectsWithTag("AudioObject");
		var video = GameObject.FindGameObjectsWithTag("VideoObject");
		var text = GameObject.FindGameObjectsWithTag("TextObject");
		var link = GameObject.FindGameObjectsWithTag("LinkObject");
		var image = GameObject.FindGameObjectsWithTag("ImageObject");

		//désolé c'est pas beau, faut tout passer en liste pour pouvoir append une liste à l'autre...
		for (int i = 0; i < other.Length; i++)
		{
			all.AddFirst<GameObject>(other[i]);
		}
		for (int i = 0; i < audio.Length; i++)
		{
			all.AddFirst<GameObject>(audio[i]);
		}
		for (int i = 0; i < video.Length; i++)
		{
			all.AddFirst<GameObject>(video[i]);
		}
		for (int i = 0; i < text.Length; i++)
		{
			all.AddFirst<GameObject>(text[i]);
		}
		for (int i = 0; i < link.Length; i++)
		{
			all.AddFirst<GameObject>(link[i]);
		}
		for (int i = 0; i < image.Length; i++)
		{
			all.AddFirst<GameObject>(image[i]);
		}

		serializableObj[] objs = new serializableObj[all.Length];



		for (int i = 0; i < all.Length; i++) {
			objs [i].x = all [i].transform.position.x;
			objs [i].y = all [i].transform.position.y;
			objs [i].z = all [i].transform.position.z;
			objs [i].xRotate = all [i].transform.rotation.eulerAngles.x;
			objs [i].yRotate = all [i].transform.rotation.eulerAngles.y;
			objs [i].zRotate = all [i].transform.rotation.eulerAngles.z;
			objs [i].objName = all [i].name;
		}

		Debug.Log (Application.persistentDataPath);

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/roomInfo.dat", FileMode.Open);

		bf.Serialize (file, objs);
		file.Close();
	}
}
