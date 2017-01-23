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

	public PathUtils.Path Path { get; set; }

	public SerializableObj(string name,
			Bolt.PrefabId id, Vector3 position, Vector3 eulerAngles,
			PathUtils.Path path)
	{
		Name = name;

		ID = id;
		PosX = position.x;
		PosY = position.y;
		PosZ = position.z;
		RotX = eulerAngles.x;
		RotY = eulerAngles.y;
		RotZ = eulerAngles.z;

		Path = path;
	}
}
