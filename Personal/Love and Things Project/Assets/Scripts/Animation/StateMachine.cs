using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

	private Animator anim;
	int swingChargeState = Animator.StringToHash("UpperBody.SwingCharge");
	int swingThrowState = Animator.StringToHash("UpperBody.ThrowSwing");

	public Transform MainCamera;

	public int swingStage;
	public AudioSource swordSwingAudio;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(MainCamera.GetComponent<Inventory2>().isInventoryOn == false)
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			anim.SetFloat("Speed", v);
			anim.SetFloat("Direction", h);

			//changes the actual swing animation of the characer
			if(Input.GetKey(KeyCode.Mouse0) && MainCamera.GetComponent<Inventory2>().curWeapon.name == "Iron Sword")
			{
				anim.SetInteger("SwingStage", 1);
			}
			else
				anim.SetInteger("SwingStage", 0);

			int oldSwingState = swingStage;
			if(anim.GetInteger("SwingStage") == 0 && Input.GetKeyUp(KeyCode.Mouse0) && MainCamera.GetComponent<Inventory2>().curWeapon.name == "Iron Sword")
			{
				swordSwingAudio.Play();//playSwordSwing sound
			}

			//used by NpcHealth.cs
			//use if(swingStage == 1) to determine time for hit collision on enemies (aka character is swinging down)
			if(anim.GetCurrentAnimatorStateInfo(1).nameHash == swingThrowState ||
			   anim.GetCurrentAnimatorStateInfo(1).nameHash == swingChargeState)
				swingStage = 1;
			else
				swingStage = 0;
		}
	}
}
