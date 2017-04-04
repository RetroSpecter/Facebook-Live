using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
	public class Talker : MonoBehaviour {

		public bool trump;
		public SpriteRenderer sr;
		[HideInInspector]
		public Sprite sprite1;
		public Sprite sprite2;
		public string words;

		// Use this for initialization
		void Start () {
			sprite1 = sr.sprite;
		}
		public void trumpChatter(string words){
			StartCoroutine (talk (words));
		}



		IEnumerator talk(string words){
			int t = 0;
			for (int i = 0; i < words.Length; i+=2){
				if (i % 4 == 0) {
					sr.sprite = sprite1;
				} else {
					sr.sprite = sprite2;
				}
				float x = AudioManager.instance.playTrump ();
				yield return new WaitForSeconds (x);
			}
			sr.sprite = sprite1;
		}

		void closeMouth(){
			sr.sprite = sprite2;
		}
		// Update is called once per frame

	}
