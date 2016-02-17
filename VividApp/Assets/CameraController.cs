using UnityEngine;

public class CameraController : MonoBehaviour
{
	//Objects
	public GameObject plane;
	public Camera mainCam;
	public WebCamTexture deviceCam = null;

	// Setting up device camera to render to game plane
	public void Start () {
		plane = GameObject.Find("Plane");
		
		deviceCam = new WebCamTexture();
		plane.GetComponent<Renderer>().material.mainTexture = deviceCam;
		plane.transform.localScale = new Vector3 (1, 1, 1);
		deviceCam.Play();
	}
}


