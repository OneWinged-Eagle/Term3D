using System.Collections;

using UnityEngine;

///<summary>
///AudioObject handlers local
///</summary>
public class AudioPlayer : Bolt.EntityBehaviour<IAudioObjectState> // TODO: à merger avec le multi et foutre dans AudioObject.cs
{
	bool isListen = false;
	AudioSource audio;
	ParticleSystem ps;

	public override void Attached()
	{
		//audio.Pause();
		gameObject.GetComponent<Renderer>().material.color = Color.red;

		state.isPlayed = false;
		state.AddCallback("isPlayed", colorPlayed);
		Debug.Log(state.isPlayed);
		//this.GetComponent<Renderer> ().material.color = Color.red;
		base.Attached();
	}

	void colorPlayed()
	{
		audio = GetComponent<AudioSource>();
		ps = GetComponent<ParticleSystem>();
		var em = ps.emission;

		if (state.isPlayed == true)
		{
			audio.Play();
			em.enabled = true;
			this.GetComponent<Renderer>().material.color = Color.green;
		}
		else if (state.isPlayed == false)
		{
			this.GetComponent<Renderer>().material.color = Color.red;
			em.enabled = false;
			audio.Pause();
		}
	}

	public void PlayAndPause()
	{
		audio = GetComponent<AudioSource>();
		//audio.clip = GetComponent<AudioObject>().GetComponent<AudioSource>();
		string path = gameObject.GetComponent<AudioObject>().Audio.RealPath;

		WWW testwww = new WWW("file://" + path);
		Debug.Log("file://" + path);

		AudioClip monSon = testwww.audioClip;
		audio.clip = monSon;

		if (!isListen)
		{
			isListen = true;
			state.isPlayed = true;
			audio.Play();
			//this.GetComponent<Renderer>().material.color = Color.green;
		}
		else
		{
			isListen = false;
			state.isPlayed = false;
			audio.Pause();
			//this.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	public void Stop()
	{
		isListen = false;
		audio.Stop();
		this.GetComponent<Renderer>().material.color = Color.grey;
	}
}
