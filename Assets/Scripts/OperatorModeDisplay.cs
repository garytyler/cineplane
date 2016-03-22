using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OperatorModeDisplay : MonoBehaviour {

	private Dictionary<string, GameObject> modeDisplays = new Dictionary<string, GameObject>();

	// Use this for initialization
	void Start () {
		modeDisplays["record"] = GameObject.Find("RecordModeDisplay");
		modeDisplays["playback"] = GameObject.Find("PlaybackModeDisplay");
		modeDisplays["standby"] = GameObject.Find("StandbyModeDisplay");

	}

	public void SetMode(string mode) {
		foreach (GameObject modeDisplay in modeDisplays.Values) {
			Util.SetChildRenderersEnabled (modeDisplay, false);
		}

		Util.SetChildRenderersEnabled (modeDisplays [mode], true);
	}

}
