 /**	@file : NumberFileDriver.cpp
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : Implementation of the NumberFileDriver class
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "NumberFileDriver.h"
 
/**
*	@pre valid 2-D array and the arguments contained within are valid.
*	@post If the arguments are valid, a number file is created following those specifications.Otherwise, the help menu is printed and no files are created.
*   @return none
*/
void NumberFileDriver :: run(int argc, char** argv)
{
	std::string mode = argv[2];
	std::string fileName = argv[3];
	int amt = atoi(argv[4]);
	if(isValidOption(mode))
	{
		if(argc == 5)
		{
			if(mode == "-a")
			{
				NumberFileGenerator::ascending(fileName,amt);
			}
			else if(mode == "-d")
			{
				NumberFileGenerator::descending(fileName,amt);
			}
		}
		else if(argc == 6 && mode == "-s")
		{
			int value = atoi(argv[5]);
			NumberFileGenerator::singleValue(fileName,amt,value);
		}
		else if(argc == 7 && mode == "-r")
		{
			int min = atoi(argv[5]);
			int max = atoi(argv[6]);
			NumberFileGenerator::random(fileName,amt,min,max);
		}
		else
		{
			printHelpMenu();
		}
	}
	else
	{
		printHelpMenu();
	}
}

/**
*	@pre none
*	@post contains help details on what counts as valid command line arguments
*   @return none
*/
void NumberFileDriver :: printHelpMenu()
{
	std::cout << "\nUse Number File Generator in one of the following ways:\n\n"
			<< "./prog -create -a filename amount\n"
			<< "./prog -create -d filename amount\n"
			<< "./prog -create -s filename amount value\n"
			<< "./prog -create -r filename amount min max\n"
			<< "Option explainations:\n"
			<< "\t-a for ascending\n"
			<< "\t-d for descending\n"
			<< "\t-s for a single value\n"
			<< "\t-r for random values\n"
			<< "\tfilename is the ouput file name\n"
			<< "\tamount is the amount of random numbers to place in the file\n"
			<< "\tvalue is the single number that will be written to file in -s mode\n"
			<< "\tmin is the low end of the range of random numbers\n"
			<< "\tmax is the high end (non-inclusive) of the range of random numbers\n";
}

/**
*	@pre none
*	@post Prints a menus to help the user use the NumberFileGenerator
*   @return True is the option passed in is valid. Valid options are: "-a", "-d", "-r", and "-s".	This is case sensitive.
*/
bool NumberFileDriver :: isValidOption(std::string option)
{
	std::string optionArr[] = {"-a","-d","-r","-s"};
	for(int i = 0; i<4; i++)
	{
		if(option == optionArr[i])
			return true;
	}
	return false;
}
