 /**	@file : AnimalPen.h
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Inherits from Stack and stores animals within its container
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "Stack.h"
#include "FarmAnimal.h"

#ifndef ANIMALPEN_H
#define ANIMALPEN_H

class AnimalPen : public Stack<FarmAnimal*>
{
    public:
        /**
        *	@pre none
        *	@post empty constructor
        *   @return none
        */
        AnimalPen();
		
		/**
        *	@pre none
        *	@post deletes animals within the stack
        *   @return empty stack
        */
        ~AnimalPen();
        
        /**
        *	@pre pointer to an animal address to place onto the stack
        *	@post adds animal onto the top of the stack
        *   @return none
        */
        void addAnimal(FarmAnimal* animal);
        
        /**
        *	@pre none
        *	@post none
        *   @return gives the next animal in the pen
        */
        FarmAnimal* peakAtNextAnimal();
        
        /**
        *	@pre none
        *	@post removes animal object and pointer from the stack
        *   @return none
        */
        void releaseAnimal();
        
        /**
        *	@pre none
        *	@post none
        *   @return true if the pen has no animals within its stack, otherwise false
        */
        bool isPenEmpty();
};
#endif
