   /**	@file : FarmAnimal.cpp
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Implementation of the FarmAnimal Class
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "FarmAnimal.h"

FarmAnimal :: FarmAnimal()
{
	m_name = "unset";
	m_sound = "unset";
}
        
std::string FarmAnimal :: getName() const
{
	return m_name;
}

void FarmAnimal :: setName(std::string name)
{
	m_name = name;
}

std::string FarmAnimal :: getSound() const
{
	return m_sound;
}

void FarmAnimal :: setSound(std::string sound)
{
	m_sound = sound;
}
