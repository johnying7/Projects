using UnityEngine;
using System.Collections;

[System.Serializable]
public class Vector3D {

	public float x;
	public float y;
	public float z;

	public Vector3D()
	{
		x = 0.0f;
		y = 0.0f;
		z = 0.0f;
	}

	public Vector3D(float xVal, float yVal, float zVal)
	{
		x = xVal;
		y = yVal;
		z = zVal;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
