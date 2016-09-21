using UnityEngine;
using System.Collections;

[System.Serializable]
public class Armor : Item {

	public int defense = 0;

	public void setItem(Armor obj)
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
		defense = obj.defense;
	}
}
