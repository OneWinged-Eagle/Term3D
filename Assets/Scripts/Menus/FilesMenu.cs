using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Text;

///<summary>
///Handle FilesMenu actions (browse and link files/directories to models)
///</summary>
[BoltGlobalBehaviour()]
public class FilesMenu : Bolt.GlobalEventListener // TODO: à vérif' (@guillaume)
{
	private const int MARGIN = 300;

	public GameObject Btn;
	public GameObject Content;
	public GameObject Model;
	public ModelsUtils.FilesTypes FileType;

	private void createContent(PathUtils.Path[] paths, int height)
	{
		if (paths.Length % 3 != 0)
			height += MARGIN;

		RectTransform rectTransform = Content.gameObject.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(0, height);
		rectTransform.anchoredPosition = new Vector2(0, -(height / 2 - MARGIN));

		for (int i = 0; i < paths.Length; ++i)
		{
			PathUtils.Path path = paths[i];
			GameObject btn = Instantiate(Btn) as GameObject;

			btn.GetComponent<RectTransform>().anchoredPosition = new Vector2((i % 3 * MARGIN) - MARGIN, (height / 2 - MARGIN / 2) - (i / 3 * MARGIN));
			btn.GetComponent<Button>().onClick.AddListener(delegate { FilesBtns(path); });
			if (FileType == ModelsUtils.FilesTypes.Image)
				btn.GetComponentInChildren<Image>().sprite = TextureUtils.FileToSprite((FileUtils.File)path);
			btn.GetComponentInChildren<Text>().text = path.GetName();
			btn.transform.SetParent(Content.transform, false);
		}
	}

	public void CreateFileList()
	{



		if (BoltNetwork.isServer) {
			DirectoryUtils.Directory dir = new DirectoryUtils.Directory (PathUtils.CurrPath);
			PathUtils.Path[] paths;
			if (FileType == ModelsUtils.FilesTypes.Link)
				paths = dir.GetDirectories ();
			else
				paths = dir.GetFiles (ModelsUtils.Extensions [(int)FileType]);
			
			createContent (paths, paths.Length / 3 * MARGIN);
		} else if (BoltNetwork.isClient) {
			var param = paramEvent.Create ();


			param.askFiles = true;
			param.filePath = "";

			param.Send ();
		}
	}
		
	public void sendPaths ()
	{
		var param = paramEvent.Create ();

		DirectoryUtils.Directory dir = new DirectoryUtils.Directory (PathUtils.CurrPath);
		PathUtils.Path[] paths;
		if (FileType == ModelsUtils.FilesTypes.Link)
			paths = dir.GetDirectories ();
		else
			paths = dir.GetFiles (ModelsUtils.Extensions [(int)FileType]);


		/*string pathsString = string.Join("#", paths.getProjectPath());
			 * */
		param.filePath = "pouetpouet";   // param.filePath = pathsString;
		param.askFiles = false;


		param.Send ();
	}

	public void getPaths(string filePaths)
	{
		//remetre les bails en array de path.utils.path ou je sais pas quoi j'ai rien compris

//		createContent (paths, paths.Length / 3 * MARGIN);

	}

	public override void OnEvent(paramEvent e)
	{
		if (BoltNetwork.isServer) 
		{
			if (e.askFiles) {
				sendPaths ();
			}
		} 
		else if (BoltNetwork.isClient) 
		{
			if (!e.askFiles)
				getPaths (e.filePath);
		}
	}

	public void FilesBtns(PathUtils.Path path)
	{
		Model.GetComponent<BaseObject>().state.Name = path.ProjectPath;
		switch (FileType)
		{
		case ModelsUtils.FilesTypes.Image:
			/*ImageObject imageObject = Model.AddComponent<ImageObject>();
			imageObject.Image = (FileUtils.File)path;
			imageObject.Apply();*/
			Model.GetComponent<SendFile>().sendFile((FileUtils.File)path);
			Model.GetComponent<ImageObject>().Image = (FileUtils.File)path;
			Model.GetComponent<ImageObject>().Apply();
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
		foreach (Transform child in Content.transform)
			GameObject.Destroy(child.gameObject);
		gameObject.SetActive(false);
	}
}
