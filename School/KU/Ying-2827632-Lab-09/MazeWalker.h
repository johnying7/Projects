/**	@file : MazeWalker.h
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : Navigates the given 2D maze
*/

#include <iostream>
#include <string>
#include <chrono>
#include <functional>
#include <ctime>
#include <cassert>
#include <fstream>

#include "Position.h"
#include <stack>
#include <queue>

#ifndef MAZEWALKER_H
#define MAZEWALKER_H

enum class Search
{
	DFS,
	BFS
};

class MazeWalker
{
	public:
		/**
		*	@pre The mazePtr pointer is to a valid maze.
		*	@post A maze walker if created ready to traverse the maze from the start position is the specified order.
		*/
		MazeWalker(const char* const* mazePtr, int startRow, int startCol, int rows, int cols, Search searchChoice);

		/**
		*
		*	@pre The visited array still exists and has the same dimensions (rows X cols)
		*	@post The visited array is deallocated
		*/
		~MazeWalker();

		/**
		*	@pre The maze is a valid maze.
		*	@post The maze is traversed until (either dfs or bfs) the end is reached or all moves are exhausted. 
		*	@return true if an exit was reached, false otherwise
		*/
		bool walkMaze();


		/**
		*	@return A const pointer to the visited array. (A pointer that cannot change the array)
		*/
		const int* const* getVisited() const;

		/**
		*	@return number of rows in maze
		*/
		int getVisitedRows() const;

		/**
		*	@return number of cols in maze
		*/
		int getVisitedCols() const;

		/**
		*	@return A const pointer to the maze. (A pointer that cannot change the array)
		*/
		const char* const* getMaze() const;

	protected:
		/**
		*	@pre The current position is valid.
		*	@post Either the stack (dfs) or queue (bfs) is loaded with valid moves from the current position.
		*/
		void storeValidMoves();


		/**
		*	@pre The position is valid.
		*	@post The current position is set to p and the position is updated as marked.
		*/
		void move(Position& p);


		/**
		*	@returns If the current position is the exit, true is returned. False is returned otherwise.
		*/		
		bool isGoalReached() const;
	

		Search m_searchType;
		const char* const* m_maze;
		int** m_visited;
		int m_rows, m_cols;
		Position m_curPos = Position(0,0);
		Position m_startPos = Position(0,0);
	
		std::stack<Position> m_moveStack;
		std::queue<Position> m_moveQueue;
	
		int m_curStep;
		
};
#endif
