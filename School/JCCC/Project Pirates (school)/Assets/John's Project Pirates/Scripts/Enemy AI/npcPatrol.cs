using UnityEngine;
using System.Collections;

public class npcPatrol : MonoBehaviour {
	
	Vector3 targetDirection;
	float turnStrength = .1f;
	GameObject [] path;
	public int nextPoint = 0;
	public int speed = 2;
	// Use this for initialization
	void Start () {
	
		nextPoint = 0;
		path = new GameObject[3];
		path[0] = GameObject.Find("point1");
		path[1] = GameObject.Find("point2");
		path[2] = GameObject.Find("point3");
	}
	
	// Update is called once per frame
	void Update () {
		if((this.transform.position - path[nextPoint].transform.position).magnitude <= 5)
		{
			if(nextPoint == 2)
			{
				nextPoint = 0;
			}
			else
			{
				nextPoint++;
			}
		}
		//targetDirection = positionA - positionB;
		targetDirection = this.transform.position - path[nextPoint].transform.position;
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(-targetDirection), turnStrength);
		this.transform.position += this.transform.forward.normalized * speed * Time.deltaTime;
		
	}
	
	
	 
}
