/**	@file : Pokemon.h
*	@author : John Ying
*	@date : 2015.8.29
*	Purpose : Creates the pokemon class stats and direct methods that will be used in colosseum to do battle.
*/

#include <string>
#ifndef POKEMON_H
#define POKEMON_H

class Pokemon
{
	public:
		/**
		*	@pre none
		*	@post creates and initializes a pokemon instance
		*/
		Pokemon();

		/**
		*	@pre amount must be a positive integer
		*	@post reduces pokemon's current hitpoints by set amount
		*/
		void reduceHP(int amount);

		/**
		*	@pre none
		*	@post none
		*/
		int getHP() const;

		/**
		*	@pre none
		*	@post sets hp variable to input amount
		*/
		void setHP(int hp);

		/**
		*	@pre none
		*	@post  none
		*/
		int getAttackLevel() const;

		/**
		*	@pre none
		*	@post sets attack variable to input amount
		*/
		void setAttackLevel(int attackLevel);

		/**
		*	@pre none
		*	@post none
		*/
		int getDefenseLevel() const;

		/**
		*	@pre none
		*	@post sets defense variable to input amount
		*/
		void setDefenseLevel(int defenseLevel);

		/**
		*	@pre none
		*	@post none
		*/
		std::string getName() const;

		/**
		*	@pre none
		*	@post sets name variable to input amount
		*/
		void setName(std::string name);

	private:
		int m_hp; //health points of the pokemon
		int m_attackLevel; //attack level of the pokemon
		int m_defenseLevel; //defense level of the pokemon
		std::string m_name; //name of the pokemon
};
#endif
