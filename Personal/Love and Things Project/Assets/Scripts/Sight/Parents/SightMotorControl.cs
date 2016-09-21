using UnityEngine;
using System.Collections;

public class SightMotorControl : SightCrosshair {

	//The mam stands for Mouse And Motor.

	protected bool motorControl;
	protected bool mouseVisible;
	
	//[HideInInspector]
	public GameObject playerCharacter;
	
	void Start() {
	}
	
	public void mamToggle()
	{
		toggleMouse();
		toggleMotorControl();
	}
		
	public void mamAimMode() //aiming mode
	{
		whiteSight();
		mouseOff();
		motorOn();
	}
	
	public void mamGuiMode() //gui mode
	{
		noSight();
		mouseOn();
		motorOff();
	}

	public void toggleMotorControl()
	{
		if(motorControl)
			motorOff();
		else if(!motorControl)
			motorOn();
	}
	
	public void toggleMouse()
	{
		if(mouseVisible) //make the mouse invisible
		{
			mouseOff();
			whiteSight();
		}
		else if(!mouseVisible) //make the mouse visible
		{
			mouseOn();
			noSight();
		}
	}
	
	public void motorOff() //changes camera turn off
	{
		MouseLook mouseLook = playerCharacter.GetComponent<MouseLook>(); //get mouselook script in mainCharacter
		mouseLook.enabled = false;
		
		MouseLook charLook = GetComponent<MouseLook>(); //get mouselook script in characterCamera
		charLook.enabled = false;
		
		CharacterMotor motor = playerCharacter.GetComponent<CharacterMotor>(); //get charactermotor script in mainCharacter
		motor.canControl = false;
		//motor.enabled = false;
		
		motorControl = false;
	}
	
	public void motorOn() //changes camera turn on
	{
		MouseLook mouseLook = playerCharacter.GetComponent<MouseLook>();
		mouseLook.enabled = true;
		
		MouseLook charLook = GetComponent<MouseLook>();
		charLook.enabled = true;
		
		CharacterMotor motor = playerCharacter.GetComponent<CharacterMotor>();
		motor.canControl = true;
		//motor.enabled = true;
		
		motorControl = true;
	}
	
	public void mouseOff() //hides the mouse
	{
		Screen.lockCursor = true;
		
		Cursor.visible = false;
		mouseVisible = false;
	}
	
	public void mouseOn() //shows the mouse
	{
		Screen.lockCursor = false;
		
		Cursor.visible = true;
		mouseVisible = true;
	}
}
