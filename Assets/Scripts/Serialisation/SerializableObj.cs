using UnityEngine;
using System.Collections;

///<summary>
///Serializable object to save GameObjects
///</summary>
[System.Serializable]
public class SerializableObj
{
	public string Name { get; set; }

	public Bolt.PrefabId ID { get; set; }
	public float PosX { get; set; }
	public float PosY { get; set; }
	public float PosZ { get; set; }
	public float RotX { get; set; }
	public float RotY { get; set; }
	public float RotZ { get; set; }

	public ImageObject Image { get; set; }// \
	public AudioObject Audio { get; set; }//  \
	public VideoObject Video { get; set; }//   |Faire une class mère et remplacer par celle-ci
	public TextObject Text { get; set; }//    /
	public LinkObject Link { get; set; }// /

	public SerializableObj(string name,
			Bolt.PrefabId id, Vector3 position, Vector3 eulerAngles,
			ImageObject image, AudioObject audio, VideoObject video, TextObject text, LinkObject link)
	{
		Name = name;

		ID = id;
		PosX = position.x;
		PosY = position.y;
		PosZ = position.z;
		RotX = eulerAngles.x;
		RotY = eulerAngles.y;
		RotZ = eulerAngles.z;

		Image = image;
		Audio = audio;
		Video = video;
		Text = text;
		Link = link;
	}
}
