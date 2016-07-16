using System.Collections;
﻿using UnityEngine;

public class addElementBehaviour : Bolt.EntityBehaviour<IPlayerState>
{
	public Transform spawnPoint;
	public Transform hook;

	public float lenghtRay;



	private GameObject modelsMenu;
	private GameObject filesMenu;


	public override void Attached()
	{
		modelsMenu = GameObject.Find("ModelsMenu");
		modelsMenu.SetActive(false);
		filesMenu = GameObject.Find ("FilesMenu");
		filesMenu.SetActive (false);
	}

	public override void SimulateOwner()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log ("E des barres");
			modelsMenu.SetActive(true);
		}

		RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (filesMenu.activeSelf == false)
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("ça appuie");
				if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
				{
					if (hit.collider.tag == "Environment")
						Debug.Log("ça otuche" + hit.collider.tag);
					if (hit.collider.tag == "OtherObject")
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
				switch (hit.collider.tag)
				{
				case "OtherObject":
					Debug.Log("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage("pickUp", false, SendMessageOptions.DontRequireReceiver);
					break;
				case "AudioObject":
					filesMenu.SetActive (true);
					filesMenu.GetComponent<FilesMenu> ().Model = hit.collider.gameObject;
					filesMenu.GetComponent<FilesMenu> ().CreateFileList (ModelsUtils.FilesTypes.Audio);
					break;
				case "TextObject":
					filesMenu.SetActive (true);
					filesMenu.GetComponent<FilesMenu> ().Model = hit.collider.gameObject;
					filesMenu.GetComponent<FilesMenu> ().CreateFileList (ModelsUtils.FilesTypes.Text);
					break;
				case "VideoObject":
					filesMenu.SetActive (true);
					filesMenu.GetComponent<FilesMenu> ().Model = hit.collider.gameObject;
					filesMenu.GetComponent<FilesMenu> ().CreateFileList (ModelsUtils.FilesTypes.Video);
					break;
				case "ImageObject":
					filesMenu.SetActive (true);
					filesMenu.GetComponent<FilesMenu> ().Model = hit.collider.gameObject;
					filesMenu.GetComponent<FilesMenu> ().CreateFileList (ModelsUtils.FilesTypes.Image);
					break;
				case "LinkObject":
					filesMenu.SetActive (true);
					filesMenu.GetComponent<FilesMenu> ().Model = hit.collider.gameObject;
					filesMenu.GetComponent<FilesMenu> ().CreateFileList (ModelsUtils.FilesTypes.Link);
					break;
				}
			}
		}
		else if (Input.GetKey(KeyCode.O))
			if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
				if (hit.collider.tag == "OtherObject")
					hit.transform.SendMessage("Destroy", true, SendMessageOptions.DontRequireReceiver);
		base.SimulateOwner();
	}
}
