using UnityEngine;
using System.Collections;

public class SensorSettings : MonoBehaviour {

	public bool on = false;
	float[] sensorSizeHeightOptions = {14f, 16.5f, 24f, 24f, 48.5f};
	string[] sensorSizeDescOptions = {"Super 35mm (16:9)", "35mm Academy (4:3)", "35mm Photo (3:2)", "35mm Square (1:1)", "70mm IMAX (1.43:1)"};
	int sensorSizeIndex = 0;
	TextMesh[] sensorSizeOptionTexts;
	ZoomSettings zoomSettings;
	GameObject sensorMenu;
	ViveInput viveInput;
	
	// Use this for initialization
	void Start () {
		sensorSizeOptionTexts = new TextMesh[5];
		sensorSizeOptionTexts [0] = GameObject.Find ("SensorSizeOption0").GetComponent<TextMesh> ();
		sensorSizeOptionTexts [1] = GameObject.Find ("SensorSizeOption1").GetComponent<TextMesh> ();
		sensorSizeOptionTexts [2] = GameObject.Find ("SensorSizeOption2").GetComponent<TextMesh> ();
		sensorSizeOptionTexts [3] = GameObject.Find ("SensorSizeOption3").GetComponent<TextMesh> ();
		sensorSizeOptionTexts [4] = GameObject.Find ("SensorSizeOption4").GetComponent<TextMesh> ();
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput> ();
		zoomSettings = GetComponent<ZoomSettings> ();

		sensorMenu = GameObject.Find ("SensorMenu");
		SetSensorSizeOptionTexts();
	}
	
	// Update is called once per frame
	void Update () {
		if (on) {
			if (viveInput.left.touchPad.quadrant.top.pressedDown) {
				ChangeSensorSize(1);
			}
			if (viveInput.left.touchPad.quadrant.bottom.pressedDown) {
				ChangeSensorSize(-1);
			}
		}
	}

	void SetSensorSizeOptionTexts() {
		for (int i = 4; i >= 0; i--) {
			int thisSensorSizeIndex = sensorSizeIndex - i - 2 + 4;
			if (thisSensorSizeIndex >= 0 && thisSensorSizeIndex < 5) {
				sensorSizeOptionTexts[i].text = sensorSizeDescOptions[thisSensorSizeIndex];
			} else {
				sensorSizeOptionTexts[i].text = "";
			}
		}
	}

	public void turnOn() {
		on = true;
		Util.SetChildRenderersEnabled (sensorMenu, true);
	}
	
	public void turnOff() {
		on = false;
		Util.SetChildRenderersEnabled (sensorMenu, false);
	}

	void ChangeSensorSize(int indexChange) {
		sensorSizeIndex += indexChange;
		sensorSizeIndex = Mathf.Clamp (sensorSizeIndex, 0, 4);
		SetSensorSizeOptionTexts();

		zoomSettings.SetSensorHeightInMm (sensorSizeHeightOptions [sensorSizeIndex]);
	}
}
