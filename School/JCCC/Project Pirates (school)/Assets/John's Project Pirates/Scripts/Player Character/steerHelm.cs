using UnityEngine;
using System.Collections;

public class steerHelm : MonoBehaviour { //attach to player character
	
	public Camera characterCamera; //first person controller camera
	public Camera shipCamera; //player ship camera
	
	public bool shipAttached = false; //is the player a child/attached to the ship

	public Transform mainCharacter; //references the object of the first person controller
	public Transform ship; //references the ship object
	public Transform shipStandPosition; //references the empty object of child of the ship
		
	// Use this for initialization
	void Start () {
		
		//makes the camera viewpoint of the character true, and the ship view false
		characterCamera.enabled = true;
		shipCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.F))
		{				
			if (shipAttached == false) //if the player isn't already attached to the ship
			{
				crossHair chScript = characterCamera.GetComponent<crossHair>();
				
				if ( chScript.helmSighted == true ) //if player is looking at helmWheeel
				{
					//Debug.Log ("You're trying to use the helm.");
					MouseLook mouseLook = mainCharacter.GetComponent<MouseLook>(); //get mouselook script in mainCharacter
					mouseLook.enabled = false;
					
					MouseLook charLook = characterCamera.GetComponent<MouseLook>(); //get mouselook script in characterCamera
					charLook.enabled = false;
					
					CharacterMotor motor = mainCharacter.GetComponent<CharacterMotor>(); //get charactermotor script in mainCharacter
					motor.enabled = false;
					
					MouseLook shipLook = shipCamera.GetComponent<MouseLook>();
					shipLook.enabled = true;
					
					mainCharacter.transform.forward = ship.transform.forward; //change the character's facing direction the same as the ship
					
					mainCharacter.transform.position = shipStandPosition.position; //move character to position behind helm
					mainCharacter.transform.parent = ship.transform; //make player a child of ship (move and turn with ship)
					characterCamera.enabled = false; //disable view from character camera
					shipCamera.enabled = true; //enable view from ship camera
						
					driveShip shipEngine = ship.GetComponent<driveShip>();
					shipEngine.enabled = true;
						
					shipAttached = true;
				}
			}
			else if (shipAttached == true) //if player is attached to ship
			{
				//Debug.Log ("Ship detach start.");
				shipAttached = false; //detach character from ship
				
				MouseLook mouseLook = mainCharacter.GetComponent<MouseLook>();
				mouseLook.enabled = true;
				
				MouseLook charLook = characterCamera.GetComponent<MouseLook>();
				charLook.enabled = true;
				
				CharacterMotor motor = mainCharacter.GetComponent<CharacterMotor>();
				motor.enabled = true;
				
				shipCamera.transform.rotation = ship.transform.rotation;
				
				MouseLook shipLook = shipCamera.GetComponent<MouseLook>();
				shipLook.enabled = false;
				
				mainCharacter.transform.parent = null; //detach the character as a child of the ship to no one
				shipCamera.enabled = false; //disable ship camera
				characterCamera.enabled = true; //enable player camera
				
				driveShip shipEngine = ship.GetComponent<driveShip>();
				shipEngine.enabled = false;
				
				//Debug.Log ("Ship detach finished.");
			}
		}
		sinkShip sinkShipScript = ship.GetComponent<sinkShip>(); //get charactermotor script in mainCharacter
		if (sinkShipScript.shipIsSunk == true)
		{
			mainCharacter.transform.position = shipStandPosition.position; //sink the player with the ship
		}
	} //end update function
} //end class function
