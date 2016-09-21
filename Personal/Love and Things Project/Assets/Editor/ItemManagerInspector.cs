using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

[CustomEditor(typeof(ItemManager))]
internal class ItemManagerInspector : Editor {
	
	bool showingWeapons = false;
	bool showingArmor = false;
	bool showingContainers = false;
	bool showingConsumables = false;
	bool showingShields = false;
	bool showingJewelry = false;

	bool isDataEncrypted = true;
	
	void OnEnable()
	{
		ItemManager im = target as ItemManager;
		im.fm.initialize();
		im.fm.createDirectory("Database");
		if(im.fm.checkFile("Database/Items.xml") == true) //if file exists, use it
		{
			loadItemDatabase();
		}
		else
		{
			XmlDocument xDoc = new XmlDocument();
			XmlElement root = xDoc.CreateElement("ItemDatabase");
			xDoc.AppendChild(root);
			im.fm.createFile("Database","Items","xml",xDoc.OuterXml,isDataEncrypted);
		}
	}
	
	void loadItemDatabase()
	{		
		ItemManager im = target as ItemManager;
		//XmlDocument xmlDoc = new XmlDocument();
		//xmlDoc.Load(Application.dataPath + "/SaveData/Database/Items.xml");
		
		XmlDocument xmlDoc = im.fm.useXmlFile("Database","Items","xml",isDataEncrypted); //returns a decrypted xml document from the directory
		
		im.itemList.Clear(); //removes all objects in the list
		im.itemList.TrimExcess(); //realocates max capacity of list in memory based on filled objects
		im.idCount = 0;

		XmlNodeList idCountElement = xmlDoc.GetElementsByTagName("idCount");
		im.idCount = int.Parse(idCountElement[0].InnerText);
		
		XmlNodeList itemXmlList = xmlDoc.GetElementsByTagName("Item");
		foreach(XmlNode itemInfo in itemXmlList)
		{
			
			XmlNodeList itemInfoList = itemInfo.ChildNodes;
			loadItem(im,itemInfoList,itemInfo);
		}
		
	}
	
	void saveItemDatabase()
	{		
		ItemManager im = target as ItemManager;
		
		XmlDocument xml = new XmlDocument();
		
		XmlDeclaration xmlDecl;
		xmlDecl = xml.CreateXmlDeclaration("1.0","UTF-8","yes");
		xml.AppendChild(xmlDecl);
		
		XmlElement rootElement = im.fm.createXmlElement(xml, "ItemDatabase");
		xml.AppendChild(rootElement);
		
		XmlElement idCountElement = im.fm.createXmlElement(xml,"idCount",im.idCount.ToString());
		rootElement.AppendChild(idCountElement);
		
		for (int i = 0; i < im.itemList.Count; i++)
		{
			XmlElement itemElement = im.fm.createXmlElement(xml,"Item",null,"ItemType",im.itemList[i].GetType().ToString());
			itemElement.AppendChild(im.fm.createXmlElement(xml,"name",im.itemList[i].name));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"description",im.itemList[i].description));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"cost",im.itemList[i].cost.ToString()));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"icon", im.itemList[i].icon.name));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"volume",im.itemList[i].volume.ToString()));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"weight",im.itemList[i].weight.ToString()));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"id",im.itemList[i].id.ToString()));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"maxDurability",im.itemList[i].maxDurability.ToString()));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"curDurability",im.itemList[i].curDurability.ToString()));
			itemElement.AppendChild(im.fm.createXmlElement(xml,"equipType",im.itemList[i].equipType.ToString())); 

			if(im.itemList[i].GetType() == typeof(Weapon))
				itemElement.AppendChild(im.fm.createXmlElement(xml,"damage",((Weapon)im.itemList[i]).damage.ToString()));
			else if(im.itemList[i].GetType() == typeof(Armor))
				itemElement.AppendChild(im.fm.createXmlElement(xml,"defense",((Armor)im.itemList[i]).defense.ToString()));
			else if(im.itemList[i].GetType() == typeof(Shield))
				itemElement.AppendChild(im.fm.createXmlElement(xml,"guard",((Shield)im.itemList[i]).guard.ToString()));
			else if(im.itemList[i].GetType() == typeof(Jewelry))
				itemElement.AppendChild(im.fm.createXmlElement(xml,"hpBuff",((Jewelry)im.itemList[i]).hpBuff.ToString()));
			else if(im.itemList[i].GetType() == typeof(Container))
				itemElement.AppendChild(im.fm.createXmlElement(xml,"holdVolume",((Container)im.itemList[i]).holdVolume.ToString()));
			else if(im.itemList[i].GetType() == typeof(Consumable))
				itemElement.AppendChild(im.fm.createXmlElement(xml,"stack",((Consumable)im.itemList[i]).stack.ToString()));
			
			rootElement.AppendChild(itemElement);
		}
		im.fm.updateFile("Database","Items","xml",xml.OuterXml,isDataEncrypted,true);
