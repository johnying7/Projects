using UnityEngine;
using System.Collections;

public class TownNodes : MonoBehaviour {

//    /// <summary>
//    /// This is used to hold a group/collection of nodes representing a "town."
//    /// </summary>
//
//    NodeManager nm;
//
//    private int rowAndColumn;
//    private int row;
//    private int column;
//    private Transform[,] nodeController;//a 2D array that holds transform/position nodes
//
//    public GameObject baseNode;
//
//	// Use this for initialization
//	void Start () {
//
//        nm = GameObject.Find("GameManager").GetComponent<NodeManager>();
//
//        initializeRowAndColumn();
//
//        nodeController = new Transform[row,column];
//        createTown(row, column);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
//    void createTown(int row, int column)
//    {
//        for(int k = 0; k < row; k++)//subhorizontal x-group
//        {
//            for(int l = 0; l < column; l++)//subvertical y-group
//            {
//                newNode = Instantiate(baseNode);
//                nodeController[k,l] = newNode.transform;
//                nodeController[k,l].position = new Vector3((i*100+k*10),0,(j*100+l*10));
//                nodeController[k,l].SetParent(this.transform);
//            }
//        }
//    }
//
//    void initializeRowAndColumn()//imports row and column variables from node manager for town specs
//    {
//        row = nm.townRow;
//        column = nm.townColumn;
//        rowAndColumn = nm.townRowAndColumn;
//
//        if (rowAndColumn != 0)
//        {
//            row = rowAndColumn;
//            column = rowAndColumn;
//        }
//    }
}
