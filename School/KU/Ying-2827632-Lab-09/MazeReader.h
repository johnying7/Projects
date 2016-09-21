/**	@file : MazeReader.h
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : Imports a maze from file and returns its values.
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>
#include "MazeCreationException.h"

#ifndef MAZEREADER_H
#define MAZEREADER_H
 
class MazeReader
{
	public:	
	
		/**
		*	@post A MazeReader is created. A 2D char array is allocated with the maze information.
		*	@throws MazeCreationExecption
		*
		*/
		MazeReader(std::string file) throw (MazeCreationException);

		/**
		*	@post The maze is deallocated.
		*/
		~MazeReader();

		/**
		*	@pre the file was formatting and read in correctly
		*	@return Returns pointer to the maze
		*/
		const char* const* getMaze() const;

		/**
		*	@pre the file was formatted and read in correctly
		*	@returns the number of columns listed in the file
		*/
		int getCols() const;

		/**
		*	@pre the file was formatted and read in correctly
		*	@returns the number of rows listed in the file
		*/
		int getRows() const;

		/**
		*	@pre the file was formatted and read in correctly
		*	@returns the starting column
		*/
		int getStartCol() const;

		/**
		*	@pre the file was formatted and read in correctly
		*	@returns the starting row
		*/
		int getStartRow() const;

		protected:
		/**
		*	@pre the file is properly formatted
		*	@post the characters representing the maze are stores
		*/
		void readMaze()	throw (MazeCreationException);
		
	private:
		int m_rows;
		int m_cols;
		int m_startRow;
		int m_startCol;
		char** maze;
		
};
#endif
