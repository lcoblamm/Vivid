using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {
    ColorPicker colorpicker;
    private Color myColor;

    public Texture2D texture = new Texture2D(128, 128);

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

		if (showText) {
			colorInfo.text = "The color is " + myColor.ToString();
		}
    }

	IEnumerator screenGrab() {
		Debug.Log ("Entered coroutine");
		yield return new WaitForEndOfFrame();

		//Takes the texture2D from the Main Camera.
		Cam = Camera.main;
		RenderTexture rTex = new RenderTexture(128, 128, 0);
		Cam.targetTexture = rTex;
		Cam.Render();
		Texture2D image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);
		image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
		image.Apply();
		Cam.targetTexture = null;
		
		//Set texture to the captured texture2D.
		texture = image;
		
		//Get the mouse position.
		int x = Mathf.FloorToInt( Input.mousePosition.x ) ;
		int y = Mathf.FloorToInt( Input.mousePosition.y );
		
		myColor = texture.GetPixel(x, y);
		showText = true;
		//selected = true;
		//changeColor(); 
	}
}
