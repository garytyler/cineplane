using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class CpControllerScriptRight : MonoBehaviour {

   /*CONTROLLERINPUT*/
   // private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
   // private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
   // private Valve.VR.EVRButtonId applicationMenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    SteamVR_TrackedObject trackedObj;
    //public bool recordingOn;
    CpMainCamBehavior CpMainCamBehavior;
   
    CpPlayback cpPlayback;


    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        CpMainCamBehavior = GameObject.Find("CpMainCam").GetComponent<CpMainCamBehavior>();
        cpPlayback = FindObjectOfType<CpPlayback>();
        
    }

    void Update()
    {   
        ///Nullcheck
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        ///Application Menu
        if (controller.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if (!CpMainCamBehavior.recordingOn)
            {
                CpMainCamBehavior.StartRecording();
                print("CpControllerScript: StartRecording");
            }
            else
            {
                CpMainCamBehavior.StopRecording();
            }
        }

        if (CpMainCamBehavior.recordingOn)
        {
            CpMainCamBehavior.RecordProcess();
        }

        ///Trigger
        if (controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //cpPlayback.TogglePlayback();


            
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
}
