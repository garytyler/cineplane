using UnityEngine;
using System.Collections;

public class ZoomSettings : MonoBehaviour {

	public bool on = false;
	GameObject zoomMenu;
	ViveInput viveInput;
	bool zoomEnabled = true;
	//CpPlayback cpPlayback;
	float fov = 63.25f;
	float focalLengthInMm = 16;
	float sensorHeightInMm = (35f * (9f/16f));
	TextMesh focalLengthText;
	//Camera camera;
	TextMesh zoomOnText;
	TextMesh zoomOffText;
	bool onForOneFrame = false;
	int[] primeFocalLengthOptions = {10, 12, 14, 16, 18, 20, 22, 24, 28, 32, 35, 40, 45, 50, 55, 60, 70, 80, 90, 100, 120, 140, 160, 180, 200};
	int primeFocalLengthIndex = 3;

	TextMesh[] focalLengthOptionTexts;


	// Use this for initialization
	void Start () {
		zoomMenu = GameObject.Find ("ZoomMenu");
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput> ();
		//cpPlayback = GameObject.Find ("CameraPlayback").GetComponent<CpPlayback> ();
		focalLengthText = GameObject.Find ("FocalLengthText").GetComponent<TextMesh> ();
		zoomOnText = GameObject.Find ("ZoomOnText").GetComponent<TextMesh> ();
		zoomOffText = GameObject.Find ("ZoomOffText").GetComponent<TextMesh> ();
		Camera camera = GameObject.Find("CpCamera").GetComponent<Camera>();

		focalLengthOptionTexts = new TextMesh[5];
		focalLengthOptionTexts [0] = GameObject.Find ("PrimeFocalLengthOption0").GetComponent<TextMesh> ();
		focalLengthOptionTexts [1] = GameObject.Find ("PrimeFocalLengthOption1").GetComponent<TextMesh> ();
		focalLengthOptionTexts [2] = GameObject.Find ("PrimeFocalLengthOption2").GetComponent<TextMesh> ();
		focalLengthOptionTexts [3] = GameObject.Find ("PrimeFocalLengthOption3").GetComponent<TextMesh> ();
		focalLengthOptionTexts [4] = GameObject.Find ("PrimeFocalLengthOption4").GetComponent<TextMesh> ();

		SetPrimeFocalLengthOptionTexts();
	}

	void Update() {
		if (on && onForOneFrame) {
			if (viveInput.left.touchPad.quadrant.left.pressedDown) {
				zoomEnabled = false;
				zoomOffText.color = new Color(1, 1, 1);
				zoomOnText.color = new Color(0.5f, 0.5f, 0.5f);

				fov = Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorHeightInMm / (2.0f * focalLengthInMm));
				ChangeFocalLengthMonitorDisplay();
			}
			if (viveInput.left.touchPad.quadrant.right.pressedDown) {
				zoomEnabled = true;
				zoomOnText.color = new Color(1, 1, 1);
				zoomOffText.color = new Color(0.5f, 0.5f, 0.5f);
				ChangeFocalLengthMonitorDisplay ();
			}
			if (viveInput.left.touchPad.quadrant.top.pressedDown) {
				ChangePrimeFocalLength(1);
			}
			if (viveInput.left.touchPad.quadrant.bottom.pressedDown) {
				ChangePrimeFocalLength(-1);
			}
		}

		if (on && !onForOneFrame) {
			onForOneFrame = true;
		}
	}

	public void turnOn() {
		on = true;
		onForOneFrame = false;
		Util.SetChildRenderersEnabled (zoomMenu, true);
	}

	public void turnOff() {
		on = false;
		onForOneFrame = false;
		Util.SetChildRenderersEnabled (zoomMenu, false);
	}

	void SetPrimeFocalLengthOptionTexts() {
		for (int i = 4; i >= 0; i--) {
			int thisPrimeFocalLengthIndex = primeFocalLengthIndex - i - 2 + 4;
			if (thisPrimeFocalLengthIndex >= 0 && thisPrimeFocalLengthIndex < 20) {
				focalLengthOptionTexts[i].text = primeFocalLengthOptions[thisPrimeFocalLengthIndex].ToString () + "mm";
			} else {
				focalLengthOptionTexts[i].text = "";
			}
		}
	}

	void ChangePrimeFocalLength(int indexChange) {
		primeFocalLengthIndex += indexChange;
		primeFocalLengthIndex = Mathf.Clamp (primeFocalLengthIndex, 0, 20);
		SetPrimeFocalLengthOptionTexts();
		float oldPrimeFov = Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorHeightInMm / (2.0f * focalLengthInMm));
		focalLengthInMm = primeFocalLengthOptions[primeFocalLengthIndex];
		float newPrimeFov = Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorHeightInMm / (2.0f * focalLengthInMm));
		fov = fov + (newPrimeFov-oldPrimeFov);
		ChangeFocalLengthMonitorDisplay ();
	}

	void ChangeFocalLengthMonitorDisplay() {
		if (zoomEnabled) {
			float zoomFocalLengthInMm = sensorHeightInMm / (2.0f * Mathf.Tan (0.5f * fov * Mathf.Deg2Rad));
			focalLengthText.text = "Z" + zoomFocalLengthInMm.ToString ("f1") + "mm";
		} else {
			focalLengthText.text = focalLengthInMm.ToString() + "mm";		            
		}
	}

	public void SetSensorHeightInMm(float newSensorHeightInMm) {
		sensorHeightInMm = newSensorHeightInMm;
		fov = Mathf.Rad2Deg * 2.0f * Mathf.Atan(sensorHeightInMm / (2.0f * focalLengthInMm));
	}
}
