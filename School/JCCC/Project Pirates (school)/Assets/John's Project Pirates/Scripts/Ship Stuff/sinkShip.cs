using UnityEngine;
using System.Collections;

public class sinkShip : MonoBehaviour {
	
	public bool shipIsSunk;
	public Transform shipStandPosition; //references the empty object of child of the ship
	public Transform ship;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnCollisionEnter (Collision col) //if attached object collides with a rigidbody object
	{
		if(col.gameObject.tag == "Land") //if collided object has a tag named theGround
		{
			crashTheShip();
		}
	}
	
	public void crashTheShip()
	{
		Debug.Log ("You have crashed!");
		gameObject.rigidbody.useGravity = true; //make the attached object use gravity (boat to sink) in water
		gameObject.rigidbody.isKinematic = false;
		shipIsSunk = true;
		driveShip helm = ship.GetComponent<driveShip>();
		helm.enabled = false; //disable movement of the script (in turn, the ship)
		audio.Play(1);
	}
}
