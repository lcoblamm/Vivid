using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ();
		Application.LoadLevel ("Menu");
	}

	IEnumerator StartCoroutine() {
		yield return new WaitForSeconds(3);
	}
}
