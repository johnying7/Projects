package
{
	import flash.events.MouseEvent;
	import flash.display.MovieClip;
	
	public class coords extends MovieClip
	{
		public function coords()
		{
			stage.addEventListener(MouseEvent.MOUSE_MOVE, showPos);
		}
		
		function showPos(evt:MouseEvent):void
		{
			posText.text = "X: " + mouseX + "   Y: " + mouseY;
		}
	}
}