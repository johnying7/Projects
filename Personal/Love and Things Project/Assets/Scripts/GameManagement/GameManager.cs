using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int day;
	public int night;

	public Transform logger;
	public Transform blacksmith;

	public List<GameObject> zombieList = new List<GameObject>();
	public GameObject sacredGround;
	public bool charge = false;
	private bool optionsToggle = false;
	public Transform mainCamera;
	float volume;
	// Use this for initialization
	void Start () {
		day = 0;
		night = 0;
		volume = 1.0f;
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(optionsToggle == true) //if options window is on, turn off
			{
				optionsToggle = false;
				if(!mainCamera.GetComponent<Inventory2>().isInventoryOn)
					mainCamera.GetComponent<SightTrigger>().mamAimMode();
			}
			else if(optionsToggle == false) //if options window is off, turn on
			{
				AudioListener.volume = volume;
				optionsToggle = true;
				mainCamera.GetComponent<SightTrigger>().mamGuiMode();
			}
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

	public void spawnAll()
	{
		spawn1();
		spawn2 ();
		spawn3 ();
		spawn4 ();
	}

	public void spawn1()
	{
		int xPos = Random.Range(50,300);
		int zPos = Random.Range(50,200);

		spawn(xPos, zPos);
	}

	public void spawn2()
	{
		int xPos = Random.Range(300,450);
		int zPos = Random.Range(50,300);
		
		spawn(xPos, zPos);
	}

	public void spawn3()
	{
		int xPos = Random.Range(200,450);
		int zPos = Random.Range(300,450);
		
		spawn(xPos, zPos);
	}

	public void spawn4()
	{
		int xPos = Random.Range(50,200);
		int zPos = Random.Range(200,450);
		
		spawn(xPos, zPos);
	}

	private void spawn(int xPos, int zPos)
	{
		RaycastHit[] hitObjs = Physics.RaycastAll(new Vector3(xPos,100,zPos),Vector3.down);
		for(int i = 0; i < hitObjs.Length; i++)
		{
			if(hitObjs[i].collider.tag == "Terrain")
			{
				Vector3 temp = hitObjs[i].point;
				temp.y += 2;
				GameObject zombie = Instantiate(Resources.Load("Zombie"), temp, Quaternion.identity)as GameObject;
				if(charge)
				{
					zombie.GetComponent<NpcState>().isCharging = true;
				}
				zombieList.Add(zombie);
				return;
			}
		}
	}

	public void gameSuccess()
	{
		Application.LoadLevel("success");//load success scene
	}
	
	public void gameDeathFailure()
	{
		Application.LoadLevel("Failed");//load death failure scene
	}
	
	public void gameContaminationFailure()
	{
		Application.LoadLevel("Failed");//load contamination failure scene
	}

	public void zombieCharge()
	{
		for(int i = 0; i < zombieList.Count; i++)
		{
			zombieList[i].GetComponent<NpcState>().isCharging = true;
		}
	}

	public void increaseDay()
	{
		Debug.Log("increase day: " + day);
		mainCamera.GetComponent<AudioSource>().Play();
		if(day == 2)
		{
			zombieCharge();
			charge = true;
			sacredGround.SetActive(false);
		}
		if(day == 3) //7 -> 8
		{
			gameSuccess();//end game success
		}
		day += 1;
	}

	public void increaseNight()
	{
		night += 1;
		mainCamera.GetComponent<AudioSource>().Stop();
	}
}
