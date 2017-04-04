using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Jitter ());
	}
	IEnumerator Jitter(){
		Vector3 v = new Vector3 (.1f, 0, 0);
		while (true) {
			v = -v;
			transform.position = transform.position + v;

			yield return new WaitForSeconds (.5f);
		}
	}

}
