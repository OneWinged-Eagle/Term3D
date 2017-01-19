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
    private GameObject      mainModelMenu;
    private GameObject      mainEscMenu;

	public override void Attached()
	{
        modelsMenu = GameObject.Find("ModelMenu");
        modelsMenuScript = modelsMenu.GetComponent<ModelsMenu>();
        modelsMenuScript.Player = gameObject;

        // Get des panels
        panelModelsManager = GameObject.Find("MenuModelManager").GetComponent<PanelManager>();
        panelEscManager = GameObject.Find("MenuEscManager").GetComponent<PanelManager>();
        // Get des anime de bases
        mainModelMenu = GameObject.Find("MainModelMenu");
        mainEscMenu = GameObject.Find("MainEscMenu");

        /*panelManager = GameObject.Find("GameMenuManager").GetComponent<PanelManager>();
        escMain = GameObject.Find("MenuEscManager").GetComponent<PanelManager>();*/

        //pauseMenu = GameObject.Find("EscMenu");
        //mainPauseMenu = GameObject.Find("MainEscMenu");
        //PanelManager pm = GameObject.Find("MenuEscManager").GetComponent<PanelManager>();
        //pm.CloseCurrent();
        //pauseMenu.SetActive(false);

        //modelsMenu = GameObject.Find("ModelMenu");
        //mainModelMenu = GameObject.Find("MainModelMenu");

        //pm = GameObject.Find("MenuModelManager").GetComponent<PanelManager>();
        //pm.CloseCurrent();
        //modelsMenu.SetActive(false);

        filesMenu = GameObject.Find("FilesMenu");
		filesMenuScript = filesMenu.GetComponent<FilesMenu>();
		filesMenu.SetActive(false);
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
            if (panelModelsManager.GetAnimator() != mainModelMenu.GetComponent<Animator>())
            {
                panelModelsManager.OpenPanel(mainModelMenu.GetComponent<Animator>());
            }
            else
            {
                panelModelsManager.CloseCurrent();
            }
        }
        //if (Input.GetButtonDown("Pause") && !filesMenu.activeSelf && !modelsMenu.activeSelf)
        //    pauseMenu.SetActive(!pauseMenu.activeSelf);

        /*if (Input.GetButtonDown("ModelsMenu"))
        { 
            //modelsMenu.SetActive(!modelsMenu.activeSelf);
            PanelManager pm = GameObject.Find("MenuModelManager").GetComponent<PanelManager>();
            Animator menu = mainModelMenu.GetComponent<Animator>();

            if (pm.GetAnimator() != menu)
            {
                modelsMenu.SetActive(true);
                mainModelMenu.SetActive(true);
                pm.CloseCurrent();
                pm.OpenPanel(menu);
            }
            else
            {
                pm.CloseCurrent();
            }
        }*/

        RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (true)
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
			else if (Input.GetButtonDown("FilesMenu"))
			{
				if (Physics.Raycast(intersectionRay, out hit, LenghtRay))
				{
					switch (hit.collider.tag)
					{
					case "ImageObject":
						filesMenu.SetActive(true);
						//Debug.Log(filesMenuScript.Model);
						//Debug.Log(hit.collider.gameObject);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Image;
						filesMenuScript.CreateFileList();
						break;
					case "AudioObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Audio;
						filesMenuScript.CreateFileList();
						break;
					case "VideoObject":
						filesMenu.SetActive (true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Video;
						filesMenuScript.CreateFileList();
						break;
					case "TextObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Text;
						filesMenuScript.CreateFileList();
						break;
					case "LinkObject":
						filesMenu.SetActive(true);
						filesMenuScript.Model = hit.collider.gameObject;
						filesMenuScript.FileType = ModelsUtils.FilesTypes.Link;
						filesMenuScript.CreateFileList();
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
