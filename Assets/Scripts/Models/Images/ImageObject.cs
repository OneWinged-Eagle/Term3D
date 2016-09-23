using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageObject : Bolt.EntityBehaviour<IImageObjectState>
{
	public FileUtils.File Image;
	private Texture texture;

	public void Apply()
	{
		if (texture == null && BoltNetwork.isServer)
			if (GetComponent<Renderer>())
				texture = GetComponent<Renderer>().material.mainTexture = TextureUtils.FileToTexture(Image);
			else
				texture = GetComponentInChildren<Renderer>().material.mainTexture = TextureUtils.FileToTexture(Image);
	}
}
