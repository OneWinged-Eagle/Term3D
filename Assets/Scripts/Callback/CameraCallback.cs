using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour("Term3D")]
public class CameraCallback : Bolt.GlobalEventListener {

	public override void SceneLoadLocalDone(string map)
	{
		//PlayerCamera.Instantiate ();
	}

	public override void ControlOfEntityGained(BoltEntity obj)
	{
		//PlayerCamera.instance.SetTarget (obj);
	}
}
