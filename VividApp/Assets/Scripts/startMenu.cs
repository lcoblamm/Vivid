using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startMenu : MonoBehaviour {

	public GameObject menu ;
	public GameObject infoPanel;
	private SceneFader fader;

	// Use this for initialization
	void Start () {
		menu = GameObject.Find ("Canvas");
		menu.SetActive (true);
		infoPanel = GameObject.Find ("InfoPanel");
		infoPanel.SetActive (false);
		fader = GameObject.FindObjectOfType<SceneFader> ();
	}

	//Red Green button Selected, set set state to 1, and load main scene.
	public void RedGreenPress() {
		GlobalControl.Instance.currMode = Mode.RedGreen ; 
		TransitionToScene ();
	}

	//Blue Yellow button Selected, set set state to 2, and load main scene.
	public void BlueYellowPress() {
		GlobalControl.Instance.currMode = Mode.BlueYellow ; 
		TransitionToScene ();
	}

	// High contrast button Selected, set state to 3, and load main scene.
	public void HighContrastPress() {
		GlobalControl.Instance.currMode = Mode.HighContrast ; 
		TransitionToScene ();
	}

	// Negative button Selected, set state to 4, and load main scene.
	public void NegativePress() {
		GlobalControl.Instance.currMode = Mode.Negative ; 
		TransitionToScene ();
	}

	// Deuteranopia simulator button Selected, set state to 11, and load main scene.
	public void RedGreenSimPress() {
		GlobalControl.Instance.currMode = Mode.RedGreenSim ; 
		TransitionToScene ();
	}

	// Tritanopia simulator button Selected, set state to 12, and load main scene.
	public void BlueYellowSimPress() {
		GlobalControl.Instance.currMode = Mode.BlueYellowSim ; 
		TransitionToScene ();
	}

	// Hue shift button selected, set state to 20, and load main scene.
	public void HueShiftPress() {
		GlobalControl.Instance.currMode = Mode.HueShift ; 
		TransitionToScene ();
	}

	// Color picker button Selected, set state to 0, and load main scene.
	public void ColorPickerPress() {
		GlobalControl.Instance.currMode = Mode.ColorPicker; 
		TransitionToScene ();
	}

	public void OpenInfoPanel() {
		infoPanel.SetActive (true);
	}

	public void CloseInfoPanel() {
		infoPanel.SetActive (false);
	}

	private void TransitionToScene() {
		// Load main scene
#if FADING
		fader.EndScene("LiveFeed");
#else
		Application.LoadLevel ("LiveFeed"); 
#endif
	}

	// Update is called once per frame
	void Update () {
	
	}
}
