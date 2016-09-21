/**	@file : NumberFileDriver.h
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : Creates a file of numbers given valid arguments or prints a help menu
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "NumberFileGenerator.h"

#ifndef NUMBERFILEDRIVER_H
#define NUMBERFILEDRIVER_H
 
class NumberFileDriver
{
	public:
		
		/**
		*	@pre valid 2-D array and the arguments contained within are valid.
		*	@post If the arguments are valid, a number file is created following those specifications.Otherwise, the help menu is printed and no files are created.
		*   @return none
		*/
		static void run(int argc, char** argv);
		
		/**
		*	@pre none
		*	@post contains help details on what counts as valid command line arguments
		*   @return none
		*/
		static void printHelpMenu();
		
	private:
	
		/**
		*	@pre none
		*	@post Prints a menus to help the user use the NumberFileGenerator
		*   @return True is the option passed in is valid. Valid options are: "-a", "-d", "-r", and "-s".	This is case sensitive.
		*/
		static bool isValidOption(std::string option);
};
#endif
