/**	@file : Position.h
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : a basic container class to hold row and column coords
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>

#ifndef POSITION_H
#define POSITION_H
 
class Position
{
	public:

	/**
	*	@post Position created with row and col values set.
	*/
	Position(int row, int col);


	/**
	*	@return row value
	*/
	int getRow() const;

	/**
	*	@return col value
	*/
	int getCol() const;

	private:
		int m_row;
		int m_col;
		
};
#endif
