using System.Collections;
﻿using UnityEngine;

public class PickUpObj : MonoBehaviour
{
	public GameObject Hook;

	private bool picked;

	void Start()
	{
		//Hook = GameObject.Find("Hook");
	}

	void Update()
	{
		if (picked == true)
		{
			transform.position = Hook.transform.position;
			transform.rotation = Hook.transform.rotation;
		}
	}

	public void pickUp(GameObject hook)
	{
		//Debug.Log ("coucou c'est sensé ramasser");
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
