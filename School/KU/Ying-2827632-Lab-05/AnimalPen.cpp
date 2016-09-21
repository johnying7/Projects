 /**	@file : AnimalPen.cpp
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Implementation of the AnimalPen class
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "AnimalPen.h"

/**
*	@pre none
*	@post empty constructor
*   @return none
*/
AnimalPen :: AnimalPen()
{
	
}

/**
*	@pre none
*	@post deletes animals within the stack
*   @return empty stack
*/
AnimalPen :: ~AnimalPen()
{
	
}

/**
*	@pre pointer to an animal address to place onto the stack
*	@post adds animal onto the top of the stack
*   @return none
*/
void AnimalPen :: addAnimal(FarmAnimal* animal)
{
	push(animal);
}

/**
*	@pre none
*	@post none
*   @return gives the next animal in the pen
*/
FarmAnimal* AnimalPen :: peakAtNextAnimal()
{
	return peek();
}

/**
*	@pre none
*	@post removes animal object and pointer from the stack
*   @return none
*/
void AnimalPen :: releaseAnimal()
{
	delete peek();
	std::cout << "Animal released\n";
	pop();
}

/**
*	@pre none
*	@post none
*   @return true if the pen has no animals within its stack, otherwise false
*/
bool AnimalPen :: isPenEmpty()
{
	return isEmpty();
}
