using UnityEngine;

///<summary>
///Utility functions and classes for Rooms
///</summary>
public class RoomUtils
{
	private const uint MAX = 10;

	private static int nb;

	public static Bolt.PrefabId Room;

	public static void Reset()
	{
		nb = GameObject.FindGameObjectsWithTag("Room").Length;
	}

	public static GameObject GetRoom(string name)
	{
		GameObject gameObj = GameObject.Find("/" + name);

		if (gameObj && gameObj.tag != "Room")
			gameObj = null;
		return gameObj;
	}

	public static GameObject CreateNewRoom(string name)
	{
		GameObject newRoom = BoltNetwork.Instantiate(Room,
				new Vector3((100 * (nb % MAX)) + (50 * (nb % MAX)), 0, ((nb / MAX) * 100) + (nb / MAX) * 50), Quaternion.identity);
		newRoom.name = name;
		nb++;
		return newRoom;
	}
}
