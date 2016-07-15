using System.IO;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;

public class TextureUtils
{
	public static Sprite GetSpriteFromAsset(GameObject asset)
	{
		#if UNITY_EDITOR
		Texture2D texture = AssetPreview.GetAssetPreview(asset);

		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)); // TODO: souvent, lors du premier run, y'a un NullReferenceException à cette ligne...
		#endif
		return (null);
	}

	public static Sprite LoadSprite(string FilePath, float PixelsPerUnit = 100.0f)
	{
		Texture2D spriteTexture = LoadTexture(FilePath);

		return Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height),new Vector2(0,0), PixelsPerUnit);
	}

	public static Texture2D LoadTexture(string FilePath)
	{
		Texture2D texture2D;
		byte[] FileData;

		if (File.Exists(FilePath))
		{
			FileData = File.ReadAllBytes(FilePath);
			texture2D = new Texture2D(2, 2);
			if (texture2D.LoadImage(FileData))
				return texture2D;
		}
		return null;
	}
}
