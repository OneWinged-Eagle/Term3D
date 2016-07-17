public class ImageObject : Bolt.EntityBehaviour<IImageObjectState>
{
	public FileUtils.File Image;

	/*public override void Attached()
	{
		if (BoltNetwork.isServer)
			state.Sprite = TextureUtils.FileToBase64(Image);
		state.AddCallBack("Sprite", spriteChanged);
	}

	private void spriteChanged()
	{
		GetComponent<Image>().sprite = TextureUtils.Base64ToSprite(state.Sprite);
	}*/
}
