/**	@file : NumberFileGenerator.h
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : Creates a file with numbers in selected order
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>

#ifndef NUMBERFILEGENERATOR_H
#define NUMBERFILEGENERATOR_H
 
class NumberFileGenerator
{
	public:
		/**
		*	@pre fileName is valid path/filename. Amount is greater than zero
		*	@post A file is created with the amount of number is ascending order. The amount of numbers in the file is the first entry in the file
		*   @return none
		*/
		static void ascending(std::string fileName, int amount);
	
		/**
		*	@pre fileName is valid path/filename. Amount is greater than zero
		*	@post A file is created with the amount of number is descending order. The amount of numbers in the file is the first entry in the file
		*   @return none
		*/
		static void descending(std::string fileName, int amount);
	
		/**
		*	@pre fileName is valid path/filename. Amount is greater than zero
		*	@post A file is created with the specifed amount of numbers. All number are random and fall between min and max (inclusively). The amount of numbers in the file is the first entry in the file
		*   @return none
		*/
		static void random(std::string fileName, int amount, int min, int max);
	
		/**
		*	@pre fileName is valid path/filename. Amount is greater than zero
		*	@post A file is created with a single number, specified by value, amount of times. The amount of numbers in the file is the first entry in the file
		*   @return none
		*/
		static void singleValue(std::string fileName, int amount, int value);
};
#endif
