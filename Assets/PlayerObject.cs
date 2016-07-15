using UnityEngine;
using System.Collections;


public class PlayerObject {
	public BoltEntity character;
	public BoltConnection connection;

	public bool isServer {
		get { return connection == null; }
	}

	public bool isClient {
		get { return connection != null; }
	}

	public void Spawn()
	{
		Debug.Log ("coucouocucoucoucoucoucoucoucoucocuocucoucououcoucocuocuocucoucoucoucuoc");
		if (!character) {
			character = BoltNetwork.Instantiate (BoltPrefabs.Player);
			if (isServer) 
			{
				Debug.Log ("pouet serveru   s" + connection);
				character.TakeControl ();
			} 
			else 
			{
				Debug.Log ("apoepazoepzaoepzaoepazoepzoepoazpea   " + connection);
				character.AssignControl (connection);
			}
		}
		character.transform.position = RandomPosition();
	}


	Vector3 RandomPosition()
	{
		float x = Random.Range (30f, 50f);
		float z = Random.Range (30f, 50f);
		return new Vector3 (x, 0.5f, z);
	}

}
