using UnityEngine;
using System.Collections;
using UnityEditor;

public class TerrainCreator : MonoBehaviour {

	[MenuItem("Terrain Manager/Create Chunk")]
	static void CreateChunk()
	{
		GameObject chunk = (GameObject)Instantiate(Resources.Load("emptyChunk"), new Vector3(0,0,0), Quaternion.Euler(0,0,0));
		chunk.name = "chunk";
		for(int i = 0; i < 10; i++)
		{
			for(int j = 0; j < 10; j++)
			{
				GameObject tempTile;
				tempTile = (GameObject)Instantiate(Resources.Load("terrainTile"),
				                                   new Vector3((i*10), 0, (j*10)),
				                                   Quaternion.Euler(0,0,0));
				chunk.GetComponent<chunkManager>().tiles[(i*10)+j] = tempTile.GetComponent<tileManager>();
				tempTile.transform.parent = chunk.transform;
			}
		}
	}

	[MenuItem("Terrain Manager/Create Terrain")]
	static void CreateTerrain()
	{
		GameObject terrainManager = (GameObject)Instantiate(Resources.Load("TerrainManager"), Vector3.zero, Quaternion.Euler(0,0,0));
		terrainManager.tag = "TerrainManager";

		for(int l = 0; l < 5; l++)
		{
			for(int k = 0; k < 5; k++)
			{
				GameObject chunk = (GameObject)Instantiate(Resources.Load("emptyChunk"), new Vector3(0,0,0), Quaternion.Euler(0,0,0));
				chunk.name = "chunk" + ((l*5)+k);
				terrainManager.GetComponent<terrainManager>().chunks[(l*5)+k] = chunk.GetComponent<chunkManager>();
				for(int i = 0; i < 10; i++)
				{
					for(int j = 0; j < 10; j++)
					{
						GameObject tempTile;
						tempTile = (GameObject)Instantiate(Resources.Load("terrainTile"),
						                                   new Vector3((i*10), 0, (j*10)),
						                                   Quaternion.Euler(0,0,0));
						tempTile.GetComponent<tileManager>().zeroTile();
						chunk.GetComponent<chunkManager>().tiles[(i*10)+j] = tempTile.GetComponent<tileManager>();
						tempTile.transform.parent = chunk.transform;
					}
				}
				chunk.transform.parent = terrainManager.transform;
				chunk.transform.position = new Vector3(k*100, 0, l*100);
			}
		}
	}

//	[MenuItem("Terrain Manager/Create Terrain")]
//	static void CreateTerrain()
//	{
//		GameObject terrainManager = (GameObject)Instantiate(Resources.Load("TerrainManager"), Vector3.zero, Quaternion.Euler(0,0,0));
//
//		for(int l = 0; l < 10; l++)
//		{
//			for(int k = 0; k < 10; k++)
//			{
//				GameObject chunk = (GameObject)Instantiate(Resources.Load("emptyChunk"), new Vector3(0,0,0), Quaternion.Euler(0,0,0));
//				chunk.name = "chunk" + ((l*10)+k);
//				chunk.GetComponent<chunkManager>().chunkIndex = (l*10)+k;
//				terrainManager.GetComponent<terrainManager>().chunks[(l*10)+k] = chunk.transform;
//				for(int i = 0; i < 10; i++)
//				{
//					for(int j = 0; j < 10; j++)
//					{
//						GameObject tempTile;
//						tempTile = (GameObject)Instantiate(Resources.Load("terrainTile"),
//						                                   new Vector3((i*10), 0, (j*10)),
//						                                   Quaternion.Euler(0,0,0));
//						tempTile.GetComponent<tileManager>().Awake();
//						chunk.GetComponent<chunkManager>().tiles[(i*10)+j] = tempTile.transform;
//						tempTile.GetComponent<tileManager>().tileIndex = (i*10)+j;
//						tempTile.transform.parent = chunk.transform;
//					}
//				}
//				chunk.transform.parent = terrainManager.transform;
//				chunk.transform.position = new Vector3(k*100, 0, l*100);
//			}
//		}
//	}

	[MenuItem ("Terrain Manager/Save Terrain")]
	static void saveTerrain()
	{
		GameObject tm = GameObject.FindWithTag("TerrainManager");
		tm.GetComponent<terrainManager>().Awake();
		tm.GetComponent<terrainManager>().saveTerrainDatabase();
	}

	[MenuItem ("Terrain Manager/Load Terrain")]
	static void loadTerrain()
	{
		GameObject tm = GameObject.FindWithTag("TerrainManager");
		tm.GetComponent<terrainManager>().Awake();
		tm.GetComponent<terrainManager>().loadTerrainDatabase();
	}
}
