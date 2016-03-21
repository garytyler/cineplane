using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class CpMainCamBehavior : MonoBehaviour {


    public bool recordingOn;
    private int numOfRecordedFrames = 0;

    private CpPlayback cpPlayback;
    private AnimationDirector animationDirector;
    private OperatorModeDisplay operatorModeDisplay;
    private TimeCode timecode;
    private AlembicExporter alembicExporter;

    private List<Vector3> cameraPositions;
    private List<Vector3> cameraRotations;
    private List<float> cameraZooms;

    private CameraData currentCameraData;
    private List<CameraData> cameraDatas = new List<CameraData>();



    void Start()
    {
        timecode = FindObjectOfType<TimeCode>();
        operatorModeDisplay = FindObjectOfType<OperatorModeDisplay>();
        alembicExporter = FindObjectOfType<AlembicExporter>();

        //currentCameraData = new CameraData();
        cameraDatas = new List<CameraData>();
    }

    void Update()
    {
        /*
        if(!cpPlayback.playbackOn)
            {
            transform.position = Mou
            }
            */
    }


    /// <summary>
    /// These Methods are used in CameraPlayback.cs for retrieving the stored camera path and camera setting data
    /// </summary>


    public List<Vector3> GetCameraPositions()
    {
        //print(currentCameraData.ToString());
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

    


    /// <summary>
    /// Below are the Methods that CpMainCamBehavior can carry out
    /// </summary>

    public void PrintCurrentCameraData(CameraData ccd)
    {
        if (ccd != null)
        {
            List<Vector3> positions = ccd.cameraPositions.ToList<Vector3>();
            List<Vector3> rotations = ccd.cameraRotations.ToList<Vector3>();
            List<float> zooms = ccd.cameraZooms.ToList<float>();
            foreach (Vector3 v in positions)
            {
                print(v.ToString());
            }
            foreach (Vector3 v in rotations)
            {
                print(v.ToString());
            }
            foreach (float f in zooms)
            {
                print(f.ToString());
            }
        }
    }
   
    public void PrintClipDatas()
    {

        foreach (ClipData cd in ClipInventory.clipDatas)
        {
            print(cd.cameraDatas.ToList<CameraData>().ElementAt(1).cameraPositions.ToList<Vector3>().ElementAt(1).ToString());
        }
    }


    /***Start Recording***/
    public void StartRecording()
    {
        //print("StartRecording currentCameraData =" + currentCameraData);
        //PrintClipDatas();
        //PrintCurrentCameraData(currentCameraData);

        if (currentCameraData != null)
        {
            cameraDatas.Add(currentCameraData);   // This is where the currentCameraData gets added to a list somewhere
            foreach (CameraData cd in cameraDatas)
            {
                
            }

        }
        print(currentCameraData.ToString());
        //Debug.Log("After ClipData.cameraDatas.Add" + cameraDatas);


        currentCameraData = new CameraData();

        recordingOn = true;
        print("CpMainCamBehavior: recordingOn = true");
        alembicExporter.BeginCapture();

        //animationDirector.RestartAllAnimations();
        //print(cpPlayback.playbackOn.ToString());

        /*
        if (cpPlayback.playbackOn)
        {
            cpPlayback.StopPlayback();
        }
        else
        {
        }
        */


        //clipScreenshot.TakeHiResShot();
        operatorModeDisplay.SetMode("record");
    }
    

    /***Record Process***/
    public void RecordProcess()
    {
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.rotation.eulerAngles;

        currentCameraData.cameraPositions.Add(position);
        currentCameraData.cameraRotations.Add(eulerAngles);
        currentCameraData.cameraZooms.Add(GetComponent<Camera>().fieldOfView);

        //print (currentCapturedShotData);

        // Sets the timecode display to the currently recorded frame
        timecode.frame = numOfRecordedFrames;
        numOfRecordedFrames++;
    }


    /***Stop Recording***/
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


   


    public bool AtEndOfRecording(int frame)
    {

        return (frame > numOfRecordedFrames);
    }

}
