using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : CreateInventory {
	
	public Item[] equippedItems;
	public Item heldItem;
	private Item tempItem;
	
	public Component[] equippedComponents;
	public Component heldComponent;
	public Component tempComponent;
	
	public ContainerItem equippedBag;
	public SightTrigger st;
	
	public bool toggleInventory = false;
	bool dropHeldItemCheck = false;
	bool closeInventory = false; //when inventory is closed and item is held, close window after item is dropped.
	
	[HideInInspector]
	public bool holdingEquippedBag = false;
	public bool shovelEquipped = false;
	
	private string _toolTip = "";
	
	public ItemManager im; //Be sure to attach the ItemManager object to this.
	//public AddItem addItem;
	
	void Start () {
		InitializeRects();
		//addItem = gameObject.GetComponent<AddItem>();
		equippedItems = new Item[10];
		equippedComponents = new Component[10];
		im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
		st = GetComponent<SightTrigger>();
		saveCheck();
		heldItem = im.FindItemByID(0);
		tempItem = im.FindItemByID(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I))
		{
			if(toggleInventory) //turning inventory OFF
			{
				if(heldItem == im.FindItemByID(0)) //if no item is held
				{
					toggleInventory = false;
					st.mamAimMode();
				}
				else
				{
					closeInventory = true;
					dropHeldItemCheck = true;
				}
			}
			else //turning inventory ON
			{
				toggleInventory = true;
				st.mamGuiMode();
			}
		}
	}
	
	void OnGUI(){

		if (toggleInventory)
		{
			if(heldItem != im.FindItemByID(0))
			{
				heldItemButton.x = Input.mousePosition.x + 10;
				heldItemButton.y = Screen.height - Input.mousePosition.y + 20;
				GUI.Button(heldItemButton,heldItem.icon);
			}
			
//			if(heldItem != im.EmptyItem() && Input.GetMouseButtonDown(0) && !inventoryRect.Contains(Event.current.mousePosition))
//			{
//				dropHeldItemCheck = true;
//			}
//			
//			if (dropHeldItemCheck)
//				dropHeldItemRect = GUI.Window(1, dropHeldItemRect, DeleteItemWindow, "Drop Held Item?");

			inventoryRect = ClampToScreen(GUI.Window(0, inventoryRect, InventoryWindow, "Inventory"));

			DisplayToolTip();
		}
		
		st._aimToolTipRect = ClampToScreen(GUI.Window(3, st._aimToolTipRect, st.AimToolTip, "View"));
		
	}
	
	void saveCheck() {
		//if the save file has a save file
			//do use these variables
		
		//else start with default inventory
		equipStartingItems();
	}
	
