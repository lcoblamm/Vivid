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
	private bool showText = false;

	public Text colorInfo;

    //Objects
    public  GameObject Object01  ;
    public Camera Cam ;

    public void changeColor()
    {
        rend.material.color = Color.Lerp(myColor, Color.white, Time.time);
    }


    // Use this for initialization
    public void Start () {
        Object01 = GameObject.Find("Plane");
        rend = GetComponent<Renderer>();
		colorInfo.text = "";
    }

    // Update is called once per frame
    public void Update () {
        //Once color is selected, continue to change frames.
        if (selected)
        {
            changeColor();
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
		RenderTexture rTex = new RenderTexture(800, 400, 24);
		Cam.targetTexture = rTex;
		Cam.Render();
		RenderTexture.active = rTex;
		Texture2D image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height, TextureFormat.RGB24, false);
		image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
		image.Apply(false);

		//byte[] bytes = image.EncodeToPNG();
		//File.WriteAllBytes ("Users/Lynne/dev/pic.png", bytes);
		Cam.targetTexture = null;
		
		//Get the mouse position.
		int x = Mathf.FloorToInt( Input.mousePosition.x ) ;
		int y = Mathf.FloorToInt( Input.mousePosition.y );

		Debug.Log("Click:" + x + " " + y);
		myColor = image.GetPixel(x, y);
		colorInfo.text = "The color is " + myColor.ToString();
		//selected = true;
		//changeColor(); 
	}
}
