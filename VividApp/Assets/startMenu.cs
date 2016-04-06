using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startMenu : MonoBehaviour {

	public Button RedGreen ;
	public Button BlueYellow ; 
	public Button None ;

	public int state ; 

	public GameObject menu ;

	//GameObject plane = GameObject.Find("Plane");
	//ColorPicker variable = plane.GetComponent<ColorPicker>();

	//ColorPicker variable = new ColorPicker() ; 
	//public Renderer rend;

	// Use this for initialization
	void Start () {
	
		DontDestroyOnLoad(transform.gameObject);

	}

	public void RedGreenPress() {

		state = 1 ; 

		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}
	public void BlueYellowPress() {

		state = 2 ; 

		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 


	}

	public void NonePress() {

		state = 0 ; 

		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 

	}

	// Update is called once per frame
	void Update () {
	

	}
}
