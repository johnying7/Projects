package { //imports the neccessary utility for the airplanes to work
	import flash.display.*;
	import flash.events.*;
	import flash.utils.getTimer;
	
	public class Airplane extends MovieClip { //starts the airplane class
		private var dx:Number;
		private var lastTime:int;
		
		public function Airplane(side:String, speed:Number, altitude:Number) { //provides the statistics for the basic airplanes needed to operate
			if (side == "left") {
				this.x = -50;
				dx = speed;
				this.scaleX = -1;
			} else if (side == "right") {
				this.x = 600;
				dx = -speed; 
				this.scaleX = 1;
			}
			this.y = altitude; //sets the altitude (height) of the entering plane
			

			this.gotoAndStop(Math.floor(Math.random()*5+1)); //moves the plane to the correct position
			

			addEventListener(Event.ENTER_FRAME,movePlane);
			lastTime = getTimer();
		}
		
		public function movePlane(event:Event) { //moves the plane based upon time(frame) passed and deletes it upon moving off screen
			var timePassed:int = getTimer()-lastTime;
			lastTime += timePassed;
			
			this.x += dx*timePassed/1000;
			

			if ((dx < 0) && (x < -50)) {
				deletePlane();
			} else if ((dx > 0) && (x > 600)) {
				deletePlane();
			}
			
		}
		

		public function planeHit() { //provides the reactions to a plane being hit
			removeEventListener(Event.ENTER_FRAME,movePlane);
			MovieClip(parent).removePlane(this);
			gotoAndPlay("explode");
		}
		

		public function deletePlane() { //actually REMOVES the associations of the deleted plane.
			removeEventListener(Event.ENTER_FRAME,movePlane);
			MovieClip(parent).removePlane(this);
			parent.removeChild(this);
		}
		
	}
}
