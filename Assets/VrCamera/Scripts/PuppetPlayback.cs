using UnityEngine;
using System.Collections;

public class PuppetPlayback : MonoBehaviour {
	
	Vector3[] puppetPositions;
	Vector3[] puppetRotations;
	int frameIndex = 0;
	public bool on = false;
	
	private VrCamera vrCamera;
	
	// Use this for initialization
	void Start () {
		vrCamera = GameObject.Find ("Operator").GetComponent<VrCamera> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if (on) {
			if (vrCamera.AtEndOfRecording (frameIndex)) {
				frameIndex = 0;
			}
			
			transform.position = puppetPositions [frameIndex];
			transform.rotation = Quaternion.Euler(puppetRotations[frameIndex]);
			frameIndex++;
		}
	}
	
	public void TurnOn() {
		on = true;
		puppetPositions = vrCamera.GetPuppetPositions ();
		puppetRotations = vrCamera.GetPuppetRotations ();
		Util.SetChildRenderersEnabled (gameObject, true);
		frameIndex = 0;
	}

	public void TurnOff() {
		Util.SetChildRenderersEnabled (gameObject, false);
	}

	public void Restart() {
		frameIndex = 0;
	}
}
