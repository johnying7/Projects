using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyShipScript : MonoBehaviour {
	
	public int health = 100;
	public GameObject currentPathNode;
	float shipTurnSpeed = 20f;
	float maxTurnSpeed = 40f;
	float shipMoveSpeed = 40f;
	int aiState;
	Vector3 direction;
	Quaternion rotation;
	GameObject [] pathNodes;
	List<cannonScript> leftCannons;
	List<cannonScript> rightCannons;	
	private GameObject playerShip;		
	private Transform myTransform;
	private Vector3 circlingCenter;
	private Transform leftMidCannonTransform;
	private Transform rightMidCannonTransform;
	private float maxFireDelay = 0.5f; //maximum amount of time to wait before a cannon fires
	// Use this for initialization
	void Start () 
	{		
		//collect all the pathNodes in the game's scene into an array
		pathNodes = GameObject.FindGameObjectsWithTag("pathNode");
		//save the transform
		myTransform = this.transform;	
		
		//find the player's ship.
		playerShip = GameObject.FindGameObjectWithTag("PlayerShip");		
		//set the aiState
		aiState = (int) constants.enemyShipAIStates["patrolling"];			
		changedState();
		
		//get references to middle cannons on this ship (both left and right) for use later.  Also save cannons for each side in array
		leftCannons = new List<cannonScript>();
		rightCannons = new List<cannonScript>();
		
	 	cannonScript [] cannonScripts = gameObject.GetComponentsInChildren<cannonScript>();
		foreach(cannonScript cScript in cannonScripts)
		{
			if(cScript.cannonSide.Contains("left"))
			{
				if(cScript.cannonSide == "left_middle")
				{
					//save this as the left mid cannon
					leftMidCannonTransform = cScript.gameObject.transform;
				}
				leftCannons.Add(cScript);			
			}
			else
			{
				
				if (cScript.cannonSide == "right_middle")
				{
					rightMidCannonTransform = cScript.gameObject.transform;				
				}
				rightCannons.Add(cScript);
			}
		}
	}
	
	void setDirectionToPathNode()
	{
		direction = Vector3.Normalize(currentPathNode.transform.position - myTransform.position);
		rotation = Quaternion.LookRotation(direction);
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,rotation,Time.deltaTime * shipTurnSpeed);//interp to location of interest	
	}
		
	void Update () 
	{
		//state logic.
		if(aiState == (int)constants.enemyShipAIStates["patrolling"])
		{		
			setDirectionToPathNode();
			myTransform.Translate(direction * shipMoveSpeed * Time.deltaTime,Space.World);
		}
		else if(aiState == (int)constants.enemyShipAIStates["circling"])
		{		
			//setDirectionToPathNode();			
			myTransform.Rotate(new Vector3(0,1,0), shipTurnSpeed * Time.deltaTime);
			myTransform.Translate(myTransform.forward * shipMoveSpeed * Time.deltaTime, Space.World);
			//check to see if we're at an angle where we could fire on the player			
			float angleToPlayer = Vector3.Angle(myTransform.forward, (playerShip.transform.position-myTransform.position));
			//print(myTransform.forward);			
			if(angleToPlayer >= 88 && angleToPlayer < 95)
			{
				shipTurnSpeed *= 0.25f;
				//we're at an angle to fire on the player. Check which side's middle cannon is closer to player and try to fire that side's cannons
				if( Vector3.Distance(rightMidCannonTransform.position,playerShip.transform.position) < Vector3.Distance(leftMidCannonTransform.position, playerShip.transform.position))
				{
					//right side is closer					
					fireCannons("right");
				}
				else
				{
					//left side is closer					
					fireCannons("left");
				}						
			}
			else
			{
				if(shipTurnSpeed != maxTurnSpeed)
					shipTurnSpeed = maxTurnSpeed;
			}
		}				
		
		//determine if the player is nearby		
		if(Vector3.Distance(myTransform.position, playerShip.transform.position) < constants.enemyShipAICannonRange)
		{
			if(aiState != (int)constants.enemyShipAIStates["circling"])
			{
				//if the player is in range, let's change state to circling so we can try to get a clean shot on them.	
				print("player in range - changing to circling!");
				aiState = (int)constants.enemyShipAIStates["circling"];
				changedState();
			}
		}
		else
		{
			//if we're not patrolling let's set us to do so
			if(aiState != (int)constants.enemyShipAIStates["patrolling"])
			{
				aiState = (int)constants.enemyShipAIStates["patrolling"];
				changedState();
			}
		}
	}
	
	void FixedUpdate()
	{
	
	}
	
	void OnCollisionEnter(Collision col)
	{
		
	}
	
	void OnTriggerEnter(Collider col)
	{				
		if(col.gameObject.tag == "pathNode" ) //&& aiState == (int)constants.enemyShipAIStates["patrolling"])
		{			
			pathNodeShip nodeScript = col.GetComponent<pathNodeShip>();			
			//get number of nodes in the list	
			if(nodeScript.nodeList.Count > 1)
			{
				int randomIndex = Random.Range(0, nodeScript.nodeList.Count);			
				currentPathNode = (GameObject)nodeScript.nodeList[randomIndex];
			}
			else // if there's just one node connected to this one, no need to randomize
			{
				currentPathNode = (GameObject)nodeScript.nodeList[0];
			}
			setDirectionToPathNode();
		}		
	}
	
	void changedState()
	{
		//do any state-specific logic because we just changed states.	
		//print("*AI* changing ai state - now " + constants.getAIStateNameByVal(aiState));
		if(aiState == (int)constants.enemyShipAIStates["patrolling"])
		{
			currentPathNode = getNearestPathNode();
		}
		else if(aiState == (int)constants.enemyShipAIStates["circling"])
		{			
			//currently nothing special to do here
		}
	}
	
	public void takeDamage(int amountOfDamage)
	{
		health -= amountOfDamage;
		checkDestroyed();		
	}
	
	void checkDestroyed()
	{
		print("health? " + health.ToString());
		if(health < 1)
		{
			//turn gravity on and let this baby sink
			Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
			rb.rigidbody.rigidbody.useGravity = true;
			aiState = (int) constants.enemyShipAIStates["sinking"];
			changedState();
			//destroy the ship after a few seconds to ensure it had enough time to sink below water.
			Destroy(this.gameObject,4);
		}
	}
	
	void fireCannons(string sideToFire)
	{
		//don't fire if we're sinking.
		if(aiState == (int)constants.enemyShipAIStates["sinking"])
			return;
		
		
		if(sideToFire == "right") // fire right side cannons...
		{			
			foreach(cannonScript cScript in rightCannons)
			{													
				cScript.waitRandomTimeToFire(maxFireDelay);
			}
		}
		else // fire left side
		{
			foreach(cannonScript cScript in leftCannons)
			{			
				cScript.waitRandomTimeToFire(maxFireDelay);
			}		
		}
	}
	
	GameObject getNearestPathNode() //find the closest pathNode, set it as the current one to change ship's direction
	{			
		//assume the closest node is the first one up front and then skip 0 in the loop
		float closestDist = Vector3.Distance(myTransform.position,pathNodes[0].transform.position);
		float dist;
		GameObject closestNode = pathNodes[0];	
		//print("pathnodes count = " + pathNodes.Length);
		//print("Assuming closest node is - " + pathNodes[0].name + " distance = " + closestDist);
		for(int i = 1; i < pathNodes.Length;i++)
		{
			dist = Vector3.Distance(myTransform.position,pathNodes[i].transform.position);
			if(dist < closestDist)
			{				
				//we have a new closest node
				//print("found a closer node - " + pathNodes[i].name + " that's dist = " + dist);
				closestDist = dist;
				closestNode = pathNodes[i];
			}
		}
		//should have the closest node now.
		return closestNode;
	}
		
}
