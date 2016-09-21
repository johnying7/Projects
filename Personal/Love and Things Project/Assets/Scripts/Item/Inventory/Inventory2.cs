using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory2 : CreateInventory2 {

	/// <summary>
	/// Make sure to adjust the bag volume & weight stats.
	/// Also, before moving an object, make sure the volume of the hold item will fit in the new bag.
	/// </summary>

	public bool isInventoryOn = false;

	public SightTrigger st;
	public ItemManager im; //Be sure to attach the ItemManager object to this.

	//used to hold empty items in the equipment slot
	public ItemObject weaponSlot;
	public ItemObject armorSlot;
	public ItemObject bagSlot;

	//the current item slot equipment
	public ItemObject curWeapon;
	public ItemObject curArmor;
	public ItemObject curBag;

	public ItemObject holdItem;
	public ItemObject tempItem;

	public int holdWeight = 100; //the max player hold weight (calculated by curBag.itemInfo.weight < holdWeight)
	private Vector2 scrollPosition; //position of the inventory scroll bar

	private bool scrollNeeded = false;

	private DropItemCheck dropItemCheck;
	public GameObject dropPosition;

	public bool shovelEquipped = false;
	public LeftHandEquip lhe;

	public GameObject craftObject;

	// Use this for initialization
	void Start () {
		InitializeRects();
		im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
		st = GetComponent<SightTrigger>();
		scrollPosition = Vector2.zero;
		invPos = 0;
		bagPos = 0;

		//fill the equipment slots with empty items
		curWeapon = weaponSlot;
		curArmor = armorSlot;
		curBag = bagSlot;

		holdItem = null;
		tempItem = null;

		dropItemCheck = transform.parent.Find("DropPosition").GetComponent<DropItemCheck>();
		craftObject = null;
	}
	
	// Update is called once per frame
	void Update () {

//		if(Input.GetKeyDown(KeyCode.LeftAlt) && st.workbench == null)
//		{
//			st.mamToggle();
//		}

		if(Input.GetKeyDown(KeyCode.I))
		{
			if(isInventoryOn) //turning inventory OFF
			{
				isInventoryOn = false;
				if(st.isPlayerCameraSet == true)
					st.mamAimMode();
			}
			else //turning inventory ON
			{
				isInventoryOn = true;
				st.mamGuiMode();
			}
		}

		if(st.workbench != null && st.workbench.GetComponent<Workbench>().isUsingWorkbench)
		{
			if(Input.GetMouseButtonDown(0))
			{
				if(craftObject == null){
					RaycastHit tempHit;
					Ray tempRay = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
					if(Physics.Raycast(tempRay,	out tempHit))
					{
						if(tempHit.collider.transform.parent.tag == "Item")
						{
							craftObject = tempHit.collider.transform.parent.gameObject;
							foreach(Transform trans in craftObject.GetComponentsInChildren<Transform>(true))
								trans.gameObject.layer = 12;
							//craftObject.rigidbody.isKinematic = true;
						}
					}
				}
				else if(craftObject != null){
					foreach(Transform trans in craftObject.GetComponentsInChildren<Transform>(true))
						trans.gameObject.layer = 0;
					//craftObject.rigidbody.isKinematic = false;
					craftObject = null;
				}
			}
			else if(Input.GetMouseButtonDown(1))
			{
				if(craftObject == null){
					RaycastHit tempHit;
					Ray tempRay = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
					if(Physics.Raycast(tempRay,	out tempHit))
					{
						if(tempHit.collider.transform.parent.tag == "Item")
						{
							st.workbench.GetComponent<Workbench>().addRemove(tempHit.collider.transform.parent.gameObject);
						}
					}
				}
			}

			if(craftObject != null)
			{
				RaycastHit hitobj;
				Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				int layerMask = 1 << 12;
				layerMask = ~layerMask;
				if(Physics.Raycast(ray, out hitobj, 10.0f, layerMask))
					craftObject.transform.position = hitobj.point;
			}
		}
	}

	void OnGUI()
	{
		if(isInventoryOn)
		{
			if(holdItem != null) //shows the held item button to the button right of the mouse (reminds user of held item)
			{
				holdItemButton.x = Input.mousePosition.x + 10;
				holdItemButton.y = Screen.height - Input.mousePosition.y + 20;
				GUI.Button(holdItemButton,holdItem.itemInfo.name);
				
				holdItemLabel.x = Input.mousePosition.x + 10 + (300-labelSpacing-spacing-spacing-labelWidth);
				holdItemLabel.y = Screen.height - Input.mousePosition.y + 20;
				if(holdItem.itemInfo.GetType() == typeof(Container))
					GUI.Label(holdItemLabel,holdItem.itemInfo.volume+"/"+((Container)holdItem.itemInfo).holdVolume);
				else
					GUI.Label(holdItemLabel,holdItem.itemInfo.volume.ToString());
			}

			//used to drop the item out into the real game world
			if(holdItem != null && Event.current.type == EventType.MouseDown && //canDropItem() &&
			   !inventoryRect.Contains(Event.current.mousePosition) && Event.current.button == 0)
			{
				if(st.workbench == null)
				{
					Debug.Log("dropping item");
					if(holdItem != curArmor && holdItem != curWeapon) //if the item isn't an equip (inside the main bag)
					{
						removeFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>()); //remove the weight and volume from the old bags
						getItemBagList(holdItem).RemoveAt(getBagIndex(holdItem));//remove the object from its old bag
					}
					else if(holdItem == curWeapon)
						curWeapon = weaponSlot;
					else if(holdItem == curArmor)
						curArmor = armorSlot;

					Debug.Log("dropping hold item");
					if(holdItem.GetComponent<Rigidbody>().isKinematic)
						holdItem.GetComponent<Rigidbody>().isKinematic = false;
					dropHoldItem(); //drop the held item
				}
				else if(st.workbench != null && holdItem.itemInfo.GetType() == typeof(Consumable)) //if using the work bench, drop consumable on table
				{
					RaycastHit hitobj;
					Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
					Vector3 placePoint;
					if(Physics.Raycast(ray,out hitobj))
					{
						removeFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>()); //remove the weight and volume from the old bags
						getItemBagList(holdItem).RemoveAt(getBagIndex(holdItem));//remove the object from its old bag
						 
						placePoint = hitobj.point;

						holdItem.gameObject.SetActive(true);
						holdItem.transform.position = placePoint;
						holdItem.transform.rotation = Quaternion.identity;
						holdItem.GetComponent<Rigidbody>().isKinematic = true;
						holdItem.transform.parent = null;
						holdItem = null;
					}
				}
			}

			if(st.workbench != null)
			{
				if(GUI.Button(new Rect(Screen.width-100,0,100,24), "Assemble"))
				{
					string createObjectName = st.workbench.GetComponent<Workbench>().assemble();					
					if (createObjectName != "") {
						GameObject newObject = (GameObject)Instantiate (Resources.Load (createObjectName),
								                 st.workbench.GetComponent<Workbench> ().dropCraft.position, 
								                 st.workbench.GetComponent<Workbench> ().dropCraft.rotation);
						newObject.name = createObjectName;
					} 
					//old broken code
//					else
//					{
//						GameObject newObject = (GameObject)Instantiate (Resources.Load (createObjectName),
//								st.workbench.GetComponent<Workbench> ().dropCraft.position, 
//								st.workbench.GetComponent<Workbench> ().dropCraft.rotation);
//						newObject.name = createObjectName;
//					}
				}
				GUI.Box(new Rect(Screen.width-100, 28, 100, st.workbench.GetComponent<Workbench>().craftList.Count*28),"");
				for(int i = 0; i < st.workbench.GetComponent<Workbench>().craftList.Count; i++)
				{
					GUI.Label(new Rect(Screen.width-100+4, i*28+28,92, 24), st.workbench.GetComponent<Workbench>().craftList[i].name);
				}
			}

			//create inventory window here
			inventoryRect = ClampToScreen(GUI.Window(0, inventoryRect, InventoryWindow, "Inventory"));
		}
	}

	void InventoryWindow(int windowID) {
		bagPos = 0;

		if(GUI.Button(weaponButton,curWeapon.itemInfo.name))
		{
			if(Event.current.button == 0)//if left click the button
			{
				if(holdItem == null && curWeapon != weaponSlot)//if not holding a weapon, hold it
				{
					holdItem = curWeapon;
				}
				else if(holdItem != null && holdItem.itemInfo.GetType() == typeof(Weapon)) //if holding a weapon, place it
				{
					lhe.activateWeapon(holdItem);
					removeFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>());
					if(curWeapon != weaponSlot)//if a real weapon is already equipped release it first
					{
						if(curBag != bagSlot) //if wearing a real bag, put it in the bag
						{
							curWeapon.transform.parent = curBag.transform;//make it a child of the bag
							((Container)curBag.itemInfo).itemsInBag.Add(curWeapon);//add it to the bag list
							equipHoldItem(curWeapon);
						}
						else //if not wearing a real bag, drop the weapon on the ground
						{
							dropEquip(curWeapon);
							equipHoldItem(curWeapon);
						}
					}
					else //equip the item
					{
						equipHoldItem(curWeapon);//equip the hold item at the curWeapon slot
					}

					if(curWeapon.itemInfo.name == "Iron Shovel")
						shovelEquipped = true;
				}
			}
			else if(Event.current.button == 1)//if right click the button
			{

			}
		}
		GUI.Label(weaponLabel,curWeapon.itemInfo.curDurability+"/"+curWeapon.itemInfo.maxDurability);

