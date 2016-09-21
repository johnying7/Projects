using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Workbench : MonoBehaviour {

	public Transform workbenchView;
	public Transform dropCraft;
	public bool isUsingWorkbench;

	public List<GameObject> craftList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		isUsingWorkbench = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void addRemove(GameObject item)
	{
		if(craftList.Count == 0)
		{
			craftList.Add(item);
		}
		else
		{
			if(isItemInList(item))
			{
				for(int i = 0; i < craftList.Count; i++)
				{
					if(item == craftList[i])
						craftList.RemoveAt(i);
				}
			}
			else
				craftList.Add(item);
		}
	}

	public string assemble() //returns the assembled gameobject name to resource load
	{
		string spawnItem = "";
		if(craftList[0].name == "Wooden Shaft")
		{
			Transform handlePoint = craftList[0].transform.Find("Assemble Point");
			if(craftList[1].name == "Shovel Head")
			{
				if(0.08f >= Vector3.Distance(handlePoint.position,craftList[1].transform.position))
					spawnItem = "Iron Shovel";
			}
			else if(craftList[1].name == "Axe Head")
			{
				if(0.08f >= Vector3.Distance(handlePoint.position,craftList[1].transform.position))
					spawnItem = "Iron Woodaxe";
			}
		}
		else if(craftList[1].name == "Wooden Shaft")
		{
				Transform handlePoint = craftList[1].transform.Find("Assemble Point");
				if(craftList[0].name == "Shovel Head")
				{
						if(1.0f >= Vector3.Distance(handlePoint.position,craftList[0].transform.position))
								spawnItem = "Iron Shovel";
				}
				else if(craftList[0].name == "Axe Head")
				{
						if(1.0f >= Vector3.Distance(handlePoint.position,craftList[0].transform.position))
								spawnItem = "Iron Woodaxe";
				}
		}

				print ("really assembled");
		foreach(GameObject piece in craftList)
			Destroy(piece);

		craftList.Clear();

		return spawnItem;
	}

	bool isItemInList(GameObject item)
	{
		for(int i = 0; i < craftList.Count; i++)
		{
			if(item == craftList[i])
				return true;
		}
		return false;
	}

	public bool isBenchLevel() //gets the tile the workbench is standing on
	{
		Vector3 thisCenter;
		thisCenter = this.transform.position;
		thisCenter.y += 1;
		RaycastHit hit;
		if(Physics.Raycast(thisCenter, this.transform.position-thisCenter, out hit))
		{
			if(hit.collider.transform.tag == "Terrain")
			{
				return hit.collider.transform.GetComponent<tileManager>().isTileLevel(); //is the stood on tile level?
			}
		}
		else
		{
			return false;
		}
		return false;
	}
}
