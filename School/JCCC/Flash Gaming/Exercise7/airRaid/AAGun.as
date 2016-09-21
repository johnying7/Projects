package { //imports the neccessary utility for the aa gun to work
	import flash.display.*;
	import flash.events.*;
	import flash.utils.getTimer;
	
	public class AAGun extends MovieClip { //starts the AAGun class
		static const speed:Number = 150.0;
		private var lastTime:int;
		
		public function AAGun() { //moves the AAGun to the starting position
			this.x = 275;
			this.y = 340;
			
			addEventListener(Event.ENTER_FRAME,moveGun);
		}

		public function moveGun(event:Event) { //moves the AAGun based upon time and key press
			var timePassed:int = getTimer()-lastTime;
			lastTime += timePassed;
			
			var newx = this.x;
			
			if (MovieClip(parent).leftArrow) {
				newx -= speed*timePassed/1000;
			}
			
			if (MovieClip(parent).rightArrow) {
				newx += speed*timePassed/1000;
			}
			
			if (newx < 10) newx = 10;
			if (newx > 540) newx = 540;
			
			this.x = newx;
		}
		
		public function deleteGun() { //removes the AAGun completely when needed
			parent.removeChild(this);
			removeEventListener(Event.ENTER_FRAME,moveGun);
		}
	}
}