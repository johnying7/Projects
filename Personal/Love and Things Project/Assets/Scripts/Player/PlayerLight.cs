﻿using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.L))
		{
			this.GetComponent<Light>().enabled = !this.GetComponent<Light>().enabled;
		}
	}
}
