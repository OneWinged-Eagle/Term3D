using System.Collections;
using UnityEngine;

public class MoveObjBehaviour : Bolt.EntityBehaviour<IOtherObjectState>
{
	public GameObject Hook;

	private bool picked;

	public override void Attached()
	{
		//Hook = GameObject.Find("Hook");
		state.OtherObjectTransform.SetTransforms(transform);
		base.Attached();
	}

	public override void SimulateOwner()
	{
		if (picked == true)
		{
			transform.position = Hook.transform.position;
			transform.rotation = Hook.transform.rotation;
		}
		base.SimulateOwner();
	}

	public void pickUp(GameObject hook)
	{
		Debug.Log("coucou c'est sensé ramasser");
		Hook = hook;
		picked = true;
		gameObject.GetComponent<Rigidbody>().useGravity = false;
	}

	public void throwObj()
	{
		picked = false;
		gameObject.GetComponent<Rigidbody>().useGravity = true;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
