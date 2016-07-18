public class LinkObject : Bolt.EntityBehaviour<ILinkObjectState>
{
	public DirectoryUtils.Directory Link;

	public void Go()
	{
		PathUtils.CurrPath = Link.RealPath;
		// se TP dans la room
	}

	public void Apply()
	{
		RoomUtils.CreateNewRoom();
	}
}
