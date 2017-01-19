using UnityEngine;
using System.Collections;

public class StreamVideo : MonoBehaviour
{
	public string url;

	IEnumerator Start ()
	{
		if (!string.IsNullOrEmpty(url))
		{
			var www = new WWW (url);
			var movieTexture = www.movie;
			while (!movieTexture.isReadyToPlay)
				yield return movieTexture;

			GetComponent<Renderer>().material.mainTexture = movieTexture;
			movieTexture.Play ();
		}
	}
}
