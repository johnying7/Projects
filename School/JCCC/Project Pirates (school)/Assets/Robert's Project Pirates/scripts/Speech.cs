using UnityEngine;
using System.Collections;

public class Speech : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Label (new Rect(100,Screen.height-500,Screen.width-100,300),"Ah, greetings young pirate!  I see you've come to this island!  There are other places with treasure about, but I am certain you know of such by now.");
	}
}
