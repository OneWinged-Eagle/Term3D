using UnityEngine;
using System.Collections;

public class AudioPlay : MonoBehaviour {

    protected string url = "http://williamnayrole.fr/test.wav";
    protected AudioSource source;

    void Start()
    {
        source = Camera.main.GetComponent(typeof(AudioSource)) as AudioSource;
        WWW www = new WWW(url);
        source = GetComponent<AudioSource>();
        source.Pause();
        source.clip = www.audioClip;
        source.Play();
    }
    void Update()
    {
        if (!source.isPlaying && source.clip.isReadyToPlay)
            source.Play();

    }
}
