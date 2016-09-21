/**	@file : Node.cpp
*	@author : John Ying
*	@date : 2015.9.15
*	Purpose : Implementation of the node class
*/

#include <iostream>
#include "Node.h"

Node :: Node()
{
    m_value = 0;
    m_next = nullptr;
}

void Node :: setValue(int value)
{
	m_value = value;
}

int Node :: getValue() const
{
	return(m_value);
}

void Node :: setNext(Node* prev)
{
	m_next = prev;
}

Node* Node :: getNext() const
{
	return(m_next);
}
