using UnityEngine;
using System.Collections;

public class ClipScreenshot : MonoBehaviour {
	
	public int resWidth = 1920; 
	public int resHeight = 1080;
	public RenderTexture rt;
	public RenderTexture operatorTexture;
	
	private bool takeHiResShot = false;

	public void TakeHiResShot() {
		takeHiResShot = true;
	}

	void LateUpdate() {
		if (takeHiResShot) {
			GetComponent<Camera>().targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			GetComponent<Camera>().Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			GetComponent<Camera>().targetTexture = operatorTexture;
			RenderTexture.active = null; // JC: added to avoid errors
			takeHiResShot = false;
		}
	}
}