using UnityEngine;
using System;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour {
    ColorPicker colorpicker;
    private Color myColor;

    public Renderer rend;
    public Boolean selected = false;

	public Text colorInfo;

    //Objects
    public Camera Cam ;

    //Used to name color
    struct NamedColor {
        public string name;
        public Vector4 rgba;
        public NamedColor(string n, Vector4 v) {
            name = n;
            rgba = v;
        }
    };

    NamedColor[] colorList = {
        //half and full value combinations
        new NamedColor("maroon", new Vector4(0.5f, 0, 0, 1)),
        new NamedColor("red", new Vector4(1, 0, 0, 1)),
        new NamedColor("green", new Vector4(0, 0.5f, 0, 1)),
        new NamedColor("lime", new Vector4(0, 1, 0, 1)),
        new NamedColor("navy", new Vector4(0, 0, 0.5f, 1)),
        new NamedColor("blue", new Vector4(0, 0, 1, 1)),
        new NamedColor("olive", new Vector4(0.5f, 0.5f, 0, 1)),
        new NamedColor("yellow", new Vector4(1, 1, 0, 1)),
        new NamedColor("purple", new Vector4(0.5f, 0, 0.5f, 1)),
        new NamedColor("fuchsia", new Vector4(1, 0, 1, 1)),
        new NamedColor("teal", new Vector4(0, 0.5f, 0.5f, 1)),
        new NamedColor("aqua", new Vector4(0, 1, 1, 1)),
        
        //common "reasonable" colors
        new NamedColor("beige", new Vector4(0.96f, 0.96f, 0.86f, 1)),
        new NamedColor("tan", new Vector4(0.87f, 0.72f, 0.53f, 1)),
        new NamedColor("blue violet", new Vector4(0.54f, 0.17f, 0.89f, 1)),
        new NamedColor("brown", new Vector4(0.65f, 0.2f, 0.2f, 1)),
        new NamedColor("pink", new Vector4(1, 0.4f, 0.7f, 1)),
        new NamedColor("yellow green", new Vector4(0.68f, 1, 0.18f, 1)),
        new NamedColor("lavender", new Vector4(0.9f, 0.9f, 0.98f, 1)),
        new NamedColor("light blue", new Vector4(0.68f, 0.85f, 0.9f, 1)),
        new NamedColor("light green", new Vector4(0.56f, 0.93f, 0.56f, 1)),
        new NamedColor("orange", new Vector4(1, 0.65f, 0, 1))
    };

    // Use this for initialization
    public void Start () {
        rend = GetComponent<Renderer>();
		colorInfo.text = "";
	}

    // Update is called once per frame
    public void Update () {
		RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
		Vector2 pixel = hit.textureCoord;
		
		 // If there are two touches on the device...
       
		if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (Camera.main.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                Camera.main.orthographicSize += deltaMagnitudeDiff * -1.7f;

                // Make sure the orthographic size never drops below zero.
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                Camera.main.fieldOfView += deltaMagnitudeDiff * -1.4f;

                // Clamp the field of view to make sure it's between 0 and 180.
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            }
        }

		// MOUSE WHEEL ZOOM
//		if (Input.GetAxis("Mouse ScrollWheel") > 0 // zoom forward wheel 
//			&& Camera.main.fieldOfView > 2.6f) // makes sure you don't zoom too far in, creates errors
//		{
//			Camera.main.fieldOfView = Camera.main.fieldOfView-5;
//			Camera.main.transform.position = new Vector3((pixel.x*7.5f)-3.5f,10,(pixel.y*7.5f)-3.5f); //converting pixels into x,y,z coords for camera position.
//		}
//		if (Input.GetAxis("Mouse ScrollWheel") < 0) // zoom backwards wheel
//		{
//			Camera.main.fieldOfView = Camera.main.fieldOfView+5;
//			Camera.main.transform.position = new Vector3((pixel.x*5)-2.5f,10,(pixel.y*5)-2.5f);//converting pixels into x,y,z coords for camera position.
//		}

		// Don't want to detect click if it's on other game object

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			StartCoroutine(screenGrab());     
		}
#elif UNITY_ANDROID
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
			StartCoroutine(screenGrab());     
        }
#endif
    }

	IEnumerator screenGrab() {
		yield return new WaitForEndOfFrame();

		//Takes the texture2D from the Main Camera.
		Cam = Camera.main;
		WebCamTexture deviceCam = CameraController.deviceCam;
		int width = deviceCam.width;
		int height = deviceCam.height;
//		RenderTexture rTex = new RenderTexture(width, height, 24); 
//		Cam.targetTexture = rTex;
//		Cam.Render();
//		RenderTexture.active = rTex;
//		Texture2D image = new Texture2D(width, height, TextureFormat.RGB24, false);
//		// applies active render texture to image
//		image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
//		image.Apply(false);
//        Cam.targetTexture = null;

		Texture2D image = new Texture2D (width, height);	
		image.SetPixels (deviceCam.GetPixels());
		image.Apply ();

        //Get click position using ray casting
        RaycastHit hit;
        Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit);
		Vector2 pixel = hit.textureCoord2 ;

		// TODO LYNNE: Clean up transformation
#if UNITY_EDITOR
		pixel.x *= image.width;
        pixel.y *= image.height;
#elif UNITY_ANDROID
		pixel.x *= image.width;
		pixel.y *= image.height;
		// rotate pixel coordinates by 90 degrees
#endif

        int x = Mathf.FloorToInt(pixel.x);
        int y = Mathf.FloorToInt(pixel.y);
		Debug.Log ("Pixel: (" + x + "," + y + ")");
		//Get color
		myColor = image.GetPixel(x,y); 
		rend.material.mainTexture = image;

		Debug.Log ("Pixel RGB: (" + myColor.r * 256 + ", " + myColor.g * 256 + ", " + myColor.b * 256 + ")");

        colorInfo.text = "The closest color is " + getColorName(myColor.gamma);
    }

    private string getColorName(Color color) {
        //Check for greyscale first
        if(Math.Abs(color.r-color.g) + Math.Abs(color.r-color.b) + Math.Abs(color.g-color.b) < 0.1) {
            return "grey";
        }
        //Find closest color
        //Note distance is not true distance - just used as an approximation
        double dist;
        double smallestDist = 4;
        int smallestIndex = -1;
        for(int i=0; i<colorList.Length; i++) {
            dist =
                (color.r - colorList[i].rgba[0]) * (color.r - colorList[i].rgba[0])
                + (color.g - colorList[i].rgba[1]) * (color.g - colorList[i].rgba[1])
                + (color.b - colorList[i].rgba[2]) * (color.b - colorList[i].rgba[2]);
            if(dist < smallestDist) {
                smallestDist = dist;
                smallestIndex = i;
            }
        }
        return colorList[smallestIndex].name;
    }
}
