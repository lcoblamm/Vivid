package com.colorcorrection.eecs582.colorcorrection;

import android.os.Bundle;
import android.hardware.Camera ;

public class MainActivity  {

    private Camera mCamera = null ;
    private cameraView mCameraView = null ;


    protected void onCreate(Bundle savedInstanceState) {


        try{
            mCamera = Camera.open();//you can use open(int) to use different cameras
        } catch (Exception e){
            //Log.d("ERROR", "Failed to get camera: " + e.getMessage());
        }

        mCamera.startPreview();


    }


}
