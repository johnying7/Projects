using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcState : MonoBehaviour {
	
	Vector3 targetDirection;
	float turnStrength = .05f;
	public float speed = 0.5f;
	
	public bool isCharging;
	public Vector3 moveDestination;
	public Transform playerDestination;
	public Transform chargeDestination;
	float nextSetTime = 0.0f;
	CharacterController controller;
	public Animator anim;

	// Use this for initialization
	void Start () {
		playerDestination = null;
		controller = GetComponent<CharacterController>();
		anim.SetInteger("isMoving", 1);
		chargeDestination = GameObject.Find("ZombieChargePosition").transform;
	}

	void OnTriggerEnter(Collider objEnter)
	{
		if(objEnter.gameObject.tag == "Player")
		{
			playerDestination = objEnter.transform;
			GetComponent<AudioSource>().Play();
		}
	}
	
	void OnTriggerExit(Collider objExit)
	{
		if(objExit.gameObject.tag == "Player")
			playerDestination = null;
	}

	// Update is called once per frame
	void Update () {
		if(nextSetTime <= Time.time) //if timer has expired
			Patrol();

		if(playerDestination != null)
			Destination(speed,playerDestination.position);
		else if(isCharging)
			Destination(speed,chargeDestination.position);
		else
			Destination(speed, moveDestination);
	}

	void Patrol()
	{ 
		nextSetTime = Time.time + 10.0f;
		//wander range is 50,50 to 460,460
		//move to the next point
		Vector3 temp;
		temp.x = Random.Range(50, 460);
		temp.y = 0;
		temp.z = Random.Range(50, 460);

		moveDestination = temp;

	}
	
	private void Destination(float speed, Vector3 targetPosition)
	{
		targetDirection = targetPosition - this.transform.position; //find next direction based on given point
		targetDirection.y = 0;
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(targetDirection), turnStrength); //face the next point over time
		targetDirection = transform.TransformDirection(Vector3.forward);
		targetDirection.y -= 9.8f * Time.deltaTime;
		controller.SimpleMove(targetDirection.normalized * speed);
	}
}
