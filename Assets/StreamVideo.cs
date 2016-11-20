using UnityEngine;
using System.Collections;

public class StreamVideo : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () 
	{
		var www = new WWW ("http://clips.vorwaerts-gmbh.de/big_buck_bunny.ogv");
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