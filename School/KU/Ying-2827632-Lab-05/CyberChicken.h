 /**	@file : CyberChicken.h
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Inherits from Chicken and is able to use cyber egg methods
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "Chicken.h"

#ifndef CYBERCHICKEN_H
#define CYBERCHICKEN_H

class CyberChicken : public Chicken
{
    public:
        /**
        *	@pre none
        *	@post Creates and initializes a CyberChicken instance
        *   @return sets name to Cyber Chicken" and sound to "Resistance is futile"
        */
        CyberChicken();
		
		/**
        *	@pre none
        *	@post none
        *   @return number of Chicken eggs
        */
        int getCyberEggs() const;
        
        /**
        *	@pre none
        *	@post changes the inherited Chicken eggs variable
        *   @return none
        */
        void setCyberEggs(int eggs);
};
#endif 
