using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider obj)
	{
		if(obj.transform.tag == "Enemy")
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().gameContaminationFailure();
		}
	}

	// Update is called once per frame
	void Update () {

	}


}
