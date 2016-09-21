using UnityEngine;
using System.Collections;

public class TreeObject : MonoBehaviour {

	int treeHP = 100; //when chopped down to 0 or below, remove kinematic and fell the tree
	int maxHP = 100; //calculates the life of the (felled) tree in decay
	public int stackAmount = 4; //removed one by one when treeHP reaches <= 0

	// Use this for initialization
	void Start () {
		//grab serialized stats otherwise initialize
	}
	
	public bool chop(int damage) //returns true if log needs to be instantiated
	{
		if(treeHP <= 0)
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
		else
		{
			if((treeHP-damage) <= 0)
			{
				this.GetComponent<Rigidbody>().isKinematic = false;
				//startDecay();
			}
			treeHP -= damage;
			return false;
		}
	}
}