//		if(GUI.Button(armorButton,curArmor.itemInfo.name))
//		{
//			if(Event.current.button == 0)//if left click the button
//			{
//				if(holdItem == null && curArmor != armorSlot)//if not holding an armor, hold it
//				{
//					holdItem = curArmor;
//				}
//				else if(holdItem != null && holdItem.itemInfo.GetType() == typeof(Armor)) //if holding an armor, place it
//				{
//					removeFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>());
//					if(curArmor != armorSlot)//if a real armor is already equipped release it first
//					{
//						if(curBag != bagSlot) //if wearing a real bag, put it in the bag
//						{
//							curArmor.transform.parent = curBag.transform;//make it a child of the bag
//							((Container)curBag.itemInfo).itemsInBag.Add(curArmor);//add it to the bag list
//							equipHoldItem(curArmor);
//						}
//						else //if not wearing a real bag, drop the armor on the ground
//						{
//							dropEquip(curArmor);
//							equipHoldItem(curArmor);
//						}
//					}
//					else //equip the item
//						equipHoldItem(curArmor);
//				}
//			}
//			else if(Event.current.button == 1)//if right click the button
//			{
//				
//			}
//		}
//		GUI.Label(armorLabel,curArmor.itemInfo.curDurability+"/"+curArmor.itemInfo.maxDurability);

		if(GUI.Button(bagButton,((Container)curBag.itemInfo).name))
		{
			if(Event.current.button == 0)//if left click the button
			{
				if(holdItem == null && curBag != bagSlot)
				{
					dropEquip(curBag); //drop the bag immediately (prevents placing bag in itself sub-bags)
					curBag = bagSlot;
				}
			}
			else if(Event.current.button == 1)//if right click the button
			{
				
			}
		}
		GUI.Label(bagLabel,((Container)curBag.itemInfo).volume+"/"+((Container)curBag.itemInfo).holdVolume);

		//bottom half inventory bag

		if(invPos >= 15) //if the inventory contains 15 or more items, increase the scroll bar space
		{
			scrollNeeded = true;
			bagScrollRect = new Rect(spacing,0,300-(spacing*2)-20,474+((invPos-15)*(buttonheight+spacing))-buttonheight);
		}
		else //make the scrollbar invisible
		{
			bagScrollRect = new Rect(spacing,0,300-(spacing*2)-20,460-buttonheight);
			scrollNeeded = false;
		}

		scrollPosition = GUI.BeginScrollView(bagDisplayRect, scrollPosition,bagScrollRect, false, scrollNeeded);

		//place the hold item into the last slot in the curBag when clicking at the bottom of the inventory bag area
		if(holdItem != null && Event.current.type == EventType.MouseDown && Event.current.button == 0 &&
		   (Event.current.mousePosition.x >= spacing && Event.current.mousePosition.x <= (300-spacing-20)) &&
		   (Event.current.mousePosition.y >= ((buttonheight+spacing)*invPos-bagDisplayRect.y) && 
		 	Event.current.mousePosition.y <= (bagScrollRect.height-scrollPosition.y)) &&
		   	curBag != bagSlot)
		{
			if(holdItem == curWeapon)
			{
				if(holdItem.itemInfo.name == "Iron Shovel")
					shovelEquipped = false;
				curWeapon = weaponSlot;
			}
			else if(holdItem == curArmor)
			{
				curArmor = armorSlot;
			}
			else
			{
				removeFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>());
				getItemBagList(holdItem).RemoveAt(getBagIndex(holdItem));//remove the object from its old bag
			}

			holdItem.transform.parent = curBag.transform;
			((Container)curBag.itemInfo).itemsInBag.Add(holdItem);
			addFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>());
			holdItem = null;
		}

		invPos = 0;
		if(curBag != bagSlot) //if an actual bag is equipped, show it
		{
			for(int i = 0; i < ((Container)curBag.itemInfo).itemsInBag.Count; i++)
			{
				displayItemGUI(((Container)curBag.itemInfo).itemsInBag[i]);
			}
			bagPos = 0;
		}
		GUI.EndScrollView();

		GUI.DragWindow(new Rect(0,0,300,18));
	}

	void displayItemGUI(ItemObject displayItem) //calls the correct item gui button show
	{
		if(displayItem.itemInfo.GetType() == typeof(Weapon) ||
		   displayItem.itemInfo.GetType() == typeof(Consumable) ||
		   displayItem.itemInfo.GetType() == typeof(Armor))
		{
			createInventoryItem(displayItem);
		}
		else if(displayItem.itemInfo.GetType() == typeof(Container))
		{
			createBagGUI(displayItem);
		}
	}
	
	void createBagGUI(ItemObject bagObject)
	{
		((Container)bagObject.itemInfo).isBagOpen = GUI.Toggle(setBagToggleRect(), ((Container)bagObject.itemInfo).isBagOpen, "");
		bagPos++;//increase the item list indent after the toggle has been displayed

		if(GUI.Button(setItemButtonRect(),((Container)bagObject.itemInfo).name))
		{
			if(Event.current.button == 0)//if left click the button
			{
				if(holdItem == null)
				{
					holdItem = bagObject;
				}
				else if(holdItem != null && holdItem != bagObject && !isObjectInItself(bagObject) &&
				        (holdItem.itemInfo.volume+bagObject.itemInfo.volume) <= ((Container)bagObject.itemInfo).holdVolume) //if holding an item, place it
				{
					if(holdItem == curWeapon)
					{
						if(holdItem.itemInfo.name == "Iron Shovel")
							shovelEquipped = false;
						curWeapon = weaponSlot;
					}
					else if(holdItem == curArmor)
					{
						curArmor = armorSlot;
					}
					else
					{
						removeFill(holdItem, holdItem.transform.parent.GetComponent<ItemObject>()); //remove the weight from the old bags
						getItemBagList(holdItem).RemoveAt(getBagIndex(holdItem));//remove the object from its old bag
					}

					holdItem.transform.parent = bagObject.transform;//make it a child of the bag
					((Container)bagObject.itemInfo).itemsInBag.Add(holdItem);//add it to the bag list
					addFill(holdItem,bagObject);
					holdItem = null;
				}
				else if(holdItem == bagObject)//if placing the object where it is, empty the reference (no change)
				{
					holdItem = null;
				}
			}
			else if(Event.current.button == 1)//if right click the button
			{
				
			}
		}
		GUI.Label(setItemLabelRect(),((Container)bagObject.itemInfo).volume+ "/"+ ((Container)bagObject.itemInfo).holdVolume);
		invPos++;//increase the item counter to mark button position
		if(((Container)bagObject.itemInfo).isBagOpen) //display the bag contents
		{
			for(int i = 0; i < ((Container)bagObject.itemInfo).itemsInBag.Count; i++)
			{
				displayItemGUI(((Container)bagObject.itemInfo).itemsInBag[i]);
			}
		}
		bagPos--; //shift the indent back
	}

	void createInventoryItem(ItemObject itemObj)
	{
		if(GUI.Button(setItemButtonRect(), itemObj.itemInfo.name))
		{
			if(Event.current.button == 0)//if left click the button
			{
				if(holdItem == null)
				{
					holdItem = itemObj;
				}
				else if(holdItem == itemObj) //if placing the object where it is, empty the reference (no change)
				{
					holdItem = null;
				}
				else if(holdItem != null) //if holding an item, place it
				{
					if(holdItem == curWeapon)
					{
						if(holdItem.itemInfo.name == "Iron Shovel")
							shovelEquipped = false;
						curWeapon = weaponSlot;
					}
					else if(holdItem == curArmor)
						curArmor = armorSlot;
					else
						getItemBagList(holdItem).RemoveAt(getBagIndex(holdItem));//remove the object from its old bag

					lhe.activateWeapon(curWeapon);
					getItemBagList(itemObj).Insert(getBagIndex(itemObj),holdItem);//insert the heldItem into this items slot (moving everything up)
					holdItem.transform.parent = itemObj.transform.parent;//make it a child of the bag
					addFill(holdItem,holdItem.transform.parent.GetComponent<ItemObject>());
					holdItem = null;
				}
			}
			else if(Event.current.button == 1)//if right click the button
			{
				
			}
		}
		GUI.Label(setItemLabelRect(),itemObj.itemInfo.volume.ToString());
		invPos++;//increase the item counter to mark button position
	}

	public List<ItemObject> getItemBagList(ItemObject itemToFind) //gets the bag list the itemToFind belongs to
	{
		return ((Container)itemToFind.transform.parent.GetComponent<ItemObject>().itemInfo).itemsInBag;
	}

	public int getBagIndex(ItemObject itemToFind) //gets the index in the bag the itemToFind belongs to
	{
		return ((Container)itemToFind.transform.parent.GetComponent<ItemObject>().itemInfo).itemsInBag.IndexOf(itemToFind);
	}

	public bool isSpace(ItemObject item, ItemObject bag) //is there enough space in the bag for the item
	{
		if(((Container)bag.itemInfo).holdVolume >= (((Container)bag.itemInfo).volume + item.itemInfo.volume))
			return true;
		else
			return false;
	}

	public bool isWearingBag() //is player wearing a real bag
	{
		if(curBag != bagSlot)
			return true;
		else
			return false;
	}

	public bool isObjectInItself(ItemObject obj)
	{
		if(obj.transform.parent.GetComponent<ItemObject>() == curBag)//found the last object in stack, it is not part of itself
		{
			return false;
		}
		else if(obj.transform.parent.GetComponent<ItemObject>() == holdItem)//found itself, player is trying to put a bag in child bag
		{
			return true;
		}
		else
		{
			return isObjectInItself(obj.transform.parent.GetComponent<ItemObject>());
		}
	}

	public void dropEquip(ItemObject obj) //drop the specified equipment
	{
//		if(canDropItem())
//		{
			obj.gameObject.SetActive(true);
			obj.transform.position = dropPosition.transform.position;
			obj.transform.position = dropPosition.transform.position;
			obj.transform.parent = null;
//		}
	}

	public void dropHoldItem() //drop the hold item onto the ground
	{
//		if(canDropItem())
//		{
			holdItem.gameObject.SetActive(true);
			holdItem.transform.position = dropPosition.transform.position;
			holdItem.transform.position = dropPosition.transform.position;
			holdItem.transform.parent = null;
			holdItem = null;
//		}
	}

	public void equipHoldItem(ItemObject curSlot)//equip the hold item into the equipment slot
	{
		getItemBagList(holdItem).RemoveAt(getBagIndex(holdItem));//remove the object from its old bag
		if(curSlot == curWeapon)
			curWeapon = holdItem;
		if(curSlot == curArmor)
			curArmor = holdItem;
		holdItem = null;
	}

	public void removeFill(ItemObject obj, ItemObject bagObject) //remove the weight and volume from the bag of the item and all parent bags
	{
		bagObject.itemInfo.volume -= obj.itemInfo.volume;
		bagObject.itemInfo.weight -= obj.itemInfo.weight;

		if(bagObject.transform.parent != null)
			if(bagObject.transform.parent.GetComponent<ItemObject>() != null)
				if(bagObject.transform.parent.GetComponent<ItemObject>().itemInfo.GetType() == typeof(Container))
					removeFill(obj, bagObject.transform.parent.GetComponent<ItemObject>());
	}

	public void addFill(ItemObject item, ItemObject bagObject) //add the weight and volume of the item to the new bag & parent bags
	{
		bagObject.itemInfo.volume += item.itemInfo.volume;
		bagObject.itemInfo.weight += item.itemInfo.weight;

		if(bagObject.transform.parent != null)
			if(bagObject.transform.parent.GetComponent<ItemObject>() != null)
				if(bagObject.transform.parent.GetComponent<ItemObject>().itemInfo.GetType() == typeof(Container))
					addFill(item,bagObject.transform.parent.GetComponent<ItemObject>());
	}

	public bool canDropItem() //check to see if there is room to drop the item
	{
		if(dropItemCheck.hitCount != 0)
		{
			Debug.Log("can't drop item");
			return false;
		}
		return true;
	}
}
