using UnityEngine;
using System.Collections;

public class AudioPlayer : Bolt.EntityBehaviour<IAudioObjectState> {
	bool isListen = false;
	AudioSource audio;

	public override void Attached()
	{
		audio.Pause ();
		//gameObject.GetComponent<Renderer> ().material.color = Color.red;
		this.GetComponent<Renderer> ().material.color = Color.red;
		base.Attached ();
		Debug.Log ("zpas loué");

	}

	public void PlayAndPause()
	{
		audio = GetComponent<AudioSource> ();
		if (!isListen) {
			isListen = true;
			audio.Play ();
			this.GetComponent<Renderer> ().material.color = Color.green;
		} else {
			isListen = false;
			audio.Pause ();
			this.GetComponent<Renderer> ().material.color = Color.red;

		}
	}


	public void Stop()
	{
		isListen = false;
		audio.Stop ();
		this.GetComponent<Renderer> ().material.color = Color.grey;
	}

}
