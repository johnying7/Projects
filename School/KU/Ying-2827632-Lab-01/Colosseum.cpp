/**	@file : Colosseum.cpp
*	@author : John Ying
*	@date : 2015.8.29
*	Purpose : Implementation of the colosseum class.
*/

#include <iostream>
#include <string>
#include "Colosseum.h"
#include "Pokemon.h"
#include "Dice.h"

//Dice d20;
//Dice d6;

Colosseum :: Colosseum()
{
	d20 = Dice(20);
	d6 = Dice(6);
}

void Colosseum::userBuild(Pokemon& p)
{
	askName(p);
	askHP(p);
	askAttackLevel(p);
	askDefenseLevel(p);
}

bool Colosseum::attack(const Pokemon& attacker, Pokemon& defender)
{
	int bonusDefense;
	int bonusAttack;
	bonusDefense = d20.roll();
	bonusAttack = d20.roll();

	std::cout << attacker.getName() << " launches an attack on " << defender.getName() << "." << std::endl;

	std::cout << attacker.getName() << " rolls an attack bonus of " << bonusAttack << std::endl;
	std::cout << defender.getName() << " rolls a defense bonus of " << bonusDefense << std::endl;
	
	int totalDef = bonusDefense + defender.getDefenseLevel();
	int totalAtk = bonusAttack + attacker.getAttackLevel();


	if(totalAtk > totalDef)
	{
		std::cout << attacker.getName() << " hits " << defender.getName() << "!" << std::endl;
		
		int die1 = d6.roll();
		int die2 = d6.roll();
		int die3 = d6.roll();
		int damage = die1 + die2 + die3;

		std::cout << "The damage stacks are " << die1 << ", " << die2 << ", and " << die3 << " for " << damage << " damage." << std::endl;
		defender.reduceHP(damage);
		std::cout << defender.getName() << " has " << defender.getHP() << " left." << std::endl;
	}
	else
	{
		std::cout << defender.getName() << " blocks the attack completely!" << std::endl;
	}
	
	if(defender.getHP() == 0)
	{
		std::cout << defender.getName() << " has been defeated!" << std::endl;
		std::cout << attacker.getName() << " wins the match!" << std::endl;
		return true;
	}
	else
		return false;
}

void Colosseum::play(Pokemon& p1, Pokemon& p2)
{
	Dice d2(2);
	int die = d2.roll();
	if(die == 1)
		std::cout << "Player 1 will go first." << std::endl;
	else
		std::cout << "Player 2 will go first." << std::endl;

	

	for (int i = 0; i < 10; i++)
	{
		std::cout << "Round " << i+1 << ":"  << std::endl;
		if(die == 1)
		{
			if(attack(p1,p2)) break;
			if(attack(p2,p1)) break;
		}
		else if(die == 2)
		{
			if(attack(p2,p1)) break;
			if(attack(p1,p2)) break;
		}

		if(i+1 == 10)
		{
			if(p1.getHP() > p2.getHP())
				std::cout << p1.getName() << " is the winner!" << std::endl;
			else if(p1.getHP() < p2.getHP())
				std::cout << p2.getName() << " is the winner!" << std::endl;
			else
				std::cout << "The game has ended in a draw." << std::endl;
		}
	}
}

void Colosseum::askName(Pokemon& p)
{
	bool validName = false;
	while( validName == false )
	{
		std::string tempName;
		std::cout << "Input the pokemon's name: ";
		std::cin >> tempName;

		if(tempName == "")
		{
			std::cout << "Sorry, you need to input something that has at least one character." << std::endl;
		}
		else
		{
			validName = true;
			p.setName(tempName);
		}
	}
}

void Colosseum::askHP(Pokemon& p)
{
	p.setHP(0);
	while (p.getHP() == 0)
	{
		int  tempHP = 0;
		std::cout << "Input the pokemon's hitpoints (1-50): ";
		

		if(!(std::cin >> tempHP))
		{
			std::cin.clear();
			std::cin.ignore(100, '\n');
			std::cout << "Sorry, you need to input something that is within the range." << std::endl;
		}
		else
		{

			if(tempHP < 1 || tempHP > 50)
			{
				std::cout << "That input is outside of the range, please input a valid hitpoints value." << std::endl;
			}
			else if (tempHP <= 50 && tempHP >= 1)
			{
				p.setHP(tempHP);
			}
		}	
	}
}

void Colosseum::askAttackLevel(Pokemon& p)
{
	p.setAttackLevel(0);
	while (p.getAttackLevel() == 0)
	{
		std::cout << "Input attack level (1-49): ";
		int tempAttackLevel = 0;
		if(!(std::cin >> tempAttackLevel))
		{
			std::cin.clear();
			std::cin.ignore(100, '\n');
			std::cout << "Sorry, you need to input something that is a number." << std::endl;
		}
		else if (tempAttackLevel < 1 || tempAttackLevel > 49)
		{
			std::cout << "Please input a number between 1-49." << std::endl;
		}
		else if (tempAttackLevel <= 49 && tempAttackLevel >= 1)
		{
			p.setAttackLevel(tempAttackLevel);
		}
		else
			std::cout << "Attack setting error." << std::endl;
	}
}
void Colosseum::askDefenseLevel(Pokemon& p)
{
	p.setDefenseLevel(0);
	while (p.getDefenseLevel() == 0)
	{
		int maxDefense = 50-p.getAttackLevel();
		std::cout << "Input defense level (1-" << maxDefense<<") : ";
		int tempDefenseLevel = 0;
		
		if(!(std::cin >> tempDefenseLevel))
		{
			std::cin.clear();
			std::cin.ignore(100, '\n');
			std::cout << "Sorry, you need to input something that is a number." << std::endl;
		}
		else if (tempDefenseLevel < 1 || tempDefenseLevel > maxDefense)
		{
			std::cout << "Please input a number between 1-" <<  maxDefense << "." << std::endl;
		}
		else if (tempDefenseLevel <= maxDefense && tempDefenseLevel >= 1)
		{
			p.setDefenseLevel(tempDefenseLevel);
		}
		else
			std::cout << "Defense setting error." << std::endl;
	}
}
