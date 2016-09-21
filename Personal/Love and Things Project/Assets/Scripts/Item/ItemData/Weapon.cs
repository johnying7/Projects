using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon : Item {

	public int damage = 0;

	public void setItem(Weapon obj)
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
		damage = obj.damage;
	}
}
