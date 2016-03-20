using UnityEngine;
using System.IO;
using System.Collections;   
using System.Collections.Generic;
using System.Linq;

public class CpPlayback : MonoBehaviour {

   // public GameObject Operator;

	public List<Vector3> cameraPositions;
	public List<Vector3> cameraRotations;
	public List<float> cameraZooms;

    CpCamController cpCamController;
    GameObject cpCamera;
    Camera mainCamera;
    int frameIndex = 0;
	public bool playbackOn = false;

   



    //private ViveInput viveInput;
    private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private AnimationDirector animationDirector;

	

   

	// Use this for initialization
	void Start () {

        cpCamera = GameObject.FindGameObjectWithTag("MainCamera");



		timecode = GameObject.Find ("Timecode").GetComponent<TimeCode> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();

		 
		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
        
        
	}
	
	// Update is called once per frame
	void Update () {

        /*
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
			if (playbackOn)
            {
				StopPlayback ();
			}
            else if (!playbackOn)
            {
				StartPlayback();
			}
		}
        */


		if (!playbackOn) {
			transform.position = cpCamera.transform.position;
			transform.rotation = cpCamera.transform.rotation;
		}

		if (playbackOn) {
			if (cpCamController.AtEndOfRecording (frameIndex)) {
				animationDirector.RestartAllAnimations();
				frameIndex = 0;
			}

			transform.position = cameraPositions[frameIndex];
			transform.rotation = Quaternion.Euler(cameraRotations[frameIndex]);
			mainCamera.fieldOfView = cameraZooms[frameIndex];

			// Sets the displayed timecode to the playing back frame.
			timecode.frame = frameIndex;
			frameIndex++;
		}

	
	
	}

	public void TogglePlayback() {
		if (playbackOn) {
			StopPlayback ();
		} else if (!playbackOn){
			StartPlayback ();
		}
	}

	public void StopPlayback() {
		playbackOn = false;
		operatorModeDisplay.SetMode ("standby");
	}

	public void StartPlayback() {
		cameraPositions = cpCamController.GetCameraPositions ();
		cameraRotations = cpCamController.GetCameraRotations ();
		cameraZooms = cpCamController.GetCameraZooms ();
		playbackOn = true;
		animationDirector.RestartAllAnimations();
		frameIndex = 0;
		if (cpCamController.recordingOn) {
            cpCamController.StopRecording();
		}
		operatorModeDisplay.SetMode ("playback");
	}
}
