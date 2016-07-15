using System.Collections;
﻿using UnityEngine;

public class FreeCam : MonoBehaviour
{
	public float _speed = 5.0f; // TODO: variable utilisée uniquement dans Update : à passer en variable locale ?
	public float _spacing = 1.0f; // TODO: variable non utilisée, à supprimer ?
	private Vector3 _pos; // TODO: variable utilisée uniquement dans Start : à passer en variable locale ?

	private void Start()
	{
		_pos = transform.position; // TODO: à quoi ça sert ?
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))
			transform.position += Vector3.forward * _speed * Time.deltaTime;

		if (Input.GetKey(KeyCode.RightArrow))
			transform.position += Vector3.right * _speed * Time.deltaTime;

		if (Input.GetKey(KeyCode.DownArrow))
			transform.position += Vector3.back * _speed * Time.deltaTime;

		if (Input.GetKey(KeyCode.LeftArrow))
			transform.position += Vector3.left * _speed * Time.deltaTime;
	}
}
