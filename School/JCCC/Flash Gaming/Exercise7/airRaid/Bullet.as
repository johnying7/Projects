package { //imports the neccessary utilities for the bullet.as to work
	import flash.display.*;
	import flash.events.*;
	import flash.utils.getTimer;
	
	public class Bullet extends MovieClip { //starts the bullet class
		private var dy:Number;
		private var lastTime:int;
		
		public function Bullet(x,y:Number, speed: Number) { //changes the bullet position aka speed
			this.x = x;
			this.y = y;
			dy = speed;
			lastTime = getTimer();
			addEventListener(Event.ENTER_FRAME,moveBullet);
		}
		
		public function moveBullet(event:Event) { //actually moves the bullet and kills it if it goes out of screen
			var timePassed:int = getTimer()-lastTime;
			lastTime += timePassed;
			
			this.y += dy*timePassed/1000;
			
			if (this.y < 0) {
				deleteBullet();
			}
			
		}

		public function deleteBullet() { //removes the bullet, and all operations associated with it
			MovieClip(parent).removeBullet(this);
			parent.removeChild(this);
			removeEventListener(Event.ENTER_FRAME,moveBullet);
		}

	}
}