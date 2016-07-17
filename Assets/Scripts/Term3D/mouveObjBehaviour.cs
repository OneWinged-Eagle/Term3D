using UnityEngine;
using System.Collections;

public class mouveObjBehaviour : Bolt.EntityBehaviour<IOtherObjectState>{
	public GameObject _hook;

	private bool picked;

	public override void Attached()
	{
		//_hook = GameObject.Find ("Hook");

		state.OtherObjectTransform.SetTransforms(transform);
		base.Attached();
	}

	public override void SimulateOwner()
	{
		if (picked == true) {
			transform.position = _hook.transform.position;
			transform.rotation = _hook.transform.rotation;
		}
		base.SimulateOwner ();
	}
		
	public void pickUp(GameObject hook)
	{
		Debug.Log ("coucou c'est sensé ramasser");
		_hook = hook;
		picked = true;
		gameObject.GetComponent<Rigidbody> ().useGravity = false;

	}

	public void throwObj()
	{
		picked = false;
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}
		
}
