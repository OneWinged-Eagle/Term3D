using UnityEngine;
using System.Collections;

///<summary>
///Serializable object to save GameObjects
///</summary>
[System.Serializable]
public class SerializableObj
{
	public float x;
	public float y;
	public float z;

	public float xRotate;
	public float yRotate;
	public float zRotate;

	public string objName; // TODO: à virer ?
	public Bolt.PrefabId objId;

	public AudioObject audio;
	public ImageObject image;
	public TextObject text;
	public LinkObject link;
	public VideoObject video;
}
