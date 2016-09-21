  /**	@file : Cow.cpp
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Implementation of the Cow Class
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "Cow.h"

Cow :: Cow()
{
	m_name = "Cow";
	m_sound = "Moo";
	m_milkProduced = 0;
}

/**
*	@pre none
*	@post none
*   @return amount of milk produced by the cow
*/
double Cow :: getMilkProduced() const
{
	return m_milkProduced;
}

/**
*	@pre amount of milk the cow is able to be produced in gallons
*	@post sets milkProduced to the parameter amount
*   @return none
*/
void Cow :: setMilkProduced(double gallons)
{
	if(gallons > 0)
		m_milkProduced = gallons;
	else
		m_milkProduced = 0;
	
}
