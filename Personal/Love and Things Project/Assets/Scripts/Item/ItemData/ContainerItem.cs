using UnityEngine;
using System.Collections;

public class ContainerItem : MonoBehaviour {
	
	public ItemManager im;
	public Item[] containerList;
	public Container bag;
	public Component[] containerListComponents;
	public GameObject mesh;
	
	// Use this for initialization
	void Start () {
		im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
		
		bag = (Container)im.FindItemByName("Small Bag");
		containerList = new Item[42];
		containerListComponents = new ItemObject[42];
		for(int i = 0; i < containerList.Length; i++)
		{
			containerList[i] = im.EmptyItem();
			 //containerList[i] = (Item)ScriptableObject.CreateInstance<Item>();
		}
		/*
		containerList[0].GetComponent<ItemObject>()._item = im.FindItemByName("Iron Hatchet");
		containerList[1].GetComponent<ItemObject>()._item = im.FindItemByName("Iron Pickaxe");
		containerList[2].GetComponent<ItemObject>()._item = im.FindItemByName("Iron Knife");
		*/
	}
}