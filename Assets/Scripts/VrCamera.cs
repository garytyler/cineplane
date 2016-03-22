using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class VrCamera : MonoBehaviour { 

	private ViveInput viveInput;
	private TimeCode timecode;
	private OperatorModeDisplay operatorModeDisplay;
	private Camera cameraMain;
	private CameraPlayback cameraPlayback;
	private AnimationDirector animationDirector;
	private ClipScreenshot clipScreenshot;


	private GameObject operatorLens;

	private AlembicExporter alembicExporter;

	public bool recording = false;

	// Use this for initialization
	void Start () {
		cameraPositions = new List<Vector3> ();
		cameraRotations = new List<Vector3> ();
        cameraZooms = new List<float>();

		recording = false;

		viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
		timecode = GameObject.Find("Timecode").GetComponent<TimeCode> ();
		operatorModeDisplay = GameObject.Find ("ModeDisplay").GetComponent<OperatorModeDisplay> ();
		animationDirector = GameObject.Find ("AnimationDirector").GetComponent<AnimationDirector> ();
		cameraMain = GameObject.Find ("CameraMain").GetComponent<Camera> (); 
		cameraPlayback = GameObject.Find ("CameraPlayback").GetComponent<CameraPlayback> (); 
		clipScreenshot = GameObject.Find("CameraMain").GetComponent<ClipScreenshot>();
		operatorLens = GameObject.Find ("OperatorLens");
        alembicExporter = GameObject.Find("AlembicExporter").GetComponent<AlembicExporter> ();

    }

	// Update is called once per frame
	void Update () {

		if (viveInput.right.topButton.pressedDown) {
			if (!recording) {
				StartRecording ();
			} else {
				StopRecording ();
			}
		}
		if (recording) {
			Record();
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

    private int numOfRecordedFrames = 0;
    private List<Vector3> cameraPositions;
    private List<Vector3> cameraRotations;
    private List<float> cameraZooms;

    private ClipData currentClipData;

    private List<ClipData> clipInventory = new List<ClipData>();



    void StartRecording()
	{
        print ("StartRecording currentCameraData =" + currentClipData);
        
        


        if (currentClipData != null) {
			clipInventory.Add (currentClipData);   // This is where the currentCameraData gets added to a list somewhere
		}

        //Debug.Log("After ClipData.cameraDatas.Add" + cameraDatas);
        

		currentClipData = new ClipData ();

		recording = true;

		alembicExporter.BeginCapture();

		animationDirector.RestartAllAnimations();
		if (cameraPlayback.playbackOn) {
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

	List<float> GetTimes() {
		List<float> timesList = new List<float> (numOfRecordedFrames);
		for (int i = 0; i < numOfRecordedFrames; i++) {
			timesList[i] = i * 0.0111f;
		}
		return timesList;
	}




	void Record() {

		Vector3 position = operatorLens.transform.position;
		Vector3 eulerAngles = operatorLens.transform.rotation.eulerAngles;

		currentClipData.cameraPositions.Add(position);
        currentClipData.cameraRotations.Add(eulerAngles);
        currentClipData.cameraZooms.Add(cameraMain.fieldOfView);


		//print (currentCapturedShotData);

		// Sets the timecode display to the currently recorded frame.
		timecode.frame = numOfRecordedFrames;
		numOfRecordedFrames++;
	}

	public bool AtEndOfRecording(int frame) {
		return (frame > numOfRecordedFrames);
	}
}
