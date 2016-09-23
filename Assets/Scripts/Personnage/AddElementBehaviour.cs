using System.Collections;
﻿using UnityEngine;

public class AddElementBehaviour : Bolt.EntityBehaviour<IPlayerState>
{
	public GameObject Hook;

	public float LenghtRay;

	private GameObject modelsMenu;
	private ModelsMenu modelsMenuScript;

	private GameObject filesMenu;
	private FilesMenu filesMenuScript;

	public override void Attached()
	{
		modelsMenu = GameObject.Find("ModelsMenu");
		modelsMenuScript = modelsMenu.GetComponent<ModelsMenu>();
		modelsMenuScript.Player = gameObject;
		modelsMenu.SetActive(false);

		filesMenu = GameObject.Find("FilesMenu");
		filesMenuScript = filesMenu.GetComponent<FilesMenu>();
		filesMenu.SetActive(false);
	}

	public override void SimulateOwner()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("E des barres");
			modelsMenu.SetActive(true);
		}

		RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (filesMenu.activeSelf == false)
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("ça appuie");
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
				{
					switch (hit.collider.tag)
					{
					case "ImageObject":
						break;
					case "AudioObject":
						Debug.Log("ça otuche" + hit.collider.tag);
						hit.transform.SendMessage("PlayAndPause", SendMessageOptions.DontRequireReceiver);
						hit.transform.SendMessage("sendPlayPauseSignal", SendMessageOptions.DontRequireReceiver);
						break;
					case "VideoObject":
						break;
					case "TextObject":
						break;
					case "LinkObject":
						hit.transform.SendMessage("Go", gameObject, SendMessageOptions.DontRequireReceiver);
						break;
					case "OtherObject":
						Debug.Log("ça otuche" + hit.collider.tag);
						hit.transform.SendMessage("pickUp", Hook, SendMessageOptions.DontRequireReceiver);
						hit.transform.SendMessage("AskControl", SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
			}
			//pas propre ici a refaire
			else if (Input.GetMouseButtonDown(1))
			{
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
				{
					switch (hit.collider.tag)
					{
					case "ImageObject":
						filesMenu.SetActive(true);
						Debug.Log(filesMenuScript.Model);
						Debug.Log(hit.collider.gameObject);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Image;
						filesMenuScript.CreateFileList();
						hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						break;
					case "AudioObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Audio;
						filesMenuScript.CreateFileList();
						hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						break;
					case "VideoObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Video;
						filesMenuScript.CreateFileList();
						hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						break;
					case "TextObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Text;
						filesMenuScript.CreateFileList();
						hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						break;
					case "LinkObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Link;
						filesMenuScript.CreateFileList();
						hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						break;
					case "OtherObject":
						Debug.Log("ça otuche" + hit.collider.tag);
						hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
			}
			else if (Input.GetKey(KeyCode.O))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					if (hit.collider.tag == "OtherObject")
						hit.transform.SendMessage("Destroy", true, SendMessageOptions.DontRequireReceiver);
      /*else if (Input.GetKey(KeyCode.J))
        if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
          if (hit.collider.tag == "NonStaticObject")
            hit.transform.SendMessage("Play", true, SendMessageOptions.DontRequireReceiver);*/
			else if (Input.GetKey(KeyCode.Keypad0))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.SendMessage("pickUp", true, SendMessageOptions.DontRequireReceiver);
    base.SimulateOwner();
	}
}
