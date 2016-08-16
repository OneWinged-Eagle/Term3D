using System.Collections;
﻿using UnityEngine;

public class MovementTest : Bolt.EntityBehaviour<IPlayerState>
{
	public float MoveSpeed;
	public float RotateSpeed;

	public GameObject Camera;

	public override void Attached()
	{
		Debug.Log("ciaehaisjq haihe  oejazo ehjzai hzeui rhiea ohriaohu iao lolololollol");
		state.Transform.SetTransforms(transform);
		//state.Speed = 10f;
		base.Attached();
	}

	public override void SimulateOwner()
	{
		transform.Translate(MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * RotateSpeed, Input.GetAxis("Mouse X"), 0f);
		if (entity.isOwner)
			Camera.SetActive(true);
		base.SimulateOwner();
	}

	// Use this for initialization
	/*
	void Start ()
	{
		MoveSpeed = 10f;
		//RotateSpeed = 1.0f;

		print("player init");
	}
	*/

	// Update is called once per frame
	/*
	void Update ()
	{
		transform.Translate(MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * RotateSpeed, Input.GetAxis("Mouse X"), 0f);
	}
	*/
}
