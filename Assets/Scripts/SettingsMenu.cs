using UnityEngine;
using System.Collections;

public class SettingsMenu : MonoBehaviour {

    private bool on = true;
    public string menuMode = "menu";
	private string oldTouching = "";

    private ViveInput viveInput;
	private ScaleSettings scaleSettings;
	private PuppetSettings puppetSettings;
	private ZoomSettings zoomSettings;
	private SensorSettings sensorSettings;
	private GameObject menuTouchPoint;

	// Use this for initialization
	void Start () {
        viveInput = GameObject.Find("ViveInput").GetComponent<ViveInput>();
		scaleSettings = GetComponent<ScaleSettings> ();
		puppetSettings = GetComponent<PuppetSettings> ();
		zoomSettings = GetComponent<ZoomSettings> ();
		sensorSettings = GetComponent<SensorSettings> ();
		menuTouchPoint = GameObject.Find ("MenuTouchPoint");
	}
	
	// Update is called once per frame
	void Update () {
        if (on) {
            string touching = GetModeTouching();
			menuTouchPoint.transform.localPosition = new Vector3 (viveInput.left.touchPad.x / 10f - 0.0016f, 0, (viveInput.left.touchPad.y / 10f) - 0.04f);

            if (touching != null)
            {
                if (viveInput.left.touchPad.pressed)
                {
					SwitchToSpecificMenu(touching);
                }
            }

			if (touching != oldTouching) {
				viveInput.left.haptic = 0.2f;
			} else {
				viveInput.left.haptic = 0;
			}

			oldTouching = touching;

        } else {
            if (viveInput.left.trigger.pressed)
            {
                SwitchToMainMenu();
            }
        }
	}

    string GetModeTouching() {
        string touching = null;

		if (viveInput.left.touchPad.octant.top.touched) {
            touching = "scale";
        }

        if (viveInput.left.touchPad.octant.topLeft.touched) {
            touching = "puppet";
        }

		if (viveInput.left.touchPad.octant.left.touched) {
			touching = "zoom";
		}

		if (viveInput.left.touchPad.octant.bottom.touched) {
			touching = "sensor";
		}

        return touching;
    }

    void SwitchToSpecificMenu(string touching)
    {
        on = false;
		menuMode = touching;
		TurnOnSpecificMenu();
        Util.SetChildRenderersEnabled(gameObject, false);
    }

    void SwitchToMainMenu()
    {
        on = true;
        Util.SetChildRenderersEnabled(gameObject, true);
		TurnOffSpecificMenus ();
        menuMode = "menu";
    }

	void TurnOnSpecificMenu() {
		if (menuMode == "scale")
			scaleSettings.TurnOn ();
		if (menuMode == "puppet")
			puppetSettings.turnOn ();
		if (menuMode == "zoom")
			zoomSettings.turnOn ();
		if (menuMode == "sensor")
			sensorSettings.turnOn ();
	}

	void TurnOffSpecificMenus() {
		if (menuMode == "scale")
			scaleSettings.TurnOff ();
		if (menuMode == "puppet")
			puppetSettings.turnOff ();
		if (menuMode == "zoom")
			zoomSettings.turnOff ();
		if (menuMode == "sensor")
			sensorSettings.turnOff ();
	}
}
