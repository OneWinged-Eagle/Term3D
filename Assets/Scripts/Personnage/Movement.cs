using System.Collections;

﻿using UnityEngine;

///<summary>
///Player's movement handler
///</summary>
public class Movement : Bolt.EntityBehaviour<IPlayerState> // TODO: mouvements du perso à refaire
{
	public float MoveSpeed;
	public float RotateSpeed;

	public GameObject CameraPlayer;

	public GameObject hook;

	public override void Attached()
	{
		MoveSpeed = 10f;
		RotateSpeed = 1.0f;

		state.Transform.SetTransforms(transform); // Assets/Scripts/Personnage/Movement.cs(20,33): warning CS0618: `Bolt.NetworkTransform.SetTransforms(UnityEngine.Transform)' is obsolete: `For setting the transform to replicate in Attached use the new IState.SetTransforms method instead, for changing the transform after it's been set use the new ChangeTransforms method'
		state.HookTransform.SetTransforms(hook.transform);
		base.Attached();
	}

	public override void SimulateOwner()
	{
		transform.Translate(MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * RotateSpeed, Input.GetAxis("Mouse X"), 0f);
		state.HookTransform.SetTransforms(hook.transform);


		if (entity.isOwner)
			CameraPlayer.SetActive(true);

		base.SimulateOwner();
	}
}
