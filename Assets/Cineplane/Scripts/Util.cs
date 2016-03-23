using UnityEngine;
using System.Collections;

public static class Util {

    static public void SetChildRenderersEnabled(GameObject gameObject, bool enabled)
    {
        foreach (Renderer r in gameObject.GetComponentsInChildren<Renderer>())
            r.enabled = enabled;
    }



	static public void PlayFromStart(Animation animation) {
		//animation.clip.Rewind (); // [0]. .Rewind ();
		//animation.clip.
		animation.Rewind("Idle");
		animation.Play ("Idle");
	}

	static public Vector3[] Vector3ArrayUntilIndex(Vector3[] array, int index) {
		Vector3[] resultArray = new Vector3[index];
		for (int i = 0; i < index; i++) {
			resultArray[i] = array[i];
		}
		return resultArray;
	}
	
	static public float[] FloatArrayUntilIndex(float[] array, int index) {
		float[] resultArray = new float[index];
		for (int i = 0; i < index; i++) {
			resultArray[i] = array[i];
		}
		return resultArray;
	}

	static public GameObject GetChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}


}