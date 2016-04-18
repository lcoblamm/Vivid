using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startMenu : MonoBehaviour {

	public Button RedGreen ;
	public Button BlueYellow ; 
	public Button None ;

	public GameObject menu ;
	
	// Use this for initialization
	void Start () {

		//Needed so that state variable will not be lost in the transistion from the start menu into the main scene. 
		DontDestroyOnLoad(transform.gameObject);

	}

	//Red Green button Selected, set set state to 1, and load main scene.
	public void RedGreenPress() {

		GlobalControl.Instance.currMode = Mode.RedGreen ; 

		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	//Blue Yellow button Selected, set set state to 2, and load main scene.
	public void BlueYellowPress() {

		GlobalControl.Instance.currMode = Mode.BlueYellow ; 

		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	// High contrast button Selected, set state to 3, and load main scene.
	public void HighContrastPress() {
		
		GlobalControl.Instance.currMode = Mode.HighContrast ; 
		
		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	// Negative button Selected, set state to 4, and load main scene.
	public void NegativePress() {
		
		GlobalControl.Instance.currMode = Mode.Negative ; 
		
		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	// Deuteranopia simulator button Selected, set state to 11, and load main scene.
	public void RedGreenSimPress() {
		
		GlobalControl.Instance.currMode = Mode.RedGreenSim ; 
		
		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	// Tritanopia simulator button Selected, set state to 12, and load main scene.
	public void BlueYellowSimPress() {
		
		GlobalControl.Instance.currMode = Mode.BlueYellowSim ; 
		
		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	// Hue shift button selected, set state to 20, and load main scene.
	public void HueShiftPress() {
		
		GlobalControl.Instance.currMode = Mode.HueShift ; 
		
		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	// Color picker button Selected, set state to 0, and load main scene.
	public void ColorPickerPress() {

		GlobalControl.Instance.currMode = Mode.ColorPicker ; 

		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 

	}

	// Update is called once per frame
	void Update () {
	

	}
}
