using UnityEngine;
using System.Collections;

public class cannonballScript : MonoBehaviour 
{
		
	public float initialVelocity = 1.0f;
	public float timeButtonWasHeld; //holds the time (in seconds) the fire button was held down
	public Vector3 target; //target 'point' we are wanting to hit
	Vector3 direction; //holds the normalized direction vector we want the cannonball to go toward
	Rigidbody rbody; //holds the gameobject's rigidbody
	private float velocityIncreaseOverTime = 10.0f; //increase the velocity based on the amount of time the button was held down - this is the value  to multiply by
	private float maxAddedVelocity = 200f; //cap the added velocity from holding down the fire button to this.
	// Use this for initialization
	void Start () 
	{				
		rbody = GetComponent<Rigidbody>(); //store rigidbody component for future use...
		Vector3 angle = (target - this.transform.position).normalized; //get the normalized Vector between 2 positions
		direction = (new Vector3(0f,0.02f,0f)) + new Vector3(angle.x,0,angle.z); //ignore y component of the angle var - we're setting Y manually.					
		//Add in velocity based on how long the firing button was held...
		rbody.AddForce(direction * Mathf.Clamp(initialVelocity * (1 + timeButtonWasHeld * velocityIncreaseOverTime),0,maxAddedVelocity),ForceMode.Impulse);						
	}
	
	void Update()
	{
		Debug.DrawLine(this.transform.position,(this.transform.position + this.rigidbody.velocity), Color.red);	//draw a debug line to show the cannon's trajectory
	}
	
	
	//detect collision with water/ship/land/etc.
	
	void OnTriggerEnter(Collider col)
	{				
		//check if it's water so we can destroy it
		if(col.gameObject.layer == 4 || col.gameObject.tag == "Land")
		{					
			destroyCannonball(0.2f);	
		}
		
		if(col.gameObject.tag == "enemyShip" && this.gameObject.tag == "cannonball") //if we're colliding with an enemy ship & this is a player's cannonball
		{
			enemyShipScript scriptObj = col.gameObject.GetComponent<enemyShipScript>();
			scriptObj.takeDamage(20);
			destroyCannonball(0);	
		}
		
		if(col.gameObject.tag == "PlayerShip" && this.gameObject.tag == "cannonBallEnemy") //if we're colliding with an enemy ship & this is a enemy cannonball
		{
			//have player take damage
			playerShipScript scriptObj = col.gameObject.GetComponent<playerShipScript>();
			scriptObj.takeDamage(20);
			destroyCannonball(0);
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
			
	}
	
	void destroyCannonball(float deletionTime)
	{
		Destroy(this.gameObject, deletionTime);//delay destruction slightly to allow the ball to leave player's view		
	}
}