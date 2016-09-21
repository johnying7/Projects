using UnityEngine;
using System.Collections;

public class crossHair : MonoBehaviour { //attach script to player character's camera
	
	RaycastHit actionSight; //variable for creating the vector that hits objects
	RaycastHit gunSight; //variable for creating the vector that hits objects
	public Texture2D[] displayTexture;
	public int cHSize; //crosshair size
	private int halfCHSize; //half crosshair size
	public int actionRange; //references range at which to notice the helm
	public int gunRange; //references the range at which to notice enemies
	
	public bool helmSighted; //outside reference for sighted helm
	public bool takeDamage; //outside reference for a shot object
	
	private bool yellowCrosshair; //activation boolean for yellow crosshair sights
	private bool redCrosshair; //activation boolean for red crosshair sights
	private AudioSource gunshotSound;
	
	// Use this for initialization
	void Start () {
		halfCHSize = cHSize / 2;
		gunshotSound = transform.audio;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 playerSightDirection = this.transform.TransformDirection(Vector3.forward); //make the character (forward) camera direction into a variable
		if (Physics.Raycast(this.transform.position, playerSightDirection, out actionSight, actionRange)) //detect if the created ray hits an object with a range of actionRange
		{
			if (actionSight.collider.gameObject.tag == "SteeringWheel") //if the ray hits an object with steeringWheel tag
			{
				//Debug.Log("you have hit the helm");
				helmSighted = true; //if player is looking at the helmWheel
				yellowSight();
			}
			else if (actionSight.collider.gameObject.tag == "ShopKeeper")
			{
				Debug.Log("you see the shop keeper");
				//activate shop keeper gui system
				/*
				GameObject shopkeeper;
				shopkeeper = actionSight.collider.gameObject;
				if (shopkeeper == blacksmith)
				{
					//open the blacksmith gui
				}
				*/
			}
		}
		else
		{
			//Debug.Log("you have released sight of the helm");
			helmSighted = false; //if player is not looking at the helmWheel
			noSight();
		}

		if (Physics.Raycast(this.transform.position, playerSightDirection, out gunSight, gunRange)) //detect if the created ray hits an object
		{
			if (gunSight.collider.gameObject.tag == "Enemy") //if the ray hits an object with steeringWheel tag
			{
				redSight();
				
				//Debug.Log ("You see the enemy.");
				
				if (Input.GetKeyDown(KeyCode.Mouse0)) //if the player fires while aiming at an enemy (left mouse button)
				{
					enemyAI enemy = gunSight.collider.GetComponent<enemyAI>(); //pull up the object script that the raycast collided with
					enemy.takeDamage(); //reduce the enemy hp
					
				}
			}
		}
		else
		{
			noSight();
			//Debug.Log("You no longer see the enemy.");
		}
		
		Debug.DrawRay(this.transform.position, playerSightDirection * actionRange, Color.yellow); //debugs the distance of the raycast (actionRange or gunRange)
	} //end update
	
	void OnGUI() { //manages the crosshair colors
		if (yellowCrosshair == true)
		{
			GUI.Label(new Rect(Screen.width / 2 - halfCHSize, Screen.height / 2 - halfCHSize,
				cHSize, cHSize), displayTexture[1]);
		}
		else if (redCrosshair == true)
		{
			GUI.Label(new Rect(Screen.width / 2 - halfCHSize, Screen.height / 2 - halfCHSize,
				cHSize, cHSize), displayTexture[2]);
		}
		else
		{
			GUI.Label(new Rect(Screen.width / 2 - halfCHSize, Screen.height / 2 - halfCHSize,
				cHSize, cHSize), displayTexture[0]);
		}
	}
	
	void redSight()
	{
		redCrosshair = true;
		yellowCrosshair = false;
	}
	
	void yellowSight()
	{
		redCrosshair = false;
		yellowCrosshair = true;
	}
	
	void noSight()
	{
		redCrosshair = false;
		yellowCrosshair = false;
	}
}
