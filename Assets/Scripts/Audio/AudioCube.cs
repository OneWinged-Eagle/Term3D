using UnityEngine;
using System.Collections;

public class AudioCube : MonoBehaviour {

    protected string url = "http://williamnayrole.fr/test.ogg";
    protected AudioSource source;

    void Start () {
        WWW www = new WWW(url);
        source = GetComponent<AudioSource>();
        source.clip = www.audioClip;
        source.Play();
    }
	
	void Update () {
        if (!source.isPlaying && source.clip.isReadyToPlay)
            source.Play();
    }
}
