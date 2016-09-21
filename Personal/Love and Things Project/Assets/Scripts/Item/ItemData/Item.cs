using UnityEngine;
using System.Collections;

[System.Serializable] //allows the creation of items as an array list in the editor in AddItem
public class Item : ScriptableObject{
	
	new public string name;
	public string description;
	public int cost;
	public Texture2D icon;
	public int volume;
	public int weight;
	public int id;
	public int maxDurability;
	public int curDurability;
	public EquipTag equipType;

	public Item()
	{
		name = "";
		description = "";
		cost = 0;
		icon = null;
		volume = 0;
		weight = 0;
		id = 0;
		maxDurability = 0;
		curDurability = 0;
		equipType = EquipTag.Bag;
	}

	public string ToolTip()
	{
		if(name == "No Item")
			return "";
		
		return name + "\n" +
			"Value " + cost + "\n" +
			"Durability " + curDurability + "/" + maxDurability + "\n" +
			"Volume " + volume + "\n" +
			"Weight " + weight + "\n";
	}
	
}

public enum EquipTag {
	Bag,
	Pouch,
	OneHanded,
	TwoHanded,
	Head,
	Chest,
	Hands,
	Legs,
	Feet,
	Jewelry,
	Other
}