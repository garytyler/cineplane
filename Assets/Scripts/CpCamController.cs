using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class CpCamController : MonoBehaviour {

    /*CONTROLLERINPUT*/
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    /*OTHER*/
    public bool recordingOn;
    OperatorModeDisplay operatorModeDisplay;
    AlembicExporter alembicExporter;
    Camera mainCamera;
    CpPlayback cpPlayback;

    /*GAMEOBJECTS*/
    GameObject cpCamera;

    /*CAMERAOPVR*/
    private TimeCode timecode;
    private Camera cameraMain; 
    private AnimationDirector animationDirector;
    private ClipScreenshot clipScreenshot;
    


    // Use this for initialization
    void Start() {


        cpCamera = GameObject.Find("cpCamera");

        recordingOn = false;
        alembicExporter = new AlembicExporter();
        cpPlayback = new CpPlayback();

        //  viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
        timecode = GameObject.Find("Timecode").GetComponent<TimeCode>();
        operatorModeDisplay = GameObject.Find("ModeDisplay").GetComponent<OperatorModeDisplay>();
        animationDirector = GameObject.Find("AnimationDirector").GetComponent<AnimationDirector>();
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        clipScreenshot = GameObject.Find("CameraMain").GetComponent<ClipScreenshot>();

        cpPlayback.playbackOn = false;
    }


    // Update is called once per frame
    void Update()
    {
        /*Application Menu*/
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("APPLICATION MENU");
            if (!recordingOn)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }
        if (recordingOn)
        {
            RecordProcess();
        }

        /*Trigger*/
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (cpPlayback.playbackOn)
            {
                cpPlayback.StopPlayback();
            }
            else if (!cpPlayback.playbackOn)
            {
                cpPlayback.StartPlayback();
            }
        }
    }



    private int numOfRecordedFrames = 0;
    private List<Vector3> cameraPositions;
    private List<Vector3> cameraRotations;
    private List<float> cameraZooms;

    private CameraData currentCameraData;
    private List<CameraData> cameraDatas = new List<CameraData>();
    private List<ClipData> lcd = ClipInventoryData.clipDatas; //clipDatas is a static variable in ClipInventoryData.cs which is why I can call it like this


    public void PrintClipDatas()
    {
        foreach (ClipData cd in lcd)
        {
            print(cd.cameraDatas.ToString());
        }
    }




    public void StopRecording()
    {

        recordingOn = false;
    //    alembicExporter.EndCapture();
        operatorModeDisplay.SetMode("standby");

    }

    List<float> GetTimes()
    {
        List<float> timesList = new List<float>(numOfRecordedFrames);
        for (int i = 0; i < numOfRecordedFrames; i++)
        {
            timesList[i] = i * 0.0111f;
        }
        return timesList;
    }




    void RecordProcess()
    {
        Vector3 position = cpCamera.transform.position;
        Vector3 eulerAngles = cpCamera.transform.rotation.eulerAngles;

        currentCameraData.cameraPositions.Add(position);
        currentCameraData.cameraRotations.Add(eulerAngles);
        currentCameraData.cameraZooms.Add(cameraMain.fieldOfView);

        //print (currentCapturedShotData);

        // Sets the timecode display to the currently recorded frame.
        timecode.frame = numOfRecordedFrames;
        numOfRecordedFrames++;
    }

    public bool AtEndOfRecording(int frame)
    {
        return (frame > numOfRecordedFrames);
    }


    void StartRecording()
    {
        print("StartRecording currentCameraData =" + currentCameraData);
        PrintClipDatas();
        if (cameraDatas != null)
        {
            Debug.Log("cameraDatas is not null");

        }



        if (currentCameraData != null)
        {
            cameraDatas.Add(currentCameraData);   // This is where the currentCameraData gets added to a list somewhere
        }

        Debug.Log("After ClipData.cameraDatas.Add" + cameraDatas);


        currentCameraData = new CameraData();

        recordingOn = true;

        alembicExporter.BeginCapture();

        animationDirector.RestartAllAnimations();
        if (cpPlayback.playbackOn)
        {
            cpPlayback.StopPlayback();
        }
        //clipScreenshot.TakeHiResShot();
        operatorModeDisplay.SetMode("record");
    }



    /***********************************************************************************/
    //These Methods are used in CameraPlayback.cs

    public List<Vector3> GetCameraPositions()
    {
        return currentCameraData.cameraPositions;
    }
    public List<Vector3> GetCameraRotations()
    {
        return currentCameraData.cameraRotations;
    }
    public List<float> GetCameraZooms()
    {
        return currentCameraData.cameraZooms;
    }

    /**********************************************************************************/
}
