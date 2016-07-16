using UnityEngine;
using System.Collections;

public class mouveObjBehaviour : Bolt.EntityBehaviour<ICubeVert> {
	public Transform _hook;

	private bool picked;
	public override void Attached()
	{
		state.CubeVertTransform.SetTransforms(transform);
		base.Attached();
	}

	public override void SimulateOwner()
	{
		if (picked == true) {
			transform.position = _hook.position;
			transform.rotation = _hook.rotation;
		}
		base.SimulateOwner ();
	}
		
	public void pickUp(Transform hook)
	{
		Debug.Log ("coucou c'est sensé ramasser");
		picked = true;
		_hook = hook;
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}
}
