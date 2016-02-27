using UnityEngine;
using System.Collections;

public class MenuCube : MonoBehaviour {

	public Material highlightedVolumeCubeMaterial;
	public Material volumeCubeMaterial;
	public string cubeName;

	private GameObject volumeCube;
	private int wandsTouching = 0;
	private bool rightInside = false;
	private bool leftInside = false;
	private ViveInput viveInput;
	private SettingsMenu2 settingsMenu;

	// Use this for initialization
	void Start () {
		volumeCube = Util.GetChildGameObject (gameObject, "VolumeCube");
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput> ();
		settingsMenu = GameObject.Find ("ProjectedMenu").GetComponent<SettingsMenu2> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rightInside && viveInput.right.trigger.pressedDown) {
			settingsMenu.Press(cubeName);
		}

		if (leftInside && viveInput.left.trigger.pressedDown) {
			settingsMenu.Press (cubeName);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "RightControllerRig" || other.name == "LeftControllerRig") {
			wandsTouching++;
			volumeCube.GetComponent<Renderer>().material = highlightedVolumeCubeMaterial;
		}

		if (other.name == "RightControllerRig") {
			rightInside = true;
		}
		if (other.name == "LeftControllerRig") {
			leftInside = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name == "RightControllerRig" || other.name == "LeftControllerRig") {
			wandsTouching--;
			if (wandsTouching == 0) {
				volumeCube.GetComponent<Renderer>().material = volumeCubeMaterial;
			}
		}

		if (other.name == "RightControllerRig") {
			rightInside = false;
		}
		if (other.name == "LeftControllerRig") {
			leftInside = false;
		}


	}
}
