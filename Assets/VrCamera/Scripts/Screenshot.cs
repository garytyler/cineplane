using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {
	
	public int resWidth = 1920; 
	public int resHeight = 1080;
	public RenderTexture rt;
	
	private bool takeHiResShot = false;
	private ViveInput viveInput;

	public static string ScreenShotName(int width, int height) {
		
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
		                     Application.dataPath, 
		                     width, height, 
		                     System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	public void TakeHiResShot() {
		takeHiResShot = true;
	}

	public void Start() {
		viveInput = GameObject.Find ("ViveInput").GetComponent<ViveInput> ();
	}

	void LateUpdate() {
		takeHiResShot |= viveInput.right.topButton.pressedLongUp; //Input.GetKeyDown("k");
		if (takeHiResShot) {
			//RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
			GetComponent<Camera>().targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			GetComponent<Camera>().Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null; // JC: added to avoid errors
			//Destroy(rt);

			byte[] bytes = screenShot.EncodeToPNG();
			string filename = ScreenShotName(resWidth, resHeight);
			System.IO.File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			takeHiResShot = false;
		}
	}
}