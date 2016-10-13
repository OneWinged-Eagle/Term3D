using UnityEngine;
using UnityEngine.UI;

[BoltGlobalBehaviour]
public class ModelsMenu : Bolt.GlobalEventListener
{
	public GameObject TypesMenu;
	public GameObject[] ModelsMenus;
	public ModelsUtils.ModelList[] ModelsList;
	public GameObject Player;

	private void Start() {}

	private void Update() {}

	public void TypesBtns(int fileType) // see enum FilesTypes in ModelsUtils
	{
		GameObject menu = ModelsMenus[fileType];
		TypesMenu.SetActive(false);
		menu.SetActive(true);

		GameObject[] models = ModelsList[fileType].Models;
		for (uint i = 0; i < models.Length; i++)
		{
			Image image = menu.transform.Find("ModelBtn_" + i).GetComponentInChildren<Image>();
			if (image.sprite == null)
				image.sprite = TextureUtils.GetSpriteFromAsset(models[i]);
		}
	}

	public void ModelsBtns(string datas)
	{
		int fileType = int.Parse(datas.Substring(0, datas.IndexOf(',')));
		int nb = int.Parse(datas.Substring(datas.IndexOf(',') + 1));
		Transform spawn = Player.GetComponentInChildren<AddElementBehaviour>().Hook.transform;

		spawn.Rotate(Vector3.up, 180);

		var spawnObjectEvent = spawnObject.Create ();
		Debug.Log (ModelsList [fileType].Models [nb]);
		spawnObjectEvent.objectTest = "coucou";
		spawnObjectEvent.objectId = ModelsList[fileType].Models[nb].GetComponent<BoltEntity>().prefabId;
		spawnObjectEvent.objectPos = spawn.position;
		spawnObjectEvent.objectRot = spawn.rotation;
		spawnObjectEvent.Send ();
	

		/////////////////////////////////
		//BoltNetwork.Instantiate(ModelsList[fileType].Models[nb], spawn.position, spawn.rotation);


		spawn.Rotate(Vector3.up, 180);

		///////////////////////////

		//Instantiate(ModelsList[fileType].Models[nb], spawnPoint.position, spawnPoint.rotation);

		CloseBtn();
	}

	public override void OnEvent(spawnObject e)
	{
		Debug.Log (e.objectTest);
		Debug.Log (e.objectId);
		Debug.Log (e.objectPos);
		Debug.Log (e.objectRot);
		if (BoltNetwork.isServer) {
			Debug.Log ("coucoucocuocuocu je suis un serveur");
			BoltNetwork.Instantiate (e.objectId, e.objectPos, e.objectRot);
		} else if (BoltNetwork.isClient)
			Debug.Log ("ne fé rien kek");
		else {
			Debug.Log ("je suis rien du tout :( ");
		}
	}


	public void BackBtn()
	{
		TypesMenu.SetActive(true);
		foreach (GameObject menu in ModelsMenus)
			if (menu != null)
				menu.SetActive(false);
	}

	public void CloseBtn()
	{
		BackBtn();
		gameObject.SetActive(false);
	}



}
