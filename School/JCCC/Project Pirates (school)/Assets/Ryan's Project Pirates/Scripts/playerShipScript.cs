using UnityEngine;
using System.Collections;

public class playerShipScript : MonoBehaviour {

	public int health = 100;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
			print ("sink the ship");
			sinkShip sink = GetComponent<sinkShip>();
			sink.crashTheShip();			
		}
	}
}
