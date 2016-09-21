using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    //intersection of 90 degrees = 10 sec delay
    //one unit is 0.0625 miles or one side of a block

    public Transform[] possiblePaths = new Transform[1]; //possible branching roads to break off into
    public int[] pathSpeeds = new int[1];

    public float final = 0.0f; //final = genPath + heuristic
    public float heuristic = 0.0f; //heuristic for distance from curPoint to destination
    public float genPath = 0.0f; //movement cost from startpoint to curpoint using generated path

    public Transform parentPath;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
