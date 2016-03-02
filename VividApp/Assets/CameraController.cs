using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour
{
	//Objects
	public GameObject plane = null;
	public WebCamTexture deviceCam = null;
	public bool isPicture = false;

	// Setting up device camera to render to game plane
	public void Start () {
		deviceCam = new WebCamTexture();
		plane = GameObject.Find ("Plane");
		plane.GetComponent<Renderer>().material.mainTexture = deviceCam;
		deviceCam.Play();

		StartCoroutine(FixAspectRatio());
	}

	public IEnumerator FixAspectRatio() {
		while (deviceCam.width < 100) {
			Debug.Log("the width/height values are not yet ready.");
			Debug.Log( deviceCam.width +" " +deviceCam.height);
			yield return null;
		}

		int correctWidth = deviceCam.width;
		int correctHeight = deviceCam.height;
		float newWidth = (float)correctWidth / (float)correctHeight;
		Debug.Log("the width/height values are now meaningful.");
		Debug.Log( correctWidth +" " +correctHeight);
		Debug.Log("Changing ratio to : " + newWidth);
		plane.transform.localScale = new Vector3 (newWidth, 1f, 1f);
	}
		
		// toggles camera on and off
	public void ToggleCamera() {
		if (isPicture) {
				plane.GetComponent<Renderer> ().material.mainTexture = deviceCam;
			isPicture = false;
		} else {
			TakeSnapshot();
			isPicture = true;
		}
	}

	public void TakeSnapshot() {
		Texture2D snap = new Texture2D (deviceCam.width, deviceCam.height);
		snap.SetPixels (deviceCam.GetPixels());
		snap.Apply ();

		plane.GetComponent<Renderer>().material.mainTexture = snap;	
	}
}


