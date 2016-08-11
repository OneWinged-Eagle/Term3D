using System.Collections;
﻿using UnityEngine;

[BoltGlobalBehaviour("Term3D")]
public class CameraCallback : Bolt.GlobalEventListener
{
	public override void SceneLoadLocalDone(string map)
	{
		//PlayerCamera.Instantiate();
	}

	public override void ControlOfEntityGained(BoltEntity obj)
	{
		Debug.Log("ça rentre ici ça prend le controle");
		//PlayerCamera.instance.SetTarget(obj);
	}
}
