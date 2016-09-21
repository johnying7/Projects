/**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : main operation code that controls all core classes and interactions.
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "MazeReader.h"
#include "MazeWalker.h"
#include "ArrayHelper.h"
#include "Test.h"


int main(int argc, char** argv)
{
	std::string mode = argv[1];
	
	
	
	if(argc == 3)
	{
		std::string fileName = argv[2];
		MazeReader mr(fileName);
		
		if(mode == "-dfs")
		{
			try
			{
				MazeWalker walker(mr.getMaze(), mr.getStartRow(), mr.getStartCol(), 
							 mr.getRows(), mr.getCols(), Search::DFS);
				walker.walkMaze();
				std::cout << "The given maze map is: " << std::endl;
				ArrayHelper<char>::print2DArray(mr.getMaze(), mr.getRows(), mr.getCols(), "\t");	
				std::cout << "The pathing sequence is: " << std::endl;
				ArrayHelper<int>::print2DArray(walker.getVisited(), walker.getVisitedRows(), walker.getVisitedCols(), "\t");
			}
			catch(...)
			{
				std::cerr << "ERROR: Unexpected exception thrown" << std::endl;
			}
		}
		else if(mode == "-bfs")
		{
			try
			{
				MazeWalker walker(mr.getMaze(), mr.getStartRow(), mr.getStartCol(), 
							 mr.getRows(), mr.getCols(), Search::BFS);
				walker.walkMaze();
				std::cout << "The given maze map is: " << std::endl;
				ArrayHelper<char>::print2DArray(mr.getMaze(), mr.getRows(), mr.getCols(), "\t");	
				std::cout << "The pathing sequence is: " << std::endl;
				ArrayHelper<int>::print2DArray(walker.getVisited(), walker.getVisitedRows(), walker.getVisitedCols(), "\t");
			}
			catch(...)
			{
				std::cout << "ERROR: Unexpected exception thrown" << std::endl;
			}
		}
		else
		{
			std::cout << "Invalid input.\n";
			return 0;
		}
	}
	else if (argc == 2 && mode == "-test")
	{
		Test test;
		test.runTests();
	}
	/*
	
	
	 //Check for number file creation flag
	if(mode == "-bfs")
	{
		std::cout << "running breadth first search\n";
				
	}
	//Check for sort flag
	else if (mode == "dfs")
	{
		//std::cout << "running depth first search\n";
		
	}
	//Check for test flag
	else if (mode == "-test")
	{
		Test test(std::cout);
		test.runTests();
	}
	*/
	return 0;
}
