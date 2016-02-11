#pragma strict

var movTexture : MovieTexture;

function Start () {
    //GetComponent.<Renderer>().material.mainTexture = movTexture;
    movTexture.Play();
}

function Update () {

    if(Input.GetKeyDown(KeyCode.Space)) {

        if (movTexture.isPlaying) {
            movTexture.Pause();
        }
        else {
            movTexture.Play();
        }
    }
}