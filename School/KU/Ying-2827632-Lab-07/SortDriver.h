/**	@file : SortDriver.h
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : Creates a file filled with timing info of selected sorts
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "Sorts.h"

#ifndef SORTDRIVER_H
#define SORTDRIVER_H
 
class SortDriver
{
	public:
		
		/**
		*	@pre valid 2-D array and input from the command line arguments also valid
		*	@post file is created containing the timing info of chosen sorts on the given file
		*   @return none
		*/
		static void run(int argc, char** argv);
		
		/**
		*	@pre none
		*	@post prints out menu to help the user use the NumberFileGenerator
		*   @return none
		*/
		static void printHelpMenu();
		
	private:
	
		/**
		*	@pre Input file created by NumberFileGenerator
		*	@post none
		*   @return true if file exists, false otherwise
		*/
		static bool isFileAccessible(std::string fileName);
		
		/**
		*	@pre none
		*	@post none
		*   @return true if the sort parameter matches a valid one
		*/
		static bool isSortValid(std::string sortParameter);
		
		/**
		*	@pre none
		*	@post none
		*   @return true if file specified by inputFileName exists and sort parameter is valid, otherwise false
		*/
		static bool areParametersValid(std::string sortName, std::string inputFileName);
		
		/**
		*	@pre the input file was created with the number file generator
		*	@post the first line of the file is read in, containing the count
		*   @return returns how many numbers are in the file
		*/
		static int getFileCount(std::ifstream& inputFile);
		
		/**
		*	@pre the input file was created with the number file generator, the size was read in, and that size if correct
		*	@post the remainder of input file numbers are read in. file is NOT closed.
		*   @return pointer to new array containing the values from the input file.
		*/
		static int* createArray(std::ifstream& inputFile, int size);
		
		/**
		*	@pre original and copy are valid arrays of the correct size
		*	@post all valuees from original are copied into copy
		*   @return none
		*/
		static void copyArray(int original[], int copy[], int size);
};
#endif
