using UnityEngine;
using System.Collections;

public class QuestManager : MonoBehaviour {
	
	#region Character Names and their Current Dialogue speech
	public int Carl = 0;
	public GameObject CarlCharacter;
	#endregion
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void QuestCheck(GameObject QuestGameObject)
	{
		if(QuestGameObject.transform.parent.tag == "Bag" && Carl == 0)
		{
			Debug.Log("Quest complete.");
			Carl = 1;
			CarlCharacter.GetComponent<DialogCreator>().curDialog = 1;
			QuestCompleted();
		}
	}
	
	private void QuestCompleted()
	{
		//play the victory quest completed sound
	}
}
