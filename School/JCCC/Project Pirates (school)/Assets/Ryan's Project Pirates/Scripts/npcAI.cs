using UnityEngine;
using System.Collections;

public class npcAI : MonoBehaviour 
{
	int aiState;
	Vector3 direction;
	Quaternion rotation;
	public GameObject pathNode1;
	public GameObject pathNode2;
	public GameObject pathNode3;
	private float walkSpeed = 3.0f;
	private Transform myTransform;
	private GameObject currentPathNode;
	private float npcTurnSpeed = 15.0f;
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		currentPathNode = getNextPathNode();
		aiState = (int) constants.npcAIStates["walking"];
		changedState();//start walking
		pathNode1.transform.parent = null;
		pathNode2.transform.parent = null;
		pathNode3.transform.parent = null;		
	}
	
	void changedState()
	{
		animation.Play ("walk");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(aiState == (int)constants.npcAIStates["walking"])
		{
			//continue to walk toward the current node.			
			setDirectionToPathNode();
			myTransform.Translate(direction * walkSpeed * Time.deltaTime,Space.World);
		}
		else if(aiState == (int)constants.npcAIStates["idle"])
		{
			//stop moving and play idle animation  (not currently in use)
		}
		else if(aiState == (int)constants.npcAIStates["turning"])
		{
			//turn toward next node (not currently in use)
		}
	}
	
	void setDirectionToPathNode()
	{		
		direction = Vector3.Normalize(currentPathNode.transform.position - myTransform.position);
		rotation = Quaternion.LookRotation(direction);		
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,rotation,Time.deltaTime * npcTurnSpeed);//interp to location of interest	
	}
	
	void OnTriggerEnter(Collider col)
	{				
		if(col.gameObject.tag == "pathNodeNPC" )
		{
			currentPathNode = getNextPathNode();
			setDirectionToPathNode();
		}
	}	
	
	GameObject getNextPathNode()
	{
		if(currentPathNode == pathNode1 || currentPathNode == null)
		{
			return pathNode2;
		}
		else if (currentPathNode == pathNode2)
		{
			return pathNode3;
		}
		else
		{
			return pathNode1;
		}
		
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
	    Gizmos.DrawSphere (pathNode1.transform.position, 1);
		Gizmos.DrawSphere (pathNode2.transform.position, 1);
		Gizmos.DrawSphere (pathNode3.transform.position, 1);
	}
}
