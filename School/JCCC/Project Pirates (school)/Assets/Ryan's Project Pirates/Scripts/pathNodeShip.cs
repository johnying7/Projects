using UnityEngine;
using System.Collections;

public class pathNodeShip : MonoBehaviour {
	
	public GameObject node1;
	public GameObject node2;
	public GameObject node3;
	public ArrayList nodeList = new ArrayList();

	// Use this for initialization
	void Start () 
	{
		//Temporary until we update editor inspector functionality to handle lists
		if(node1 != null)
			nodeList.Add(node1);
		if(node2 != null)
			nodeList.Add(node2);
		if(node3 != null)
			nodeList.Add(node3);			
	}
	
	// Update is called once per frame
	void Update () {
				
	}
	
	void OnDrawGizmos()
	{
		//Temporary until we update editor inspector functionality to handle lists
		nodeList.Clear();
		if(node1 != null)
			nodeList.Add(node1);
		if(node2 != null)
			nodeList.Add(node2);
		if(node3 != null)
			nodeList.Add(node3);
		
		
		 // Draw a yellow sphere at the transform's position
	    Gizmos.color = Color.yellow;
	    Gizmos.DrawSphere (transform.position, 3);		
		foreach(GameObject node in nodeList)
		{					
			Gizmos.color = Color.red;
			Gizmos.DrawLine(this.transform.position, node.transform.position);
		}		
	}	
}
