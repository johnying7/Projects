using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NodeCreator))]
public class NodeCreatorEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		NodeCreator nc = (NodeCreator)target;
		if(nc.nm == null)
		{
			nc.nm = nc.GetComponent<NodeManager>();
            nc.baseNode = (GameObject)Resources.Load("Prefabs/Node",typeof(GameObject));
		}
		
		if(GUILayout.Button("Build Map"))
		{
			nc.createMap();

		}

		if(GUILayout.Button("Set Map"))
		{
			nc.setMap();

		}
	}
}
