using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShaderController : MonoBehaviour {

	public float sliderPosition;
	public Renderer rend;
	bool shaded = false;

	// Use this for initialization
	void Start () {


		rend = GetComponent<Renderer>();

		//If the "None" option was selected in the start menu.
		//if (GlobalControl.Instance.state == 0) {




		//}
	
	}

	public void OnValueChanged(float pos) {
		sliderPosition = pos;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
        {
            if (shaded == false)
            {
                rend.material.SetFloat ("_SliderValue", sliderPosition);
                shaded = true;
            }
            else
            {
                rend.material.SetFloat ("_SliderValue", 0);
                shaded = false;
            }
        }
	}
}
