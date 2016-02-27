using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class VrCamera : MonoBehaviour {

    private int numOfRecordedFrames = 0;
    private Vector3[] cameraPositions;
	private Vector3[] cameraRotations;
	private float[] cameraZooms;

	private Vector3[] puppetPositions;
	private Vector3[] puppetRotations;

	private ViveInput viveInput;
	private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private Camera camera;
	private CameraPlayback cameraPlayback;
	private AnimationDirector animationDirector;
	private ClipScreenshot clipScreenshot;
	//private SettingsMenu settingsMenu;

	private GameObject operatorLens;

	private AlembicExporter alembicExporter;

	public bool recording = false;

	public GameObject cameraPathPixel;

	// Use this for initialization
	void Start () {
        cameraPositions = new Vector3[9000];
		cameraRotations = new Vector3[9000];
		cameraZooms = new float[9000];

		puppetPositions = new Vector3[9000];
		puppetRotations = new Vector3[9000];
		recording = false;

		viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
		timecode = GameObject.Find("Timecode").GetComponent<TimeCode> ();
		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> (); 
		cameraPlayback = GameObject.Find ("CameraPlayback").GetComponent<CameraPlayback> (); 
		clipScreenshot = GameObject.Find("Main Camera").GetComponent<ClipScreenshot>();
		//settingsMenu = GameObject.Find ("Menu").GetComponent<SettingsMenu> ();

		operatorLens = GameObject.Find ("OperatorLens");

		alembicExporter = GameObject.Find ("AlembicExporter").GetComponent<AlembicExporter> ();

	}

	// Update is called once per frame
	void Update () {
		if (viveInput.right.topButton.pressedDown) {
			if (!recording) {
				StartRecording ();
				Debug.Log("Start recording!");
			} else {
				StopRecording ();
			}
		}

		if (recording) {
			Record();
		}		
    }

	public Vector3[] GetCameraPositions() {
		return cameraPositions;
	}

	public Vector3[] GetCameraRotations() {
		return cameraRotations;
	}

	public Vector3[] GetPuppetPositions() {
		return puppetPositions;
	}
	
	public Vector3[] GetPuppetRotations() {
		return puppetRotations;
	}

	public float[] GetCameraZooms() {
		return cameraZooms;
	}

	void StartRecording()
	{
		recording = true;
		alembicExporter.BeginCapture();
		//animationDirector.RestartAllAnimations();
		if (cameraPlayback.on) {
			cameraPlayback.StopPlayback ();
		}
		//clipScreenshot.TakeHiResShot();
		operatorModeDisplay.SetMode ("record");
	}
	
	public void StopRecording() 
	{
		recording = false;
		alembicExporter.EndCapture();
		operatorModeDisplay.SetMode ("standby");
	}

	float[] GetTimes() {
		float[] timesArray = new float[numOfRecordedFrames];
		for (int i = 0; i < numOfRecordedFrames; i++) {
			timesArray[i] = i * 0.0111f;
		}
		return timesArray;
	}

	void Record() {
		Vector3 position = operatorLens.transform.position; 
		cameraPositions [numOfRecordedFrames] = position;
		Vector3 eulerAngles = operatorLens.transform.rotation.eulerAngles; 
		cameraRotations [numOfRecordedFrames] = eulerAngles;
		cameraZooms [numOfRecordedFrames] = camera.fieldOfView;

		/*if (settingsMenu.menuMode == "puppet") {
			Vector3 puppetPosition = viveInput.left.gameObject.transform.position;
			puppetPositions [numOfRecordedFrames] = puppetPosition;
			Vector3 puppetAngles = viveInput.left.gameObject.transform.rotation.eulerAngles;
			puppetRotations [numOfRecordedFrames] = puppetAngles;
		}*/

		// Sets the timecode display to the currently recorded frame.

		// Instantiate(cameraPathPixel, position, Quaternion.Euler(eulerAngles));

		timecode.frame = numOfRecordedFrames;
		numOfRecordedFrames++;
	}

	public bool AtEndOfRecording(int frame) {
		return (frame > numOfRecordedFrames);
	}
}