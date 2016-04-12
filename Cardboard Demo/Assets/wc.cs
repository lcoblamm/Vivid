using UnityEngine;
using System.Collections;

public class wc : MonoBehaviour
{
	public static WebCamTexture back;
    bool paused = false;
    // Use this for initialization
    void Start()
    {

        back = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = back;
        back.Play();


    }

   public void pauseFeed()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Entering Pause");
            if (paused == false)
            {
                back.Pause();
                paused = true;
            }
            else
            {
                back.Play();
                paused = false;
            }
        }
    }
}