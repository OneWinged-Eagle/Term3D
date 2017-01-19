using UnityEngine;

///<summary>
///LinkObject handlers
///</summary>
public class LinkObject : Bolt.EntityBehaviour<ILinkObjectState>
{
	public DirectoryUtils.Directory Link;

	private GameObject room;

	public void Go(GameObject player)
	{
		if (!room && name != "LinkObject")
		{
			room = RoomUtils.GetRoom(name);
			Link.RealPath = PathUtils.GetPathFromRelative(name);
			Link.ProjectPath = name;
		}

		if (room)
		{
			PathUtils.CurrPath = Link.RealPath;
			GameObject.Find("pwd").GetComponent<UnityEngine.UI.Text>().text = Link.ProjectPath;
			player.transform.position = room.transform.Find("Spawn").transform.position;
		}
	}

	public void Apply()
	{
		room = RoomUtils.GetRoom(Link.ProjectPath);
		if (!room)
			room = RoomUtils.CreateNewRoom(Link.ProjectPath);
	}
}
