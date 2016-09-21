using UnityEngine;
using System.Collections;

public class BuildingHealth : MonoBehaviour {

	public int curHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void damageBuilding()
	{
		curHealth -= 5;
		if(curHealth <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}
