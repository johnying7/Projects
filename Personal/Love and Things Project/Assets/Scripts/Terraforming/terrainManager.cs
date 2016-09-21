using UnityEngine;
using System.Collections;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class terrainManager : MonoBehaviour {

	public chunkManager[] chunks = new chunkManager[100];
	public terrainObject terrainObj = new terrainObject();

	private fileManager fm = new fileManager();
	private bool isDataEncrypted = false;
	Vector3 temp = new Vector3(0,0,0);
	public void Awake()
	{
		fm.initialize();
		fm.createDirectory("Database"); //creates a file directory if one does not exist
		if(fm.checkFile("Database/Terrain.dat") == true) //if file exists, use it
		{
			Debug.Log("File Terrain.dat found. Please load terrain during RUNTIME.");
		}
		else //else create a new file if there isn't one
		{
			Debug.Log("Creating new file");
			fm.createFile("Database","Terrain","dat","",isDataEncrypted);
		}
	}

	public void loadTerrainDatabase()//takes a file and stores it to the serialized class
	{
		BinaryFormatter bf = new BinaryFormatter();

		//decrypts or reads file, converts to bits and puts that into a stream
		MemoryStream ms = new MemoryStream(Convert.FromBase64String(fm.useFile("Database", "Terrain", "dat", isDataEncrypted)));
		terrainObj = (terrainObject)bf.Deserialize(ms); //transfers byte stream info to terrainObj

		loadTerrainData();
		}

	public void saveTerrainDatabase()//takes the serialized class and stores it to file
	{
		storeTerrainData();//stores mesh info to class

		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();

		bf.Serialize(ms,terrainObj);//changes terrainObj into a byte array and stores it into ms
		fm.updateFile("Database","Terrain","dat",Convert.ToBase64String(ms.GetBuffer()),isDataEncrypted, true);
	}

	void storeTerrainData()//gathers the vertices of the tiles and stores them into one serialized class
	{
		for(int i = 0; i < 25; i++) //chunk index
		{
			for(int j = 0; j < 100; j++) //tile index
			{
				for(int k = 0; k < 5; k++) //vert index
				{
					terrainObj.chunks[i].tiles[j].savedVerts[k].x = chunks[i].tiles[j].getVerts()[k].x;
					terrainObj.chunks[i].tiles[j].savedVerts[k].y = chunks[i].tiles[j].getVerts()[k].y;
					terrainObj.chunks[i].tiles[j].savedVerts[k].z = chunks[i].tiles[j].getVerts()[k].z;
				}
			}
		}
	}

	public void loadTerrainData()//takes the terrainObj class object and applies it to all meshes
	{
		for(int i = 0; i < 25; i++) //chunk index
		{
			for(int j = 0; j < 100; j++) //tile index
			{
				for(int k = 0; k < 5; k++) //vert index
				{

					float adjAmount;

					temp.x = terrainObj.chunks[i].tiles[j].savedVerts[k].x;
					adjAmount = terrainObj.chunks[i].tiles[j].savedVerts[k].y;
					temp.z = terrainObj.chunks[i].tiles[j].savedVerts[k].z;

					chunks[i].tiles[j].setMesh(temp, adjAmount);
				}
			}
		}
	}
}
