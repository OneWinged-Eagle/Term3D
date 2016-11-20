using System.Collections;

﻿using UnityEngine;

///<summary>
///Player's inputs handler
///</summary>
public class InputHandler : Bolt.EntityBehaviour<IPlayerState> // TODO: à retaper 100%
{
	public GameObject Hook;

	public float LenghtRay;

	private GameObject pauseMenu;

	private GameObject modelsMenu;
	private ModelsMenu modelsMenuScript;

	private GameObject filesMenu;
	private FilesMenu filesMenuScript;

	public override void Attached()
	{
		pauseMenu = GameObject.Find("PauseMenu");
		pauseMenu.SetActive(false);

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
			pauseMenu.SetActive(true);

		if (Input.GetKeyDown(KeyCode.Tab))
			modelsMenu.SetActive(true);

		RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (!pauseMenu.activeSelf && !modelsMenu.activeSelf && !filesMenu.activeSelf)
		{
			if (Input.GetMouseButtonDown(0))
			{
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
						Debug.Log ("ça otuche" + hit.collider.tag);
						Bolt.NetworkId networkId = GetComponent<BoltEntity> ().networkId;
						//hit.transform.SendMessage("pickUp", Hook, SendMessageOptions.DontRequireReceiver);
						hit.transform.SendMessage("AskControl",networkId, SendMessageOptions.DontRequireReceiver);
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
						//hit.transform.SendMessage("throwObj", SendMessageOptions.DontRequireReceiver);
						Bolt.NetworkId networkId = GetComponent<BoltEntity> ().networkId;
						hit.transform.SendMessage("giveUpControl",networkId, SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.O))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					if (hit.collider.tag == "OtherObject")
						hit.transform.SendMessage("Destroy", true, SendMessageOptions.DontRequireReceiver);
      /*else if (Input.GetKey(KeyCode.J))
        if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
          if (hit.collider.tag == "NonStaticObject")
            hit.transform.SendMessage("Play", true, SendMessageOptions.DontRequireReceiver);*/

			if (Input.GetKeyDown(KeyCode.E))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.SendMessage("pickUp", Hook, SendMessageOptions.DontRequireReceiver);

			if (Input.GetKey(KeyCode.Keypad4))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.Rotate(Vector3.up, -50 * Time.deltaTime);

			if (Input.GetKey(KeyCode.Keypad6))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.Rotate(Vector3.up, 50 * Time.deltaTime);
		}
    base.SimulateOwner();
	}
}
