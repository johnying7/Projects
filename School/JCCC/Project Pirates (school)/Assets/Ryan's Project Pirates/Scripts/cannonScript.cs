using UnityEngine;
using System.Collections;

public class cannonScript : MonoBehaviour {
	
	public GameObject cBall;
	public string shipGameTag; // used to determine if the ship this cannon is attached to is player/cpu
	public string cannonSide;
	public float refireWaitTime = 2.0f; //minimum time between cannon firings	
	public bool canFire = true; //true as long as the player can fire this cannon.
	private Transform myTransform;	
	private float heldButtonTime = 0.0f; // amount of time the player holds down the button before letting go to fire cannon.
	private float totalButtonHeld = 0.0f;
	private float maxFireDelay = 0.5f;
	private steerHelm helmScript;
	private AudioSource cannonFireSound;
	// Use this for initialization
	void Start () 
	{			
		helmScript = GameObject.FindGameObjectWithTag("Player").GetComponent<steerHelm>();		
		myTransform = this.gameObject.transform;
		shipGameTag = myTransform.parent.gameObject.tag; //should be enemyShip or playerShip
		cannonFireSound = this.audio;
	}
	
	public void fireCannon(float timeButtonWasHeld = 1.0f) //public so that the AI ships can call this method
	{					
		if(canFire && helmScript.shipAttached)
		{						
			//instantiate cannonball
			GameObject go = (GameObject)Instantiate(cBall,this.transform.position, Quaternion.identity);				
			cannonballScript cb = go.GetComponent<cannonballScript>();				
			//cb.target = (myTransform.position + myTransform.forward) * 10; //set the target for the cannonball to try to hit				
			cb.target = (myTransform.position + myTransform.forward); //Currently using cylinders for cannons, which need to be 'sideways' to look natural. This makes 'UP' tanslate to 'FORWARD'				
			cb.timeButtonWasHeld = timeButtonWasHeld;	
			cannonFireSound.Play();
			flipCanFire();
		}
	}	
	
	void flipCanFire() //used to switch the canFire variable to true/false - whatever the opposite of its current state is.
	{
		if(canFire)
		{
			//If we're setting this to false, that means we are firing, so we want to make sure to invoke the function again to allow firing
			//again after we wait for the refireTime cooldown.
			canFire = false;
			Invoke("flipCanFire", refireWaitTime);
		}
		else
		{
			canFire = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(shipGameTag == "PlayerShip")
		{				
			//print("this is a player ship");
			if(cannonSide.Contains("left"))
			{
				if(Input.GetMouseButtonDown(0))
				{			
					heldButtonTime = Time.time;	//store the gametime for when the button is first pressed		
				}
				if(Input.GetMouseButtonUp(0))
				{
					totalButtonHeld = Time.time - heldButtonTime;
					waitRandomTimeToFire(maxFireDelay);					
				}
			}
			else
			{
				if(Input.GetMouseButtonDown(1))
				{			
					heldButtonTime = Time.time;	//store the gametime for when the button is first pressed	
				}
				if(Input.GetMouseButtonUp(1))
				{
					totalButtonHeld = Time.time - heldButtonTime;
					waitRandomTimeToFire(maxFireDelay);
				}
			}
		}
		else //this is a cpu controlled ship
		{
			
		}			
	}
	
	public void waitRandomTimeToFire(float maxTimeToWait = 1.0f) // used for invoking purposes
	{
		float waitTime = Random.Range(0,maxTimeToWait);
		Invoke("delayedFire", waitTime);
	}
	
	void delayedFire()
	{
		if(totalButtonHeld == 0.0f)
		{
			totalButtonHeld = 1.0f;
		}
		fireCannon(totalButtonHeld); //Send the difference between the time button was pressed to time let go		
	}
}
