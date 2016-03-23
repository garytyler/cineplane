using UnityEngine;
using System.Collections;

public class TimeCode : MonoBehaviour {

	public int frame = 0;
	TextMesh text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		int FPS = 90;

		int frames = (frame%FPS);

		int secondsInAMinute = 60;
		int totalSeconds = frame / FPS;
		int seconds = totalSeconds % secondsInAMinute;

		int minutesInAnHour = 60;
		int framesPerMinute = FPS * secondsInAMinute;
		int totalMinutes = frame / framesPerMinute;
	    int minutes = totalMinutes % minutesInAnHour;

		int secondsInAnHour = secondsInAMinute * minutesInAnHour;
		int framesPerHour = FPS * secondsInAnHour;
		int totalHours = frame / framesPerHour;
		int hours = totalHours;

		text.text = hours.ToString ("00") + ":" + minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + frames.ToString("00");
	}
}
