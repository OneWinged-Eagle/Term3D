using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class loadWorld : MonoBehaviour {

	public GameObject cubeVert;
	public GameObject cylindre;
	public GameObject audioObj;


	public serializableObj[] objs;
	// Use this for initializatio
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.M)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/roomInfo.dat", FileMode.Open);

			objs = (serializableObj[]) bf.Deserialize(file);
			//Debug.Log (bf.Deserialize (file));

			//Debug.Log (file);
			file.Close ();

			Quaternion rotate; 
			Debug.Log (objs [0].objName);
			Debug.Log (objs [1].objName);

			for (int i = 0; i < 10; i++) {
				Debug.Log (objs [i].objName);
				if (objs [i].objName == "Cube vert(Clone)") {
					Debug.Log ("coucou ici");
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					Instantiate (cubeVert, new Vector3 (objs [i].x, objs [i].y, objs [i].z), rotate);
				} else if (objs [i].objName == "Cylindre(Clone)") {
					Debug.Log ("coucou");
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					Instantiate (cylindre, new Vector3 (objs [i].x, objs [i].y, objs [i].z), rotate);
				}else if (objs [i].objName == "Audio(Clone)") {
					Debug.Log ("coucou");
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					Instantiate (audioObj, new Vector3 (objs [i].x, objs [i].y, objs [i].z), rotate);
				}
			}
		}
	}
}
