using UnityEngine;
using System;
using System.Collections;
using System.IO;
using UnityEngine.UI;

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
		Camera.main.orthographic =false; //used field of view for zooming
	}

    // Update is called once per frame
    public void Update () {
		RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
		Vector2 pixel = hit.textureCoord;
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0 // zoom forward wheel 
			&& Camera.main.fieldOfView > 2.6f) // makes sure you don't zoom too far in, creates errors
		{
			Camera.main.fieldOfView = Camera.main.fieldOfView-5;
			Camera.main.transform.position = new Vector3((pixel.x*7.5f)-3.5f,10,(pixel.y*7.5f)-3.5f); //converting pixels into x,y,z coords for camera position.
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // zoom backwards wheel
		{
			Camera.main.fieldOfView = Camera.main.fieldOfView+5;
			Camera.main.transform.position = new Vector3((pixel.x*5)-2.5f,10,(pixel.y*5)-2.5f);//converting pixels into x,y,z coords for camera position.
		}

        if (Input.GetMouseButtonDown(0))
        {
			StartCoroutine(screenGrab());     
        }
    }

	IEnumerator screenGrab() {
		yield return new WaitForEndOfFrame();

		//Takes the texture2D from the Main Camera.
		Cam = Camera.main;
		RenderTexture rTex = new RenderTexture(400, 400, 24); //The aspect ratio should match plane's
		Cam.targetTexture = rTex;
		Cam.Render();
		RenderTexture.active = rTex;
		Texture2D image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height, TextureFormat.RGB24, false);
		image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
		image.Apply(false);
        Cam.targetTexture = null;

        //Get click position using ray casting
        RaycastHit hit;
        Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit);
        Vector2 pixel = hit.textureCoord;
        pixel.x *= image.width;
        pixel.y *= image.height;
        int x = Mathf.FloorToInt(pixel.x);
        int y = Mathf.FloorToInt(pixel.y);

        //Get color
        myColor = image.GetPixel(x, y);
        colorInfo.text = "The closest color is " + getColorName(myColor);
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
