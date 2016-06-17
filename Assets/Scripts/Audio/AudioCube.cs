using UnityEngine;
using System.Collections;

public class AudioCube : MonoBehaviour {

    protected AudioSource source;

    void Start () {
        this.setClipByUrl("http://williamnayrole.fr/silence.ogg");
    }
	
	void Update () {
        if (!source.isPlaying && source.clip.isReadyToPlay)
            source.Play();
    }

    public void setClipByUrl(string url)
    {
        WWW www = new WWW(url);
        source = GetComponent<AudioSource>();
        source.clip = www.audioClip;
    }

    public void setClipByAudioClip(AudioClip audio)
    {
        source = GetComponent<AudioSource>();
        source.clip = audio;
    }

    public void setVolume(float vol)
    {
        source = GetComponent<AudioSource>();
        source.volume = vol;
    }

    public void Play()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    public void Pause()
    {
        source = GetComponent<AudioSource>();
        source.Pause();
    }

    public void Stop()
    {
        source = GetComponent<AudioSource>();
        source.Stop();
    }
}
