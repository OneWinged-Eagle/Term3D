using UnityEngine;
using System.Collections;

public class StreamVideo2 : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () 
	{
		var www = new WWW ("https://upload.wikimedia.org/wikipedia/commons/e/e6/Typing_example.ogv");
		var movieTexture = www.movie;
		while (!movieTexture.isReadyToPlay)
			yield return movieTexture;

		GetComponent<Renderer>().material.mainTexture = movieTexture;
		movieTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}