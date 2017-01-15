///<summary>
///VideoObject handlers
///</summary>

using UnityEngine;
using System.Collections;


[System.Serializable]
public class VideoObject : Bolt.EntityBehaviour<IVideoObjectState>
{
	public FileUtils.File Video;

	public void Start()
	{
		Debug.Log ("TEST");
	}

	public void Apply()
	{

	}
}
