using UnityEngine;
using System.Collections;

public class NodeCreator : MonoBehaviour {

	public NodeManager nm;
	public GameObject baseNode;

	GameObject newNode;

    public int townRow = 2;
    public int townColumn = 2;

    public int stateSizeWidth = 4;
    public int stateSizeHeight = 4;

    public Transform[,,,] nodeController;//a 4D array that holds transform nodes

	//int w,x,y,z = 0;
	//int count = 0;//counts how many nodes are created in one frame

	// Use this for initialization
	void Start () {
        nodeController = new Transform[stateSizeWidth, stateSizeHeight, townRow, townColumn];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createMap()
	{
        for(int i = 0; i < stateSizeWidth; i++)//horizontal group
		{
            for(int j = 0; j < stateSizeHeight; j++)//vertical group
			{
                for(int k = 0; k < townRow; k++)//subhorizontal x-group
				{
                    for(int l = 0; l < townColumn; l++)//subvertical y-group
					{
						newNode = Instantiate(baseNode);
						nodeController[i,j,k,l] = newNode.transform;
						nodeController[i,j,k,l].position = new Vector3((i*100+k*10),0,(j*100+l*10));
						nodeController[i,j,k,l].SetParent(this.transform);
					}
				}
			}
		}
	}

	public void setMap()//sets connecting path points
	{
        for(int i = 0; i < stateSizeWidth; i++)//horizontal group
		{
            for(int j = 0; j < stateSizeHeight; j++)//vertical group
			{
                for(int k = 0; k < townRow; k++)//subhorizontal x-group
				{
                    for(int l = 0; l < townColumn; l++)//subvertical y-group
					{
						if( (i != 0 || i != 9) && (k != 0 || k != 9)
							&& (j != 0 || l != 0) && (j != 9 || l != 9))
						{
							//nodeController[i,j,k,l].GetComponent<Node>().neighbor.Add();
						}

						if( i == 0 && k == 0)//if on the top group, top row
						{
                            if (j == 0 && l == 0)//if on the top left node
                            {
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k + 1, l]);
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k, l + 1]);
                            }
                            else if (j == stateSizeHeight && l == townColumn)//if on the top right node
                            {
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k - 1, l]);
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k, l - 1]);
                            }
                            else//if on the top row, no corners (middle section)
                            {
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k, l - 1]);
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k, l + 1]);
                                nodeController[i, j, k, l].GetComponent<Node>().neighbor.Add(
                                    nodeController[i, j, k + 1, l]);
                            }
							
						}
						else if( l == 0 && j == 0)//if on the left column (side)
						{
							
						}
						else if ( i == 9 && k == 9)//if on the bottom row
						{
							
						}
						else if (l == 9 && j == 9)//if on the right column (side)
						{
							
						}


					}
				}
			}
		}
	}
}
