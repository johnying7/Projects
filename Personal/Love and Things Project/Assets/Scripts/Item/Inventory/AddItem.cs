using UnityEngine;
using System.Collections;

public class AddItem : MonoBehaviour {
	
	public Item[] loot;
	public Inventory inventory;
	
	// Use this for initialization
  	void Start () {
		inventory = gameObject.GetComponent<Inventory>();
	}
	
	void OnGUI() {
		GUILayout.BeginArea(new Rect(420,10,100,50));
		if(GUILayout.Button("Add Loot!"))
		{
			GiveLoot();
		}
		GUILayout.EndArea();
	}
	
	void GiveLoot() {
		for (int x = 0; x < loot.Length; x++)
		{
			//inventory.playerInventory.Add(loot[x]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
