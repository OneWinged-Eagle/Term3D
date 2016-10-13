using UnityEngine;
using System.Collections;

[System.Serializable]
public class SerializableObj
{
	public float x;
	public float y;
	public float z;

	public float xRotate;
	public float yRotate;
	public float zRotate;

	public string objName; // a virer ?
	public Bolt.PrefabId objId;

	public AudioObject audio;
	public ImageObject image;
	public TextObject text;
	public LinkObject link;
	public VideoObject video;

}
