using UnityEngine;
using System.Collections;

public class SightTrigger : SightInventory { //attach script to player character's camera
	
	public float actionRange; //references range at which to notice the helm
	private RaycastHit actionSight; //variable for creating the vector that hits objects
	private GameObject sightedObject; //declares a gameobject variable to access collided objects scripts & resources
	private Transform[] litChildren; //to store the children of the currently highlighted buildingblock
	private bool dialogActive;

	public GameObject dropPosition;

	public Transform outlinedTerrain;
	private Shader diffuse;
	private Shader outlinedDiffuse;
	float digAdjust = 0.0f;

	ItemManager im;
	QuestManager qm;

	//used with the raycast to ignore the terrain
	public LayerMask layerMask;
	
	[HideInInspector]
	public Rect _aimToolTipRect;

	Texture2D cooldownTexture;
	float percentage;
	float count;
	float endTime;
	bool isTimerOn;
	bool isTimerFinished;
	GameObject aimedObject;

	public Transform cameraPos;
	public Transform workbench;

	public bool isPlayerCameraSet = true;
	// Use this for initialization
	void Start () {
		if (actionRange == 0.0f) //if the editor doesn't have a specific action range set, set it to 5.0f
			actionRange = 5.0f;

		//used to outline the terrain tiles
		outlinedTerrain = null;
		diffuse = Shader.Find("Diffuse");
		outlinedDiffuse = Shader.Find("Outlined/Silhouetted Diffuse");


		_aimToolTipRect = new Rect(Screen.width-300, 0, 200, 50);
		dialogActive = false;
		im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
		qm = playerCharacter.GetComponent<QuestManager>();
		dropPosition = GameObject.Find("DropPosition");
		//SightCrosshair initialization
		halfCHSize = cHSize / 2;
		whiteCrosshair = true;
		
		//SightMotorControl initialization
		playerCharacter = transform.parent.gameObject;
		mamAimMode();
		mouseVisible = false;
		motorControl = true;
		
		//SightInventory initialization
		inv = GetComponent<Inventory2>();
		cooldownTexture = (Texture2D)Resources.Load("cooldown",typeof(Texture2D));
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 sightDir = transform.TransformDirection(Vector3.forward); //make the character (forward) camera direction into a variable
		if (Physics.Raycast(transform.position, sightDir, out actionSight, actionRange,layerMask)) //detect if the created ray hits an object with a range of actionRange
		{
			collisionCheck(actionSight);
		}
		else //if the player is not looking at anything useable
		{
			if(sightIsActive() && inv.isInventoryOn != true)
				whiteSight();

			if(outlinedTerrain != null)
			{
				outlinedTerrain.GetComponent<Renderer>().material.shader = diffuse;
				outlinedTerrain = null;
			}
			isTimerOn = false;
			emptyHighlight();
		}
		Debug.DrawRay(transform.position, sightDir * actionRange, Color.yellow); //debugs the ray so it is visible in the scene editor mode
		
		if(dialogActive)//if dialog activated, turn the npc to the character through lerp
		{
			Vector3 targetDirection;
			sightedObject = actionSight.collider.gameObject;
			targetDirection = this.transform.position - sightedObject.transform.position;
			targetDirection.y = 0.0f;
			sightedObject.transform.rotation = Quaternion.Slerp(sightedObject.transform.rotation, Quaternion.LookRotation(targetDirection), .1f);
		}

		if(Input.GetKeyDown(KeyCode.I) && workbench != null && workbench.GetComponent<Workbench>().isUsingWorkbench)
		{
			workbench.GetComponent<Workbench>().isUsingWorkbench = false;
			workbench.GetComponent<Rigidbody>().isKinematic = false;
			isPlayerCameraSet = false;
			StartCoroutine(MoveObject(this.transform.position, cameraPos.position, 0.5f));
			StartCoroutine(RotateObject(this.transform.rotation, cameraPos.rotation, 0.5f));
			workbench = null;
						dropPosition.SetActive (true);
		}

		if(isPlayerCameraSet == false && this.transform.position == cameraPos.position)
		{
			isPlayerCameraSet = true;
			mamAimMode();
		}
	}

