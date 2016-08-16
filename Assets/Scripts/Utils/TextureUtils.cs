using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class TextureUtils
{
	public static Sprite GetSpriteFromAsset(GameObject asset) // TODO: à supprimer, fonction inutile :'(
	{
		#if UNITY_EDITOR
		Texture2D texture = AssetPreview.GetAssetPreview(asset);

		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); // TODO: souvent, lors du premier run, y'a un NullReferenceException à cette ligne...
		#endif
		return (null);
	}

	public static string FileToBase64(FileUtils.File File)
	{
		string base64;
		byte[] FileData;

		if (File.IsFile())
		{
			FileData = File.GetData();
			base64 = Convert.ToBase64String(FileData);
			return base64;
		}
		return null;
	}

	public static string FileToBase64(string FilePath)
	{
		return FileToBase64(new FileUtils.File(FilePath));
	}

	public static Sprite Base64ToSprite(string base64, float PixelsPerUnit = 100.0f)
	{
		Texture2D spriteTexture = Base64ToTexture(base64);

		return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
	}

	public static Texture2D Base64ToTexture(string base64)
	{
		byte[] data = Convert.FromBase64String(base64);
		Texture2D texture2D = new Texture2D(2, 2);

		if (texture2D.LoadImage(data))
			return texture2D;
		return null;
	}

	public static Sprite FileToSprite(FileUtils.File File, float PixelsPerUnit = 100.0f)
	{
		Texture2D spriteTexture = FileToTexture(File);

		return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
	}

	public static Sprite FileToSprite(string FilePath, float PixelsPerUnit = 100.0f)
	{
		return FileToSprite(new FileUtils.File(FilePath), PixelsPerUnit);
	}

	public static Texture2D FileToTexture(FileUtils.File File)
	{
		if (File.IsFile())
		{
			byte[] FileData = File.GetData();
			Texture2D texture2D = new Texture2D(2, 2);

			if (texture2D.LoadImage(FileData))
				return texture2D;
		}
		return null;
	}

	public static Texture2D FileToTexture(string FilePath)
	{
		return FileToTexture(new FileUtils.File(FilePath));
	}
}
