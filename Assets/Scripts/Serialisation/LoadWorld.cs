using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadWorld : MonoBehaviour
{
	public GameObject cubeVert;
	public GameObject cylindre;
	public GameObject audioObj;
  public GameObject videoObj;
  public GameObject linkObj;
  public GameObject textObj;
  public GameObject imageObj;
  public GameObject roomObj;

  public SerializableObj[] objs;

	// Use this for initializatio
	void Start() {}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.M))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/roomInfo.dat", FileMode.Open);

			objs = (SerializableObj[])bf.Deserialize(file);
			file.Close();

			Quaternion rotate;

			for (int i = 0; i < objs.Length; i++)
			{
				Debug.Log (objs [i].objName);
				if (objs [i].objName == "Cube vert(Clone)")
				{
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					GameObject cube = BoltNetwork.Instantiate (BoltPrefabs.Cube_vert, new Vector3 (objs [i].x, objs [i].y, objs [i].z), rotate);
				}
				else if (objs [i].objName == "Cylindre(Clone)")
				{
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					GameObject cyl = BoltNetwork.Instantiate(BoltPrefabs.Cylindre, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
		    }
		    else if (objs [i].objName == "Audio(Clone)")
				{
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					GameObject audio = BoltNetwork.Instantiate(BoltPrefabs.Audio, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
		      audio.AddComponent<AudioObject>();
		    }
		    else if (objs[i].objName == "Video(Clone)")
		    {
			    rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
					GameObject video = BoltNetwork.Instantiate(BoltPrefabs.Video, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
			    video.AddComponent<VideoObject>();
		    }
		    else if (objs[i].objName == "Link(Clone)")
		    {
		      rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
					GameObject link = BoltNetwork.Instantiate(BoltPrefabs.Link, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
		      link.AddComponent<LinkObject>();
		    }
		    else if (objs[i].objName == "Text(Clone)")
		    {
		      rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
					GameObject text = BoltNetwork.Instantiate(BoltPrefabs.Text, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
		      text.AddComponent<TextObject>();
		    }
		    else if (objs[i].objName == "Image(Clone)")
		    {
		    	rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
					GameObject image = BoltNetwork.Instantiate(BoltPrefabs.Image, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
		      image.AddComponent<ImageObject>();
		    }
		    else if (objs[i].objName == "Room(Clone)")
		    {
		    	rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
					GameObject room = BoltNetwork.Instantiate(BoltPrefabs.Room, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
		    }
		  }
		}
	}
}
