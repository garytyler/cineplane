using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class VrCamera : MonoBehaviour {

    private int numOfRecordedFrames = 0;
	private List<Vector3> cameraPositions;
	private List<Vector3> cameraRotations;
	private List<float> cameraZooms;

	private ClipData currentClipData;
	public List<ClipData> clipInventory;

	private ViveInput viveInput;
	private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private Camera cineplaneCameraMainCam;
	private CameraPlayback cameraPlayback;
	private AnimationDirector animationDirector;
	private ClipScreenshot clipScreenshot;
	//private SettingsMenu settingsMenu;

	private GameObject operatorLens;

	private AlembicExporter alembicExporter;

	public bool recordingOn = false;

	// Use this for initialization
	void Start () {
		cameraPositions = new List<Vector3> ();
		cameraRotations = new List<Vector3> ();
        cameraZooms = new List<float>();

        clipInventory = new List<ClipData>();

		recordingOn = false;

		viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
		timecode = GameObject.Find("Timecode").GetComponent<TimeCode> ();
		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();
		cineplaneCameraMainCam = GameObject.Find ("CineplaneCameraMain").GetComponent<Camera> (); 
		cameraPlayback = GameObject.Find ("CameraPlayback").GetComponent<CameraPlayback> (); 
		clipScreenshot = GameObject.Find("CineplaneCameraMain").GetComponent<ClipScreenshot>();
		//settingsMenu = GameObject.Find ("Menu").GetComponent<SettingsMenu> ();
		operatorLens = GameObject.Find ("OperatorLens");
		alembicExporter = GameObject.Find ("AlembicExporter").GetComponent<AlembicExporter> ();

	}

	// Update is called once per frame
	void Update () {

		if (viveInput.right.topButton.pressedDown) {
			if (!recordingOn) {
				StartRecording ();
			} else {
				StopRecording ();
			}
		}
		if (recordingOn) {
			RecordProcess();
		}		
    }



/***********************************************************************************/
//These Methods are used in CameraPlayback.cs

	public List<Vector3> GetCameraPositions() {
		return currentClipData.cameraPositions;
	}
	public List<Vector3> GetCameraRotations() {
		return currentClipData.cameraRotations;
	}
	public List<float> GetCameraZooms() {
		return currentClipData.cameraZooms;
	}

/**********************************************************************************/


    /***Start Recording***/
    void StartRecording()
	{
		print (currentClipData);

		if (currentClipData != null) {
			clipInventory.Add (currentClipData);
		}

        Debug.Log("ShotDatas =" + clipInventory.Count);
        

		currentClipData = new ClipData ();

		recordingOn = true;

		alembicExporter.BeginCapture();

		animationDirector.RestartAllAnimations();
		if (cameraPlayback.playbackOn) {
			cameraPlayback.StopPlayback ();
		}
		//clipScreenshot.TakeHiResShot();
		operatorModeDisplay.SetMode ("record");
	}



    /***Record Process***/
    void RecordProcess() {

		Vector3 position = operatorLens.transform.position;
		Vector3 eulerAngles = operatorLens.transform.rotation.eulerAngles;

		currentClipData.cameraPositions.Add(position);
		currentClipData.cameraRotations.Add(eulerAngles);
		currentClipData.cameraZooms.Add(cineplaneCameraMainCam.fieldOfView);

        print("Pos: " + position.ToString() + "  Rot: " + eulerAngles + " FOV: " + cineplaneCameraMainCam.fieldOfView.ToString());

		//print (currentShotData);

		// Sets the timecode display to the currently recorded frame.
		timecode.frame = numOfRecordedFrames;
		numOfRecordedFrames++;
	}



    /***Stop Recording***/
    public void StopRecording() 
	{
		recordingOn = false;
		alembicExporter.EndCapture();
		operatorModeDisplay.SetMode ("standby");
	}

	List<float> GetTimes() {
		List<float> timesList = new List<float> (numOfRecordedFrames);
		for (int i = 0; i < numOfRecordedFrames; i++) {
			timesList[i] = i * 0.0111f;
		}
		return timesList;
	}
}