	public void startTimer(float timerLength, GameObject collidedObj) //initiate the cooldown timer
	{
		isTimerOn = true;
		isTimerFinished = false;
		count = 0;
		endTime = timerLength;
		percentage = 0;
		aimedObject = collidedObj; //used to check if the player is still aiming at the resource, otherwise cancel the timer
	}

	void OnGUI() {//all OnGUI() calls must be in parent class (not children)

		_aimToolTipRect = ClampToScreen(GUI.Window(3, _aimToolTipRect, AimToolTip, "View"));

		//manages the crosshair colors in SightCrosshair Class
		if (greenCrosshair == true)
			GUI.Label(new Rect(Screen.width / 2 - halfCHSize, Screen.height / 2 - halfCHSize, cHSize, cHSize), greenTexture);
		else if (redCrosshair == true)
			GUI.Label(new Rect(Screen.width / 2 - halfCHSize, Screen.height / 2 - halfCHSize, cHSize, cHSize), redTexture);
		else if (whiteCrosshair == true)
			GUI.Label(new Rect(Screen.width / 2 - halfCHSize, Screen.height / 2 - halfCHSize, cHSize, cHSize), whiteTexture);

		if(isTimerOn) //create the timer bar
		{
			GUI.Box(new Rect(Screen.width/2-128, Screen.height/2-8+128,256,16),"");
			GUI.DrawTexture(new Rect(Screen.width/2-128, Screen.height/2-8+128, percentage*256,16),cooldownTexture);
		}
	}

