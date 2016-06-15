using UnityEngine;
using System.Collections;

public class pickUpObj : MonoBehaviour {

	public GameObject hook;

	private bool picked;

	// Use this for initialization
	void Start () {
		
		hook = GameObject.Find ("Hook");
	}
	
	// Update is called once per frame
	void Update () {

		if (picked == true) {
			transform.position = hook.transform.position;
			transform.rotation = hook.transform.rotation;
		}
	
	}

	public void pickUp(bool pickedUp)
	{
		Debug.Log ("coucou ça otuche ici hihi");

		picked = pickedUp;
	}
}
