using UnityEngine;
using System.Collections;
using System.IO;

public class AlembicExporterNameUpdate : MonoBehaviour {

    AlembicExporter ae;


	// Use this for initialization
	void Start () {

        ae = GetComponent<AlembicExporter>();
        if (!Directory.Exists("Cineplane/"))
        {
            Directory.CreateDirectory("Cineplane/");
        }

    }
	
	// Update is called once per frame
	void Update () {
        ae.m_outputPath = "Cineplane/" + System.DateTime.UtcNow.ToString("MM-dd_HH-mm-ss") + ".abc";
    }
}
