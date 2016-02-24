using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
	//Objects
	public GameObject plane = null;
	public WebCamTexture deviceCam = null;
	[SerializeField] private Button picButton = null;

	// Setting up device camera to render to game plane
	public void Start () {
		plane = GameObject.Find("Plane");
		deviceCam = new WebCamTexture();
		plane.GetComponent<Renderer>().material.mainTexture = deviceCam;
		deviceCam.Play();
	}

	public void TakeSnapshot() {
		print ("Entering TakeSnapshot");
		Texture2D snap = new Texture2D (deviceCam.width, deviceCam.height);
		snap.SetPixels (deviceCam.GetPixels());
		snap.Apply ();

		plane.GetComponent<Renderer>().material.mainTexture = snap;
	}
}


