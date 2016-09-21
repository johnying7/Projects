package {
	import flash.display.*;
	import flash.events.*;
	import flash.text.TextField;
	
	public class BalloonPop extends MovieClip {
		private var balloons:Array;
		private var cannonball:Cannonball;
		private var cannonballDX, cannonballDY:Number;
		private var leftArrow, rightArrow:Boolean;
		private var shotsUsed:int;
		private var speed:Number;
		private var gameLevel:int;
		private const gravity:Number = .05;
		
		//sets the starting VARIABLES required for the balloon pop level
		public function startBalloonPop() {
			gameLevel = 1;
			shotsUsed = 0;
			speed = 6;
			gotoAndStop("level1");
		}
		
		//adds the beginning FUNCTIONS for the game, including event listeners
		public function startLevel() {
			showGameScore();
			findBalloons();
			stage.addEventListener(KeyboardEvent.KEY_DOWN,keyDownFunction);
			stage.addEventListener(KeyboardEvent.KEY_UP,keyUpFunction);
			addEventListener(Event.ENTER_FRAME,gameEvents);
		}
		
		//applies a random color to a balloon through a random number and array frame
		public function findBalloons() {
			balloons = new Array();
			for(var i:int=0;i<numChildren;i++) {
				if (getChildAt(i) is Balloon) {
					MovieClip(getChildAt(i)).gotoAndStop(Math.floor(Math.random()*5)+1);
					balloons.push(getChildAt(i));
				}
			}
		}
		
		//calls the frame by frame check functions needed for game events
		public function gameEvents(event:Event) {
			moveCannon();
			moveCannonball();
			checkForHits();
		}
		
		//rotates the cannon image based on user key input
		public function moveCannon() {
			var newRotation = cannon.rotation;
			
			if (leftArrow) {
				newRotation -= 1;
			}
			
			if (rightArrow) {
				newRotation += 1;
			}
			
			if (newRotation < -90) newRotation = -90;
			if (newRotation > -20) newRotation = -20;
			
			cannon.rotation = newRotation;
		}

		//moves the cannonball by a small increment, and the effect of physics/gravity
		public function moveCannonball() {
			
			if (cannonball != null) {
				cannonball.x += cannonballDX;
				cannonball.y += cannonballDY;
				cannonballDY += gravity;
				if (cannonball.y > 340) {
					removeChild(cannonball);
					cannonball = null;
				}
			}
		}
		
		//checks to see if the cannonball has hit one of the balloons and plays the explode animation
		public function checkForHits() {
			if (cannonball != null) {
				
				for (var i:int=balloons.length-1;i>=0;i--) {
					if (cannonball.hitTestObject(balloons[i])) {
						balloons[i].gotoAndPlay("explode");
						break;
					}
				}
			}
		}
		
		//checks for keys down/still pressed based upon an event listener
		public function keyDownFunction(event:KeyboardEvent) {
			if (event.keyCode == 37) {
				leftArrow = true;
			} else if (event.keyCode == 39) {
				rightArrow = true;
			} else if (event.keyCode == 32) {
				fireCannon();
			}
		}
		
		//changes the bool variables saying that the keys are up
		public function keyUpFunction(event:KeyboardEvent) {
			if (event.keyCode == 37) {
				leftArrow = false;
			} else if (event.keyCode == 39) {
				rightArrow = false;
			}
		}
		
		//increases cannonball score, creates a new child cannonball to be shot, and adjusts the firing angle
		public function fireCannon() {
			if (cannonball != null) return;
			
			shotsUsed++;
			showGameScore();
			cannonball = new Cannonball();
			cannonball.x = cannon.x;
			cannonball.y = cannon.y;
			addChild(cannonball);
			addChild(cannon);
			addChild(cannonbase);
			cannonballDX = speed*Math.cos(2*Math.PI*cannon.rotation/360);
			cannonballDY = speed*Math.sin(2*Math.PI*cannon.rotation/360);
		}
		
		//calls and removes the children and necessary functions for the game
		public function balloonDone(thisBalloon:MovieClip) {
			
			removeChild(thisBalloon);
			for(var i:int=0;i<balloons.length;i++) {
				if (balloons[i] == thisBalloon) {
					balloons.splice(i,1);
					break;
				}
			}

			if (balloons.length == 0) {
				cleanUp();
				if (gameLevel == 3) {
					endGame();
				} else {
					endLevel();
				}
			}
		}
		
		//removes the excess children and event listeners for the end of the game so they aren't running
		public function cleanUp() {
			
			stage.removeEventListener(KeyboardEvent.KEY_DOWN,keyDownFunction);
			stage.removeEventListener(KeyboardEvent.KEY_UP,keyUpFunction);
			removeEventListener(Event.ENTER_FRAME,gameEvents);
			if (cannonball != null) {
				removeChild(cannonball);
				cannonball = null;
			}
			removeChild(cannon);
			removeChild(cannonbase);
		}

		//changes the picture to the levelover frame
		public function endLevel() {
			gotoAndStop("levelover");
		}
		
		//changes the picture to the gameover frame
		public function endGame() {
			gotoAndStop("gameover");
		}

		//changes the picture to the level frame and increases the variable deciding current level
		public function clickNextLevel(e:MouseEvent) {
			gameLevel++;
			gotoAndStop("level"+gameLevel);
		}
		
		//displays how many shots are used
		public function showGameScore() {
			showScore.text = String("Shots: "+shotsUsed);
		}
		
	}
}
