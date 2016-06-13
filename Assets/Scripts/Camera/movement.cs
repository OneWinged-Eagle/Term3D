using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float moveSpeed;
	public float rotateSpeed;

	// Use this for initialization
	void Start () {
		moveSpeed = 10f;
		rotateSpeed = 1.0f;

		print ("player init");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical") * Time.deltaTime);
		//transform.Rotate (Input.GetAxis ("Mouse Y") * Time.deltaTime * rotateSpeed, Input.GetAxis("Mouse X"), 0f); 
	}
}
