using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization; 

public class saveWorld : MonoBehaviour {

	public BoltEntity test;
	public GameObject lelel;
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
//		test = BoltEntity.FindObjectOfType<ICubeVert> ();
		lelel = GameObject.Find("Cube vert(Clone)");
		//test = GameObject.Find("Cube vert(clone)");
		all = GameObject.FindGameObjectsWithTag("NonStaticObject");




		for (int i = 0; i < 10; i++) {
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
