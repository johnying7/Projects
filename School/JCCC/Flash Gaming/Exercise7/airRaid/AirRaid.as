package { //imports the neccessary utilities for the basic air raid code to work
	import flash.display.*;
	import flash.events.*;
	import flash.utils.Timer;
	import flash.text.TextField;
	
	public class AirRaid extends MovieClip { //establishes the classes and other functions needed
		private var aagun:AAGun;
		private var airplanes:Array;
		private var bullets:Array;
		public var leftArrow, rightArrow:Boolean;
		private var nextPlane:Timer;
		private var shotsLeft:int;
		private var shotsHit:int;
		
		public function startAirRaid() { //establishes basic statistics and objects(classes) in the game
			shotsLeft = 50;
			shotsHit = 0;
			showGameScore();
			
			aagun = new AAGun();
			addChild(aagun);
			
			airplanes = new Array();
			bullets = new Array();
			
			stage.addEventListener(KeyboardEvent.KEY_DOWN,keyDownFunction);
			stage.addEventListener(KeyboardEvent.KEY_UP,keyUpFunction);
			
			addEventListener(Event.ENTER_FRAME,checkForHits);

			setNextPlane();
		}
		
		public function setNextPlane() { //provides the game with the information of the "next plane" to pop up to shoot for the user
			nextPlane = new Timer(1000+Math.random()*1000,1);
			nextPlane.addEventListener(TimerEvent.TIMER_COMPLETE,newPlane);
			nextPlane.start();
		}
		
		public function newPlane(event:TimerEvent) { //actually creates the plane and the basic statistics like speed, etc.
			if (Math.random() > .5) {
				var side:String = "left";
			} else {
				side = "right";
			}
			var altitude:Number = Math.random()*50+20;
			var speed:Number = Math.random()*150+150;
			
			var p:Airplane = new Airplane(side,speed,altitude);
			addChild(p);
			airplanes.push(p);
			
			setNextPlane();
		}
		
		public function checkForHits(event:Event) { //calls all the classes and functions associated with a bullet hitting a plane such as killing the plane, bullet, and increasing score, etc.
			for(var bulletNum:int=bullets.length-1;bulletNum>=0;bulletNum--){ 
				for (var airplaneNum:int=airplanes.length-1;airplaneNum>=0;airplaneNum--) {
					if (bullets[bulletNum].hitTestObject(airplanes[airplaneNum])) {
						shotsHit += 100 * airplanes[airplaneNum].currentFrame;
						airplanes[airplaneNum].planeHit();
						bullets[bulletNum].deleteBullet();
						showGameScore();
						break;
					}
				}
			}
			
			if ((shotsLeft == 0) && (bullets.length == 0)) { //ends the game when bullets have run out
				endGame();
			}
		}
		
		public function keyDownFunction(event:KeyboardEvent) { //moves the AAGun depending on the key pressed
			if (event.keyCode == 37) {
				leftArrow = true;
			} else if (event.keyCode == 39) {
				rightArrow = true;
			} else if (event.keyCode == 32) {
				fireBullet();
			}
		}
		
		public function keyUpFunction(event:KeyboardEvent) { //lets the game know the movement key has been released for the AAGun
			if (event.keyCode == 37) {
				leftArrow = false;
			} else if (event.keyCode == 39) {
				rightArrow = false;
			}
		}

		public function fireBullet() { //calls all the functions for firing a bullet at a plane by the user
			if (shotsLeft <= 0) return;
			var b:Bullet = new Bullet(aagun.x,aagun.y,-300);
			addChild(b);
			bullets.push(b);
			shotsLeft--;
			showGameScore();
		}
		
		public function showGameScore() { //does the calculations for the score and calls the classes needed
			showScore.text = String("Score: "+shotsHit);
			showShots.text = String("Shots Left: "+shotsLeft);
		}
		
		public function removePlane(plane:Airplane) { //calls the classes needed to delete a plane in the array of lined up planes in the game
			for(var i in airplanes) {
				if (airplanes[i] == plane) {
					airplanes.splice(i,1);
					break;
				}
			}
		}
		
		public function removeBullet(bullet:Bullet) { //removes the classes of bullet left on the screen when they aren't needed anymore
			for(var i in bullets) {
				if (bullets[i] == bullet) {
					bullets.splice(i,1);
					break;
				}
			}
		}
		
		public function endGame() { //calls all the classes needed to end the game and delete junk that's cluttering the screen
			for(var i:int=airplanes.length-1;i>=0;i--) {
				airplanes[i].deletePlane();
			}
			airplanes = null;
			
			aagun.deleteGun();
			aagun = null;
			
			stage.removeEventListener(KeyboardEvent.KEY_DOWN,keyDownFunction);
			stage.removeEventListener(KeyboardEvent.KEY_UP,keyUpFunction);
			removeEventListener(Event.ENTER_FRAME,checkForHits);
			
			nextPlane.stop();
			nextPlane = null;
			
			gotoAndStop("gameover");
		}

	}
}
