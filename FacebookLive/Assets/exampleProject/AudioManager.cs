using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public AudioSource source;
	public AudioClip[] clips;
	public AudioClip[] type;
	public AudioClip[] trumpClips;
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	void Start(){
		source = GetComponent<AudioSource> ();
	}
	public void playSound(int x){
		source.PlayOneShot(clips[x]);
	}

    public void playSound(AudioClip x){
        source.PlayOneShot(x);
    }

    public void playSound(int x, float volume){
		source.PlayOneShot(clips [x], volume);
	}

	public float playTrump(){
		AudioClip clip = trumpClips [Random.Range (0, trumpClips.Length)];
		source.PlayOneShot(clip);
		return clip.length;
	}

	public void Type(){
		source.PlayOneShot(type [Random.Range(0,type.Length)]);
	}
}
