using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilesMenu : Bolt.GlobalEventListener
{
	public GameObject Btn;
	public GameObject Content;
	public GameObject Model;
	public ModelsUtils.FilesTypes FileType;

	public void CreateFileList()
	{
		DirectoryUtils.Directory dir = new DirectoryUtils.Directory(PathUtils.PathToProjectPath(PathUtils.CurrPath));
		PathUtils.Path[] paths;
		if (FileType == ModelsUtils.FilesTypes.Link)
			paths = dir.GetDirectories();
		else
			paths = dir.GetFiles(ModelsUtils.Extensions[(int)FileType]);

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
			if (FileType == ModelsUtils.FilesTypes.Image)
				btn.GetComponentInChildren<Image>().sprite = TextureUtils.FileToSprite((FileUtils.File)path);
			btn.GetComponentInChildren<Text>().text = path.GetName();
			btn.transform.SetParent(Content.transform, false);
		}
	}

	public void FilesBtns(PathUtils.Path path)
	{
		switch (FileType)
		{
		case ModelsUtils.FilesTypes.Image:
			ImageObject imageObject = Model.AddComponent<ImageObject>();
			imageObject.Image = (FileUtils.File)path;
			imageObject.Apply();
			break;
		case ModelsUtils.FilesTypes.Audio:
			Model.AddComponent<AudioObject>().Audio = (FileUtils.File)path;
			break;
		case ModelsUtils.FilesTypes.Video:
			Model.AddComponent<VideoObject>().Video = (FileUtils.File)path;
			break;
		case ModelsUtils.FilesTypes.Text:
			Model.AddComponent<TextObject>().Text = (FileUtils.File)path;
			break;
		case ModelsUtils.FilesTypes.Link:
			LinkObject linkObject = Model.AddComponent<LinkObject>();
			linkObject.Link = (DirectoryUtils.Directory)path;
			linkObject.Apply();
			break;
		}
		CloseBtn();
	}

	public void CloseBtn()
	{
		gameObject.SetActive(false);
	}
}
