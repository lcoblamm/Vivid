using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShaderController : MonoBehaviour {

	public float sliderPosition;
	public Renderer rend;
	public GameObject slider;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		slider = GameObject.Find ("Slider");
		//If the "None" option was selected in the start menu.
		if (GlobalControl.Instance.state == 0) {
			slider.SetActive (false);
		} else if (GlobalControl.Instance.state == 1) {
			rend.material.SetFloat ("_RedGreenEnabled", 1);
		} else if (GlobalControl.Instance.state == 2) {
			rend.material.SetFloat ("_RedGreenEnabled", 0);
		}
	}

	//Menu button pressed.
	public void MenuPress() {
		Application.LoadLevel ("Menu");
	}


	public void OnValueChanged(float pos) {
		sliderPosition = pos;
	}
	
	// Update is called once per frame
	void Update () {
		rend.material.SetFloat ("_SliderValue", sliderPosition);
	}
}
