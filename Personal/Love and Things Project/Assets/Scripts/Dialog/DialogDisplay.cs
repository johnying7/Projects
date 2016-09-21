using UnityEngine;
using System.Collections;

public class DialogDisplay : MonoBehaviour {
	
	private Rect dialogBox;
	private Rect textBox;
	private Rect npcNameBox;
	
	private bool _toggleDialog = false;
	private bool _dialogComplete = false;
	
	private string inputName;
	private string displayDialog;
	private char[] dialogArray;
	
	private float timer;
	private float displaySpeed;
	private float wordSpace;
	private float endSentenceWait;
	
	private int count; //counts which character in the array to add next to the string.
	
	void Start() {
		createDialogRects();
		saveCheck();
	}
	
	void OnGUI()
	{
		if(_toggleDialog)
		{
			GUI.Box(dialogBox, "");
			GUI.Label(npcNameBox, inputName);
			GUI.Label(textBox, displayDialog);
		}
	}
	
	void Update() {
		updateDialog();
	}
	
	private void createDialogRects()
	{
		float dialogBoxHeight = 160.0f;
		float textBoxWidth = 800.0f;
		float textBoxSpace = 10.0f;
		float npcNameBoxHeight = 30.0f;

		dialogBox = new Rect((Screen.width * 0.5f) - (textBoxWidth * 0.5f),Screen.height-dialogBoxHeight,textBoxWidth,dialogBoxHeight);
		
		npcNameBox = new Rect((Screen.width * 0.5f) - (textBoxWidth * 0.5f) + textBoxSpace,
			Screen.height-dialogBoxHeight + textBoxSpace,
			textBoxWidth - textBoxSpace - textBoxSpace,
			npcNameBoxHeight);
		
		textBox = new Rect((Screen.width * 0.5f) - (textBoxWidth * 0.5f) + textBoxSpace,
			Screen.height-dialogBoxHeight + textBoxSpace + npcNameBoxHeight,
			textBoxWidth - textBoxSpace - textBoxSpace,
			dialogBoxHeight - textBoxSpace - textBoxSpace - npcNameBoxHeight);
	}
	
	public void startDialog(string npcName, string dialogText)
	{
		dialogArray = dialogText.ToCharArray();
		inputName = npcName;
		count = 0;
		_toggleDialog = true;
	}
	
	public void closeDialog()
	{
		dialogArray[0] = '\0';
		SightTrigger st = GetComponent<SightTrigger>();
		st.greenSight();
		_dialogComplete = false;
		displayDialog = null;
		enabled = false;
	}
	
	void saveCheck()
	{
		displaySpeed = 0.015f;
		endSentenceWait = 0.096f;
		wordSpace = 0.004f;
	}
	
	void updateDialog()
	{
		if(!_dialogComplete) //if the character array has not been gone through.
		{
			if(dialogArray == null)
				return;
			else if(dialogArray.Length == count)
			{
				_dialogComplete = true;
			}
			else if(Time.time >= timer)
			{
				//audio.Play();
				displayDialog += dialogArray[count];
				if(dialogArray[count] == '.' || dialogArray[count] == ',')
				{
					Debug.Log("pause");
					timer = Time.time + displaySpeed + wordSpace + endSentenceWait;
				}
				else if(dialogArray[count] == ' ')
				{
					timer = Time.time + displaySpeed + wordSpace;
				}
				else
				{
					timer = Time.time + displaySpeed;
				}
				count++;
			}
		}
	}
	
	public void toggleDialog()
	{
		_toggleDialog = !_toggleDialog;
	}
}
