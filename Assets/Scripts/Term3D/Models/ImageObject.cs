using UnityEngine;
using UnityEngine.UI;

public class ImageObject : Bolt.EntityBehaviour<IImageObjectState>
{
	public FileUtils.File Image;
	private Texture texture;

	public void Apply()
	{
		if (texture == null && BoltNetwork.isServer)
			texture = GetComponent<Renderer>().material.mainTexture = TextureUtils.FileToTexture(Image);
	}
}
