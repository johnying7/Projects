using UnityEngine;
using System.Collections;

public class OreObject : MonoBehaviour {
	
	public int stackAmount = 4; //removed one by one when treeHP reaches <= 0
	
	// Use this for initialization
	void Start () {
		//grab serialized stats otherwise initialize
	}
	
	public bool chop() //returns true if log needs to be instantiated
	{
		if(stackAmount <= 1) //if the stack amt is < 1, destroy the tree 
		{
			Destroy (this.gameObject); //destroy should automatically be called AFTER the current update loop, so safe to call before return
			return true; //tell the player to create a log from the tree
		}
		else
		{
			stackAmount -= 1;
			return true;
		}
	}
}