//		im.fm.deleteFile("Database/Items.xml");
//		im.fm.createFile("Database","Items","xml",xml.OuterXml,false);
	}
	
	public override void OnInspectorGUI()
	{
		ItemManager im = target as ItemManager;
		
		List<Weapon> weapons = new List<Weapon>();
		List<Armor> armors = new List<Armor>();
		List<Container> containers = new List<Container>();
		List<Consumable> consumables = new List<Consumable>();
		List<Shield> shields = new List<Shield>();
		List<Jewelry> jewelries = new List<Jewelry>();
		
		#region Fill Created Lists
		for(int i = 0; i < im.itemList.Count; i++)
		{
			if (im.itemList[i].GetType() == typeof(Weapon))
			{
				weapons.Add((Weapon)im.itemList[i]);
			}
			
			if (im.itemList[i].GetType() == typeof(Armor))
			{
				armors.Add((Armor)im.itemList[i]);
			}
			
			if (im.itemList[i].GetType() == typeof(Container))
			{
				containers.Add((Container)im.itemList[i]);
			}
			
			if (im.itemList[i].GetType() == typeof(Consumable))
			{
				consumables.Add((Consumable)im.itemList[i]);
			}
			
			if (im.itemList[i].GetType() == typeof(Shield))
			{
				shields.Add((Shield)im.itemList[i]);
			}
			
			if (im.itemList[i].GetType() == typeof(Jewelry))
			{
				jewelries.Add((Jewelry)im.itemList[i]);
			}
		}
		#endregion
		
		#region Initialize Button
		if(GUILayout.Button("Initialize List"))
		{
			Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			newWeapon.name = "No Item";
			newWeapon.description = "";
			newWeapon.cost = 0;
			newWeapon.maxDurability = 0;
			newWeapon.curDurability = newWeapon.maxDurability;
			newWeapon.damage = 0;
			newWeapon.equipType = EquipTag.OneHanded;
			newWeapon.icon = Resources.Load("icon_emptyItem") as Texture2D;
			newWeapon.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newWeapon);
			
			Armor newArmor1 = (Armor)ScriptableObject.CreateInstance<Armor>();
			newArmor1.name = "Shirt";
			newArmor1.description = "Keeps the nips nice and fresh.";
			newArmor1.cost = 10;
			newArmor1.maxDurability = 100;
			newArmor1.curDurability = newArmor1.maxDurability;
			newArmor1.icon = Resources.Load("icon_shirt1") as Texture2D;
			newArmor1.defense = 1;
			newArmor1.equipType = EquipTag.Chest;
			newArmor1.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newArmor1);
			
			Armor newArmor2 = (Armor)ScriptableObject.CreateInstance<Armor>();
			newArmor2.name = "Gloves";
			newArmor2.description = "Helps prevent blisters.";
			newArmor2.cost = 10;
			newArmor2.maxDurability = 100;
			newArmor2.icon = Resources.Load("icon_gloves1") as Texture2D;
			newArmor2.defense = 1;
			newArmor2.equipType = EquipTag.Hands;
			newArmor2.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newArmor2);
			
			Armor newArmor3 = (Armor)ScriptableObject.CreateInstance<Armor>();
			newArmor3.name = "Pants";
			newArmor3.description = "Cold breezes on the privates can be both a blessing and a curse.";
			newArmor3.cost = 10;
			newArmor3.maxDurability = 100;
			newArmor3.curDurability = newArmor3.maxDurability;
			newArmor3.icon = Resources.Load("icon_pants1") as Texture2D;
			newArmor3.defense = 1;
			newArmor3.equipType = EquipTag.Legs;
			newArmor3.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newArmor3);
			
			Armor newArmor4 = (Armor)ScriptableObject.CreateInstance<Armor>();
			newArmor4.name = "Sandals";
			newArmor4.description = "Only slightly better than going barefoot.";
			newArmor4.cost = 10;
			newArmor4.maxDurability = 100;
			newArmor4.curDurability = newArmor4.maxDurability;
			newArmor4.icon = Resources.Load("icon_shoes1") as Texture2D;
			newArmor4.defense = 1;
			newArmor4.equipType = EquipTag.Feet;
			newArmor4.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newArmor4);
			
			Container newContainer1 = (Container)ScriptableObject.CreateInstance<Container>();
			newContainer1.name = "Small Pouch";
			newContainer1.description = "Useful for quick access and visibility to small items.";
			newContainer1.cost = 20;
			newContainer1.icon = Resources.Load("icon_container1") as Texture2D;
			newContainer1.holdVolume = 20;
			newContainer1.equipType = EquipTag.Pouch;
			newContainer1.id = im.idCount;
			im.idCount++;
			im.itemList.Add (newContainer1);
			
			Container newContainer2 = (Container)ScriptableObject.CreateInstance<Container>();
			newContainer2.name = "Small Bag";
			newContainer2.description = "If it fits, it can go in!";
			newContainer2.cost = 40;
			newContainer2.icon = Resources.Load("icon_container1") as Texture2D;
			newContainer2.holdVolume = 40;
			newContainer2.equipType = EquipTag.Bag;
			newContainer2.id = im.idCount;
			im.idCount++;
			im.itemList.Add (newContainer2);
			
			Jewelry newJewelry = (Jewelry)ScriptableObject.CreateInstance<Jewelry>();
			newJewelry.name = "Engagement Ring";
			newJewelry.description = "A thing like this would definitely increase a persons moral with it in their possession.";
			newJewelry.cost = 5000;
			newJewelry.maxDurability = 100;
			newJewelry.curDurability = newJewelry.maxDurability;
			newJewelry.icon = Resources.Load("icon_ring1") as Texture2D;
			newJewelry.equipType = EquipTag.Jewelry;
			newJewelry.id = im.idCount;
			im.idCount++;
			im.itemList.Add (newJewelry);
			
			Weapon newWeapon1 = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			newWeapon1.name = "Iron Hatchet";
			newWeapon1.description = "Useful for cutting down trees and chopping them into logs.";
			newWeapon1.cost = 30;
			newWeapon1.maxDurability = 50;
			newWeapon1.curDurability = newWeapon1.maxDurability;
			newWeapon1.damage = 3;
			newWeapon1.equipType = EquipTag.TwoHanded;
			newWeapon1.icon = Resources.Load("icon_axe3") as Texture2D;
			newWeapon1.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newWeapon1);
			
			Weapon newWeapon2 = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			newWeapon2.name = "Iron Pickaxe";
			newWeapon2.description = "Used to mine ore from stone.";
			newWeapon2.cost = 30;
			newWeapon2.maxDurability = 50;
			newWeapon2.curDurability = newWeapon2.maxDurability;
			newWeapon2.damage = 3;
			newWeapon2.equipType = EquipTag.TwoHanded;
			newWeapon2.icon = Resources.Load("icon_pickaxe2") as Texture2D;
			newWeapon2.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newWeapon2);
			
			Weapon newWeapon3 = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			newWeapon3.name = "Iron Knife";
			newWeapon3.description = "An all purpose cutting knife.";
			newWeapon3.cost = 30;
			newWeapon3.maxDurability = 50;
			newWeapon3.curDurability = newWeapon3.maxDurability;
			newWeapon3.damage = 3;
			newWeapon3.equipType = EquipTag.OneHanded;
			newWeapon3.icon = Resources.Load("icon_dagger2") as Texture2D;
			newWeapon3.id = im.idCount;
			im.idCount++;
			//don't forget to add the other variable numbers
			im.itemList.Add (newWeapon3);
		}
		#endregion
		
		if(GUILayout.Button("Save/Load"))
		{
			saveItemDatabase();
			
			if(im.fm.checkFile("Database/Items.xml") == true) //if file exists, use it
			{
				//XmlDocument xmlDoc = im.fm.useXmlFile("Database","Items","xml",false);
				loadItemDatabase();
			}
			else
			{
				Debug.Log("file did not save properly to load");
			}
		}
		
		im.idCount = EditorGUILayout.IntField("ID Count: ", im.idCount); //idCount saved in im for variable persistence
		
		#region Weapon Dropdown
		showingWeapons = EditorGUILayout.Foldout(showingWeapons, "Weapons"); //showingWeapons = changes bool by dropdown arrow
		if (showingWeapons == true) //if true, make the list visible
		{
			EditorGUI.indentLevel = 2; //number of times to tab the following guitext
			
			for (int i = 0; i < weapons.Count; i++) //display all of the weapons in our database
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(weapons[i].name);
				if(GUILayout.Button("-"))
				{
					im.itemList.Remove(weapons[i]);
				}
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel += 1;
				//changes the variable by user input
				weapons[i].id = int.Parse(EditorGUILayout.TextField("ID: ", weapons[i].id.ToString()));
				weapons[i].name = EditorGUILayout.TextField("Name: ", weapons[i].name);
				weapons[i].description = EditorGUILayout.TextField("Description: ", weapons[i].description);				
				weapons[i].cost = EditorGUILayout.IntField("Cost: ", weapons[i].cost);
				weapons[i].volume = EditorGUILayout.IntField("Volume: ", weapons[i].volume);
				weapons[i].weight = EditorGUILayout.IntField("Weight: ", weapons[i].weight);
				
				weapons[i].equipType = (EquipTag)EditorGUILayout.EnumPopup("Weapon Type: ", weapons[i].equipType);
				switch(weapons[i].equipType)
				{
				case EquipTag.OneHanded:
					weapons[i].equipType = EquipTag.OneHanded;
					break;
				case EquipTag.TwoHanded:
					weapons[i].equipType = EquipTag.TwoHanded;
					break;
				}
				
				weapons[i].maxDurability = EditorGUILayout.IntField("Durability: ", weapons[i].maxDurability);
				weapons[i].curDurability = weapons[i].maxDurability;
				weapons[i].damage = EditorGUILayout.IntField("Damage: ", weapons[i].damage);
				weapons[i].icon = (Texture2D)EditorGUILayout.ObjectField(weapons[i].icon, typeof(Texture2D), false);
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space(); //adds space between objects
			}
			
			if (GUILayout.Button("Add New Weapon"))
			{
				Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
				newWeapon.name = "NEW WEAPON";
				newWeapon.description = "";
				newWeapon.cost = 0;
				newWeapon.maxDurability = 0;
				newWeapon.curDurability = newWeapon.maxDurability;
				newWeapon.damage = 0;
				newWeapon.equipType = EquipTag.OneHanded;
				newWeapon.id = im.idCount;
				im.idCount++;
				//don't forget to add the other variable numbers
				im.itemList.Add (newWeapon);
			}
			
			EditorGUI.indentLevel = 0; //makes sure only this dropdown is indented
		}
		#endregion
		
		#region Armor Dropdown
		showingArmor = EditorGUILayout.Foldout(showingArmor, "Armor");
		if (showingArmor == true)
		{
			EditorGUI.indentLevel = 2;
			for (int i = 0; i < armors.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(armors[i].name);
				if(GUILayout.Button("-"))
				{
					im.itemList.Remove(armors[i]);
				}
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel += 1;
				armors[i].id = int.Parse(EditorGUILayout.TextField("ID: ", armors[i].id.ToString()));
				armors[i].name = EditorGUILayout.TextField("Name: ", armors[i].name);
				armors[i].description = EditorGUILayout.TextField("Description: ", armors[i].description);
				armors[i].cost = EditorGUILayout.IntField("Cost: ", armors[i].cost);
				armors[i].volume = EditorGUILayout.IntField("Volume: ", armors[i].volume);
				armors[i].weight = EditorGUILayout.IntField("Weight: ", armors[i].weight);
				
				armors[i].equipType = (EquipTag)EditorGUILayout.EnumPopup("Armor Type: ", armors[i].equipType);
				switch(armors[i].equipType)
				{
				case EquipTag.Head:
					armors[i].equipType = EquipTag.Head;
					break;
				case EquipTag.Chest:
					armors[i].equipType = EquipTag.Chest;
					break;
				case EquipTag.Hands:
					armors[i].equipType = EquipTag.Hands;
					break;
				case EquipTag.Legs:
					armors[i].equipType = EquipTag.Legs;
					break;
				case EquipTag.Feet:
					armors[i].equipType = EquipTag.Feet;
					break;
				}
				
				armors[i].maxDurability = EditorGUILayout.IntField("Durability: ", armors[i].maxDurability);
				armors[i].curDurability = armors[i].maxDurability;
				armors[i].defense = EditorGUILayout.IntField("Defense: ", armors[i].defense);
				armors[i].icon = (Texture2D)EditorGUILayout.ObjectField(armors[i].icon, typeof(Texture2D), false);
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space(); //adds space between objects
			}
			
			if (GUILayout.Button("Add New Armor"))
			{
				Armor newArmor = (Armor)ScriptableObject.CreateInstance<Armor>();
				newArmor.name = "NEW ARMOR";
				newArmor.description = "";
				newArmor.cost = 0;
				newArmor.maxDurability = 0;
				newArmor.curDurability = newArmor.maxDurability;
				newArmor.defense = 0;
				newArmor.equipType = EquipTag.Head;
				newArmor.id = im.idCount;
				im.idCount++;
				//don't forget to add the other variable numbers
				im.itemList.Add (newArmor);
			}
			
			EditorGUI.indentLevel = 0; //makes sure only this dropdown is indented	
		}
		#endregion
		
		#region Container Dropdown
		showingContainers = EditorGUILayout.Foldout(showingContainers, "Containers");
		if (showingContainers == true)
		{
			EditorGUI.indentLevel = 2;
			for (int i = 0; i < containers.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(containers[i].name);
				if(GUILayout.Button("-"))
				{
					im.itemList.Remove(containers[i]);
				}
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel += 1;
				containers[i].id = int.Parse(EditorGUILayout.TextField("ID: ", containers[i].id.ToString()));
				containers[i].name = EditorGUILayout.TextField("Name: ", containers[i].name);
				containers[i].description = EditorGUILayout.TextField("Description: ", containers[i].description);
				containers[i].cost = EditorGUILayout.IntField("Cost: ", containers[i].cost);
				containers[i].volume = EditorGUILayout.IntField("Volume: ", containers[i].volume);
				containers[i].weight = EditorGUILayout.IntField("Weight: ", containers[i].weight);
				
				containers[i].equipType = (EquipTag)EditorGUILayout.EnumPopup("Container Type: ", containers[i].equipType);
				switch(containers[i].equipType)
				{
				case EquipTag.Bag:
					containers[i].equipType = EquipTag.Bag;
					break;
				case EquipTag.Pouch:
					containers[i].equipType = EquipTag.Pouch;
					break;
				}
				
				containers[i].holdVolume = EditorGUILayout.IntField("Hold Volume: ", containers[i].holdVolume);
				containers[i].icon = (Texture2D)EditorGUILayout.ObjectField(containers[i].icon, typeof(Texture2D), false);
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space(); //adds space between objects
			}
			
			if (GUILayout.Button("Add New Container"))
			{
				Container newContainer = (Container)ScriptableObject.CreateInstance<Container>();
				newContainer.name = "NEW CONTAINER";
				newContainer.description = "";
				newContainer.cost = 0;
				newContainer.holdVolume = 0;
				newContainer.equipType = EquipTag.Bag;
				newContainer.id = im.idCount;
				im.idCount++;
				im.itemList.Add (newContainer);
			}
			
			EditorGUI.indentLevel = 0; //makes sure only this dropdown is indented	
		}
		#endregion
		
		#region Consumable Dropdown
		showingConsumables = EditorGUILayout.Foldout(showingConsumables, "Consumables");
		if (showingConsumables == true)
		{
			EditorGUI.indentLevel = 2;
			for (int i = 0; i < consumables.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(consumables[i].name);
				if(GUILayout.Button("-"))
				{
					im.itemList.Remove(consumables[i]);
				}
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel += 1;
				consumables[i].id = int.Parse(EditorGUILayout.TextField("ID: ", consumables[i].id.ToString()));
				consumables[i].name = EditorGUILayout.TextField("Name: ", consumables[i].name);
				consumables[i].description = EditorGUILayout.TextField("Description: ", consumables[i].description);
				consumables[i].cost = EditorGUILayout.IntField("Cost: ", consumables[i].cost);
				consumables[i].volume = EditorGUILayout.IntField("Volume: ", consumables[i].volume);
				consumables[i].weight = EditorGUILayout.IntField("Weight: ", consumables[i].weight);
				consumables[i].icon = (Texture2D)EditorGUILayout.ObjectField(consumables[i].icon, typeof(Texture2D), false);
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space(); //adds space between objects
			}
			
			if (GUILayout.Button("Add New Consumable"))
			{
				Consumable newConsumable = (Consumable)ScriptableObject.CreateInstance<Consumable>();
				newConsumable.name = "NEW CONSUMABLE";
				newConsumable.description = "";
				newConsumable.cost = 0;
				newConsumable.stack = 1;
				newConsumable.equipType = EquipTag.Other;
				newConsumable.id = im.idCount;
				im.idCount++;
				im.itemList.Add (newConsumable);
			}
			
			EditorGUI.indentLevel = 0; //makes sure only this dropdown is indented	
		}
		#endregion
		
		#region Shield Dropdown
		showingShields = EditorGUILayout.Foldout(showingShields, "Shields");
		if (showingShields == true)
		{
			EditorGUI.indentLevel = 2;
			for (int i = 0; i < shields.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shields[i].name);
				if(GUILayout.Button("-"))
				{
					im.itemList.Remove(shields[i]);
				}
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel += 1;
				shields[i].id = int.Parse(EditorGUILayout.TextField("ID: ", shields[i].id.ToString()));
				shields[i].name = EditorGUILayout.TextField("Name: ", shields[i].name);
				shields[i].description = EditorGUILayout.TextField("Description: ", shields[i].description);
				shields[i].cost = EditorGUILayout.IntField("Cost: ", shields[i].cost);
				shields[i].volume = EditorGUILayout.IntField("Volume: ", shields[i].volume);
				shields[i].weight = EditorGUILayout.IntField("Weight: ", shields[i].weight);
				shields[i].maxDurability = EditorGUILayout.IntField("Durability: ", shields[i].maxDurability);
				shields[i].curDurability = shields[i].maxDurability;
				shields[i].guard = EditorGUILayout.IntField("Guard: ", shields[i].guard);
				shields[i].icon = (Texture2D)EditorGUILayout.ObjectField(shields[i].icon, typeof(Texture2D), false);
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space(); //adds space between objects
			}
			
			if (GUILayout.Button("Add New Shield"))
			{
				Shield newShield = (Shield)ScriptableObject.CreateInstance<Shield>();
				newShield.name = "NEW SHIELD";
				newShield.description = "";
				newShield.cost = 0;
				newShield.maxDurability = 0;
				newShield.curDurability = newShield.maxDurability;
				newShield.guard = 0;
				newShield.equipType = EquipTag.OneHanded;
				newShield.id = im.idCount;
				im.idCount++;
				im.itemList.Add (newShield);
			}
			
			EditorGUI.indentLevel = 0; //makes sure only this dropdown is indented	
		}
		#endregion
		
		#region Jewelry Dropdown
		showingJewelry = EditorGUILayout.Foldout(showingJewelry, "Jewelries");
		if (showingJewelry == true)
		{
			EditorGUI.indentLevel = 2;
			for (int i = 0; i < jewelries.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(jewelries[i].name);
				if(GUILayout.Button("-"))
				{
					im.itemList.Remove(jewelries[i]);
				}
				EditorGUILayout.EndHorizontal();
				
				EditorGUI.indentLevel += 1;
				jewelries[i].id = int.Parse(EditorGUILayout.TextField("ID: ", jewelries[i].id.ToString()));
				jewelries[i].name = EditorGUILayout.TextField("Name: ", jewelries[i].name);
				jewelries[i].description = EditorGUILayout.TextField("Description: ", jewelries[i].description);
				jewelries[i].cost = EditorGUILayout.IntField("Cost: ", jewelries[i].cost);
				jewelries[i].volume = EditorGUILayout.IntField("Volume: ", jewelries[i].volume);
				jewelries[i].weight = EditorGUILayout.IntField("Weight: ", jewelries[i].weight);
				jewelries[i].maxDurability = EditorGUILayout.IntField("Durability: ", jewelries[i].maxDurability);
				jewelries[i].curDurability = jewelries[i].maxDurability;
				jewelries[i].icon = (Texture2D)EditorGUILayout.ObjectField(jewelries[i].icon, typeof(Texture2D), false);
				EditorGUI.indentLevel -= 1;
				EditorGUILayout.Space(); //adds space between objects
			}
			
			if (GUILayout.Button("Add New Jewelry"))
			{
				Jewelry newJewelry = (Jewelry)ScriptableObject.CreateInstance<Jewelry>();
				newJewelry.name = "NEW JEWELRY";
				newJewelry.description = "";
				newJewelry.cost = 0;
				newJewelry.maxDurability = 0;
				newJewelry.curDurability = newJewelry.maxDurability;
				newJewelry.equipType = EquipTag.Jewelry;
				newJewelry.id = im.idCount;
				im.idCount++;
				im.itemList.Add (newJewelry);
			}
			
			EditorGUI.indentLevel = 0; //makes sure only this dropdown is indented	
		}
		#endregion
	}
	
	void loadItem(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		loadWeapon(im,itemInfoList,itemInfo);
		loadArmor(im,itemInfoList,itemInfo);
		loadContainer(im,itemInfoList,itemInfo);
		loadConsumable(im,itemInfoList,itemInfo);
		loadShield(im,itemInfoList,itemInfo);
		loadJewelry(im,itemInfoList,itemInfo);
	}
	
	void loadWeapon(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		if(itemInfo.Attributes["ItemType"].Value == "Weapon")
		{
			Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
			foreach(XmlNode profileItems in itemInfoList)
			{
		
					if(profileItems.Name == "name")
						newWeapon.name = profileItems.InnerText;
					else if(profileItems.Name == "description")
						newWeapon.description = profileItems.InnerText;
					else if(profileItems.Name == "cost")
						newWeapon.cost = int.Parse(profileItems.InnerText);
					else if(profileItems.Name == "icon")
						newWeapon.icon = Resources.Load(profileItems.InnerText) as Texture2D;
					else if(profileItems.Name == "volume")
						newWeapon.volume = int.Parse(profileItems.InnerText);
					else if(profileItems.Name == "weight")
						newWeapon.weight = int.Parse(profileItems.InnerText);
					else if(profileItems.Name == "id")
						newWeapon.id = int.Parse(profileItems.InnerText);
					else if(profileItems.Name == "maxDurability")
						newWeapon.maxDurability = int.Parse(profileItems.InnerText);
					else if(profileItems.Name == "curDurability")
						newWeapon.curDurability = int.Parse(profileItems.InnerText);
					else if(profileItems.Name == "equipType")
						newWeapon.equipType = (EquipTag)System.Enum.Parse(typeof(EquipTag),profileItems.InnerText);
					else if(profileItems.Name == "damage")
						newWeapon.damage = int.Parse(profileItems.InnerText);
			}
			im.itemList.Add (newWeapon);
		}
	}
	
	void loadArmor(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		if(itemInfo.Attributes["ItemType"].Value == "Armor")
		{
			Armor newArmor = (Armor)ScriptableObject.CreateInstance<Armor>();
			foreach(XmlNode profileItems in itemInfoList)
			{
	
				if(profileItems.Name == "name")
					newArmor.name = profileItems.InnerText;
				else if(profileItems.Name == "description")
					newArmor.description = profileItems.InnerText;
				else if(profileItems.Name == "cost")
					newArmor.cost = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "icon")
					newArmor.icon = Resources.Load(profileItems.InnerText) as Texture2D;
				else if(profileItems.Name == "volume")
					newArmor.volume = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "weight")
					newArmor.weight = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "id")
					newArmor.id = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "maxDurability")
					newArmor.maxDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "curDurability")
					newArmor.curDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "equipType")
					newArmor.equipType = (EquipTag)System.Enum.Parse(typeof(EquipTag),profileItems.InnerText);
				else if(profileItems.Name == "defense")
					newArmor.defense = int.Parse(profileItems.InnerText);
			}
			im.itemList.Add (newArmor);
		}
	}
	
	void loadContainer(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		if(itemInfo.Attributes["ItemType"].Value == "Container")
		{			
			Container newContainer = (Container)ScriptableObject.CreateInstance<Container>();
			foreach(XmlNode profileItems in itemInfoList)
			{
				if(profileItems.Name == "name")
					newContainer.name = profileItems.InnerText;
				else if(profileItems.Name == "description")
					newContainer.description = profileItems.InnerText;
				else if(profileItems.Name == "cost")
					newContainer.cost = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "icon")
					newContainer.icon = Resources.Load(profileItems.InnerText) as Texture2D;
				else if(profileItems.Name == "volume")
					newContainer.volume = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "weight")
					newContainer.weight = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "id")
					newContainer.id = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "maxDurability")
					newContainer.maxDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "curDurability")
					newContainer.curDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "equipType")
					newContainer.equipType = (EquipTag)System.Enum.Parse(typeof(EquipTag),profileItems.InnerText);
				else if(profileItems.Name == "holdVolume")
					newContainer.holdVolume = int.Parse(profileItems.InnerText);
			}
			im.itemList.Add (newContainer);
		}
	}
	
	void loadConsumable(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		if(itemInfo.Attributes["ItemType"].Value == "Consumable")
		{			
			Consumable newConsumable = (Consumable)ScriptableObject.CreateInstance<Consumable>();
			foreach(XmlNode profileItems in itemInfoList)
			{
				if(profileItems.Name == "name")
					newConsumable.name = profileItems.InnerText;
				else if(profileItems.Name == "description")
					newConsumable.description = profileItems.InnerText;
				else if(profileItems.Name == "cost")
					newConsumable.cost = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "icon")
					newConsumable.icon = Resources.Load(profileItems.InnerText) as Texture2D;
				else if(profileItems.Name == "volume")
					newConsumable.volume = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "weight")
					newConsumable.weight = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "id")
					newConsumable.id = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "maxDurability")
					newConsumable.maxDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "curDurability")
					newConsumable.curDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "equipType")
					newConsumable.equipType = (EquipTag)System.Enum.Parse(typeof(EquipTag),profileItems.InnerText);
				else if(profileItems.Name == "stack")
					newConsumable.stack = int.Parse(profileItems.InnerText);
			}
			im.itemList.Add (newConsumable);
		}
	}
	
	void loadShield(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		if(itemInfo.Attributes["ItemType"].Value == "Shield")
		{			
			Shield newShield = (Shield)ScriptableObject.CreateInstance<Shield>();
			foreach(XmlNode profileItems in itemInfoList)
			{
				if(profileItems.Name == "name")
					newShield.name = profileItems.InnerText;
				else if(profileItems.Name == "description")
					newShield.description = profileItems.InnerText;
				else if(profileItems.Name == "cost")
					newShield.cost = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "icon")
					newShield.icon = Resources.Load(profileItems.InnerText) as Texture2D;
				else if(profileItems.Name == "volume")
					newShield.volume = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "weight")
					newShield.weight = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "id")
					newShield.id = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "maxDurability")
					newShield.maxDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "curDurability")
					newShield.curDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "equipType")
					newShield.equipType = (EquipTag)System.Enum.Parse(typeof(EquipTag),profileItems.InnerText);
				else if(profileItems.Name == "guard")
					newShield.guard = int.Parse(profileItems.InnerText);
			}
			im.itemList.Add (newShield);
		}
	}
	
	void loadJewelry(ItemManager im, XmlNodeList itemInfoList, XmlNode itemInfo)
	{
		if(itemInfo.Attributes["ItemType"].Value == "Jewelry")
		{			
			Jewelry newJewelry = (Jewelry)ScriptableObject.CreateInstance<Jewelry>();
			foreach(XmlNode profileItems in itemInfoList)
			{
				if(profileItems.Name == "name")
					newJewelry.name = profileItems.InnerText;
				else if(profileItems.Name == "description")
					newJewelry.description = profileItems.InnerText;
				else if(profileItems.Name == "cost")
					newJewelry.cost = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "icon")
					newJewelry.icon = Resources.Load(profileItems.InnerText) as Texture2D;
				else if(profileItems.Name == "volume")
					newJewelry.volume = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "weight")
					newJewelry.weight = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "id")
					newJewelry.id = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "maxDurability")
					newJewelry.maxDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "curDurability")
					newJewelry.curDurability = int.Parse(profileItems.InnerText);
				else if(profileItems.Name == "equipType")
					newJewelry.equipType = (EquipTag)System.Enum.Parse(typeof(EquipTag),profileItems.InnerText);
				else if(profileItems.Name == "hpBuff")
					newJewelry.hpBuff = int.Parse(profileItems.InnerText);
			}
			im.itemList.Add (newJewelry);
		}
	}
}
