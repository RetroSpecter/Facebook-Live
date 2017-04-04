using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Facebook.Unity.Example
{
	public class GameManager : MenuBase {

		public static GameManager instance;
		public bool TEST;
		public string apiQuery;
		string query = "?fields=reactions.type(LIKE).limit(0).summary(1).as(like),reactions.type(LOVE).limit(0).summary(1).as(love),reactions.type(WOW).limit(0).summary(1).as(wow),reactions.type(HAHA).limit(0).summary(1).as(haha),reactions.type(SAD).limit(0).summary(1).as(sad),reactions.type(ANGRY).limit(0).summary(1).as(angry)";
		public bool votingPeriod;
		public static int timerlength = 100;
		public int timer = 100;
		[HideInInspector]
		public bool twochainz;
		public GameObject timerGameObject;
		public Text textBox;
		public Text timerText;
		Animator timerTextAnimation;

		public Text[] votesText;
		public Text[] optionsText;
		public GameObject[] options;
		long[] currentVotes = new long[3];
		long[] initialVotes = new long[3];
		bool initialQuery = true;
		int optionCount;

		public string[] script;
		// Use this for initialization
		void Awake () {
			instance = this;
			if (!FB.IsInitialized && !TEST) {
				Application.LoadLevel ("MainMenu");
			}
			apiQuery = apiQuery + query;
			setTimerText ();
			timerTextAnimation = timerText.GetComponent<Animator> ();
			StartCoroutine (startcountdown ());
		}

		void Update(){
			if (votingPeriod) {
				if (Input.GetKeyDown(KeyCode.Alpha1)){
					forceChoice (0);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2)){
					forceChoice (1);
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3)){
					forceChoice (2);
				}
			}
		}

		void forceChoice(int choice){
			votingPeriod = false;
			StopAllCoroutines ();
			timerGameObject.SetActive (false);
			for (int i = 0; i < options.Length; i++) {
				options [i].SetActive (false);
			}
			Scene.instance.anim.SetTrigger ("" + choice);
		}

		public void startMultipleChoice(string[] s){
			votingPeriod = true;
			initialQuery = true;
			timer = timerlength;
			if (FB.IsInitialized){
				StartCoroutine (UpdateVotes ());
			}
			StartCoroutine (countdown ());
			optionCount = s.Length;
			for (int i = 0; i < s.Length; i++) {
				optionsText [i].text = s [i];
				options [i].SetActive (true);
			}

		}

		IEnumerator countdown(){
			timerGameObject.SetActive (true);
			while (timer > 0) {
				if (timer == 10) {
					timerTextAnimation.SetTrigger ("10left");
				}
				timer--;
				setTimerText ();
				yield return new WaitForSeconds (1);
			}
			votingPeriod = false;
			timerTextAnimation.SetTrigger ("0left");

			int choice = evaluateChoice ();
			for (int i = 0; i < options.Length; i++) {
				options [i].SetActive (false);
			}
			yield return new WaitForSeconds (2);
			options [choice].SetActive (true);
			AudioManager.instance.playSound (4);
			yield return new WaitForSeconds (4);
			options [choice].SetActive (false);
			timerGameObject.SetActive (false);
			Scene.instance.anim.SetTrigger ("" + choice);
		}

		IEnumerator startcountdown(){
			timerGameObject.SetActive (true);
			while (timer > 0) {
				if (timer == 10) {
					timerTextAnimation.SetTrigger ("10left");
				}
				timer--;
				setTimerText ();
				yield return new WaitForSeconds (1);
			}
			timerTextAnimation.SetTrigger ("0left");
			yield return new WaitForSeconds (4);
			timerGameObject.SetActive (false);
			Scene.instance.anim.SetTrigger ("continue");
		}


		int evaluateChoice(){
			float mychoice = 0;
			if (optionCount == 2) {
				if (currentVotes [0] > currentVotes [1]) {
					return 0;
				} else {
					return 1;
				}
			}
			if (currentVotes [0] > currentVotes [1]) {
				if (currentVotes [0] > currentVotes [2]) {
					return 0;
				} else {
					return 2;
				}

			} else {
				if (currentVotes [1] > currentVotes [2]) {
					return 1;
				} else {
					return 2;
				}
			}
		}


		void setTimerText(){
			string zero = ":";
			if (timer % 60 < 10) {
				zero = ":0";
			}
			timerText.text = "" + timer / 60 + zero + timer % 60; 
		}

		void endCountdown(){
			votingPeriod = false;
		}


		IEnumerator UpdateVotes () {
			
			while (true) {
				if (votingPeriod) {
					print ("OK");
					FB.API (apiQuery, HttpMethod.GET, this.HandleResult);
				}
				yield return new WaitForSeconds (1);
			}
		}

		public void getVotes(Dictionary<string, long> votes){
			long[] newvotes = new long[3];
			newvotes [0] = votes ["love"];
			newvotes [1] = votes ["haha"];
			newvotes [2] = votes ["angry"];


			for (int i = 0; i < newvotes.Length; i++) {
				currentVotes [i] = newvotes [i];
			}

			for (int i = 0; i < currentVotes.Length; i++) {
				votesText [i].text = "" + currentVotes [i];
			}
		}


		protected void OnGUI()
		{
		}

	}
}