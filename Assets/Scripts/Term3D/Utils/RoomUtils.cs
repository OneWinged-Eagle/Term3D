using UnityEngine;

public class RoomUtils
{
	private static uint nb;
	private static uint max = 10;

	public static void CreateNewRoom()
	{
		GameObject newRoom = BoltNetwork.Instantiate(BoltPrefabs.Room, new Vector3(0, 0, 0), Quaternion.identity);
		newRoom.transform.Translate((100 * (nb % max)) + (50 * (nb % max)), 0, ((nb / max) * 100) + (nb / max) * 50);
		nb++;
	}
}
