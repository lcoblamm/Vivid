// #define FADING

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour {
	public Image fadeImg;
	public float fadeSpeed = 1.5f;

	private bool sceneStarting;
	private bool sceneEnding;
	string nextScene;

	// Use this for initialization
	void Start () {
#if FADING
		sceneStarting = true;
		sceneEnding = false;
		// expand image to cover screen and set to black
		fadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		fadeImg.color = Color.black;
#endif
	}
	
	// Update is called once per frame
	void Update () {
#if FADING
		if (sceneStarting) {
			StartScene ();
		}
		if (sceneEnding) {
			EndScene ();
		}
#endif
	}

	// Fade scene in 
	void StartScene() {
		FadeToClear ();

		// if fade image is almost clear, set to clear and disable
		if (fadeImg.color.a <= 0.05) {
			fadeImg.color = Color.clear;
			fadeImg.enabled = false;
			sceneStarting = false;
		}
	}

	// Public function to start fade out and load next scene
	public void EndScene(string sceneToLoad) {
		nextScene = sceneToLoad;
		sceneEnding = true;
		fadeImg.enabled = true;
	}

	// fade scene out
	void EndScene() {
		FadeToBlack ();
		
		// if fade image is almost black, exit scene
		if (fadeImg.color.a >= 0.95) {
			Application.LoadLevel (nextScene);
		}
	}

	void FadeToClear() {
		fadeImg.color = Color.Lerp (fadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack() {
		fadeImg.color = Color.Lerp (fadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
	}
}
