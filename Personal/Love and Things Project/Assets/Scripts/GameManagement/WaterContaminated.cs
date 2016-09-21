using UnityEngine;
using System.Collections;

public class WaterContaminated : MonoBehaviour {

	GameManager gm;
	void Start(){
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider msg)
	{
		if(msg.gameObject.name == "HitCollision")
		{
			gm.gameContaminationFailure();
		}
	}
}
