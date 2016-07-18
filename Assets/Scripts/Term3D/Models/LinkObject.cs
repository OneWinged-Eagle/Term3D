using UnityEngine;

[System.Serializable]
public class LinkObject : Bolt.EntityBehaviour<ILinkObjectState>
{
	public DirectoryUtils.Directory Link;
	private GameObject room;

	public void Go(GameObject player)
	{
		PathUtils.CurrPath = Link.RealPath;
		player.transform.position = room.transform.Find("Spawn").transform.position;
	}

	public void Apply()
	{
		room = RoomUtils.CreateNewRoom();
	}
}
