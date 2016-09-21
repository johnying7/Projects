using UnityEngine;
using System.Collections;

public class HowToPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		GUI.Label(new Rect(100,50,Screen.width/2,Screen.height/2),"In this game, you play as a pirate whom is searching for treasure, while also fending off other pirate ships.");
		GUI.Label(new Rect(100,82,Screen.width/2,Screen.height/2),"To play while on land, you move with the WASD keys.  You also can shoot with the left mouse key, and swing your sword with the right mouse key.");
		GUI.Label(new Rect(100,128,Screen.width/2,Screen.height/2),"When on your boat, you click on the steering wheel to disembark on it.  Then, you use the WASD keys to move and steer, and each side of your ship shoots cannon balls with the appropriate mouse click.");
		GUI.Label(new Rect(100,188,Screen.width/2,Screen.height/2),"While out, you need to be prepared to fight other pirate ships, and also solve puzzles to be able to gain treasure hidden within islands.");
		if(GUI.Button(new Rect(200,250,100,50),"Return to menu")){
			Application.LoadLevel ("Menu");
		}
		GUI.EndGroup();
	}
}
