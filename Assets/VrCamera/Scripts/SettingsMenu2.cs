using UnityEngine;
using System.Collections;

public class SettingsMenu2 : MonoBehaviour {

	public bool on = false;
	public GameObject hmd;

	private ViveInput viveInput;
	private GameObject menu;
	private GameObject mainMenu;
	private GameObject tabletopMenu;
	private TabletopSettings tabletopSettings;
	private GameObject playbackMenu;

	private GameObject vrContainer;
	private Light normalLight;
		
	// Use this for initialization
	void Start () {
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput> ();
		menu = GameObject.Find ("ProjectedMenu");
		mainMenu = GameObject.Find ("MainMenu");
		tabletopMenu = GameObject.Find ("TabletopMenu");
		tabletopSettings = GameObject.Find ("ProjectedMenu").GetComponent<TabletopSettings> ();
		playbackMenu = GameObject.Find ("PlaybackMenu");
		vrContainer = GameObject.Find ("SteamVRContainer");
		normalLight = GameObject.Find ("NormalLight").GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (viveInput.left.topButton.pressedDown) {
			if (on) {
				TurnOff ();
			} else {
				TurnOn();
			}
		}
	}

	void TurnOn() {
		on = true;
		menu.transform.localScale = vrContainer.transform.localScale;
		menu.transform.position = hmd.transform.position;
		Vector3 hmdRotation = hmd.transform.rotation.eulerAngles;
		menu.transform.rotation = Quaternion.Euler (new Vector3(hmdRotation.x, hmdRotation.y, 0));
		Util.SetChildRenderersEnabled (mainMenu, true);
		Util.SetChildMenuCubesEnabled (mainMenu, true);
		normalLight.enabled = false;
	}

	void TurnOff() {
		on = false;
		Util.SetChildRenderersEnabled (menu, false);
		Util.SetChildMenuCubesEnabled (menu, false);
		normalLight.enabled = true;
	}

	public void Press(string cubeName) {
		if (cubeName == "Tabletop") {
			Util.SetChildRenderersEnabled(mainMenu, false);
			Util.SetChildMenuCubesEnabled(mainMenu, false);

			Util.SetChildRenderersEnabled(tabletopMenu, true);
			Util.SetChildMenuCubesEnabled(tabletopMenu, true);
		}

		if (cubeName == "Playback") {
			Util.SetChildRenderersEnabled(mainMenu, false);
			Util.SetChildMenuCubesEnabled(mainMenu, false);

			Util.SetChildRenderersEnabled(playbackMenu, true);
			Util.SetChildMenuCubesEnabled(playbackMenu, true);

		}

		if (cubeName == "TabletopMode") {
			tabletopSettings.SetTabletopMode(true);
		}

		if (cubeName == "GroundMode") {
			tabletopSettings.SetTabletopMode(false);
		}
	}
}
