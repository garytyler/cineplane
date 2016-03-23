using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraPlayback : MonoBehaviour {

	public GameObject Operator;
	public ClipData clipData;
	public List<Vector3> pbCameraPositions;
	public List<Vector3> pbCameraRotations;
	public List<float> pbCameraZooms;

	int currentIndex = 0;
	public bool playbackOn = false;
	public GameObject operatorLens;


	private ViveInput viveInput;
	private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private AnimationDirector animationDirector;
	private Camera cineplaneCameraMain;
	private VrCamera vrCamera;

	// Use this for initialization
	void Start () {
		viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
		timecode = GameObject.Find ("Timecode").GetComponent<TimeCode> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();
		cineplaneCameraMain = GameObject.Find ("CineplaneCameraMain").GetComponent<Camera> (); 
		vrCamera = GameObject.Find ("Operator").GetComponent<VrCamera> (); 
		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
		operatorLens = GameObject.Find ("OperatorLens");
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (viveInput.right.trigger.pressedDown) {
			if (playbackOn) {
				StopPlayback ();
			} else if (!playbackOn) {
				StartPlayback();
			}
		}


		if (playbackOn)
        {
            if (currentIndex == pbCameraPositions.Count)
            {
                animationDirector.RestartAllAnimations();
                currentIndex = 0;
            }

            transform.position = pbCameraPositions[currentIndex];
			transform.rotation = Quaternion.Euler(pbCameraRotations[currentIndex]);
			cineplaneCameraMain.fieldOfView = pbCameraZooms[currentIndex];

			// Sets the displayed timecode to the playing back frame.
			timecode.frame = currentIndex;
			currentIndex++;
		}


        if (!playbackOn) {
			transform.position = operatorLens.transform.position;
			transform.rotation = operatorLens.transform.rotation;
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
		pbCameraPositions = vrCamera.GetCameraPositions ();
		pbCameraRotations = vrCamera.GetCameraRotations ();
		pbCameraZooms = vrCamera.GetCameraZooms ();
		playbackOn = true;
		animationDirector.RestartAllAnimations();
		currentIndex = 0;
		if (vrCamera.recordingOn) {
			vrCamera.StopRecording();
		}
		operatorModeDisplay.SetMode ("playback");
	}
}
