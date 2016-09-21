using UnityEngine;
using System.Collections;

public class DoorOpening : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetButton("Fire1"))&&(renderer.isVisible)){
			Destroy(gameObject);	
		}
	}
	

}
