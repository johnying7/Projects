/**	@file : NumberFileGenerator.cpp
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : Implementation of the NumberFileGenerator class
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <random>
#include <ctime>
#include <cassert>
#include <fstream>
#include "NumberFileGenerator.h"

/**
*	@pre fileName is valid path/filename. Amount is greater than zero
*	@post A file is created with the amount of number is ascending order. The amount of numbers in the file is the first entry in the file
*   @return none
*/
void NumberFileGenerator :: ascending(std::string fileName, int amount)
{
	std::ofstream myOutputFile(fileName);
	myOutputFile << amount << std::endl;
	myOutputFile << 0;
	for(int i = 1; i < amount; i++)
	{
		myOutputFile << std::endl << i;
	}
	myOutputFile.close();
}

/**
*	@pre fileName is valid path/filename. Amount is greater than zero
*	@post A file is created with the amount of number is descending order. The amount of numbers in the file is the first entry in the file
*   @return none
*/
void NumberFileGenerator :: descending(std::string fileName, int amount)
{
	std::ofstream myOutputFile(fileName);
	myOutputFile << amount << std::endl;
	myOutputFile << amount-1;
	for(int i = amount-2; i >= 0; i--)
	{
		myOutputFile << std::endl << i ;
	}
	myOutputFile.close();
}

/**
*	@pre fileName is valid path/filename. Amount is greater than zero
*	@post A file is created with the specifed amount of numbers. All number are random and fall between min and max (inclusively). The amount of numbers in the file is the first entry in the file
*   @return none
*/
void NumberFileGenerator :: random(std::string fileName, int amount, int min, int max)
{
	std::ofstream myOutputFile(fileName);
	myOutputFile << amount << std::endl;
	
	std::default_random_engine generator(time(nullptr));
	std::uniform_int_distribution<int> distribution(min,max);
	
	myOutputFile << distribution(generator);
	for(int i = 1; i < amount; i++)
	{
		myOutputFile  << std::endl << distribution(generator);
	}
	myOutputFile.close();
}

/**
*	@pre fileName is valid path/filename. Amount is greater than zero
*	@post A file is created with a single number, specified by value, amount of times. The amount of numbers in the file is the first entry in the file
*   @return none
*/
void NumberFileGenerator :: singleValue(std::string fileName, int amount, int value)
{
	std::ofstream myOutputFile(fileName);
	myOutputFile << amount << std::endl;
	
	myOutputFile << value;
	for(int i = 1; i < amount; i++)
	{
		myOutputFile << std::endl << value;
	}
	myOutputFile.close();
}
