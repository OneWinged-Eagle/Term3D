using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class loadWorld : MonoBehaviour
{
	public GameObject cubeVert;
	public GameObject cylindre;
	public GameObject audioObj;
  public GameObject videoObj;
  public GameObject linkObj;
  public GameObject textObj;
  public GameObject imageObj;
  public GameObject roomObj;

  public serializableObj[] objs;

	// Use this for initializatio
	void Start() {}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.M))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/roomInfo.dat", FileMode.Open);

			objs = (serializableObj[])bf.Deserialize(file);

			file.Close();

			Quaternion rotate;
			Debug.Log(objs[0].objName);
			Debug.Log(objs[1].objName);

			for (int i = 0; i < objs.Length; i++)
			{
				Debug.Log (objs [i].objName);
				if (objs [i].objName == "Cube vert(Clone)")
				{
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
					GameObject cube = (GameObject) Instantiate (cubeVert, new Vector3 (objs [i].x, objs [i].y, objs [i].z), rotate);
				}
				else if (objs [i].objName == "Cylindre(Clone)")
				{
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
          GameObject cyl = (GameObject)Instantiate(cylindre, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
        }
        else if (objs [i].objName == "Audio(Clone)")
				{
					rotate = Quaternion.Euler (objs [i].xRotate, objs [i].yRotate, objs [i].zRotate);
        	GameObject audio = (GameObject)Instantiate(audioObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          audio.AddComponent<AudioObject>();
        }
        else if (objs[i].objName == "Video(Clone)")
        {
          rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
          GameObject video = (GameObject)Instantiate(videoObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          video.AddComponent<VideoObject>();
        }
        else if (objs[i].objName == "Link(Clone)")
        {
          rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
          GameObject link = (GameObject)Instantiate(linkObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          link.AddComponent<LinkObject>();
        }
        else if (objs[i].objName == "Text(Clone)")
        {
          rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
          GameObject text = (GameObject)Instantiate(textObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          text.AddComponent<TextObject>();
        }
        else if (objs[i].objName == "Image(Clone)")
        {
          rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
          GameObject image = (GameObject)Instantiate(imageObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          image.AddComponent<ImageObject>();
        }
        else if (objs[i].objName == "Room(Clone)")
        {
          rotate = Quaternion.Euler(objs[i].xRotate, objs[i].yRotate, objs[i].zRotate);
          Instantiate(imageObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          GameObject room = (GameObject)Instantiate(roomObj, new Vector3(objs[i].x, objs[i].y, objs[i].z), rotate);
          room.AddComponent<ImageObject>();
        }
      }
		}
	}
}
