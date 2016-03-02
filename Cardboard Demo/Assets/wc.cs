using UnityEngine;
using System.Collections;

public class wc : MonoBehaviour
{

    WebCamTexture back;

    // Use this for initialization
    void Start()
    {

        back = new WebCamTexture();

        GetComponent<Renderer>().material.mainTexture = back;
        back.Play();


    }

   public void pauseFeed()
    {
        bool paused = false;

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
    // Update is called once per frame
    void Update()
    {

    }
}