using UnityEngine;
using System.Collections;

public class AlembicExporterNameUpdate : MonoBehaviour {

    AlembicExporter ae;


	// Use this for initialization
	void Start () {

        ae = GetComponent<AlembicExporter>();


	}
	
	// Update is called once per frame
	void Update () {
        ae.m_outputPath = System.DateTime.UtcNow.ToString("MM-dd_HH-mm-ss") + ".abc";
    }
}
