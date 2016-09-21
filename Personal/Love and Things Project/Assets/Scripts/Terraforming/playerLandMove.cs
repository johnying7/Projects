using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerLandMove : MonoBehaviour {

	/// <summary>
	/// The player component that calculates the nearest main handle 
	/// </summary>

	float playerHeight = 1.05f;
	float aboveLand = 0.5f; //extra amount to move the player ABOVE land
	RaycastHit landRay; //used for detecting the collision of the terrain tile
	
	public Transform MainCamera; //main player camera
	public Transform terrainTile; //used for the tile under the players feet

	int layerMask = 1 << 10; //shift layer mask 10 to a variable

	public Collider[] tilePointColliders = new Collider[4]; //stores collision objects at the tile intersection dig point

	void OnTriggerEnter(Collider hitObject)
	{
		if(hitObject.gameObject.tag == "Terrain") //everytime the player steps on a new "Terrain" tile
		{
			terrainTile = hitObject.transform; //make that tile the currently stepped on one
		}
	}

	void Update()
	{
//		if(Input.GetKeyDown(KeyCode.Mouse0) && MainCamera.GetComponent<Inventory2>().isInventoryOn == false)
//		{
//			moveHandle(-0.5f);
//		}
//		if(Input.GetKeyDown(KeyCode.Mouse1) && MainCamera.GetComponent<Inventory2>().isInventoryOn == false)
//		{
//			moveHandle(0.5f);
//		}
	}

	public void moveHandle(float adjustAmount) //moves the land and player (use this to call the multiplayer function)
	{
		if(MainCamera.GetComponent<Inventory2>().shovelEquipped)
		{
			Vector3 oldPoint = DigPoint(); //store the dig point in a non-changing variable (when looping and changing physics)
			tilePointColliders = Physics.OverlapSphere(DigPoint(), 1.0f, layerMask); //get all terrain tiles connect to the dig point within 1.0f radius

			for(int i = 0; i < tilePointColliders.Length; i++)
			{
				tilePointColliders[i].transform.GetComponent<tileManager>().changeMesh(oldPoint,adjustAmount);
			}
			checkDrop(); //move the player above the new terrain change to prevent falling through the ground
		}
	}

	public bool isHandleMovable()
	{
		tilePointColliders = Physics.OverlapSphere(DigPoint(), 1.0f, layerMask);

		for(int i = 0; i < tilePointColliders.Length; i++)
		{
			if(tilePointColliders[i].transform.GetComponent<Renderer>().material.name == "TownLand (Instance)")
				return false;
		}
		return true;
	}

	//moves player up after terraforming the ground
	void checkDrop()
	{
		if(Physics.Raycast(this.transform.position, Vector3.down, out landRay,Mathf.Infinity,layerMask))
		{
			if(landRay.collider.gameObject.tag == "Terrain")
			{
				Vector3 temp;
				temp = this.transform.position;
				temp.y = landRay.point.y + playerHeight + aboveLand;
				transform.position = temp;
			}
		}
	}

	Vector3 DigPoint() //finds the nearest dig point to the character
	{
		Vector3 temp;

		//gets one of the tiles vertices list at the dig point x and z position
		Vector3[] tileVerts = terrainTile.GetComponent<tileManager>().mesh.vertices;//grab stood on tile vertices

		temp = this.transform.position;
		temp.x = Mathf.Round(temp.x * 0.1f) * 10; //finds the nearest tile corner X
		temp.z = Mathf.Round(temp.z * 0.1f) * 10; //finds the nearest tile corner Z

		//finds the nearest Y tile corner by using the stood on terrain tile
		for(int i = 0; i < tileVerts.Length; i++)
		{
			if(terrainTile.position.x + tileVerts[i].x == temp.x &&
			   terrainTile.position.z + tileVerts[i].z == temp.z)
				temp.y = terrainTile.position.y + tileVerts[i].y;
		}
		return temp;//returns the desired dig point
	}
}
