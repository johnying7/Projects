using UnityEngine;
using System.Collections;

public class enemyWalksHome : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.parent);
		transform.position += transform.forward * 5.0f * Time.deltaTime;
	}
	
	void OnTriggerEnter (Collider home)
	{
		if (home.gameObject.transform.parent == transform.parent)
		{
			this.enabled = false;
		}
	}
}
