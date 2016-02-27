using UnityEngine;
using System.Collections;

public class ScaleSettings : MonoBehaviour {


	private ViveInput viveInput;
	private GameObject steamVrContainer;
    private SettingsMenu settingsMenu;
	private GameObject scaleMenu;
	private GameObject leftRig;
	private GameObject rightRig;
	private TextMesh scaleText;

	public GameObject hmd;

	public bool on = false;
	float scale = 1;

	void Start () {
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput>();
		steamVrContainer = GameObject.Find ("SteamVRContainer");
		scaleMenu = GameObject.Find ("ScaleMenu");
		leftRig = GameObject.Find ("LeftControllerRig");
		rightRig = GameObject.Find ("RightControllerRig");
		scaleText = GameObject.Find ("VirtualScale").GetComponent<TextMesh> ();
	}
	
	void Update () {
        if (on) {
            if (Mathf.Abs(viveInput.left.touchPad.y) > 0.1f)
            {
				ChangeScale();
                viveInput.left.haptic = 0.01f;
				ScaleAndShiftContainer();

			} else {
				viveInput.left.haptic = 0f;
			}
		}

		if (viveInput.left.touchPad.pressed) {
			scale = 1;
			ScaleAndShiftContainer ();
		}
	}

	public void TurnOn() {
		on = true;
		Util.SetChildRenderersEnabled (scaleMenu, true);
	}

	public void TurnOff() {
		on = false;
		Util.SetChildRenderersEnabled (scaleMenu, false);
	}

	void ChangeScale() {
		scale *= 1 + (viveInput.left.touchPad.y / 500f);
		scale = Mathf.Clamp(scale, 0.10f, 10);
		rightRig.transform.localScale = new Vector3(scale, scale, scale);
		leftRig.transform.localScale = new Vector3(scale, scale, scale);
		scaleText.text = scale.ToString ("f2");
	}

	void ScaleAndShiftContainer() {
		float oldHmdX = hmd.transform.position.x;
		float oldHmdZ = hmd.transform.position.z;
		
		steamVrContainer.transform.localScale = new Vector3(scale, scale, scale);
		
		float newContainerX = steamVrContainer.transform.position.x - (hmd.transform.position.x - oldHmdX);
		float newContainerZ = steamVrContainer.transform.position.z - (hmd.transform.position.z - oldHmdZ);
		steamVrContainer.transform.position = new Vector3(newContainerX, 0, newContainerZ);
	}
}
