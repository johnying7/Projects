using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogCreator : MonoBehaviour {
	
	public List<Dialog> dialogList = new List<Dialog>();
	public int idCount = 0;
	
	public int curDialog = 0;
	
	public Dialog GetDialog()
	{
		return dialogList[curDialog];
	}
	
	public Dialog FindDialogByID(int id)
	{
		for (int i = 0; i < dialogList.Count; i++)
		{
			if (dialogList[i].id == id)
			{
				return dialogList[i];
			}
		}
		
		return dialogList[0];
	}
}
