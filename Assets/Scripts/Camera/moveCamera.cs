using UnityEngine;
using System.Collections;

public class moveCamera : MonoBehaviour {
	public GameObject player;
	public float rotateSpeed;

	// Use this for initialization
	void Start () {
		rotateSpeed = 1f;
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Input.GetAxis("Mouse X"));
		//transform.Rotate (Input.GetAxis ("Mouse Y"), Input.GetAxis("Mouse X")  * Time.deltaTime * rotateSpeed, 0f); 
		transform.RotateAround(player.transform.position, new Vector3(1.0f, 0.0f, 0.0f), Input.GetAxis("Mouse Y") * 100 *Time.deltaTime);
		//transform.RotateAround (player.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), Input.GetAxis ("Mouse X") * 100 * Time.deltaTime);
	}
}
