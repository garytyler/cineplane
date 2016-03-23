using UnityEngine;
using System.Collections;
using System.IO;

public class AlembicExporterNameUpdate : MonoBehaviour
{

    AlembicExporter ae;


    // Use this for initialization
    void Start()
    {

        ae = GetComponent<AlembicExporter>();
        if (!Directory.Exists("Cineplane_Output/"))
        {
            Directory.CreateDirectory("Cineplane_Output/");
        }

    }

    // Update is called once per frame
    void Update()
    {
        ae.m_outputPath = "Cineplane_Output/" + System.DateTime.Now.ToString("MM-dd_HH-mm-ss") + ".abc";
    }
}
