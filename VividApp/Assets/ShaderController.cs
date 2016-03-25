using UnityEngine;
using System.Collections;

public class ShaderController : MonoBehaviour {

	public float sliderPosition;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}

	public void OnValueChanged(float pos) {
		sliderPosition = pos;
	}

	// Update is called once per frame
	void Update () {
		rend.material.SetFloat("_SliderValue", sliderPosition);
	}
}
