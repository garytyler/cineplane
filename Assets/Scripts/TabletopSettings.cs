using UnityEngine;
using System.Collections;

public class TabletopSettings : MonoBehaviour {

	private GameObject container;
	private GameObject tabletopTarget;
	private GameObject liveActionTarget;
	private GameObject leftRig;
	private GameObject rightRig;
	private GameObject menu;

	public GameObject hmd;

	// Use this for initialization
	void Start () {
		container = GameObject.Find ("SteamVRContainer");
		tabletopTarget = GameObject.Find ("TabletopScaleTarget");
		liveActionTarget = GameObject.Find ("LiveActionScaleTarget");

		leftRig = GameObject.Find ("LeftControllerRig");
		rightRig = GameObject.Find ("RightControllerRig");
		menu = GameObject.Find ("ProjectedMenu");
	}

	public void SetTabletopMode(bool on) {
		if (on) {
			container.transform.position = tabletopTarget.transform.position;
			container.transform.localScale = tabletopTarget.transform.localScale;
			rightRig.transform.localScale = tabletopTarget.transform.localScale;
			leftRig.transform.localScale = tabletopTarget.transform.localScale;
			menu.transform.localScale = tabletopTarget.transform.localScale;
			menu.transform.position = hmd.transform.position;
		} else {
			container.transform.position = liveActionTarget.transform.position;
			container.transform.localScale = liveActionTarget.transform.localScale;
			rightRig.transform.localScale = liveActionTarget.transform.localScale;
			leftRig.transform.localScale = liveActionTarget.transform.localScale;
			menu.transform.localScale = liveActionTarget.transform.localScale;
			menu.transform.position = hmd.transform.position;
		}
	}
}
