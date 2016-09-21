/**	@file : MazeCreationException.cpp
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : exception definition that inherits from runtime_error and calls when a runtime error occurs
*/
 
#include <iostream>
#include <string>
#include <stdexcept>
#include "MazeCreationException.h"

MazeCreationException :: MazeCreationException(const char* message) : std::runtime_error(message)
{

}