//	void DeleteItemWindow(int windowID) {
//		if(GUI.Button(new Rect(70-30-20, 24, 40, 16),"Yes") && st.canDropItem() == true)
//		{
//			
//			if(!holdingEquippedBag)
//			{
//				Debug.Log("dropping item now");
//				//st.dropItem();
//			}
//			else
//			{
//				Debug.Log("dropping bag now");
//				//st.dropEquipBag();
//			}
//			
//			heldItem = im.FindItemByID(0);
//			dropHeldItemCheck = false;
//			if(closeInventory)
//			{
//				toggleInventory = false;
//				st.mamAimMode();
//				closeInventory = false;
//			}
//		}
//		else if(GUI.Button(new Rect(70+10, 24, 40, 16),"No"))
//		{
//			toggleInventory = true;
//			dropHeldItemCheck = false;
//		}
//	}
	
	void InventoryWindow(int windowID) {
		createEquipButtons();
		
		//bottom half inventory bag
		if(bagIsEquipped())
			showEquippedBag();
		
		GUI.DragWindow(new Rect(0,0,300,18));
	}
	
	public bool hasSpace()
	{
		for(int i = 0; i < 42; i++)
		{
			if(equippedBag.containerList[i] == im.FindItemByID(0))
				return true;
		}
		
		return false;
	}
	
	public int getFreeSpace()
	{
		for(int i = 0; i < 42; i++)
		{
			if(equippedBag.containerList[i] == im.FindItemByID(0))
				return i;
		}
		
		return 43;
	}
	
	private void showEquippedBag()
	{
		GUILayout.BeginArea(openBagButton);
		for(int x = 0; x < 7; x++)
		{
			GUILayout.BeginHorizontal();
			for(int i = 0; i < 6; i++)
			{
				if(GUILayout.Button(new GUIContent(equippedBag.containerList[i+(6*x)].icon, equippedBag.containerList[i+(6*x)].ToolTip())))
				{
					tempItem = heldItem;
					heldItem = equippedBag.containerList[i+(6*x)];
					equippedBag.containerList[i+(6*x)] = tempItem;
					
					tempComponent = heldComponent;
					heldComponent = equippedBag.containerListComponents[i+(6*x)];
					if(tempComponent == null)
						equippedBag.containerListComponents[i+(6*x)] = null;
					else
						equippedBag.containerListComponents[i+(6*x)] = tempComponent;
				}
				SetToolTip();
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();
	} 
	
	private void SetToolTip()
	{
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
		{
			if(_toolTip != "")
				_toolTip = "";
			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;
		}
	}
	
	private void DisplayToolTip()
	{
		if(heldItem == im.EmptyItem())
		{
			if(_toolTip != "")
				GUI.Box(new Rect(Input.mousePosition.x + 10, Screen.height - Input.mousePosition.y + 20, 200, 100), _toolTip);
		}
		else
		{
			if(_toolTip != "")
				GUI.Box(new Rect(Input.mousePosition.x + 42 + 15, Screen.height - Input.mousePosition.y + 20, 200, 100), _toolTip);

			GUI.depth = 1;
		}
	}
	
	private bool bagIsEquipped()
	{
		if(equippedItems[(int)EquipSlot.Bag] != im.EmptyItem())
		{
			return true;
		}
		
		return false;
	}
	
	void equipStartingItems()
	{
		/*
		equippedItems[(int)EquipSlot.Head] = im.FindItemByID(0); //fill head with no item
		equippedItems[(int)EquipSlot.Chest] = (Armor)im.FindItemByID(1); //fill chest with shirt
		equippedItems[(int)EquipSlot.Hands] = (Armor)im.FindItemByID(2); //fill hands with gloves
		equippedItems[(int)EquipSlot.Legs] = (Armor)im.FindItemByID(3); //fill legs with pants
		equippedItems[(int)EquipSlot.Feet] = (Armor)im.FindItemByID(4); //fill feet with sandals
		
		equippedItems[(int)EquipSlot.RightHand] = (Weapon)im.FindItemByID(0); //fill right hand with no item
		equippedItems[(int)EquipSlot.LeftHand] = (Weapon)im.FindItemByID(0); //fill left hand with no item
		equippedItems[(int)EquipSlot.Pouch] = (Container)im.FindItemByID(5); //fill pouch with small pouch
		equippedItems[(int)EquipSlot.Bag] = im.EmptyItem(); //fill bag with small bag
		equippedItems[(int)EquipSlot.Jewelry] = (Jewelry)im.FindItemByID(7); //fill engagement ring slot with nothing
		*/
		
		equippedItems[(int)EquipSlot.Head] = im.EmptyItem(); //fill head with no item
		equippedItems[(int)EquipSlot.Chest] = im.EmptyItem(); //fill chest with shirt
		equippedItems[(int)EquipSlot.Hands] = im.EmptyItem(); //fill hands with gloves
		equippedItems[(int)EquipSlot.Legs] = im.EmptyItem(); //fill legs with pants
		equippedItems[(int)EquipSlot.Feet] = im.EmptyItem(); //fill feet with sandals
		
		equippedItems[(int)EquipSlot.RightHand] = im.EmptyItem(); //fill right hand with no item
		equippedItems[(int)EquipSlot.LeftHand] = im.EmptyItem(); //fill left hand with no item
		equippedItems[(int)EquipSlot.Pouch] = im.EmptyItem(); //fill pouch with small pouch
		equippedItems[(int)EquipSlot.Bag] = im.EmptyItem(); //fill bag with small bag
		equippedItems[(int)EquipSlot.Jewelry] = im.EmptyItem(); //fill engagement ring slot with nothing
	}
	
	private void createEquipButtons()
	{		
		//left column
		createRegularButton(headButton, EquipSlot.Head, EquipTag.Head);
		createRegularButton(chestButton, EquipSlot.Chest, EquipTag.Chest);
		createRegularButton(handsButton, EquipSlot.Hands, EquipTag.Hands);
		createRegularButton(legsButton, EquipSlot.Legs, EquipTag.Legs);
		createRegularButton(feetButton, EquipSlot.Feet, EquipTag.Feet);
		
		//center column
		GUI.Button(profileButton, "Character Picture");
		
		//right column
		createLeftHandButton();
		createRightHandButton();
		
		createRegularButton(pouchButton, EquipSlot.Pouch, EquipTag.Pouch);
		createBagButton(bagButton, EquipSlot.Bag, EquipTag.Bag);
		createRegularButton(jewelryButton, EquipSlot.Jewelry, EquipTag.Jewelry);
		SetToolTip();
	}
	
	private void createRegularButton(Rect buttonRect, EquipSlot slotType, EquipTag tagType)
	{
		if(GUI.Button(buttonRect, new GUIContent(equippedItems[(int)slotType].icon, equippedItems[(int)slotType].ToolTip())) &&
			(heldItem == im.FindItemByID(0) || heldItem.equipType == tagType))
			swapItems(slotType);
	}
	
	private void createBagButton(Rect buttonRect, EquipSlot slotType, EquipTag tagType)
	{
		if(GUI.Button(buttonRect, new GUIContent(equippedItems[(int)slotType].icon, equippedItems[(int)slotType].ToolTip())) &&
			(heldItem == im.FindItemByID(0) || heldItem.equipType == tagType))
		{
			swapItems(slotType);
			if(equippedItems[(int)slotType] == im.EmptyItem())
			{
				holdingEquippedBag = true;
			}
			else
			{
				holdingEquippedBag = false;
			}
		}
	}
	
	private void createLeftHandButton()
	{
		if(GUI.Button(leftHandButton, new GUIContent(equippedItems[(int)EquipSlot.LeftHand].icon, equippedItems[(int)EquipSlot.LeftHand].ToolTip())))
			swapHeldWithHand(EquipSlot.LeftHand, EquipSlot.RightHand);
	}
	
	private void createRightHandButton()
	{
		if(GUI.Button(rightHandButton, new GUIContent(equippedItems[(int)EquipSlot.RightHand].icon, equippedItems[(int)EquipSlot.RightHand].ToolTip())))
			swapHeldWithHand(EquipSlot.RightHand, EquipSlot.LeftHand);
	}

	/// <summary>
	/// held item is an item in the inventory whose icon follows the mouse for rearranging
	/// items in the inventory and swapping equipped with bag held items.
	/// (heldItem is not the same as temp items, temp is unseen)
	/// </summary>
	private void swapHeldWithHand(EquipSlot leftClickHand, EquipSlot rightClickHand)
	{
		if(heldItem.equipType != EquipTag.TwoHanded)
		{
			if(equippedItems[(int)leftClickHand].equipType != EquipTag.TwoHanded)
			{
				swapItems(leftClickHand);
			}
			else if(equippedItems[(int)leftClickHand].equipType == EquipTag.TwoHanded)
			{
				swapItems(leftClickHand);
				emptySlot(rightClickHand);				}
		}
		else if(heldItem.equipType == EquipTag.TwoHanded)
		{
			if(equippedItems[(int)leftClickHand].equipType == EquipTag.TwoHanded ||
			   (equippedItems[(int)rightClickHand] == im.FindItemByID(0) && equippedItems[(int)leftClickHand] == im.FindItemByID(0)))
			{
				swapItems(leftClickHand);
				tempToEquipped(rightClickHand);
			}
			else
			{
				if(equippedItems[(int)leftClickHand] != im.FindItemByID(0) && equippedItems[(int)rightClickHand] == im.FindItemByID(0))
				{
					swapItems(leftClickHand);
					tempToEquipped(rightClickHand);
				}
				else if(equippedItems[(int)leftClickHand] == im.FindItemByID(0) && equippedItems[(int)rightClickHand] != im.FindItemByID(0))
				{
					swapItems(rightClickHand);
					tempToEquipped(leftClickHand);
				}
				else
				{
					if(this.hasSpace())
					{
						swapItems(leftClickHand);
						equippedBag.containerList[getFreeSpace()] = equippedItems[(int)rightClickHand];
					}
					else
					{
						//play message or sound effect showing player needs to make space
					}
				}
			}
		}
	}
	
	#region Item Swap Functions
	private void heldToTemp()
	{
		tempItem = heldItem;
		tempComponent = heldComponent;
	}
	
	private void equippedToHeld(EquipSlot slotType)
	{
		heldItem = equippedItems[(int)slotType];
		heldComponent = equippedComponents[(int)slotType];
	}
	
	private void tempToEquipped(EquipSlot slotType)
	{
		equippedItems[(int)slotType] = tempItem;
		equippedComponents[(int)slotType] = tempComponent;
		if(equippedItems[(int)slotType].name == im.FindItemByName("Iron Shovel").name)
			shovelEquipped = true;
		else
			shovelEquipped = false;
	}
	
	private void swapItems(EquipSlot slotType) //swaps the held item to the designated equip slot type
	{
		heldToTemp();
		equippedToHeld(slotType);
		tempToEquipped(slotType);
	}
	
	private void emptySlot(EquipSlot slotType) //nulls the specified equip slot
	{
		equippedItems[(int)slotType] = im.FindItemByID(0);
		equippedComponents[(int)slotType] = null;
	}
	#endregion
}

public enum EquipSlot
{
	Head = 0,
	Chest = 1,
	Hands = 2,
	Legs = 3,
	Feet = 4,
	RightHand = 5,
	LeftHand = 6,
	Pouch = 7,
	Bag = 8,
	Jewelry = 9
}