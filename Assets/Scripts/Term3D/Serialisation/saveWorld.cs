using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class saveWorld : MonoBehaviour {

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
		GameObject[] other = GameObject.FindGameObjectsWithTag("OtherObject");
        GameObject[] audio = GameObject.FindGameObjectsWithTag("AudioObject");
        GameObject[] video = GameObject.FindGameObjectsWithTag("VideoObject");
        GameObject[] text = GameObject.FindGameObjectsWithTag("TextObject");
        GameObject[] link = GameObject.FindGameObjectsWithTag("LinkObject");
        GameObject[] image = GameObject.FindGameObjectsWithTag("ImageObject");
        GameObject[] room = GameObject.FindGameObjectsWithTag("Room");

        List<GameObject> allL = new List<GameObject>();

        allL.AddRange(other);
        allL.AddRange(audio);
        allL.AddRange(video);
        allL.AddRange(text);
        allL.AddRange(link);
        allL.AddRange(image);
        allL.AddRange(room);

        GameObject[] all = new GameObject[allL.Count];

        all = allL.ToArray();
		serializableObj[] objs = new serializableObj[all.Length];

        for (int i = 0; i < all.Length; i++) {
            objs[i] = new serializableObj();

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

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/roomInfo.dat", FileMode.OpenOrCreate);

		bf.Serialize (file, objs);
		file.Close();
	}
}
