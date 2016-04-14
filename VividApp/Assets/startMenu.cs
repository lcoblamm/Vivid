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

		GlobalControl.Instance.state = 1 ; 

		Application.LoadLevel (1); 
		menu.SetActive (false); 
	}

	//Blue Yellow button Selected, set set state to 2, and load main scene.
	public void BlueYellowPress() {

		GlobalControl.Instance.state = 2 ; 

		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 


	}

	//No shader button Selected, set set state to 0, and load main scene.
	public void NonePress() {

		GlobalControl.Instance.state = 20 ; 

		//Load main scene
		Application.LoadLevel (1); 
		menu.SetActive (false); 

	}

	// Update is called once per frame
	void Update () {
	

	}
}
