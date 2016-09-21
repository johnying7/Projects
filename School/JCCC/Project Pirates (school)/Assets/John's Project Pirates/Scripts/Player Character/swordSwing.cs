using UnityEngine;
using System.Collections;

public class swordSwing : MonoBehaviour {
	
	public GameObject mainCharacter; //references the object of the first person controller
	public GameObject playerBody;
	
	// Use this for initialization
	void Start () {
		mainCharacter = GameObject.FindGameObjectWithTag("Player"); //finds the player in the game by tag
		playerBody = GameObject.FindGameObjectWithTag("PlayerMesh");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Mouse1)) //if the right mouse button is pressed
		{
			steerHelm helm = mainCharacter.GetComponent<steerHelm>();//grab the boat driving script
			if (helm.shipAttached == false) //if the player isn't steering the boat
			{
				playerAnimation animate = playerBody.GetComponent<playerAnimation>();
				
				if (animate.finishedAnimation == true)
				{
					
				}
				
				audio.Play();
				animate.currentState = 3;
			}
		}
	}//end update function
}//end class function
