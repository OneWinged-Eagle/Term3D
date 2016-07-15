using UnityEngine;
using UnityEngine.UI;

public class FilesMenu : Bolt.GlobalEventListener
{
	public GameObject Btn;
	public GameObject Content;

	public void CreateFileList(int fileType) // see enum FilesTypes in ModelsUtils
	{
		PathUtils.RootPath = "C:\\Users\\szwank_g\\Pictures";
		PathUtils.CurrPath = "C:\\Users\\szwank_g\\Pictures";
		DirectoryUtils.Directory rootDir = new DirectoryUtils.Directory("C:\\Users\\szwank_g\\Pictures");

		FileUtils.File[] files = rootDir.GetFiles(ModelsUtils.ImagesExtension);
		int height = files.Length / 3 * 100;
		Content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, height); // TODO: bout en haut qui se voit pas
		for (uint i = 0; i < files.Length; ++i)
		{
			FileUtils.File file = files[i];
			GameObject btn = Instantiate(Btn) as GameObject;

			btn.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 + (i % 3 * 100), (height / 2 + 50) - (i / 3 * 100));
			btn.GetComponentInChildren<Image>().sprite = TextureUtils.LoadSprite(file.RealPath);
			btn.GetComponentInChildren<Text>().text = file.GetFileName();
			btn.transform.SetParent(Content.transform, false);
		}
	}

	public void CloseBtn()
	{
		gameObject.SetActive(false);
	}
}
