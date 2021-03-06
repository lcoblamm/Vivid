﻿using UnityEngine;
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
		//If the "color picker" or "negative" option was selected in the start menu 
		if (GlobalControl.Instance.currMode == Mode.ColorPicker || GlobalControl.Instance.currMode == Mode.Negative) {
			slider.SetActive (false);
		}
		rend.material.SetFloat ("_FilterType", (int)GlobalControl.Instance.currMode);
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
