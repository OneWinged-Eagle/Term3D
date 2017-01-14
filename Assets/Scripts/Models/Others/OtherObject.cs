using System.Collections;

using UnityEngine;

///<summary>
///OtherObject handlers
///</summary>
public class OtherObject : Bolt.EntityBehaviour<IOtherObjectState>
{
	public GameObject Hook;
	private bool picked;

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
