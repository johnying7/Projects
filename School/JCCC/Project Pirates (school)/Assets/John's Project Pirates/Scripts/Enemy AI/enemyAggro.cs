using UnityEngine;
using System.Collections;

public class enemyAggro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GetComponentInChildren<enemyAI>().enabled = true;
			GetComponentInChildren<enemyWalksHome>().enabled = false;
		}
	}
	
	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			GetComponentInChildren<enemyAI>().enabled = false;
			GetComponentInChildren<enemyWalksHome>().enabled = true;
		}
	}	
}
