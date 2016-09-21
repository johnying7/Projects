using UnityEngine;
using System.Collections;

public class SightCrosshair : MonoBehaviour { //do not attach script to anything, inherited by SightTrigger.cs
	
	public Texture2D whiteTexture; //drag and drop the default white crosshair texture to this variable
	public Texture2D greenTexture; //drag and drop the green crosshair texture to this variable
	public Texture2D redTexture; //drag and drop the red crosshair texture to this variable
	public int cHSize; //adjustable crosshair size
	protected int halfCHSize; //half crosshair size
	
	protected bool redCrosshair; //activation boolean for red crosshair sights
	protected bool greenCrosshair; //activation boolean for green crosshair sights
	protected bool whiteCrosshair;
	
	public void redSight()
	{
		if(sightIsActive())
		{
			redCrosshair = true;
			greenCrosshair = false;
			whiteCrosshair = false;
		}
	}
	
	public void greenSight()
	{
		if(sightIsActive())
		{
			redCrosshair = false;
			greenCrosshair = true;
			whiteCrosshair = false;
		}
	}
	
	public void whiteSight()
	{
		redCrosshair = false;
		greenCrosshair = false;
		whiteCrosshair = true;
	}
	
	public void noSight()
	{
		redCrosshair = false;
		greenCrosshair = false;
		whiteCrosshair = false;
	}
	
	public bool sightIsActive()
	{
		if(redCrosshair || greenCrosshair || whiteCrosshair)
			return true;
		
		return false;
	}
}
