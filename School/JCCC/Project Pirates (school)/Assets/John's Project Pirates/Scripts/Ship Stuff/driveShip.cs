using UnityEngine;
using System.Collections;

public class driveShip : MonoBehaviour { //attach to ship object
	
	public float halfSailSpeed; //half sail boat speed (test 5)
	public float fullSailSpeed; //full sail boat speed (test 10)
	public float turnSpeed; //adjustable turn speed (test 45)
	
	//the speed(mode) of the boat
	bool halfSail = false;
	bool fullSail = false;
	
	// Use this for initialization
	void Start () {
		//halfSailSpeed = 10.0f;
		//fullSailSpeed = 30.0f;
	}
	
	// Update is called once per frames
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.W)) //if w key is hit, increase speed
		{
			if(halfSail == false && fullSail == false) //if sailing still, go half sail
			{
				halfSail = true;
			}
			else if(halfSail == true) //if already half sail, go full sail speed
			{
				halfSail = false;
				fullSail = true;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.S)) //if s key is hit, decrease boat speed
		{
			if(halfSail == true) //if already half sail, go no sail (stalled)
			{
				halfSail = false;
			}
			else if(fullSail == true) //if full sail, go down to half sail speed
			{
				fullSail = false;
				halfSail = true;
			}
		}
		
		if(Input.GetKey (KeyCode.A)) //turn left
		{
			transform.Rotate ( 0.0f, -turnSpeed * Time.deltaTime, 0.0f);
		}
		
		if(Input.GetKey (KeyCode.D)) //turn right
		{
			transform.Rotate ( 0.0f, turnSpeed * Time.deltaTime, 0.0f);
		}
		
		if(halfSail == true) //if halfSail (mode), go halfSailSpeed
		{
			transform.position += transform.forward * halfSailSpeed * Time.deltaTime; //move the ship in the direction of forward at halfSailSpeed
		}
		
		if(fullSail == true) //if fullSail (mode), go fullSailSpeed
		{
			transform.position += transform.forward * fullSailSpeed * Time.deltaTime; //move the ship in the direction of forward at fullSailSpeed
		}
	}
}
