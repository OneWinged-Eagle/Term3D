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
		if (!room)
			room = RoomUtils.GetRoom(name);

		if (room)
		{
			PathUtils.CurrPath = Link.RealPath;
			GameObject.Find("pwd").GetComponent<UnityEngine.UI.Text>().text = PathUtils.PathToProjectPath(PathUtils.CurrPath);
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
