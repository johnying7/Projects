 /**	@file : SortDriver.cpp
*	@author : John Ying
*	@date : 2015.10.27
*	Purpose : Implementation of the Sort Driver class
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "SortDriver.h"

/**
*	@pre valid 2-D array and input from the command line arguments also valid
*	@post file is created containing the timing info of chosen sorts on the given file
*   @return none
*/
void SortDriver :: run(int argc, char** argv)
{
	
	if(SortDriver::areParametersValid(argv[2],argv[3]))
	{
		std::string sortType = argv[2];
		
		std::ifstream inputFile(argv[3]);
		std::ofstream myOutputFile(argv[4]);
		
		int numberOfNums = getFileCount(inputFile);
		int* array = createArray(inputFile,numberOfNums);
		
		
		if(sortType == "-bubble")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::bubbleSort,array, numberOfNums);
			myOutputFile << "bubble " << numberOfNums << " " << time;
		}
		else if(sortType == "-selection")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::selectionSort,array, numberOfNums);
			myOutputFile << "selection " << numberOfNums << " " << time;
		}
		else if(sortType == "-insertion")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::insertionSort,array, numberOfNums);
			myOutputFile << "insertion " << numberOfNums << " " << time;
		}
		else if(sortType == "-quick")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::quickSort,array, numberOfNums);
			myOutputFile << "quick " << numberOfNums << " " << time;
		}
		else if(sortType == "-quick3")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::quickSortWithMedian,array, numberOfNums);
			myOutputFile << "quick3 " << numberOfNums << " " << time;
		}
		else if(sortType == "-merge")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::mergeSort,array, numberOfNums);
			myOutputFile << "merge " << numberOfNums << " " << time;
		}
		else if(sortType == "-all")
		{
			double time = Sorts<int>::sortTimer(Sorts<int>::bubbleSort,array, numberOfNums);
			myOutputFile << "bubble " << numberOfNums << " " << time << std::endl;

			time = Sorts<int>::sortTimer(Sorts<int>::selectionSort,array, numberOfNums);
			myOutputFile << "selection " << numberOfNums << " " << time << std::endl;

			time = Sorts<int>::sortTimer(Sorts<int>::insertionSort,array, numberOfNums);
			myOutputFile << "insertion " << numberOfNums << " " << time << std::endl;

			time = Sorts<int>::sortTimer(Sorts<int>::quickSort,array, numberOfNums);
			myOutputFile << "quick " << numberOfNums << " " << time << std::endl;

			time = Sorts<int>::sortTimer(Sorts<int>::quickSortWithMedian,array, numberOfNums);
			myOutputFile << "quick3 " << numberOfNums << " " << time << std::endl;

			time = Sorts<int>::sortTimer(Sorts<int>::mergeSort,array, numberOfNums);
			myOutputFile << "merge " << numberOfNums << " " << time;
		}
		else
		{
			std::cout << "invalid input\n";
		}
		
		delete []array;
		myOutputFile.close();
	}
}

/**
*	@pre none
*	@post prints out menu to help the user use the NumberFileGenerator
*   @return none
*/
void SortDriver :: printHelpMenu()
{
	std::cout << "\nSorting is done one of the following ways:\n\n"
			<< "./prog -sort -bubble inputFile outputFile\n"
			<< "./prog -sort -selection inputFile outputFile\n"
			<< "./prog -sort -insertion inputFile outputFile\n"
			<< "./prog -sort -quick inputFile outputFile\n"
			<< "./prog -sort -quick3 inputFile outputFile\n"
			<< "./prog -sort -merge inputFile outputFile\n"
			<< "./prog -sort -all inputFile outputFile\n"
			<< "Option explainations:\n"
			<< "\t-bubble to time bubble sort and store all timing results in outputFile\n"
			<< "\t-selection to time selection sort and store all timing results in outputFile\n"
			<< "\t-insertion to time insertion sort and store all timing results in outputFile\n"
			<< "\t-quick to time quick sort and store all timing results in outputFile\n"
			<< "\t-quick3 to time quick3 sort  and store all timing results in outputFile\n"
			<< "\t-merge to time merge sort  and store all timing results in outputFile\n"
			<< "\t-all to time all of the sorts and store all timing results in outputFile\n"
			<< "\tinputFile must be file created by a NumberFileGenerator\n"
			<< "\toutputFile must be to a valid path. It will hold the timing results\n";
}

/**
*	@pre Input file created by NumberFileGenerator
*	@post none
*   @return true if file exists, false otherwise
*/
bool SortDriver :: isFileAccessible(std::string fileName)
{
	std::ifstream file(fileName);
	
	if(file.good())
	{
		return true;
	}
	else
	{
		return false;
	}
}

/**
*	@pre none
*	@post none
*   @return true if the sort parameter matches a valid one
*/
bool SortDriver :: isSortValid(std::string sortParameter)
{
	std::string listOfSorts[] = {"-bubble", "-selection","-insertion", "-quick", "-quick3", "-merge","-all"};
	for(int i = 0; i < 7; i++)
	{
		if(sortParameter == listOfSorts[i])
			return true;
	}
	
	return false;
}

/**
*	@pre none
*	@post none
*   @return true if file specified by inputFileName exists and sort parameter is valid, otherwise false
*/
bool SortDriver :: areParametersValid(std::string sortName, std::string inputFileName)
{
	if(SortDriver::isFileAccessible(inputFileName) && SortDriver::isSortValid(sortName))
		return true;
	else
		return false;
}

/**
*	@pre the input file was created with the number file generator
*	@post the first line of the file is read in, containing the count
*   @return returns how many numbers are in the file
*/
int SortDriver :: getFileCount(std::ifstream& inputFile)
{
	int firstNumber = 0;
	
	inputFile >> firstNumber;
	
	return firstNumber;
}

/**
*	@pre the input file was created with the number file generator, the size was read in, and that size if correct
*	@post the remainder of input file numbers are read in. file is NOT closed.
*   @return pointer to new array containing the values from the input file.
*/
int* SortDriver :: createArray(std::ifstream& inputFile, int size)
{
	int* arr = new int[size];
	for(int i = 0; i < size; i++)
	{
		inputFile >> arr[i];
	}
	return arr;
}

/**
*	@pre original and copy are valid arrays of the correct size
*	@post all valuees from original are copied into copy
*   @return none
*/
void SortDriver :: copyArray(int original[], int copy[], int size)
{
	for(int i = 0; i < size; i++)
	{
		copy[i] = original[i];
	}
}
