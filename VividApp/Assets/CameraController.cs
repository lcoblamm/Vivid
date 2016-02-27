using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
	//Objects
	public GameObject plane = null;
	public WebCamTexture deviceCam = null;

	// Setting up device camera to render to game plane
	public void Start () {
		Debug.Log ("Entering Start this(" + this + ")");
		deviceCam = new WebCamTexture(Screen.width, Screen.height);
		plane = GameObject.Find ("Plane");
		plane.GetComponent<Renderer>().material.mainTexture = deviceCam;
		deviceCam.Play();
	}

	public void TakeSnapshot() {
		Debug.Log ("Entering TakeSnapshot this(" + this + ")");
		Texture2D snap = new Texture2D (deviceCam.width, deviceCam.height);
		snap.SetPixels (deviceCam.GetPixels());
		snap.Apply ();

		plane.GetComponent<Renderer>().material.mainTexture = snap;	
	}
}


