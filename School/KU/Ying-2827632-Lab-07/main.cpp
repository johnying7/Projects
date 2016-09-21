 /**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : main operation code that controls all core classes and interactions.
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "Sorts.h"
#include "Test.h"
#include "NumberFileDriver.h"
#include "SortDriver.h"

int main(int argc, char** argv)
{
	
	std::string mode = argv[1];
	
	 //Check for number file creation flag
	if(mode == "-create")
	{
		std::cout << "created!!!!\n";
		NumberFileDriver::run(argc, argv);		
	}
	//Check for sort flag
	else if (mode == "-sort")
	{
		SortDriver::run(argc, argv);
	}
	//Check for test flag
	else if (mode == "-test")
	{
		Test test(std::cout);
		test.runTests();
	}
	else if(mode == "-r")
	{
		NumberFileDriver::run(argc, argv);		
	}
	return 0;
}
