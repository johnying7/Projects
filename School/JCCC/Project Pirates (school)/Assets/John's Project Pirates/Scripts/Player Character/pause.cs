using UnityEngine;
using System.Collections;

public class pause : MonoBehaviour {
	
	public bool paused;
	public GameObject mainCharacter; //references the object of the first person controller
	public GameObject characterCamera;
	public GameObject shipCamera;
	public GameObject bony;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("cursor lock start");
		Screen.lockCursor = true;
		Screen.showCursor = false;
		paused = false;
		Time.timeScale = 1;
		mainCharacter = GameObject.FindGameObjectWithTag("Player");
		characterCamera = GameObject.FindGameObjectWithTag("MainCamera");
		shipCamera = GameObject.FindGameObjectWithTag("ShipCamera");
		bony = GameObject.FindGameObjectWithTag("PlayerMesh");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (paused == false) //if the game isn't paused
			{
				pauseGame(); //pause the game
			}
			else if (paused == true) //if the game is paused
			{
				unPauseGame(); //unpause the game
			}
		}
	}
	
	void unPauseGame(){ //resumes the game if paused
		Debug.Log ("cursor lock toggle1");
		
		steerHelm steerShip = mainCharacter.GetComponent<steerHelm>();
		if (steerShip.shipAttached == true)
		{
			MouseLook shipLook = shipCamera.GetComponent<MouseLook>();
			shipLook.enabled = true;
		}
		else
		{
			MouseLook mouseLook = mainCharacter.GetComponent<MouseLook>();
			mouseLook.enabled = true;
			
			MouseLook charLook = characterCamera.GetComponent<MouseLook>();
			charLook.enabled = true;
			
			playerAnimation animate = bony.GetComponent<playerAnimation>();
			animate.enabled = true;
		}
		
		CharacterMotor motor = mainCharacter.GetComponent<CharacterMotor>();
		motor.enabled = true;
		
		Screen.lockCursor = true; //unlocks the position of the mouse to the user
		Screen.showCursor = false; //shows the mouse pointer on the screen
		Time.timeScale = 1; //sets the time to real time
		paused = false; //sets the paused boolean to false
		

	}
	
	void pauseGame(){ //pauses the game if it is not paused
		Debug.Log ("cursor lock toggle2");
		
		MouseLook mouseLook = mainCharacter.GetComponent<MouseLook>(); //get mouselook script in mainCharacter
		mouseLook.enabled = false;
		
		MouseLook charLook = characterCamera.GetComponent<MouseLook>(); //get mouselook script in characterCamera
		charLook.enabled = false;
		
		CharacterMotor motor = mainCharacter.GetComponent<CharacterMotor>(); //get charactermotor script in mainCharacter
		motor.enabled = false;
		
		MouseLook shipLook = shipCamera.GetComponent<MouseLook>();
		shipLook.enabled = false;
		
		playerAnimation animate = bony.GetComponent<playerAnimation>();
		animate.enabled = false;
		
		Screen.lockCursor = false; //locks the position of the mouse to the center of the screen
		Screen.showCursor = true; //hides the mouse pointer from the user
		Time.timeScale = 0; //freezes animations, physics, or any Update code that operates on the delta time
		paused = true;
		

	}
}
