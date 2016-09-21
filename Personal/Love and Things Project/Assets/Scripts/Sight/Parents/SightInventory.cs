using UnityEngine;
using System.Collections;

public class SightInventory : SightMotorControl {
	
	protected Inventory2 inv;
	
	protected void equipBag(GameObject newBag)
	{
		inv.curBag = newBag.GetComponent<ItemObject>();
		newBag.SetActive(false);
		newBag.transform.position = playerCharacter.transform.position;
		newBag.transform.rotation = playerCharacter.transform.rotation;
		newBag.transform.parent = playerCharacter.transform;
	}
	
	protected void lootItem(GameObject PickedUpItem) //stores the item in the currently equipped bag
	{
		if(inv.isSpace(PickedUpItem.GetComponent<ItemObject>(), inv.curBag))
		{
			((Container)inv.curBag.itemInfo).itemsInBag.Add(PickedUpItem.GetComponent<ItemObject>()); //add the item to the main inventory bag
			inv.curBag.itemInfo.weight += PickedUpItem.GetComponent<ItemObject>().itemInfo.weight; //add the weight to the main bag
			inv.curBag.itemInfo.volume += PickedUpItem.GetComponent<ItemObject>().itemInfo.volume; //add the volume to the main bag
			PickedUpItem.SetActive(false);
			PickedUpItem.transform.position = inv.curBag.transform.position;
			PickedUpItem.transform.rotation = inv.curBag.transform.rotation;
			PickedUpItem.transform.parent = inv.curBag.transform;
		}
	}

	protected void lootEquip(GameObject PickedUpItem) //stores the item in the equip slot directly if there is nothing equipped
	{
		if(PickedUpItem.GetComponent<ItemObject>().itemInfo.GetType() == typeof(Weapon) && inv.curWeapon == inv.weaponSlot)
		{
			inv.curWeapon = PickedUpItem.GetComponent<ItemObject>();
			inv.lhe.activateWeapon(inv.curWeapon);
			if(inv.curWeapon.itemInfo.name == "Iron Shovel")
				inv.shovelEquipped = true;
			PickedUpItem.SetActive(false);
			PickedUpItem.transform.position = playerCharacter.transform.position;
			PickedUpItem.transform.rotation = playerCharacter.transform.rotation;
			PickedUpItem.transform.parent = playerCharacter.transform;
		}
		else if(PickedUpItem.GetComponent<ItemObject>().itemInfo.GetType() == typeof(Armor) && inv.curArmor == inv.armorSlot)
		{
			inv.curArmor = PickedUpItem.GetComponent<ItemObject>();
			PickedUpItem.SetActive(false);
			PickedUpItem.transform.position = playerCharacter.transform.position;
			PickedUpItem.transform.rotation = playerCharacter.transform.rotation;
			PickedUpItem.transform.parent = playerCharacter.transform;
		}

	}
}
