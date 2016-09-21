using UnityEngine;
using System.Collections;

public class HitPlayer : MonoBehaviour {

	float hitCooldown;
	AudioSource aS;

	// Use this for initialization
	void Start () {
		hitCooldown =  0.0f;
		aS = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider obj)
	{
		if(obj.GetComponent<Collider>().tag == "Player" && hitCooldown < Time.time)
		{
			hitCooldown = Time.time + 0.5f;
			obj.transform.GetComponent<PlayerHealth>().curHealth -= 10;
			aS.Play();
		}

		if(this.transform.parent.GetComponent<NpcState>().isCharging && obj.GetComponent<Collider>().tag == "Wall")
		{
			hitCooldown = Time.time + 2.0f;
			obj.transform.GetComponent<BuildingHealth>().damageBuilding();
			aS.Play();
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
