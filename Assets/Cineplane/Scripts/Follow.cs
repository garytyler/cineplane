using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public GameObject target;

	// Update is called once per frame
	void Update () {
		//transform.parent = target.transform;
		// make sure its exactly on it
		transform.localPosition = target.transform.position; //Vector3.zero;
		transform.localRotation = target.transform.rotation; //Quaternion.identity;	
	}
}
