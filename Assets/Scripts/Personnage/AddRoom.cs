using UnityEngine;
using System.Collections;

public class AddRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

            int nbRooms = rooms.Length;
            int maxLine = 10;

            GameObject newRoom = (GameObject)BoltNetwork.Instantiate(BoltPrefabs.Room, new Vector3(0, 0, 0), Quaternion.identity);
            newRoom.transform.Translate((100 * (nbRooms % maxLine)) + (50 * (nbRooms % maxLine)), 0, ((nbRooms / maxLine) * 100) + (nbRooms / maxLine) * 50);
        }
    }
}
