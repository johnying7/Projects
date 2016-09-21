/**	@file : MazeReader.cpp
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : initialization of hte mazereader class
*/

#include "MazeReader.h"

/**
*	@post A MazeReader is created. A 2D char array is allocated with the maze information.
*	@throws MazeCreationExecption
*
*/
MazeReader::MazeReader(std::string file) throw (MazeCreationException)
{
	std::ifstream myInputFile(file);

	if( !myInputFile.good() )
	{
	   throw MazeCreationException("File doesn't exist.\n");//we couldn't access the file. 
	}
	
	myInputFile >> m_rows;
	myInputFile >> m_cols;
	if(m_rows < 1 || m_cols < 1)
	{
		throw MazeCreationException("Maze not large enough.\n");
	}
	//std::cout << "\nrows: " << m_rows << "\ncols: " << m_cols << "\n";
	
	myInputFile >> m_startRow;
	myInputFile >> m_startCol;
	
	if(m_startRow < 0 || m_startRow > m_rows || m_startCol < 0 || m_startCol > m_cols)
	{
		throw MazeCreationException("Maze start position not valid based on described ranges.\n");
	}
	
	//std::cout << "\nStartRow: " << m_startRow << "\nStartCol: " << m_startCol << "\n";
	
	maze = new char*[m_rows];
	for(int k = 0; k < m_rows; k++)
	{
		maze[k]=new char[m_cols];
	}
	
	for(int i = 0; i < m_rows; i++)
	{
		for(int j = 0; j < m_cols; j++)
		{
			myInputFile >> maze[i][j];
		}
	}
	myInputFile.close();
}

/**
*	@post The maze is deallocated.
*/
MazeReader::~MazeReader()
{
	for(int i = 0; i < m_rows; i++)
	{
		delete[] maze[i];
	}
	delete[] maze;
}

/**
*	@pre the file was formatting and read in correctly
*	@return Returns pointer to the maze
*/
const char* const* MazeReader::getMaze() const
{
	return maze;
}

/**
*	@pre the file was formatted and read in correctly
*	@returns the number of columns listed in the file
*/
int MazeReader::getCols() const
{
	return m_cols;
}

/**
*	@pre the file was formatted and read in correctly
*	@returns the number of rows listed in the file
*/
int MazeReader::getRows() const
{
	return m_rows;
}

/**
*	@pre the file was formatted and read in correctly
*	@returns the starting column
*/
int MazeReader::getStartCol() const
{
	return m_startCol;
}

/**
*	@pre the file was formatted and read in correctly
*	@returns the starting row
*/
int MazeReader::getStartRow() const
{
	return m_startRow;
}

/**
*	@pre the file is properly formatted
*	@post the characters representing the maze are stored
*/
void MazeReader::readMaze()	throw (MazeCreationException)
{
	
}
