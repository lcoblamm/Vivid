package com.colorcorrection.eecs582.colorcorrection;

import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.hardware.Camera ;
import android.content.Context ;
import java.io.IOException;

/**
 * Created by joshalbrecht on 2/2/16.
 */
public class cameraView extends SurfaceView implements SurfaceHolder.Callback {

    private SurfaceHolder mHolder ;
    private Camera mCamera ;

    public cameraView ( Context context, Camera camera){

        super ( context ) ;

        mHolder = getHolder() ;
        mHolder.addCallback(this);

        mHolder.setType( SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS) ;
    }

    public void surfaceCreated( SurfaceHolder holder ){

        try{

            mCamera.setPreviewDisplay( holder );
            mCamera.startPreview() ;

        } catch ( IOException e ){


        }
    }

    public void surfaceChanged( SurfaceHolder holder, int format, int w, int h ){


    }

    public void surfaceDestroyed( SurfaceHolder holder ){



    }



}