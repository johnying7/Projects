using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour {
	
	public int hp; //hitpoints
	
	// Use this for initialization
	void Start () {
		hp = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0) //if the enemy character has zero or less hp
		{
			Destroy(this.gameObject); //destroy the enemy character
		}
	}
	
	void OnTriggerEnter (Collider sword) //if attached object collides with a rigidbody object
	{
		if(sword.gameObject.tag == "PlayerSword") //if collided object has a tag named sword
		{

		}
	}
}
