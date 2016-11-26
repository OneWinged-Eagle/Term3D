using UnityEngine;
using System.Collections;

public class StreamVideo1 : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () 
	{
		var www = new WWW ("http://techslides.com/demos/sample-videos/small.ogv");
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