using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SavePlayer : MonoBehaviour {
	
	public fileManager fm = new fileManager();
	
	void Awake ()
	{
		fm.initialize();
		fm.createDirectory("PlayerSaves");
		if(fm.checkFile("PlayerSaves/PlayerInfo.xml") == true) //if file exists, use it
		{
			Debug.Log("File found");
			
			loadPlayerHealth();
		}
		else
		{
			Debug.Log("Creating new file");
			XmlDocument xDoc = new XmlDocument();
			XmlElement root = xDoc.CreateElement("PlayerSaves");
			xDoc.AppendChild(root);
			fm.createFile("PlayerSaves","PlayerInfo","xml",xDoc.OuterXml,true);
		}
	}
	
	private void loadPlayerHealth()
	{
		PlayerHealth pH = GetComponent<PlayerHealth>();
		
		XmlDocument xmlDoc = fm.useXmlFile("PlayerSaves","PlayerInfo","xml",true); //returns an decrypted xml document from the directory
		
		XmlNodeList curHealthElement = xmlDoc.GetElementsByTagName("idCount");
		pH.curHealth = int.Parse(curHealthElement[0].InnerText);
		Debug.Log(int.Parse(curHealthElement[0].InnerText));
	}
		
	private void savePlayerInfo()
	{		
		Debug.Log("Begin saving");
		
		XmlDocument xml = new XmlDocument();
		
		XmlDeclaration xmlDecl;
		xmlDecl = xml.CreateXmlDeclaration("1.0","UTF-8","yes");
		xml.AppendChild(xmlDecl);
		
		XmlElement rootElement = fm.createXmlElement(xml, "PlayerSaveInfo");
		xml.AppendChild(rootElement);
		
		PlayerHealth pH = GetComponent<PlayerHealth>(); //grabs the player health script
		rootElement.AppendChild(fm.createXmlElement(xml,"curHealth", pH.curHealth.ToString())); //creates an xml element of current player health
		
		fm.updateFile("PlayerSaves","PlayerInfo","xml",xml.OuterXml,true,true);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