	public void AimToolTip(int windowID)
	{
		if(actionSight.collider != null)
		{
			if(actionSight.collider.gameObject.tag == "NPC" ||
				actionSight.collider.gameObject.tag == "BuildingBlock")
			{
				GUILayout.Box(actionSight.collider.gameObject.name);
			}
			else if(actionSight.collider.gameObject.tag == "Terrain")
			{
				GUILayout.Box("Terrain");
			}
			else if(actionSight.collider.gameObject.name == "HitCollision")
			{
				GUILayout.Box("Zombie");
			}
			else if(actionSight.collider.gameObject.tag == "Well")
			{
				GUILayout.Box(actionSight.collider.gameObject.name);
			}
			else if(actionSight.collider.gameObject.tag == "Ore")
			{
				GUILayout.Box(actionSight.collider.gameObject.name);
			}
			else if(actionSight.collider.transform.parent != null &&
				actionSight.collider.transform.parent.tag == "Item" ||
				actionSight.collider.transform.parent.tag == "Bag")
			{
				GUILayout.Box(actionSight.collider.transform.parent.name);
			}// if(actionSight.collider.transform.parent.tag == "Untagged")
		}
		GUI.DragWindow(new Rect(0,0,_aimToolTipRect.width,18));
	}
	private void increaseTimer(RaycastHit obj)
	{
		if(isTimerOn && aimedObject == obj.collider.gameObject)
		{
			if(count < endTime)
			{
				count += Time.deltaTime;
				percentage = count/endTime;
			}
			else if(count >= endTime)
			{
				isTimerOn = false;
				isTimerFinished = true;
			}
		}
		else
			isTimerOn = false;
	}
	private void collisionCheck(RaycastHit hitObject)
	{
		highlightObject(hitObject, "BuildingBlock");

		increaseTimer(hitObject);

		if (hitObject.collider.gameObject.tag == "Enemy")
		{
			redSight();
		}
		else if (hitObject.collider.gameObject.tag == "NPC")
		{
			greenSight();
			if(Input.GetKeyDown(KeyCode.F))
			{
				mamToggle(); //in the SightMotorControl script
				toggleDialog();
			}
		}
		else if (hitObject.collider.transform.tag == "Terrain")
		{
			if(GetComponent<Inventory2>().shovelEquipped)
			{
				if( !isTimerOn && !inv.isInventoryOn && inv.curWeapon.name == "Iron Shovel")
				{
					if(Input.GetMouseButtonDown(0) && this.transform.parent.GetComponent<playerLandMove>().isHandleMovable()){
						startTimer(1, hitObject.collider.gameObject);
						digAdjust = -0.5f;
					}
					else if(Input.GetMouseButtonDown(1) && this.transform.parent.GetComponent<playerLandMove>().isHandleMovable()){
						startTimer(1, hitObject.collider.gameObject);
						digAdjust = 0.5f;
					}
				}
				
				if(isTimerFinished)
				{
					isTimerFinished = false;
					
					//execute timer finish action
					if(this.transform.parent.GetComponent<playerLandMove>().isHandleMovable())
						this.transform.parent.GetComponent<playerLandMove>().moveHandle(digAdjust);
				}

				if(outlinedTerrain == null)
				{
					outlinedTerrain = hitObject.collider.transform;
					hitObject.collider.transform.GetComponent<Renderer>().material.shader = outlinedDiffuse;
				}
				if(outlinedTerrain != hitObject.collider.transform)
				{
					outlinedTerrain.GetComponent<Renderer>().material.shader = diffuse;
					hitObject.collider.transform.GetComponent<Renderer>().material.shader = outlinedDiffuse;
				}
				
				outlinedTerrain = hitObject.collider.transform;
				greenSight();
			}
			else
			{
				whiteSight();
				hitObject.collider.transform.GetComponent<Renderer>().material.shader = diffuse;
			}
		}
		else if(hitObject.collider.transform.tag == "Well")
		{
			greenSight();
			if(Input.GetKeyDown(KeyCode.F))
			{
				this.transform.parent.GetComponent<Hunger>().curHunger = 100;
			}
		}
		else if(hitObject.collider.transform.tag == "Ore")
		{
			greenSight();
			if(Input.GetMouseButtonDown(0) && !isTimerOn && !inv.isInventoryOn && inv.curWeapon.name == "Iron Pickaxe")
				startTimer(1, hitObject.collider.gameObject);
			
			if(isTimerFinished)
			{
				isTimerFinished = false;
				
				//returns true if log needs to be instantiated
				if(hitObject.collider.transform.GetComponent<OreObject>().chop())
				{
					GameObject gameObj = (GameObject)Instantiate(Resources.Load("Iron Ore"),inv.dropPosition.transform.position, Quaternion.identity);//instantiate a log at drop position
					gameObj.name = "Iron Ore";
				}
			}
		}
		else if(hitObject.collider.transform.parent.tag != null)//section specifically for objects which have a parent tag
		{
			if (hitObject.collider.transform.parent.tag == "Item")
			{
				greenSight();

				if(Input.GetKeyDown(KeyCode.F))
				{
					if(hitObject.collider.transform.parent.GetComponent<ItemObject>().itemInfo.GetType() == typeof(Container))
					{
						if(!inv.isWearingBag()) //if not wearing a bag
							equipBag(hitObject.collider.transform.parent.gameObject);//wear the bag
						else //if a bag is being worn, store the new bag in your worn bag
							lootItem(hitObject.collider.transform.parent.gameObject);//loot the item

						qm.QuestCheck(hitObject.collider.gameObject);
					}
					else if(inv.isWearingBag()) //if wearing a bag
						lootItem(hitObject.collider.transform.parent.gameObject);
					else
						lootEquip(hitObject.collider.transform.parent.gameObject);//equips the item if one is not already equipped
				}
			}
			else if(hitObject.collider.transform.parent.tag == "Tree")
			{
				greenSight();

				if(Input.GetMouseButtonDown(0) && !isTimerOn && !inv.isInventoryOn && inv.curWeapon.name == "Iron Woodaxe")
					startTimer(1, hitObject.collider.gameObject);

				if(isTimerFinished)
				{
					isTimerFinished = false;

					//returns true if log needs to be instantiated
					if(hitObject.collider.transform.parent.GetComponent<TreeObject>().chop(((Weapon)inv.curWeapon.itemInfo).damage))
					{
						GameObject gameObj = (GameObject)Instantiate(Resources.Load("Log"),inv.dropPosition.transform.position,Quaternion.identity);//instantiate a log at drop position
						gameObj.name = "Log";
					}
				}
			}
			else if (hitObject.collider.transform.parent.tag == "Workbench")
			{
				greenSight();
				if(Input.GetKeyDown(KeyCode.F))
				{
					if(hitObject.collider.transform.parent.GetComponent<Workbench>().isBenchLevel() &&
					   !hitObject.collider.transform.parent.GetComponent<Workbench>().isUsingWorkbench)
					{
						dropPosition.SetActive (false);
						mamGuiMode();
						inv.isInventoryOn = true;
						workbench = hitObject.collider.transform.parent;
						workbench.GetComponent<Workbench>().isUsingWorkbench = true;
						workbench.GetComponent<Rigidbody>().isKinematic = true;
						shiftObject(this.transform, workbench.GetComponent<Workbench>().workbenchView, 1.0f);
					}
				}
			}
		}
	}

