using UnityEngine;
using System.Collections;

public class pickUpObj : MonoBehaviour {

	public GameObject hook;

	private bool picked;

	void Start () {
		
		hook = GameObject.Find ("Hook");
	}
	
	void Update () {

		if (picked == true) {
			transform.position = hook.transform.position;
			transform.rotation = hook.transform.rotation;
		}
	
	}

	public void pickUp(bool pickedUp)
	{
		Debug.Log ("coucou c'est sensé ramasser");
		picked = pickedUp;
	}

	public void Destroy()
	{
		Destroy (gameObject);
	}
}
