using UnityEngine;
using System.Collections;

[System.Serializable]
public class Consumable : Item {

	public int stack = 1;

	public void setItem(Consumable obj)
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
		stack = obj.stack;
	}
}
