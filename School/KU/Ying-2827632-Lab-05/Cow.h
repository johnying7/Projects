  /**	@file : Cow.h
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Hold milk properties specific to a Cow FarmAnimal
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "FarmAnimal.h"

#ifndef COW_H
#define COW_H

class Cow : public FarmAnimal
{
    public:
        /**
        *	@pre none
        *	@post sets name to "Cow" and sound to "Moo"
        *   @return none
        */
        Cow();
        
        /**
        *	@pre none
        *	@post none
        *   @return amount of milk produced by the cow
        */
        double getMilkProduced() const;
        
        /**
        *	@pre amount of milk the cow is able to be produced in gallons
        *	@post sets milkProduced to the parameter amount
        *   @return none
        */
        void setMilkProduced(double gallons);
            
    protected:
    	double m_milkProduced;
};
#endif
