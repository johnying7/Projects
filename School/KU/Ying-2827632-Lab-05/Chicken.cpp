  /**	@file : Chicken.cpp
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Implementation of the Chicken class
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "Chicken.h"


/**
*	@pre none
*	@post Creates and initializes a Chicken instance
*   @return sets name to "Chicken" and sound to "Cluck"
*/
Chicken :: Chicken()
{
	m_name = "Chicken";
	m_sound = "Cluck";
	m_eggs = 0;
}

/**
*	@pre none
*	@post none
*   @return the number of eggs the chicken has
*/
int Chicken :: getEggs() const
{
	return m_eggs;
}

/**
*	@pre an integer number of eggs the chicken will have
*	@post changes the number of eggs by input parameter
*   @return none
*/
void Chicken :: setEggs(int eggs)
{
	m_eggs = eggs;
}
