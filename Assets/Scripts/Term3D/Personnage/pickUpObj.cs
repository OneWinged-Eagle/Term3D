using UnityEngine;
using System.Collections;

public class pickUpObj : MonoBehaviour {

	public GameObject _hook;

	private bool picked;

	void Start () {
		
		//hook = GameObject.Find ("Hook");
	}
	
	void Update () {

		if (picked == true) {
			transform.position = _hook.transform.position;
			transform.rotation = _hook.transform.rotation;
		}
	
	}

	public void pickUp(GameObject hook)
	{
		//Debug.Log ("coucou c'est sensé ramasser");
		_hook = hook;
		picked = true;
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
	}

	public void throwObj()
	{
		picked = false;
		gameObject.GetComponent<Rigidbody> ().useGravity = true;

	}


	public void Destroy()
	{
		Destroy (gameObject);
	}
}
