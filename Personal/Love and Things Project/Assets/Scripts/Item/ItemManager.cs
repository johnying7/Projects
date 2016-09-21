using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	
	public List<Item> itemList = new List<Item>();
	public int idCount;
	public fileManager fm = new fileManager();
	
	public Item FindItemByID(int id)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i].id == id)
			{
				return itemList[i];
			}
		}
		
		return null;
	}
	
	public Item FindItemByName(string name)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i].name == name)
			{
				return itemList[i];
			}
		}
		
		return null;
	}
	
	public Item EmptyItem()
	{
		return itemList[0];
	}
}
