using UnityEngine;
using System.Collections;

public class constants : MonoBehaviour {
	
	public static float enemyShipAICannonRange = 300f;
	public static float enemyShipAICirclingRadius = 200f;
	public static Hashtable enemyShipAIStates = new Hashtable
	{
		{"patrolling", 0}, //for when player is not near
		{"circling", 1},   //when in a battle with the player, move in a circle and fire as possible on the player's position when faced towards them
		{"sinking", 2}
	};
	
	// Use this for initialization

	
	public static string getAIStateNameByVal(int stateVal)
	{
		string [] arr = new string[enemyShipAIStates.Count]; //get create string array to store string values (i.e. the state names)	
		constants.enemyShipAIStates.Keys.CopyTo(arr, 0); //copy contents of the Hashtable into the string array
		return arr[stateVal]; //since the array and hashtable are both 0 based, the string value we want to return, is the same as the index value passed in
	}
	
	public static Hashtable npcAIStates = new Hashtable
	{
		{"walking", 0}, //for when player is not near
		{"turning", 1},   //when in a battle with the player, move in a circle and fire as possible on the player's position when faced towards them
		{"idle", 2}
	};
	
	public static string getNPCStateNameByVal(int stateVal)
	{
		string [] arr = new string[npcAIStates.Count]; //get create string array to store string values (i.e. the state names)	
		constants.npcAIStates.Keys.CopyTo(arr, 0); //copy contents of the Hashtable into the string array
		return arr[stateVal]; //since the array and hashtable are both 0 based, the string value we want to return, is the same as the index value passed in
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
