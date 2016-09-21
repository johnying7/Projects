using UnityEngine;
using System.Collections;

public class DropItemCheck : MonoBehaviour {
	
	public int hitCount; //counts the current number of objects in the way for item dropping
	
	// Use this for initialization
	void Start () {
		hitCount = 0;
		Physics.IgnoreLayerCollision(8,8,true);
	}
	
	void OnTriggerEnter(Collider hit) {
		if(hit.gameObject.tag != "Enemy")
		{
			hitCount++;
			Debug.Log("Increase drop: " + hit.gameObject.name);
		}
	}
	
	void OnTriggerExit(Collider hit) {
		hitCount--;
		if(hitCount < 0)
			hitCount = 0;
	}
}
