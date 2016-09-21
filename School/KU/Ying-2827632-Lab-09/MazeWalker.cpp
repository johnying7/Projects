/**	@file : MazeWalker.cpp
*	@author : John Ying
*	@date : 2015.11.10
*	Purpose : Instance of hte MazeWalker class
*/

#include "MazeWalker.h"

/**
*	@pre The mazePtr pointer is to a valid maze.
*	@post A maze walker if created ready to traverse the maze from the start position is the specified order.
*/
MazeWalker :: MazeWalker(const char* const* mazePtr, int startRow, int startCol, int rows, int cols, Search searchChoice)
{
	m_searchType = searchChoice;
	m_maze = mazePtr;
	m_visited = new int*[rows];
	for(int i = 0; i < rows; i++)
	{
		m_visited[i] = new int[cols];
	}
	for(int j = 0; j < rows; j++)
	{
		for(int k = 0; k < cols; k++)
		{
			m_visited[j][k] = 0;
		}
	}
	
	m_rows = rows;
	m_cols = cols;
	
	m_startPos = Position(startRow,startCol);
	m_curPos = m_startPos;
	m_curStep = 2;
	m_visited[startRow][startCol] = 1;
	storeValidMoves();
}

/**
*
*	@pre The visited array still exists and has the same dimensions (rows X cols)
*	@post The visited array is deallocated
*/
MazeWalker :: ~MazeWalker()
{
	for(int i = 0; i < m_rows; i++)
	{
		delete[] m_visited[i];
	}
	delete[] m_visited;
}

/**
*	@pre The maze is a valid maze.
*	@post The maze is traversed until (either dfs or bfs) the end is reached or all moves are exhausted. 
*	@return true if an exit was reached, false otherwise
*/
bool MazeWalker :: walkMaze()
{	
	if(m_searchType == Search::DFS)
	{
		while(!isGoalReached() && !m_moveStack.empty())
		{
			move(m_moveStack.top());
			m_moveStack.pop();
			storeValidMoves();
		}
		
	}
	else if(m_searchType == Search::BFS)
	{
		while(!isGoalReached() && !m_moveQueue.empty())
		{			
			move(m_moveQueue.front());
			m_moveQueue.pop();
			storeValidMoves();
		}
	}
	
	if(isGoalReached())
	{
		std::cout << "Goal has been reached!!!!\n";
		return true;
	}
	else
	{
		std::cout << "Goal has NOT been reached!!!!\n";
		return false;
	}

	return false;
}


/**
*	@return A const pointer to the visited array. (A pointer that cannot change the array)
*/
const int* const* MazeWalker :: getVisited() const
{
	return m_visited;
}

/**
*	@return number of rows in maze
*/
int MazeWalker :: getVisitedRows() const
{
	return m_rows;
}

/**
*	@return number of cols in maze
*/
int MazeWalker :: getVisitedCols() const
{
	return m_cols;
}

/**
*	@return A const pointer to the maze. (A pointer that cannot change the array)
*/
const char* const* MazeWalker :: getMaze() const
{
	return m_maze;
}

/**
*	@pre The current position is valid.
*	@post Either the stack (dfs) or queue (bfs) is loaded with valid moves from the current position.
*/
void MazeWalker :: storeValidMoves()
{
	int cRow = m_curPos.getRow();
	int cCol = m_curPos.getCol();
	
	//up
	if( cRow-1 >= 0 &&
		m_visited[cRow-1][cCol] == 0 &&
		m_maze[cRow-1][cCol] != 'W')
	{
		Position up(cRow-1,cCol);
		if(m_searchType == Search::DFS)
			m_moveStack.push(up);
		else
			m_moveQueue.push(up);
	}
	//right
	if( cCol+1 < m_cols &&
		m_visited[cRow][cCol+1] == 0 &&
		m_maze[cRow][cCol+1] != 'W')
	{
		Position right(cRow,cCol+1);
		if(m_searchType == Search::DFS)
			m_moveStack.push(right);
		else
			m_moveQueue.push(right);
	}
	//down
	if( cRow+1 < m_rows &&
		m_visited[cRow+1][cCol] == 0 &&
		m_maze[cRow+1][cCol] != 'W')
	{
		Position right(cRow+1,cCol);
		if(m_searchType == Search::DFS)
			m_moveStack.push(right);
		else
			m_moveQueue.push(right);
	}
	//left
	if( cCol-1 >= 0 &&
		m_visited[cRow][cCol-1] == 0 &&
		m_maze[cRow][cCol-1] != 'W')
	{
		Position left(cRow,cCol-1);
		if(m_searchType == Search::DFS)
			m_moveStack.push(left);
		else
			m_moveQueue.push(left);
	}
}


/**
*	@pre The position is valid.
*	@post The current position is set to p and the position is updated as marked.
*/
void MazeWalker :: move(Position& p)
{
	m_curPos = p;
	m_visited[p.getRow()][p.getCol()] = m_curStep;
	m_curStep++;
}


/**
*	@returns If the current position is the exit, true is returned. False is returned otherwise.
*/		
bool MazeWalker :: isGoalReached() const
{
	if(m_maze[m_curPos.getRow()][m_curPos.getCol()] == 'E')
	{
		return true;
	}
	else
	{
		return false;
	}
}

