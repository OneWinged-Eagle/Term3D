using System.Collections;

using UnityEngine;

///<summary>
///OtherObject handlers
///</summary>
public class OtherObject : Bolt.EntityBehaviour<IOtherObjectState>
{
	public GameObject Hook;
	private bool picked;


	public override void Attached()
	{

		state.OtherObjectTransform.SetTransforms(transform); // Assets/Scripts/Models/Others/OtherObject.cs(16,44): warning CS0618: `Bolt.NetworkTransform.SetTransforms(UnityEngine.Transform)' is obsolete: `For setting the transform to replicate in Attached use the new IState.SetTransforms method instead, for changing the transform after it's been set use the new ChangeTransforms method'

		base.Attached();
	}

	public override void SimulateOwner()
	{
		if (picked)
		{
			transform.position = Hook.transform.position;
			transform.rotation = Hook.transform.rotation;

		}
		base.SimulateOwner();
	}

	public void pickUp(GameObject hook)
	{

		Debug.Log ("ça rammasse");
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
