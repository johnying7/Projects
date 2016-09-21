using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour {

	bool attackAnimation = false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			GetComponent<Animation>().Play("SwordSwing");
			attackAnimation = true;
		}	
	}

	public bool playerIsAttacking()
	{
		if(GetComponent<Animation>().isPlaying && attackAnimation == true)
		{
			attackAnimation = false;
			return true;
		}
		else
			return false;
	}
}
