using System.Collections;

﻿using UnityEngine;

///<summary>
///Free movement camera
///</summary>
///<remarks>
///To use in tests
///</remarks>
public class FreeCam : MonoBehaviour
{
	public float speed = 5.0f;

	private Vector3 pos;

	public void Start()
	{
		pos = transform.position;
	}

	public void Update()
	{
		if (Input.GetKey (KeyCode.UpArrow))
			pos += Vector3.forward * speed * Time.deltaTime;
		if (Input.GetKey (KeyCode.RightArrow))
			pos += Vector3.right * speed * Time.deltaTime;
		if (Input.GetKey (KeyCode.DownArrow))
			pos += Vector3.back * speed * Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftArrow))
			pos += Vector3.left * speed * Time.deltaTime;
	}
}
