using UnityEngine;

///<summary>
///Utility functions and classes for Rooms
///</summary>
public class RoomUtils
{
	private const uint MAX = 10;

	private static uint nb;

	public static void Reset()
	{
		nb = 0;
	}

	public static GameObject CreateNewRoom()
	{
		GameObject newRoom = BoltNetwork.Instantiate(BoltPrefabs.Room, new Vector3(0, 0, 0), Quaternion.identity);
		newRoom.transform.Translate((100 * (nb % MAX)) + (50 * (nb % MAX)), 0, ((nb / MAX) * 100) + (nb / MAX) * 50);
		nb++;
		return newRoom;
	}
}
