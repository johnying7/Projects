using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {
	
	int idleStance = 0;
	int jump = 1;
	int shoot = 2;
	int swing = 3;
	int walk = 4;
	int drawWeapons = 5;
	
	AnimationClip idleStanceAnimation;
	AnimationClip jumpAnimation;
	AnimationClip shootAnimation;
	AnimationClip swingAnimation;
	AnimationClip walkAnimation;
	AnimationClip drawWeaponsAnimation;
	
	public int currentState = 0;
	
	Animation charAnimation;
	public bool finishedAnimation;
	
	GameObject mainCharacter;
	
	// Use this for initialization
	void Start () {
		mainCharacter = GameObject.FindGameObjectWithTag("Player");
		charAnimation = GetComponent<Animation>();
		if (charAnimation == false)
			print("animation isn't loaded");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKey == false)
		{
			currentState = idleStance;
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			steerHelm steerShip = mainCharacter.GetComponent<steerHelm>();
			if (steerShip.shipAttached == false)
			{
				currentState = walk;
			}
		}
		else if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			currentState = shoot;
			audio.Play();
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			currentState = jump;
		}
		
		if(currentState == idleStance)
		{
			charAnimation.CrossFade("idle_stance");
		}
		else if(currentState == walk)
		{
			charAnimation.CrossFade("walk");
		}
		else if(currentState == shoot)
		{
			charAnimation.Play("shoot");
		}
		else if (currentState == jump)
		{
			charAnimation.CrossFade("jump");
		}
		else if (currentState == swing)
		{
			charAnimation.Play("swing");
		}
	}
}
