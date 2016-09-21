using UnityEngine;
using System.Collections;

public class tileManager : MonoBehaviour {

	public Mesh mesh;
	public Vector3[] verts; //temporary changing for the vertices positions based on the tile mesh position

	// Use this for initialization
	public void Awake () {
		mesh = GetComponent<MeshFilter>().mesh;
		verts = mesh.vertices; //gets the default mesh positions
	}

	//changes a corner of this mesh that matches the old point and changes it by the adjustAmount
	public void changeMesh(Vector3 oldPoint, float adjustAmount)
	{
		//Debug.Log("Old Point: " + oldPoint);
		mesh = GetComponent<MeshFilter>().mesh;
		verts = mesh.vertices;
		for(int i = 0; i < verts.Length; i++)
		{
			if(verts[i] + transform.position == oldPoint) //if the vertex matches the oldPoint
			{
				verts[i].y = oldPoint.y - transform.position.y + adjustAmount; //adjust the vertex
			}
		}

		//when switching meshes, make sure to double check that the CENTER vertex is the averaged one (randomly different per mesh)
		verts[0].y = (verts[2].y + verts[1].y + verts[3].y + verts[4].y) / 4; //averages the center vertex
		mesh.vertices = verts; //applies the changes to the actual mesh

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		resetMeshCollider();
	}

	//changes a corner of this mesh that matches the old point and changes it by the adjustAmount
	public void setMesh(Vector3 oldPoint, float adjustAmount)
	{
		mesh = GetComponent<MeshFilter>().mesh;
		verts = mesh.vertices;
		for(int i = 0; i < verts.Length; i++)
			if(verts[i] == oldPoint) //if the vertex matches the oldPoint
				verts[i].y = oldPoint.y - transform.position.y + adjustAmount; //adjust the vertex

		verts[2].y = (verts[0].y + verts[1].y + verts[3].y + verts[4].y) / 4; //averages the center vertex
		mesh.vertices = verts; //applies the changes to the actual mesh
		
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		resetMeshCollider();
	}

	public Vector3[] getVerts()
	{
		verts = GetComponent<MeshFilter>().mesh.vertices;
		return verts;
	}

	void resetMeshCollider() //resets the physics mesh collider to represent the modified terrain mesh
	{
		GetComponent<MeshCollider>().sharedMesh = null;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	public void printLog(Vector3 oldPoint, int k)
	{
		Vector3[] verticesArray = mesh.vertices;
		Debug.Log(verticesArray[k]);
		if(verts[k] + transform.position == oldPoint)
		{
			Debug.Log("the points match");
		}
	}

	public void zeroTile() //completely flattens the tile (used for terrain creation)
	{
		mesh = GetComponent<MeshFilter>().sharedMesh;
		verts = mesh.vertices;
		verts[0].y = 0.0f;
		verts[1].y = 0.0f;
		verts[2].y = 0.0f;
		verts[3].y = 0.0f;
		verts[4].y = 0.0f;
		mesh.vertices = verts;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		resetMeshCollider();
	}

	public bool isTileLevel()
	{
		verts = mesh.vertices;
		float matchHeight;
		matchHeight = verts[0].y;

		for(int j = 1; j < verts.Length; j++)
		{
			if(Mathf.Approximately(verts[j].y, matchHeight))
			{
				return false;
			}
		}
		return true;
	}
}
