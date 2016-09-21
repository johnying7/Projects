/**	@file : LinkedList.cpp
*	@author : John Ying
*	@date : 2015.9.15
*	Purpose : Implementation of the LinkedList class
*/

#include <iostream>
#include "LinkedList.h"

LinkedList :: LinkedList()
{
    m_size = 0;
    m_front = nullptr;
}

LinkedList :: ~LinkedList()
{
	while(!isEmpty())
	{
		removeBack();
	}
}

bool LinkedList :: isEmpty() const
{
	if(m_size == 0)
	{
		return(true);
	}
	else
	{
		return(false);
	}
}

int LinkedList :: size() const
{
	return(m_size);
}

bool LinkedList :: search(int value) const
{
	Node* traverse = nullptr;
	
	if(!isEmpty())
	{
		traverse = m_front;
		while(traverse->getNext() != nullptr)//hits each node
		{
			if(traverse->getValue() == value)
			{
				return(true);
			}
			traverse = traverse->getNext();
		}
		if(traverse->getValue() == value)
		{
			return(true);
		}
		std::cout << "Could not find the value in the list.\n";
		return(false);
	}
	else
	{
		std::cout << "Sorry, the list is empty and cannot find a value that does not exist." << std::endl;
		return(false);
	}
}

void LinkedList :: printList() const
{
	Node* traverse = nullptr;
	if(!isEmpty())
	{
		traverse = m_front;
		std::cout << traverse->getValue() << " ";
		while(traverse->getNext() != nullptr)//hits each node
		{
			traverse = traverse->getNext();
			std::cout << traverse->getValue() << " ";
		}
		std::cout << std::endl;
	}
	else
	{
		std::cout << "";
	}
	
}

void LinkedList :: addBack(int value)
{
	Node* traverse = nullptr;
	
	if(m_size == 0)
	{
		m_front = new Node();
		m_front -> setValue(value);
		m_size++;
	}
	else if(m_size > 0)
	{
		m_size++;
		traverse = m_front;
		while (traverse->getNext() != nullptr)//finds last node
		{
			traverse = traverse->getNext();
		}
		Node* temp = new Node();
		temp->setValue(value);
		traverse->setNext(temp);
	}
}

void LinkedList :: addFront(int value)
{
	Node* temp = nullptr;
	
	if(m_size == 0)
	{
		m_front = new Node();
		m_front -> setValue(value);
		m_size++;
	}
	else if(m_size > 0)
	{
		m_size++;
		temp = m_front;
		m_front = new Node();
		m_front->setValue(value);
		m_front->setNext(temp);
	}
}

bool LinkedList :: removeBack()
{
	Node* traverse = nullptr;
	Node* temp = nullptr;
	if(isEmpty())
	{
		return(false);
	}
	else
	{
		m_size--;
		traverse = m_front;
		
		while(traverse->getNext() != nullptr)
		{
			temp = traverse;
			traverse = traverse->getNext();
		}
		
		//traverse = traverse->getNext();
		delete traverse;
		if(m_size != 0)
		{
			temp->setNext(nullptr);
		}
		traverse = nullptr;
		return(true);
	}
}

bool LinkedList :: removeFront()
{
	Node* temp = nullptr;
	
	if(isEmpty())//if size is 0
	{
		return(false);
	}
	else
	{
		m_size--;
		temp = m_front;
		m_front = m_front->getNext();
		delete temp;
		temp = nullptr;
		return(true);
	}
}
