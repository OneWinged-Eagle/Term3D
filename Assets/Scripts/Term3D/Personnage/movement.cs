using UnityEngine;
using System.Collections;

public class movement : Bolt.EntityBehaviour<IPlayerState> {
	public float moveSpeed;
	public float rotateSpeed;

	public GameObject cameraPouet;


	public override void Attached ()
	{

		state.Transform.SetTransforms(transform);
		state.Speed = 10f;
		base.Attached ();
	}


	public override void SimulateOwner ()
	{
		transform.Translate (moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate (Input.GetAxis ("Mouse Y") * Time.deltaTime * rotateSpeed, Input.GetAxis("Mouse X"), 0f); 
		if (entity.isOwner) {
			cameraPouet.SetActive (true);
		}
		base.SimulateOwner ();
	}

	/*// Use this for initialization
	void Start () {
		moveSpeed = 10f;
		//rotateSpeed = 1.0f;

		print ("player init");
	}*/
	
	// Update is called once per frame
/*	void Update () {
		transform.Translate (moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate (Input.GetAxis ("Mouse Y") * Time.deltaTime * rotateSpeed, Input.GetAxis("Mouse X"), 0f); 
	}*/
}
