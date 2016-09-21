/**	@file : Pokemon.cpp
*	@author : John Ying
*	@date : 2015.8.29
*	Purpose : Implementation of the pokemon class.
*/

#include "Pokemon.h"
#include <string>

Pokemon :: Pokemon()
{
	m_hp = 0;
	m_attackLevel = 0;
	m_defenseLevel = 0;
	m_name = "";
}

void Pokemon :: reduceHP (int amount)
{
	m_hp -= amount;
	if (m_hp < 0) m_hp = 0;
}

int Pokemon :: getHP() const
{
	return (m_hp);
}

void Pokemon :: setHP(int hp)
{
	m_hp = hp;
}

int Pokemon :: getAttackLevel() const
{
	return (m_attackLevel);
}

void Pokemon :: setAttackLevel(int attackLevel)
{
	m_attackLevel= attackLevel;
}

int Pokemon :: getDefenseLevel() const
{
	return (m_defenseLevel);
}

void Pokemon :: setDefenseLevel(int defenseLevel)
{
	m_defenseLevel= defenseLevel;
}

std::string Pokemon :: getName() const
{
	return (m_name);
}

void Pokemon :: setName(std::string name)
{
	m_name = name;
}
