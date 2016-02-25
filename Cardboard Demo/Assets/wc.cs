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

    // Update is called once per frame
    void Update()
    {

    }
}