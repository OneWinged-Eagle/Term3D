using System.Collections;
﻿using UnityEngine;

public class addElementBehaviour : Bolt.EntityBehaviour<IPlayerState>
{
	public Transform spawnPoint;
	public GameObject spawnObject;
	public GameObject spawnObject2;

	public float lenghtRay;


	private GameObject modelsMenu;


	public override void Attached()
	{
		modelsMenu = GameObject.Find("ModelsMenu");
		modelsMenu.SetActive(false);
	}

	public override void SimulateOwner()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log ("E des barres");
			modelsMenu.SetActive(true);
			//BoltNetwork.Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
			//Instantiate (spawnObject, spawnPoint.position, spawnPoint.rotation);
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log("obj spawn");
			BoltNetwork.Instantiate(BoltPrefabs.Cylindre, spawnPoint.position, Quaternion.identity);
			//Instantiate (spawnObject2, spawnPoint.position, spawnPoint.rotation);
		}

		RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("ça appuie");
			if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
			{
				if (hit.collider.tag == "Environment")
					Debug.Log("ça otuche" + hit.collider.tag);
				if (hit.collider.tag == "NonStaticObject")
				{
					Debug.Log("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage("pickUp", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		//pas propre ici a refaire
		else if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
			{
				if (hit.collider.tag == "NonStaticObject")
				{
					Debug.Log("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage("pickUp", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		else if (Input.GetKey(KeyCode.O))
			if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
				if (hit.collider.tag == "NonStaticObject")
					hit.transform.SendMessage("Destroy", true, SendMessageOptions.DontRequireReceiver);
		base.SimulateOwner();
	}
}
