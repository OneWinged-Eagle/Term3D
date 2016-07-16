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
		Debug.Log ("ça rentre ici ça prend le contreol");
		//PlayerCamera.instance.SetTarget (obj);
	}
}
