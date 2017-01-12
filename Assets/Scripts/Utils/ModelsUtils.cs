using UnityEngine;

///<summary>
///Utility functions and classes for Models
///</summary>
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

	public static readonly string[] Tags =
	{
		"Room",
		"ImageObject",
		"AudioObject",
		"VideoObject",
		"TextObject",
		"LinkObject",
		"OtherObject"
	};

	public static readonly string[] ImagesExtensions =
	{
		".jpg", ".jpeg",
		".png"
	};

	public static readonly string[] AudiosExtensions =
	{
		".mp3",
		".ogg"
	};

	public static readonly string[] VideosExtensions =
	{
		".mp4"
	};

	public static readonly string[] TextsExtensions =
	{
		".txt"
	};

	public static readonly string[][] Extensions =
	{
		ImagesExtensions,
		AudiosExtensions,
		VideosExtensions,
		TextsExtensions
	};
}
