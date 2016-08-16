using System.Collections;
﻿using UnityEngine;

public class Movement : Bolt.EntityBehaviour<IPlayerState>
{
	public float moveSpeed;
	public float rotateSpeed;

	public GameObject cameraPlayer;

	// Use this for initialization
	public override void Attached()
	{
		print("player init");

		moveSpeed = 10f;
		//rotateSpeed = 1.0f;

		state.Transform.SetTransforms(transform);
		//state.Speed = moveSpeed;
		base.Attached();
	}

	// Update is called once per frame
	public override void SimulateOwner()
	{
		transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed, Input.GetAxis("Mouse X"), 0f);
		if (entity.isOwner)
			cameraPlayer.SetActive (true);
		base.SimulateOwner();
	}
}
