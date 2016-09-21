using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Container : Item {
	
	//choose either volume (infinite slots) or slots
	public bool isBagOpen = true;
	public List<ItemObject> itemsInBag = new List<ItemObject>();
	public int holdVolume = 0;

	public void setItem(Container obj)
	{
		name = obj.name;
		description = obj.description;
		cost = obj.cost;
		icon = obj.icon;
		volume = obj.volume;
		weight = obj.weight;
		id = obj.id;
		maxDurability = obj.maxDurability;
		curDurability = obj.curDurability;
		equipType = obj.equipType;
		isBagOpen = obj.isBagOpen;
		holdVolume = obj.holdVolume;
	}
}
