using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scene : MonoBehaviour {
	public static Scene instance;
	[HideInInspector]
	public Animator anim;

	public Animator twochainz;
	public AudioSource audio;
	public AudioClip[] music;
	public SpriteRenderer endguy;
	public Sprite twochainzsprite;
	public Talker trump;
	void Awake(){
		instance = this;
		anim = GetComponent<Animator> ();
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Space) && !Facebook.Unity.Example.GameManager.instance.votingPeriod) {
			StopAllCoroutines ();
			anim.SetTrigger ("continue");
		}
	}

	void trumpTalk(string words){
		trump.trumpChatter (words);
	}


    public GameObject blonde;
    public GameObject fedora;
    public void putHair() {
        if (anim.GetBool("blondie"))
        {
            blonde.SetActive(true);
        } else {
            fedora.SetActive(true);
        }
    }

	public void setMusic(int x){
        if(x == -1) {
            audio.Stop();
            return;
        }

		if (x < music.Length) {
			audio.clip = music [x];
		}
		audio.Play ();
	}

	public void playSound(AudioClip x){
		AudioManager.instance.playSound (x);
	}

	public void multipleChoice(string choices){
		if (!Facebook.Unity.Example.GameManager.instance.votingPeriod) {
			Facebook.Unity.Example.GameManager.instance.textBox.text = "";
			string[] script = choices.Split (new char[1]{ '`' });
			Facebook.Unity.Example.GameManager.instance.startMultipleChoice (script);
		}
	}

	public void narration(string script){
		StopAllCoroutines ();
		StartCoroutine (narrateCOROUTINE (script));
	}

	public void narrationTwoChainz(string s){
		string[] script = s.Split ('/');
		StopAllCoroutines ();
		if (Facebook.Unity.Example.GameManager.instance.twochainz) {
			StartCoroutine (narrateCOROUTINE (script[0]));
		} else {
			StartCoroutine (narrateCOROUTINE (script[1]));
		}
	}

	public void setBlondie(){
		Facebook.Unity.Example.GameManager.instance.twochainz = true;
        anim.SetBool("blondie", true);
	}

	public void giveFrasier(){
		anim.SetBool ("hasFrasier", false);
	}

	private IEnumerator narrateCOROUTINE (string s){
		string[] script = s.Split ('`');
		for (int x = 0; x < script.Length; x++) {
			int t = 0;
			for (int i = 0; i < script[x].Length; i++) {
				Facebook.Unity.Example.GameManager.instance.textBox.text = script[x].Substring (0, i);

				if (script[x].Substring (Mathf.Max (0, i - 1), 1).Equals (" ")) {
					yield return new WaitForSeconds (.05f);
				} else {
					AudioManager.instance.Type ();
					yield return new WaitForSeconds (.05f);
				}
			}
			Facebook.Unity.Example.GameManager.instance.textBox.text = script [x];
			yield return new WaitForSeconds (2);

		}
		anim.SetTrigger ("continue");
	}
}

