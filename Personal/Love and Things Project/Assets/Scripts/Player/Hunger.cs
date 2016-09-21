using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour { //handles all hunger operations
	
	public int curHunger;
	private int maxHunger;
	private float timer; //total time before count resets in seconds
	private float count; //counts time in seconds
	
	public float hungerBarLength;
	public Texture2D hungerTexture;
	private float percentage;

	// Use this for initialization
	void Start () {
		maxHunger = 100;
		count = 0;
		timer = 18;
		hungerBarLength = Screen.width / 2;
		saveCheck();
	}
	
	/// <summary>
	/// Note: Based on initial timer (18 seconds), hunger reaches zero from 100 after approx 30 minutes (1800 seconds)
	/// </summary>/
	
	// Update is called once per frame
	void Update () {
		if (count < timer)
		{
			count += 1.0f * Time.deltaTime; //increase count time by one second
		}
		else if (count >= timer)
		{
			count = 0.0f;
			adjustHunger (-1);
		}
	}
	
	void saveCheck()
	{
		//if hunger in the save file are different, swap to that
		//curHunger = savefile;
		
		//else use the below code to initialize curHunger
		curHunger = 100;
	}
	
	public void adjustHunger(int amount) //adjust the hunger meter from amount
	{
		curHunger += amount;
		
		if (curHunger <= 0) //if player is starving
		{
			curHunger = 0; //resets hunger to zero (in cases of negative health)
			PlayerHealth hp = GetComponent<PlayerHealth>();
			hp.takeDamage(1);
		}
		
		if (curHunger > maxHunger) //if hunger is over the max
		{
			curHunger = maxHunger; //set current hunger to max
		}
	}
	
	void OnGUI(){
		//GUI.Box(new Rect(10, 40, hungerBarLength, 20), curHunger + "/" + maxHunger);
		percentage = (float)curHunger/(float)maxHunger;
		GUI.Box(new Rect(10,40,256,16),"");
		GUI.DrawTexture(new Rect(10, 40, percentage*256,16),hungerTexture);
	}
}
