using UnityEngine;
using UnityEngine.UI;

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
		Transform spawn = Player.GetComponentInChildren<addElementBehaviour>().Hook.transform;

		BoltNetwork.Instantiate(ModelsList[fileType].Models[nb], spawn.position, spawn.rotation);

		//Instantiate(ModelsList[fileType].Models[nb], spawnPoint.position, spawnPoint.rotation);

		CloseBtn();
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
