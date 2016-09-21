using UnityEngine;
using System.Collections;

public class ItemCollection : MonoBehaviour {

	int logsNeeded = 22;
	int shovelsNeeded = 20;
	int woodaxesNeeded = 20;
	int ironOreNeeded = 22;

	int curLogs = 0;
	int curShovels = 0;
	int curWoodaxes = 0;
	int curIronOres = 0;
	// Use this for initialization

	public WallHolder wH;

	float logBuildCooldown = 0.0f;
	int logWallCount = 0;
	float incrementTime = 60.0f;

	public GameManager gameManager;
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(logBuildCooldown < Time.time && curLogs > logWallCount)
		{
			wH.wallList[logWallCount].gameObject.SetActive(true);
			logWallCount += 1;
			logBuildCooldown = Time.time + incrementTime;
		}
	}

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width-140,Screen.height-100, 140, 100),"");
		GUI.Label(new Rect(Screen.width-140, Screen.height-100, 140,24),"Logs Needed: " + curLogs + "/" + logsNeeded);
		GUI.Label(new Rect(Screen.width-140, Screen.height-100+24, 140,24), "Shovels Needed: " + curShovels + "/" + shovelsNeeded);
		GUI.Label(new Rect(Screen.width-140, Screen.height-100+48, 140,24), "Axes Needed: " + curWoodaxes + "/" + woodaxesNeeded);
		GUI.Label(new Rect(Screen.width-140, Screen.height-100+48+24, 140,24), "Ores Needed: " + curIronOres + "/" + ironOreNeeded);
	}

	void OnTriggerEnter(Collider obj)
	{
		if(obj.transform.parent.name == "Log")
		{
			Destroy(obj.gameObject);
			curLogs += 1;//increase log count by one
			winCheck();
		}
		if(obj.transform.parent.name == "Iron Shovel")
		{
			Destroy(obj.gameObject);
			curShovels += 1;//increase pickaxe count
			winCheck();
		}
		if(obj.transform.parent.name == "Iron Woodaxe")
		{
			Destroy(obj.gameObject);
			curWoodaxes += 1;//increase woodaxe count
			winCheck();
		}
		if(obj.transform.parent.name == "Iron Ore")
		{
			Destroy(obj.gameObject);
			incrementTime -= 1;
			curIronOres += 1;//increase iron ore count
			winCheck();
		}
	}

	void winCheck()
	{
		if(logsNeeded == curLogs &&
		   shovelsNeeded == curShovels &&
		   woodaxesNeeded == curWoodaxes &&
		   ironOreNeeded == curIronOres)
		{
			gameManager.gameSuccess();
		}
	}
}
