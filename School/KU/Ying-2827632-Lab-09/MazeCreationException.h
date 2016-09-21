/**	@file : MazeCreationException.h
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : Exception class that inherits from runtime_error to establish exception errors
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>

#ifndef MAZECREATIONEXCEPTION_H
#define MAZECREATIONEXCEPTION_H
 
class MazeCreationException : public std::runtime_error
{
	public:
		
		/**
		*	@pre 
		*	@post creates an exception with the message
		*   @return none
		*/
		MazeCreationException(const char* message);
		
};
#endif
