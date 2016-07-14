using UnityEngine;
using System.Collections;

public class addElement : MonoBehaviour {
	public Transform spawnPoint;
	public GameObject spawnObject;
	public GameObject spawnObject2;


	public float lenghtRay;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("obj spawn");
			BoltNetwork.Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
			//Instantiate (spawnObject, spawnPoint.position, spawnPoint.rotation);
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			Debug.Log ("obj spawn");
			BoltNetwork.Instantiate(BoltPrefabs.Cylindre, spawnPoint.position, Quaternion.identity);
			//Instantiate (spawnObject2, spawnPoint.position, spawnPoint.rotation);
		}

		RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay (new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("ça appuie");
			if (Physics.Raycast (intersectionRay, out hit, lenghtRay)) {
				if (hit.collider.tag == "Environment")
					Debug.Log ("ça otuche" + hit.collider.tag);
				if (hit.collider.tag == "NonStaticObject") {
					Debug.Log ("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage ("pickUp", true, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		//pas propre ici a refaire
		else if (Input.GetMouseButtonDown (1)) {
			if (Physics.Raycast (intersectionRay, out hit, lenghtRay)) {
				if (hit.collider.tag == "NonStaticObject") {
					Debug.Log ("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage ("pickUp", false, SendMessageOptions.DontRequireReceiver);
				}
			}
		} else if (Input.GetKey (KeyCode.O))
			if (Physics.Raycast (intersectionRay, out hit, lenghtRay))
				if (hit.collider.tag == "NonStaticObject")
					hit.transform.SendMessage ("Destroy", true, SendMessageOptions.DontRequireReceiver);
	}
}