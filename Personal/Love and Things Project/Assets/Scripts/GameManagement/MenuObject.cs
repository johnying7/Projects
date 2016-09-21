using UnityEngine;
using System.Collections;

public class MenuObject : MonoBehaviour {
	
	public bool isQuit = false;
	public bool isPlay = false;
	public bool isOptions = false;
	public bool optionsToggle = false;
	public float volume;
	
	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.blue;
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.green;
	}
	
	void OnMouseDown()
	{
		if(isQuit)
		{
			Application.Quit ();
		}
		else if(isPlay)
		{
			Application.LoadLevel(1);
		}
		else if(isOptions)
		{
			optionsToggle = true;
		}
	}
	
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.green;
		volume = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(optionsToggle == true)
		{
			AudioListener.volume = volume;
		}
	}
	
	void OnGUI()
	{
		if(optionsToggle == true)
		{
			GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 600, 440), "Options Menu");

			GUI.Label(new Rect(Screen.width/2 - 296, Screen.height /2 - 110, 100, 30), "Volume");
			volume = GUI.HorizontalSlider(new Rect(Screen.width/2 - 296, Screen.height /2 - 110+20, 100, 30), volume, 0.0f, 1.0f);

			GUI.Label(new Rect(Screen.width/2 - 296, Screen.height /2 -110+40, 590, 380),
			          "Bugs: \n" +
			          "\n" +
			          "Difficult to access the inventory when the resolution is smaller than 800x600.\n" +
			          "\n" +
			          "Controls:\n" +
			          "\n" +
			          "Esc: Options Window\n" +
			          "WASD: Movement\n" +
			          "Space: Jump\n" +
			          "F: Action (Talk / Use Workbench / Loot World Item)\n" +
			          "LMB: Use equipped tool or weapon\n" +
			          "LMB (Inventory): Pickup or place item in inventory / Drop Item In World\n" +
			          "Right Mouse Button: Add item to assemble list (at workbench)\n" +
			          "\n" +
			          "To craft:\n" +
			          "Place items appropriately from inventory onto work bench.Right click tool handle\n" +
			          "and then right click tool head. Then click the assemble button.\n");

			if(GUI.Button(new Rect(Screen.width/2 - 50-56 , Screen.height /2 + 240, 100, 30), "Close"))
			{
				optionsToggle = false;
			}

			if(GUI.Button(new Rect(Screen.width/2 - 50+56 , Screen.height /2 + 240, 100, 30), "Exit Game"))
			{
				Application.Quit();
			}
		}
	}
}
