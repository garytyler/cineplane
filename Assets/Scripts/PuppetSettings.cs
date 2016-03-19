using UnityEngine;
using System.Collections;

public class PuppetSettings : MonoBehaviour {

	GameObject puppet;
	public bool on;

	// Use this for initialization
	void Start () {
        puppet = GameObject.Find("Puppet");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void turnOn() {
		Util.SetChildRenderersEnabled (puppet, true);
		on = true;
    }

    public void turnOff() {
		Util.SetChildRenderersEnabled (puppet, false);
		on = false;
    }
}
