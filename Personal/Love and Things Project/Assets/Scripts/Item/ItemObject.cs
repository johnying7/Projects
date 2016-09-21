using UnityEngine;
using System.Collections;

public class ItemObject : MonoBehaviour {

	//serializing the item object class eliminates unity inspector itemInfo persistence
	public Item itemInfo;

	public ItemManager im;

	void Start()
	{
		im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
		if(im.FindItemByName(transform.name).GetType() == typeof(Weapon))
		{
			Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			newWeapon.setItem((Weapon)im.FindItemByName(transform.name));
			itemInfo = newWeapon;
		}
		else if(im.FindItemByName(transform.name).GetType() == typeof(Container))
		{
			Container newContainer = (Container)ScriptableObject.CreateInstance<Container>();
			newContainer.setItem((Container)im.FindItemByName(transform.name));
			itemInfo = newContainer;
		}
		else if(im.FindItemByName(transform.name).GetType() == typeof(Armor))
		{
			Armor newArmor = (Armor)ScriptableObject.CreateInstance<Armor>();
			newArmor.setItem((Armor)im.FindItemByName(transform.name));
			itemInfo = newArmor;
		}
		else if(im.FindItemByName(transform.name).GetType() == typeof(Consumable))
		{
			Consumable newConsumable = (Consumable)ScriptableObject.CreateInstance<Consumable>();
			newConsumable.setItem((Consumable)im.FindItemByName(transform.name));
			itemInfo = newConsumable;
		}
	}
}
