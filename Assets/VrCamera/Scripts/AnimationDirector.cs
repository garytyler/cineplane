using UnityEngine;
using System.Collections;

public class AnimationDirector : MonoBehaviour {

	Animation[] animations;

	// Use this for initialization
	void Start () {
		animations = new Animation[3];
		//animations[0] = GameObject.Find ("KittenNPC").GetComponent<Animation> ();
		//animations[1] = GameObject.Find ("KittenNPC1").GetComponent<Animation> ();
		//animations[2] = GameObject.Find ("KittenNPC2").GetComponent<Animation> ();

	}

	public void RestartAllAnimations() {
		//foreach (Animation animation in animations) {
		//	Util.PlayFromStart (animation);
		//}
	}
}
