using UnityEngine;
using System.Collections;

public class CameraPlayback : MonoBehaviour {

	public GameObject Operator;
	Vector3[] cameraPositions;
	Vector3[] cameraRotations;
	float[] cameraZooms;
	int frameIndex = 0;
	public bool on = false;
	public GameObject operatorLens;


	private ViveInput viveInput;
	private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private AnimationDirector animationDirector;
	private Camera camera;
	private VrCamera vrCamera;
	private PuppetPlayback puppetPlayback;

	// Use this for initialization
	void Start () {
		viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
		timecode = GameObject.Find ("Timecode").GetComponent<TimeCode> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> (); 
		vrCamera = GameObject.Find ("Operator").GetComponent<VrCamera> (); 
		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
		puppetPlayback = GameObject.Find ("PuppetPlayback").GetComponent<PuppetPlayback> ();
		operatorLens = GameObject.Find ("OperatorLens");
	}
	
	// Update is called once per frame
	void Update () {
/*		if (viveInput.right.trigger.pressedDown) {
			if (on) {
				StopPlayback ();
			} else if (!on) {
				StartPlayback();
			}
		
		}
*/	
		if (!on) {
			transform.position = operatorLens.transform.position;
			transform.rotation = operatorLens.transform.rotation;
		}

		if (on) {
			if (vrCamera.AtEndOfRecording (frameIndex)) {
				puppetPlayback.Restart ();
				animationDirector.RestartAllAnimations();
				frameIndex = 0;
			}

			transform.position = cameraPositions [frameIndex];
			transform.rotation = Quaternion.Euler(cameraRotations[frameIndex]);
			camera.fieldOfView = cameraZooms[frameIndex];

			// Sets the displayed timecode to the playing back frame.
			timecode.frame = frameIndex;
			frameIndex++;
		}
	}

	public void TogglePlayback() {
		if (on) {
			StopPlayback ();
		} else if (!on){
			StartPlayback ();
		}
	}

	public void StopPlayback() {
		on = false;
		operatorModeDisplay.SetMode ("standby");
		puppetPlayback.TurnOff ();
	}

	public void StartPlayback() {
		cameraPositions = vrCamera.GetCameraPositions ();
		cameraRotations = vrCamera.GetCameraRotations ();
		cameraZooms = vrCamera.GetCameraZooms ();
		on = true;
		animationDirector.RestartAllAnimations();
		puppetPlayback.Restart ();
		puppetPlayback.TurnOn ();
		frameIndex = 0;
		if (vrCamera.recording) {
			vrCamera.StopRecording();
		}
		operatorModeDisplay.SetMode ("playback");
	}
}
