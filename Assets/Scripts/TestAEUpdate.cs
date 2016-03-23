using UnityEngine;
using System.Collections;

public class TestAEUpdate : MonoBehaviour {

    private AlembicExporter ae;

	// Use this for initialization
	void Start () {
        /*
        ae = GameObject.Find("AlembicExporter").GetComponent<AlembicExporter>();
        GameObject camera = GameObject.Find("CameraMain");
        
        for (int i = 0; i < 6000; i++)
        {
            camera.transform.position = new Vector3(i, 0, 0);
            ae.UpdateRecording();
        }
        */  
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
