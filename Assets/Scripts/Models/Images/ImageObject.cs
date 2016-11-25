using UnityEngine;
using UnityEngine.UI;

///<summary>
///ImageObject handlers
///</summary>
[System.Serializable]
public class ImageObject : Bolt.EntityBehaviour<IImageObjectState>
{
	public FileUtils.File Image;
	public string pathToFile;

	private Texture texture;

	public void Apply()
	{
		if (texture == null && BoltNetwork.isServer)
			if (GetComponent<Renderer> ())
				texture = GetComponent<Renderer> ().material.mainTexture = TextureUtils.FileToTexture (Image);
			else
				texture = GetComponentInChildren<Renderer> ().material.mainTexture = TextureUtils.FileToTexture (Image);
		else if (BoltNetwork.isServer)
			texture = TextureUtils.FileToTexture (Image);
		else if (texture == null && BoltNetwork.isClient)
			if (GetComponent<Renderer> ())
				texture = GetComponent<Renderer> ().material.mainTexture = TextureUtils.FileToTexture (pathToFile);
			else
				texture = GetComponentInChildren<Renderer> ().material.mainTexture = TextureUtils.FileToTexture (pathToFile);
		else if (BoltNetwork.isClient)
			texture = TextureUtils.FileToTexture (pathToFile);
	}
}
