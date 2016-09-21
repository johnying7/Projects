  /**	@file : CyberChicken.cpp
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Implementation of the CyberChicken class
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "CyberChicken.h"


/**
*	@pre none
*	@post Creates and initializes a CyberChicken instance
*   @return sets name to "Cyber Chicken" and sound to "Resistance is futile"
*/
CyberChicken :: CyberChicken()
{
	m_name = "Cyber Chicken";
	m_sound = "Resistance is futile";
} 

/**
*	@pre none
*	@post none
*   @return number of Chicken eggs
*/
int CyberChicken :: getCyberEggs() const
{
	return m_eggs;
}

/**
*	@pre none
*	@post changes the inherited Chicken eggs variable
*   @return none
*/
void CyberChicken :: setCyberEggs(int eggs)
{
	m_eggs = eggs;
}
