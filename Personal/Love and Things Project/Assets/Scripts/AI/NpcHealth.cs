using UnityEngine;
using System.Collections;

public class NpcHealth : MonoBehaviour {

	public int health = 100;

	float hitCooldown = 0;

	AudioSource audioSource;
	public AudioClip hurt;
	public AudioClip death;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider hitObject)
	{
		if(hitObject.transform.name == "Sword Trigger" && hitCooldown < Time.time &&
		   hitObject.transform.GetComponent<WeaponCollision>().playerCharacter.GetComponent<StateMachine>().swingStage == 1)
		{
			hitCooldown = Time.time + 1.0f;
			health -= 36;
			if(health <= 0)
			{
				AudioSource.PlayClipAtPoint(death, this.transform.position);
				Destroy(this.transform.parent.gameObject);
			}
			else
			{
				audioSource.clip = hurt;
				audioSource.Play();
			}
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
