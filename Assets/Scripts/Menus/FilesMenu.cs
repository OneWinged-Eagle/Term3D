using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilesMenu : Bolt.GlobalEventListener
{
	public GameObject Btn;
	public GameObject Content;
	public GameObject Model;

	public void CreateFileList(int fileType) // see enum FilesTypes in ModelsUtils
	{
		PathUtils.RootPath = "C:\\Users\\szwank_g\\Pictures"; // à
		PathUtils.CurrPath = "C:\\Users\\szwank_g\\Pictures"; // virer
		DirectoryUtils.Directory dir = new DirectoryUtils.Directory(PathUtils.CurrPath);
		PathUtils.Path[] paths;
		if (fileType == (int)ModelsUtils.FilesTypes.Link)
			paths = dir.GetDirectories();
		else
			paths = dir.GetFiles(ModelsUtils.Extensions[fileType]);

		int nb = paths.Length;
		int height = nb / 3 * 100;
		if (nb % 3 != 0)
			height += 100;
		RectTransform rectTransform = Content.gameObject.GetComponent<RectTransform>();

		rectTransform.sizeDelta = new Vector2(0, height);
		rectTransform.anchoredPosition = new Vector2(0, -(height / 2 - 100));
		for (uint i = 0; i < nb; ++i)
		{
			PathUtils.Path path = paths[i];
			GameObject btn = Instantiate(Btn) as GameObject;

			btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100 + (i % 3 * 100), (height / 2 - 50) - (i / 3 * 100));
			btn.GetComponent<Button>().onClick.AddListener(delegate { FilesBtns(path); });
			if (fileType == (int)ModelsUtils.FilesTypes.Image)
				btn.GetComponentInChildren<Image>().sprite = TextureUtils.LoadSprite(path.RealPath);
			btn.GetComponentInChildren<Text>().text = path.GetName();
			btn.transform.SetParent(Content.transform, false);
		}
	}

	public void FilesBtns(PathUtils.Path path)
	{
		if (path.IsFile())
			Model.AddComponent<FileModel>().File = (FileUtils.File)path;
		else if (path.IsDirectory())
			Model.AddComponent<DirectoryModel>().Directory = (DirectoryUtils.Directory)path;
	}

	public void CloseBtn()
	{
		gameObject.SetActive(false);
	}
}
