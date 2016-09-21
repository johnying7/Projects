using UnityEngine;
using System.Collections;

public class CreateInventory : MonoBehaviour {
	
	#region Private Inventory Rects
	protected Rect inventoryRect = new Rect(10, 10, 300, 600);
	protected Rect headButton;
	protected Rect chestButton;
	protected Rect handsButton;
	protected Rect legsButton;
	protected Rect feetButton;
	
	protected Rect profileButton;
	
	protected Rect leftHandButton;
	protected Rect rightHandButton;
	protected Rect pouchButton;
	protected Rect bagButton;
	protected Rect jewelryButton;
	
	protected Rect openBagButton;
	protected Rect dropHeldItemRect;
	protected Rect heldItemButton;
	#endregion
	
	public Rect ClampToScreen(Rect r)
	{
		r.x = Mathf.Clamp(r.x,0,Screen.width-r.width);
		r.y = Mathf.Clamp(r.y,0,Screen.height-r.height);
		return r;
	}
	
	public void InitializeRects()
	{
		int dragRoom = 24;
		int square = 42;
		int spacing = 4;
		int rightColumn = 16;
		int leftColumn = 300 - rightColumn - square; //300 represents inventory window width
		int interval = square + spacing;
		int row1 = dragRoom;
		int row2 = row1 + interval;
		int row3 = row2 + interval;
		int row4 = row3 + interval;
		int row5 = row4 + interval;
		
		headButton = new Rect(rightColumn,row1,square,square);
		chestButton = new Rect(rightColumn,row2,square,square);
		handsButton = new Rect(rightColumn,row3,square,square);
		legsButton = new Rect(rightColumn,row4,square,square);
		feetButton = new Rect(rightColumn,row5,square,square);
		
		profileButton = new Rect(150-rightColumn-rightColumn-square, row1,
			300-(4*rightColumn)-square-square, row5+square-dragRoom);
		
		rightHandButton = new Rect(leftColumn,row1,square,square);
		leftHandButton = new Rect(leftColumn,row2,square,square);
		pouchButton = new Rect(leftColumn,row3,square,square);
		bagButton = new Rect(leftColumn,row4,square,square);
		jewelryButton = new Rect(leftColumn,row5,square,square);
		
		openBagButton = new Rect(spacing, row5+interval+spacing+spacing,
			300-spacing-spacing, 600-row5-interval-interval-spacing-spacing);
		
		dropHeldItemRect = new Rect(Screen.width/2-70,Screen.height/2-25,140,50);
		heldItemButton = new Rect(0,0,square,square);
	}
	
}
