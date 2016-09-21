/**	@file : PreconditionViolationException.h
*	@author : John Ying
*	@date : 2015.9.29
*	Purpose : Exception class that inherits from runtime_error to establish exception errors
*/

#ifndef PRECONDITIONVIOLATION_H
#define PRECONDITIONVIOLATION_H
#include <iostream>
#include <string>
#include <stdexcept>
 
class PreconditionViolationException : public std::runtime_error
{
	public:
		/**
		*	@pre letter variable that will represent the type of message to be displayed
		*	@post throws a runtime_error exception
		*	@return none
		*/
	 	PreconditionViolationException(const char* message);
};
#endif
