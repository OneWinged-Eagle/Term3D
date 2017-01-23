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
	public string pathToFile;


	public override void Attached()
	{
		//audio.Pause();

		state.IsPlayed = false;
		state.AddCallback("IsPlayed", colorPlayed);
		//Debug.Log(state.IsPlayed);
		//this.GetComponent<Renderer> ().material.color = Color.red;
		base.Attached();
	}


	void colorPlayed()
	{
		audio = GetComponent<AudioSource>();
		ps = GetComponent<ParticleSystem>();
		var em = ps.emission;

		if (state.IsPlayed == true)
		{
			audio.Play();
			em.enabled = true;
		}
		else if (state.IsPlayed == false)
		{
			em.enabled = false;
			audio.Pause();
		}
	}

	public void PlayAndPause()
	{
		if (audio == null || string.IsNullOrEmpty(pathToFile))
		{
			audio = GetComponent<AudioSource>();

			pathToFile = gameObject.GetComponent<AudioObject>().Audio.RealPath;

			WWW audioLoader = new WWW("file://" + pathToFile);
			while (!audioLoader.isDone)
				;//Debug.Log ("kek");
			//Debug.Log(audioLoader.GetAudioClip(true).name);
			audio.clip = audioLoader.GetAudioClip(true);
			audio.Play();
		}

		if (!isListen)
		{
			isListen = true;
			state.IsPlayed = true;
			audio.Play();
			//this.GetComponent<Renderer>().material.color = Color.green;
		}
		else
		{
			isListen = false;
			state.IsPlayed = false;
			audio.Pause();
			//this.GetComponent<Renderer>().material.color = Color.red;
		}
	}


	public void playClient()
	{
		audio = GetComponent<AudioSource>();

		WWW audioLoader = new WWW("file://" + pathToFile);
		while (!audioLoader.isDone)
			;//Debug.Log ("kek");
		//Debug.Log(audioLoader.GetAudioClip(true).name);
		audio.clip = audioLoader.GetAudioClip(true);
		audio.Play();
	}

	public void Stop()
	{
		isListen = false;
		audio.Stop();
	}
}
