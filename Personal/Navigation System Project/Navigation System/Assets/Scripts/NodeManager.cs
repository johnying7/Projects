using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager: MonoBehaviour {

	//List<Transform> searchList = new List<Transform>();

//    public int townRow = 0;
//    public int townColumn = 0;
//    public int townRowAndColumn = 2;


	//using nodeMasterL

	/// <summary>
	/// array vs list:
	/// 
	/// Array....
	/// array's will search MUCH faster during driving search time which is more desireable
	/// 
	/// List....
	/// lists will be more flexible to update and modify as well as add new roads, intersections,
	/// etc. for anything new. while the time for list is painful, the customer
	/// preference is to have longer update, shorter pathfinding so going with arrays.
	/// </summary>





	//public Transform[,,,] nodeController = new Transform[10,10,2,2];//a 4D array that holds transform nodes
	




    //nodeController parameters (group row, group column, node row, node column)

	//node creator is a button that creates nodes and fills them up given specified instructions
	//nodes are emptygameobjects that simply hold position
	//node needs to also have connecting nodes (gameobjects) that it can travel to
	//node connection also must have a specified path as well as distance

	//create a list of all nodes to connecting goal
	//create a list of nodes that have been emptied
	//create a list of connecting nodes not yet searched through

	//begin with a basic nodeGroup which has a list of nodes

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
