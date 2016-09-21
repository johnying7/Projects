using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Texture2D iconOne;
	public Texture2D iconTwo;
	public Texture2D iconThree;
	private GUIStyle transparent = new GUIStyle();
	
	void OnGUI() {
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		if(GUI.Button(new Rect(150,Screen.height/2,128,40),iconOne, transparent)){
			Application.LoadLevel ("capstone_Pirates");
		}
		if(GUI.Button(new Rect(310,Screen.height/2,70,40),iconTwo, transparent)){
			Application.LoadLevel("HowToPlay");
		}
		if(GUI.Button(new Rect(390,Screen.height/2,70,40),iconThree, transparent)){
			Application.Quit();
		}
		GUI.EndGroup();
	}
}
