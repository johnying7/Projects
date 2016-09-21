using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeftHandEquip : MonoBehaviour {

	public Transform[] weapons;

	Transform currentWeapon;

	// Use this for initialization
	void Start () {
		currentWeapon = null;
	}
	
	public void activateWeapon(ItemObject weaponToActivate)
	{
		if(currentWeapon != null)
			currentWeapon.gameObject.SetActive(false);
		for(int i = 0; i < weapons.Length; i++)
		{
			if(weaponToActivate.itemInfo.name == weapons[i].name)
			{
				currentWeapon = weapons[i];
				currentWeapon.gameObject.SetActive(true);
			}
		}
//		if(weaponToActivate.itemInfo.name == "Weapon") //doesn't currently work but used to set no weapon to empty hands
//			currentWeapon.gameObject.SetActive(false);
	}
}
