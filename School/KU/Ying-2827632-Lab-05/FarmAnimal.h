 /**	@file : FarmAnimal.h
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : Base class for future animals to inherit from with basic properties
*/

#include <iostream>
#include <string>

#ifndef FARMANIMAL_H
#define FARMANIMAL_H

class FarmAnimal
{
    public:
        /**
        *	@pre none
        *	@post Creates and initializes a FarmAnimal instance
        *   @return sets name and sound to string "unset"
        */
        FarmAnimal();
        
        /**
        *	@pre none
        *	@post none
        *   @return gives m_name
        */
        std::string getName() const;
        
        /**
        *	@pre desired name of the animal as a string
        *	@post m_name is changed
        *   @return none
        */
        void setName(std::string name);
        
        /**
        *	@pre none
        *	@post none
        *   @return gives m_sound
        */
        std::string getSound() const;
        
        /**
        *	@pre chosen sound of the animal as a string
        *	@post changes m_sound
        *   @return none
        */
        void setSound(std::string sound);
        
	protected:
        std::string m_name;
        std::string m_sound;
};
#endif 
