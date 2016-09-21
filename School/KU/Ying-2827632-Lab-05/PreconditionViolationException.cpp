/**	@file : PreconditionViolationException.cpp
*	@author : John Ying
*	@date : 2015.9.29
*	Purpose : exception definition that inherits from runtime_error and calls when a runtime error occurs
*/
 
#include <iostream>
#include <string>
#include <stdexcept>
#include "PreconditionViolationException.h"

PreconditionViolationException :: PreconditionViolationException(const char* message) : std::runtime_error(message)
{

}
