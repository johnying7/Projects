using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(DialogCreator))]
internal class DialogCreatorInspector : Editor {
		
	public override void OnInspectorGUI()
	{
		DialogCreator dc = target as DialogCreator;
		
		dc.idCount = EditorGUILayout.IntField("ID Count: ", dc.idCount);
		
		for(int i = 0; i < dc.dialogList.Count; i++)
		{				
			EditorGUILayout.BeginHorizontal();
			dc.dialogList[i].dialogOption = EditorGUILayout.Foldout(dc.dialogList[i].dialogOption, "Option " + dc.dialogList[i].id);
			if(GUILayout.Button("DELETE"))
			{
				dc.dialogList.RemoveAt(i);
			}
			EditorGUILayout.EndHorizontal();

			if(dc.dialogList[i].dialogOption == true)
			{
				EditorGUI.indentLevel = 2;
				
				dc.dialogList[i].id = EditorGUILayout.IntField("Dialog ID: ", dc.dialogList[i].id);
				dc.dialogList[i].text = EditorGUILayout.TextArea(dc.dialogList[i].text);

				EditorGUI.indentLevel = 0;
			}
		}
		
		if(GUILayout.Button("Add New Dialog"))
		{
			Dialog newDialog = (Dialog)ScriptableObject.CreateInstance<Dialog>();
			newDialog.text = "Enter new dialog here.";
			newDialog.id = dc.idCount;
			newDialog.dialogOption = false;
			dc.idCount++;
			dc.dialogList.Add(newDialog);
		}
	}
}
