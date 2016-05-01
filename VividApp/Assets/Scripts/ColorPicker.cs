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
	public GameObject colorTextBox;

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

	//public int shaderState;

	// List of color names and corresponding RGB values
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
        new NamedColor("cyan", new Vector4(0, 1, 1, 1)),
        
        //common "reasonable" colors
        new NamedColor("beige", new Vector4(0.96f, 0.96f, 0.86f, 1)),
        new NamedColor("tan", new Vector4(0.87f, 0.72f, 0.53f, 1)),
        new NamedColor("blue violet", new Vector4(0.54f, 0.17f, 0.89f, 1)),
        new NamedColor("brown", new Vector4(0.6f, 0.2f, 0.2f, 1)),
        new NamedColor("pink", new Vector4(1, 0.4f, 0.7f, 1)),
        new NamedColor("yellow green", new Vector4(0.68f, 1, 0.18f, 1)),
        new NamedColor("lavender", new Vector4(0.9f, 0.9f, 0.98f, 1)),
        new NamedColor("light blue", new Vector4(0.68f, 0.85f, 0.9f, 1)),
        new NamedColor("light green", new Vector4(0.56f, 0.93f, 0.56f, 1)),
        new NamedColor("orange", new Vector4(1, 0.65f, 0, 1)),
        new NamedColor("aquamarine", new Vector4(0.5f, 1, 0.83f, 1)),
        new NamedColor("cornflower blue", new Vector4(0.39f, 0.58f, 0.93f, 1)),
        new NamedColor("coral", new Vector4(1, 0.5f, 0.31f, 1)),
        new NamedColor("salmon", new Vector4(0.98f, 0.5f, 0.45f, 1)),
        new NamedColor("crimson", new Vector4(0.86f, 0.08f, 0.24f, 1)),
        new NamedColor("hot pink", new Vector4(1, 0.08f, 0.58f, 1)),
        new NamedColor("forest green", new Vector4(0.13f, 0.55f, 0.13f, 1)),
        new NamedColor("ivory", new Vector4(1, 1, 0.94f, 1)),
        new NamedColor("golden yellow", new Vector4(1, 0.84f, 0, 1)),
        new NamedColor("yellow green", new Vector4(0.6f, 0.9f, 0.18f, 1)),
        new NamedColor("indigo", new Vector4(0.29f, 0, 0.51f, 1)),
        new NamedColor("red orange", new Vector4(1, 0.3f, 0, 1)),
        new NamedColor("purple", new Vector4(0.63f, 0.13f, 0.94f, 1)),
        new NamedColor("sky blue", new Vector4(0.53f, 0.81f, 0.92f, 1))
    };

    // Use this for initialization
    public void Start () {
		// disable color info if we're not in color blidness or color picker mode
		colorTextBox = GameObject.Find ("ColorTextBox");
		if (GlobalControl.Instance.currMode != Mode.ColorPicker 
			&& GlobalControl.Instance.currMode != Mode.RedGreen
			&& GlobalControl.Instance.currMode != Mode.BlueYellow) {
			colorTextBox.SetActive (false);
		}

        rend = GetComponent<Renderer>();
		colorInfo.text = "";
		colorInfo.color = Color.white;

		//GameObject canv = GameObject.Find("Canvas");
		//startMenu variable = canv.GetComponent<startMenu>();

	}

    // Update is called once per frame
    public void Update () {
		RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
		Vector2 pixel = hit.textureCoord;

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
		// pause so we don't get half updated screen
		yield return new WaitForEndOfFrame();

		// get aspect ratio of device camera
		Cam = Camera.main;
		WebCamTexture deviceCam = CameraController.deviceCam;
		int width = deviceCam.width;
		int height = deviceCam.height;

		RenderTexture rt = new RenderTexture (width, height, 24);
		// copy image that's on plane to new render texture
		Graphics.Blit (GameObject.Find ("Plane").GetComponent<Renderer> ().material.mainTexture, rt);
		// set new render texture as active so we can use readPixels to copy it to a Texture2D
		RenderTexture.active = rt;
		Texture2D image = new Texture2D (width, height);
		image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		image.Apply ();

        // Get click position using ray casting
        RaycastHit hit;
        Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit);
		Vector2 pixel = hit.textureCoord2 ;

		// translate 0 to 1 coordinates to actual coordinates
		pixel.x *= image.width;
        pixel.y *= image.height;
        int x = Mathf.FloorToInt(pixel.x);
        int y = Mathf.FloorToInt(pixel.y);
		Debug.Log ("Pixel: (" + x + "," + y + ")");

		// Get pixel from copied image
		myColor = image.GetPixel(x,y); 

        Color c1 = image.GetPixel(x - 1, y);
        Color c2 = image.GetPixel(x + 1, y);
        Color c3 = image.GetPixel(x - 1, y - 1);
        Color c4 = image.GetPixel(x + 1, y - 1);
        Color c5 = image.GetPixel(x - 1, y + 1);
        Color c6 = image.GetPixel(x + 1, y + 1);
        Color c7 = image.GetPixel(x, y - 1);
        Color c8 = image.GetPixel(x, y + 1);

        double red = (c1.r + c2.r + c3.r + c4.r + c5.r + c6.r + c7.r + c8.r + myColor.r) / 9;
        double green = (c1.g + c2.g + c3.g + c4.g + c5.g + c6.g + c7.g + c8.g + myColor.g) / 9;
        double blue = (c1.b + c2.b + c3.b + c4.b + c5.b + c6.b + c7.b + c8.b + myColor.b) / 9;
        colorInfo.text = "The closest color is " + getColorName(red, green, blue);
    }

    private string getColorName(double r, double g, double b) {
        //Check for greyscale first
        if(Math.Abs(r-g) + Math.Abs(r-b) + Math.Abs(g-b) < 0.1) {
            return "grey";
        }
        //Find closest color
        //Note distance is not true distance - just used as an approximation
        double dist;
        double smallestDist = 4;
        int smallestIndex = -1;
        for(int i=0; i<colorList.Length; i++) {
            dist =
                (r - colorList[i].rgba[0]) * (r - colorList[i].rgba[0])
                + (g - colorList[i].rgba[1]) * (g - colorList[i].rgba[1])
                + (b - colorList[i].rgba[2]) * (b - colorList[i].rgba[2]);
            if(dist < smallestDist) {
                smallestDist = dist;
                smallestIndex = i;
            }
        }
        return colorList[smallestIndex].name;
    }
}
