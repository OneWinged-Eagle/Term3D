using System.Collections;
﻿using UnityEngine;

public class FreeCam : MonoBehaviour
{
	public float speed = 5.0f;
	public float spacing = 1.0f;
	private Vector3 pos;

	void Start()
	{
		pos = transform.position;
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.UpArrow))
			transform.position += Vector3.forward * speed * Time.deltaTime;
		if (Input.GetKey (KeyCode.DownArrow))
			transform.position += Vector3.back * speed * Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.position += Vector3.left * speed * Time.deltaTime;
		if (Input.GetKey (KeyCode.RightArrow))
			transform.position += Vector3.right * speed * Time.deltaTime;
	}
}