	//clears the layer back to default so that the color can be normalized.
	private void emptyHighlight()
	{
		if(litChildren != null)
		{
			for(int i = 1; i < litChildren.Length; i++)
			{
				litChildren[i].gameObject.layer = 0;
			}
			playerCharacter.GetComponent<Light>().enabled = false;
			litChildren = null;
		}
	}

	//changes the objects layer (so that the color can be changed) {Note: Not efficient}
	private void highlightObject(RaycastHit curHitObject, string hightlightTag)
	{
		if(curHitObject.collider.tag == hightlightTag)
		{
			greenSight();
			litChildren = curHitObject.collider.transform.GetComponentsInChildren<Transform>();
			for(int i = 1; i < litChildren.Length; i++)
			{
				litChildren[i].gameObject.layer = 9;
			}
				playerCharacter.GetComponent<Light>().enabled = true;
		}
		else if(curHitObject.collider.tag != hightlightTag)
		{
			if(litChildren != null)
			{
				for(int i = 1; i < litChildren.Length; i++)
				{
					litChildren[i].gameObject.layer = 0;
				}
				playerCharacter.GetComponent<Light>().enabled = false;
				litChildren = null;
			}
		}
	}

	void toggleDialog()
	{
		Debug.Log("toggledialog called");
		Debug.Log(dialogActive);
		if(!dialogActive) //activate dialog
		{
			///make sighted object y and player y equal zero
			
			noSight();
			sightedObject = actionSight.collider.gameObject;
			DialogCreator dc = sightedObject.GetComponent<DialogCreator>();
			DialogDisplay dd = GetComponent<DialogDisplay>();
			dd.enabled = true;
			dd.startDialog(sightedObject.name, dc.GetDialog().text);

			dialogActive = true;
		}
		else if(dialogActive) //deactivate dialog
		{
			whiteSight();
			DialogDisplay dd = GetComponent<DialogDisplay>();
			dd.closeDialog();
			dd.enabled = false;

			dialogActive = false;
		}
	}

	public Rect ClampToScreen(Rect r)
	{
		r.x = Mathf.Clamp(r.x,0,Screen.width-r.width);
		r.y = Mathf.Clamp(r.y,0,Screen.height-r.height);
		return r;
	}

	IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime)
	{
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		transform.position = target;
	}

	IEnumerator RotateObject(Quaternion source, Quaternion target, float overTime)
	{
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			transform.rotation = Quaternion.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		transform.rotation = target;
	}

	public void shiftObject(Transform beginTransform, Transform endTransform, float overTime)
	{
		StartCoroutine(MoveObject(beginTransform.position, endTransform.position, overTime));
		StartCoroutine(RotateObject(beginTransform.rotation, endTransform.rotation, overTime));
	}
}
