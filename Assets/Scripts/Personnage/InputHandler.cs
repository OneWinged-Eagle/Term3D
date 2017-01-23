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
    private GameObject mainPauseMenu;

	private GameObject modelsMenu;
    //private GameObject mainModelMenu;

	private ModelsMenu modelsMenuScript;

	public GameObject filesMenu;
	private FilesMenu filesMenuScript;

    private PanelManager panelManager;
    private Animator escMain;
    private Animator modelMain;

    //Gestion menus ingame
    private PanelManager    panelModelsManager;
    private PanelManager    panelEscManager;
    private PanelManager    panelFilesManager;

    private GameObject      mainModelMenu;
    private GameObject      mainEscMenu;
    private GameObject      mainFilesMenu;

	public override void Attached()
	{
        modelsMenu = GameObject.Find("ModelMenu");
        modelsMenuScript = modelsMenu.GetComponent<ModelsMenu>();
        modelsMenuScript.Player = gameObject;

        // Get des panels
        panelModelsManager = GameObject.Find("MenuModelManager").GetComponent<PanelManager>();
        panelEscManager = GameObject.Find("MenuEscManager").GetComponent<PanelManager>();
        panelFilesManager = GameObject.Find("MenuFilesManager").GetComponent<PanelManager>();
        // Get des anime de bases
        mainModelMenu = GameObject.Find("MainModelMenu");
        mainEscMenu = GameObject.Find("MainEscMenu");
        mainFilesMenu = GameObject.Find("MainFileMenu");

        filesMenu = GameObject.Find("FilesMenu");
        filesMenuScript = filesMenu.GetComponent<FilesMenu>();

    }

    public override void SimulateOwner()
	{
        if (Input.GetButtonDown("Pause") && panelModelsManager.GetAnimator() == null)
        {
            if (panelEscManager.GetAnimator() != mainEscMenu.GetComponent<Animator>())
                panelEscManager.OpenPanel(mainEscMenu.GetComponent<Animator>());
            else
                panelEscManager.CloseCurrent();
        }

        if (Input.GetButtonDown("ModelsMenu") && panelEscManager.GetAnimator() == null)
        {
            if (panelModelsManager.GetAnimator() == null)
            {
                modelsMenu.SetActive(true);
                panelModelsManager.OpenPanel(mainModelMenu.GetComponent<Animator>());
            }
            else
            {
                panelModelsManager.CloseCurrent();
            }
        }

        RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (panelEscManager.GetAnimator() == null)
		{
			if (Input.GetButtonDown("Interact"))
			{
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
				{
					switch (hit.collider.tag)
					{
					case "ImageObject":
						break;
					case "AudioObject":
						//Debug.Log("ça otuche" + hit.collider.tag);
						hit.transform.SendMessage("PlayAndPause", SendMessageOptions.DontRequireReceiver);
						hit.transform.SendMessage("sendPlayPauseSignal", SendMessageOptions.DontRequireReceiver);
						break;
					case "VideoObject":
						//Debug.Log ("OpenCanvas");
						hit.transform.SendMessage("OpenCanvas", gameObject, SendMessageOptions.DontRequireReceiver);
						break;
					case "TextObject":
						break;
					case "LinkObject":
						hit.transform.SendMessage("Go", gameObject, SendMessageOptions.DontRequireReceiver);
						break;
					case "OtherObject":
						//Debug.Log ("ça otuche" + hit.collider.tag);
						//hit.transform.SendMessage("pickUp", Hook, SendMessageOptions.DontRequireReceiver);
						hit.transform.SendMessage("AskControl", GetComponent<BoltEntity>().networkId, SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
			}
			//pas propre ici a refaire
			else if (Input.GetButtonDown("FilesMenu") && panelFilesManager.GetAnimator() == null)
			{
        if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
				{
          //Debug.Log("FilesMenu");
          //Debug.Log(hit.collider.tag);
          switch (hit.collider.tag)
					{
					case "ImageObject":
            filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Image;
						filesMenuScript.CreateFileList();
            panelFilesManager.OpenPanel(mainFilesMenu.GetComponent<Animator>());
            break;
					case "AudioObject":
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Audio;
						filesMenuScript.CreateFileList();
						panelFilesManager.OpenPanel(mainFilesMenu.GetComponent<Animator>());
						break;
					case "VideoObject":
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Video;
						filesMenuScript.CreateFileList();
						panelFilesManager.OpenPanel(mainFilesMenu.GetComponent<Animator>());
						break;
					case "TextObject":
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Text;
						filesMenuScript.CreateFileList();
						panelFilesManager.OpenPanel(mainFilesMenu.GetComponent<Animator>());
						break;
					case "LinkObject":
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Link;
						filesMenuScript.CreateFileList();
						panelFilesManager.OpenPanel(mainFilesMenu.GetComponent<Animator>());
						break;
					case "OtherObject":
						hit.transform.SendMessage("giveUpControl", GetComponent<BoltEntity>().networkId, SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
			}

			if (Input.GetButtonDown("Destroy"))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
						hit.transform.SendMessage("Destroy", true, SendMessageOptions.DontRequireReceiver);

			if (Input.GetButtonDown("PickUp"))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.SendMessage("AskControl", GetComponent<BoltEntity>().networkId, SendMessageOptions.DontRequireReceiver);

			if (Input.GetButtonDown("Throw"))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.SendMessage("giveUpControl", GetComponent<BoltEntity>().networkId, SendMessageOptions.DontRequireReceiver);

			if (Input.GetKey(KeyCode.Keypad4))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.Rotate(Vector3.up, -50 * Time.deltaTime);

			if (Input.GetKey(KeyCode.Keypad6))
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
					hit.transform.Rotate(Vector3.up, 50 * Time.deltaTime);

			if (Input.GetKeyDown(KeyCode.P))
				SavesHandler.Save();

			if (Input.GetKeyDown(KeyCode.M))
			{
				SavesHandler.ToLoad = true;
				SavesHandler.Load();
			}
		}
    base.SimulateOwner();
	}
}
