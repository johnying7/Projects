using UnityEngine;
using System.Collections;

public class CreateInventory2 : MonoBehaviour {

	protected Rect inventoryRect = new Rect(10, 10, 300, 600);


	public int invPos; //current inventory position marker
	public int bagPos; //current bag counter marker (how far a bag is inside of another bag)
	
	protected Rect weaponButton;
	protected Rect weaponLabel;
	protected Rect armorButton;
	protected Rect armorLabel;
	protected Rect bagButton;
	protected Rect bagLabel;
	
	protected Rect bagDisplayRect; //where the bag area is displayed
	protected Rect bagScrollRect; //the size of the scroll area (this is what changes based on amount of items in list)

	protected Rect holdItemButton; //shows the held item button next to the mouse
	protected Rect holdItemLabel;

	protected int buttonheight = 24;
	protected int indentAmt = 16;
	protected int spacing = 8;//space between the edge of the window
	protected int nextLine = 4; //space between buttons
	protected int labelWidth = 32; //width of the curDurability/maxDurability label
	protected int labelSpacing;//durability width + breathing room
	protected int dropDownSpacing = 16; //checkmark box spacing for the dropdown

	public void InitializeRects()
	{
		labelSpacing = spacing + labelWidth;
		weaponButton = new Rect(spacing, spacing*3, 300-(spacing*2), buttonheight);
		weaponLabel = new Rect(300-labelSpacing-spacing, spacing*3, labelWidth, buttonheight);
		armorButton = new Rect(spacing, spacing*3+buttonheight+nextLine, 300-(spacing*2), buttonheight);
		armorLabel = new Rect(300-labelSpacing-spacing, spacing*3+buttonheight+nextLine, labelWidth, buttonheight);
		bagButton = new Rect(spacing, (buttonheight+nextLine)*2+spacing*3, 300-(spacing*2), buttonheight);
		bagLabel = new Rect(300-labelSpacing-spacing, (buttonheight+nextLine)*2+spacing*3, labelWidth, buttonheight);

		bagDisplayRect = new Rect(spacing,(buttonheight+nextLine)*3+buttonheight+spacing*2, 300-(spacing*2), 600-(spacing+buttonheight+spacing)*3-spacing-buttonheight);

		holdItemButton = new Rect(0, 0, 300-(spacing*2)-labelWidth, buttonheight);
		holdItemLabel = new Rect(0,0, labelWidth, buttonheight);
	}

	public Rect ClampToScreen(Rect r)
	{
		r.x = Mathf.Clamp(r.x,0,Screen.width-r.width);
		r.y = Mathf.Clamp(r.y,0,Screen.height-r.height);
		return r;
	}

	protected Rect setItemButtonRect()
	{
		Rect temp = new Rect(bagPos*indentAmt+spacing, invPos*(buttonheight+nextLine), 300-(bagPos*indentAmt)-(spacing*2)-20, buttonheight);
		return temp;
	}

	protected Rect setItemLabelRect()
	{
		Rect temp = new Rect(300-labelSpacing-indentAmt-spacing, invPos*(buttonheight+nextLine), labelWidth, buttonheight);
		return temp;
	}

	protected Rect setBagToggleRect()
	{
		Rect temp = new Rect(bagPos*indentAmt+spacing, invPos*(buttonheight+nextLine), dropDownSpacing, dropDownSpacing);
		return temp;
	}

//	protected Rect getBagScrollRect()
//	{
//		if(invPos > 16)
//		{
//			bagScrollRect = new Rect(spacing,0,300-(spacing*2)-20,476+((invPos-15)*(buttonheight+spacing)));
//		}
//		else
//			bagScrollRect = new Rect(spacing,0,300-(spacing*2)-20,476);
//
//		return bagScrollRect;
//	}
}
