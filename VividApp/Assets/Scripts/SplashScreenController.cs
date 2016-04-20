using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour {

	private SceneFader fader;
	private float totalTime;
	private bool faderCalled = false;

	// Use this for initialization
	void Start () {
		fader = GameObject.FindObjectOfType<SceneFader> ();
		totalTime = 0;
	}

	void Update() {
		totalTime += Time.deltaTime;
		if (totalTime >= 3) {
			End ();
		}
	}

	void End() {
#if FADING
		if (!faderCalled) {
			// fade scene out and load menu
			fader.EndScene ("Menu");
			faderCalled = true;
		}
#else
		Application.LoadLevel ("Menu");
#endif
	}
}
