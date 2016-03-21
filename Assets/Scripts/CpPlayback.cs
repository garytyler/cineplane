using UnityEngine;
using System.IO;
using System.Collections;   
using System.Collections.Generic;
using System.Linq;

public class CpPlayback : MonoBehaviour {


    public bool playbackOn = false;
    int frameIndex = 0;

    GameObject cpMainCam;

    CpControllerScriptRight cpControllerScriptRight;
    CpMainCamBehavior cpMainCamBehavior;

    public List<Vector3> cameraPositions;
	public List<Vector3> cameraRotations;
	public List<float> cameraZooms;

    private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private AnimationDirector animationDirector;


	// Use this for initialization
	void Start () {
        playbackOn = false;

        //cameraPositions = new List<Vector3>();
        //cameraRotations = new List<Vector3>();
        //cameraZooms = new List<float>();

        cpMainCamBehavior = GameObject.Find("cpMainCam").GetComponent<CpMainCamBehavior>();
        
        cpControllerScriptRight = FindObjectOfType<CpControllerScriptRight>();
        cpMainCam = GameObject.Find("CpMainCam");
		timecode = GameObject.Find("Timecode").GetComponent<TimeCode> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();

		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
	}
	

	// Update is called once per frame
	void Update ()
    {
     /*   
		if (!playbackOn)
        {
            cpMainCam.transform = ;
            cpMainCam.transform.;
		}
    */
		if (playbackOn)
        {
            if (cpMainCam.GetComponent<CpMainCamBehavior>().AtEndOfRecording(frameIndex)) 
				animationDirector.RestartAllAnimations();
				frameIndex = 0;
		}
        
			transform.position = cameraPositions[frameIndex];
			transform.rotation = Quaternion.Euler(cameraRotations[frameIndex]);
			cpMainCam.GetComponentInParent<Camera>().fieldOfView = cameraZooms[frameIndex];

			// Sets the displayed timecode to the playing back frame.
			timecode.frame = frameIndex;
			frameIndex++;
	}
    
	
	
	

	public void TogglePlayback()
    {
		if (playbackOn) {
			StopPlayback ();
		} else if (!playbackOn){
			StartPlayback ();
		}
	}

	public void StopPlayback()
    {

		playbackOn = false;
		operatorModeDisplay.SetMode ("standby");
	}

	public void StartPlayback()
    {
     
        cameraPositions = cpMainCamBehavior.GetCameraPositions();
        print(cameraPositions.ToString() + "START PLAYBACK");
		cameraRotations = cpMainCamBehavior.GetCameraRotations();
		cameraZooms = cpMainCamBehavior.GetCameraZooms ();
		playbackOn = true;
		animationDirector.RestartAllAnimations();
		frameIndex = 0;
		if (cpMainCamBehavior.recordingOn)
        {
            cpMainCamBehavior.StopRecording();
		}
		operatorModeDisplay.SetMode ("playback");
	}
}
