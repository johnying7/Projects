using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(ItemObject))]
public class ItemObjectInspector : Editor {
	
	string itemName;
	int itemID;
	string oldItemName;
	int oldItemID;

	bool showItem;


	void Start()
	{
		ItemObject itemObj = target as ItemObject;
		//initializes the item by gameobject name (mainly for prefab setup)
		if(itemObj.im.FindItemByName(itemObj.gameObject.name) != null || itemObj.gameObject.name != "")
		{
			itemID = itemObj.im.FindItemByName(itemObj.gameObject.name).id;
			itemName = itemObj.im.FindItemByName(itemObj.gameObject.name).name;
		}
		changeItem();
		if(itemObj.itemInfo != null)
		{
			itemName = itemObj.itemInfo.name;
			itemID = itemObj.itemInfo.id;
		}
		oldItemName = itemName;
		oldItemID = itemID;
		
	}

	void OnEnable()
	{
		ItemObject itemObj = target as ItemObject;
		//initializes the item by gameobject name (mainly for prefab setup)
		if(itemObj.im.FindItemByName(itemObj.gameObject.name) != null || itemObj.gameObject.name != "")
		{
			itemID = itemObj.im.FindItemByName(itemObj.gameObject.name).id;
			itemName = itemObj.im.FindItemByName(itemObj.gameObject.name).name;
		}
		changeItem();
		if(itemObj.itemInfo != null)
		{
			itemName = itemObj.itemInfo.name;
			itemID = itemObj.itemInfo.id;
		}
		oldItemName = itemName;
		oldItemID = itemID;

	}
	
	public override void OnInspectorGUI()
	{
		ItemObject itemObj = target as ItemObject;
		itemObj.im = GameObject.Find("ItemManager").GetComponent<ItemManager>();

		itemName = EditorGUILayout.TextField("Item Name: ", itemName);
		
		itemID = EditorGUILayout.IntField("Item ID: ", itemID);

		if(GUI.changed)
		{
			//change the item id from the matching item name
			if(itemName != oldItemName && itemObj.im.FindItemByName(itemName) != null)
			{
				itemObj.itemInfo = itemObj.im.FindItemByName(itemName);
				oldItemName = itemName;
				itemID = itemObj.itemInfo.id;
				oldItemID = itemID;
				changeItem();
			}
			//change the item name from the matching item id
			else if(itemID != oldItemID && itemObj.im.FindItemByID(itemID) != null)
			{
				itemObj.itemInfo = itemObj.im.FindItemByID(itemID);
				oldItemID = itemID;
				oldItemName = itemObj.itemInfo.name;
				itemName = oldItemName;
				changeItem();
			}
			else
				itemObj.itemInfo = null;
		}

		//shows item TYPE specific attributes that may need to be seen during development
		showItem = EditorGUILayout.Foldout(showItem, "Item Stats");
		if(showItem && itemObj.itemInfo != null)
		{
			if(itemObj.itemInfo.equipType == EquipTag.OneHanded || itemObj.itemInfo.equipType == EquipTag.TwoHanded)
			{
				EditorGUILayout.TextField("Damage: " + ((Weapon)itemObj.itemInfo).damage);
			}
			else if(itemObj.itemInfo.equipType == EquipTag.Head ||
			        itemObj.itemInfo.equipType == EquipTag.Chest ||
			        itemObj.itemInfo.equipType == EquipTag.Hands ||
			        itemObj.itemInfo.equipType == EquipTag.Legs ||
			        itemObj.itemInfo.equipType == EquipTag.Feet)
			{
				EditorGUILayout.TextField("Armor: " + ((Armor)itemObj.itemInfo).defense);
			}

			EditorGUILayout.TextField("Durability: " + itemObj.itemInfo.curDurability + "/" + itemObj.itemInfo.maxDurability);
		}
	}
	
	void changeItem()
	{
		ItemObject itemObj = target as ItemObject;

		if(itemObj.im.FindItemByID(itemID).GetType() == typeof(Weapon))
		{
			Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			newWeapon.setItem((Weapon)itemObj.im.FindItemByID(itemID));
			itemObj.itemInfo = newWeapon;
		}
		else if(itemObj.im.FindItemByID(itemID).GetType() == typeof(Container))
		{
			Container newContainer = (Container)ScriptableObject.CreateInstance<Container>();
			newContainer.setItem((Container)itemObj.im.FindItemByID(itemID));
			itemObj.itemInfo = newContainer;
		}
		else if(itemObj.im.FindItemByID(itemID).GetType() == typeof(Armor))
		{
			Armor newArmor = (Armor)ScriptableObject.CreateInstance<Armor>();
			newArmor.setItem((Armor)itemObj.im.FindItemByID(itemID));
			itemObj.itemInfo = newArmor;
		}
		else if(itemObj.im.FindItemByID(itemID).GetType() == typeof(Consumable))
		{
			Consumable newConsumable = (Consumable)ScriptableObject.CreateInstance<Consumable>();
			newConsumable.setItem((Consumable)itemObj.im.FindItemByID(itemID));
			itemObj.itemInfo = newConsumable;
		}
		itemObj.gameObject.name = itemName;
		EditorUtility.SetDirty(itemObj);
	}
}
