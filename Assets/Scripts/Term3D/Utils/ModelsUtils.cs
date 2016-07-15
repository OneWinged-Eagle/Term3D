using UnityEngine;

public class ModelsUtils
{
	[System.Serializable]
	public class ModelList
	{
		public GameObject[] Models;
	}

	public enum FilesTypes
	{
		Image,
		Audio,
		Video,
		Text,
		Link,
		Other
	}

	public static readonly string[] ImagesExtension =
	{
		".jpg", ".jpeg", ".png"
	};

	public static readonly string[] AudiosExtension =
	{
		".mp3", ".ogg"
	};

	public static readonly string[] VideosExtension =
	{
		".mp4"
	};

	public static readonly string[] TextsExtension =
	{
		".txt"
	};
}
