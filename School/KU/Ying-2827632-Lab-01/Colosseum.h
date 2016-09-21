/**	@file : Colosseum.h
*	@author : John Ying
*	@date : 2015.8.29
*	Purpose : Header file of colosseum class. Creates the building and interaction of the pokemon class.
*/

#ifndef COLOSSEUM_H
#define COLOSSEUM_H
#include <iostream>
#include <string>
#include "Pokemon.h"
#include "Dice.h"

class Colosseum
{
	public:
		/**
		*	@pre None
		*	@post initializes dice to appropriate sizes
		*/
		Colosseum();
		
		/**
		*	@pre None
		*	@post modifies the input pokemon with user input
		*/ 
		void userBuild(Pokemon& p);

		/**
		*	@pre None
		*	@post applies attackers attack level to defenders defense level plus rolls into damage
		*/ 
		bool attack(const Pokemon& attacker,Pokemon& defender); 

		/**
		*	@pre fully completed pokemon made in userbuild
		*	@post randomly determines player order, executes that, and the win/lose/draw state
		*/ 
		void play(Pokemon& p1, Pokemon& p2);

	private:
		Dice d20;//creates 20-sided die
		Dice d6; //creates 6-sided die

		/**
		*	@pre None
		*	@post asks user to modify input pokemon's name
		*/ 
		void askName(Pokemon& p);

		/**
		*	@pre None
		*	@post asks user to modify input pokemon's hitpoints
		*/
		void askHP(Pokemon& p);

		/**
		*	@pre None
		*	@post asks user to modify input pokemon's attack level
		*/
		void askAttackLevel(Pokemon& p);

		/**
		*	@pre None
		*	@post asks user to modify input pokemon's defense level
		*/
		void askDefenseLevel(Pokemon& p);
};
#endif
