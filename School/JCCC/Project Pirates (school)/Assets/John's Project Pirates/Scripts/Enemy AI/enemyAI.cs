using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {
	
	Transform mainCharacterPosition;
	GameObject mainCharacter;
	GameObject bony;
	public bool playerIsHit;
	public bool dazed;	
	// Use this for initialization
	void Start () {
		mainCharacter = GameObject.FindGameObjectWithTag("Player"); //finds the player in the game by tag
		mainCharacterPosition = mainCharacter.transform;
		playerIsHit = false;
		dazed = false;
		bony = GameObject.FindGameObjectWithTag("PlayerMesh");
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIsHit == true)
		{
			StartCoroutine(charge());
		}
		else if (playerIsHit == false && dazed == false)
		{
			transform.LookAt(mainCharacterPosition);
			transform.position += transform.forward * 8.0f * Time.deltaTime;
		}
	}
	
	void OnTriggerEnter (Collider sword) //if attached object collides with a rigidbody object
	{
		if(sword.gameObject.tag == "PlayerSword") //if collided object has a tag named sword
		{
			if(bony.GetComponent<Animation>().IsPlaying("swing") == true)
			{
				print ("the box is taking damage");
				takeDamage ();
			}
		}
	}
	
	public void takeDamage()
	{
		GetComponent<enemyHealth>().hp--;
		knockback();
		audio.Play();
	}
	
	void OnCollisionEnter (Collision col) //if attached object collides with a rigidbody object
	{
		if(col.gameObject.tag == "Player") //if collided object has a tag named Player
		{
			//player takes damage (needs to be coded)
			knockback();
			mainCharacterPosition.position += transform.forward * 2.0f;
			//Debug.Log ("You've been hit!");
			
		}
	}
	
	private IEnumerator charge(){
		playerIsHit = false;
		dazed = true;
		//Debug.Log("The enemy gets ready to charge!");
		yield return new WaitForSeconds (3.0f);
		//print ("Enemy has charged.");
		dazed = false;
		
	}
	
	public void knockback()
	{
		playerIsHit = true;
		transform.position += mainCharacterPosition.forward * 2.0f;
	}
}
