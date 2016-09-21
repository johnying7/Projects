  /**	@file : Chicken.h
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Inherits from FarmAnimal and holds an amount of eggs
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "FarmAnimal.h"

#ifndef CHICKEN_H
#define CHICKEN_H

class Chicken : public FarmAnimal
{
    public:
        /**
        *	@pre none
        *	@post Creates and initializes a Chicken instance
        *   @return sets name to "Chicken" and sound to "Cluck"
        */
        Chicken();
	
	protected:
		/**
        *	@pre none
        *	@post none
        *   @return the number of eggs the chicken has
        */
        int getEggs() const;
        
        /**
        *	@pre an integer number of eggs the chicken will have
        *	@post changes the number of eggs by input parameter
        *   @return none
        */
        void setEggs(int eggs);
        
        int m_eggs;
};
#endif
