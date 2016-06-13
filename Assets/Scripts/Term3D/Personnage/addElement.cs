using UnityEngine;
using System.Collections;

public class addElement : MonoBehaviour {
	public Transform spawnPoint;
	public GameObject spawnObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log ("obj spawn");
			Instantiate (spawnObject, spawnPoint.position, spawnPoint.rotation);
		}

			
	
	}
}
