using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour { //holds the health points for the player
	
	public int curHealth;
	private int maxHealth;
	
	public float healthBarLength;
	public Texture2D healthTexture;
	private float percentage;
	float nextHealTime;

	// Use this for initialization
	void Start () {
		maxHealth = 100;
		healthBarLength = Screen.width / 2;
		curHealth = maxHealth;
	}
	
	void Update()
	{
		if(curHealth <= 0)
		{
			GameObject.Find("GameManager").GetComponent<GameManager>().gameDeathFailure();//load end death scene
		}
		else if(curHealth < maxHealth && nextHealTime < Time.time && GetComponent<Hunger>().curHunger > 0)
		{
			nextHealTime = Time.time + 10.0f;
			curHealth +=1;
		}
	}
	
	void OnGUI(){
		//GUI.Box(new Rect(10, 10, healthBarLength * (curHealth / (float)maxHealth), 20), curHealth + "/" + maxHealth);
		percentage = (float)curHealth/(float)maxHealth;
		GUI.Box(new Rect(10, 10,256,16),"");
		GUI.DrawTexture(new Rect(10, 10, percentage*256,16),healthTexture);
	}
	
	public void takeDamage(int damage)
	{
		curHealth -= damage;
	}
}
