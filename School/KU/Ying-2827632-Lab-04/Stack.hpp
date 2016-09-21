/**	@file : Stack.hpp
*	@author : John Ying
*	@date : 2015.9.29
*	Purpose : Implementation of the Stack class
*/ 

#include <iostream>
#include <string>
#include <stdexcept>

template <typename T>
Stack<T> :: Stack()
{
	m_top = nullptr;
	m_size = 0;
}

template <typename T>
Stack<T> :: ~Stack()
{
	while(!isEmpty())
	{
		pop();
	}
}

template <typename T>
bool Stack<T> :: isEmpty() const
{
	if(m_size == 0)
	{
		return (true);
	}
	else
	{
		return(false);
	}
}

template <typename T>
void Stack<T> :: push(const T newEntry)
{
	if(isEmpty())
	{
		m_top = new Node<T>();
		m_top->setValue(newEntry);
		m_size++;
	}
	else if(!isEmpty())
	{
		Node<T>* newValue = new Node<T>();
		newValue->setValue(newEntry);
		newValue->setNext(m_top);
		m_top->setPrevious(newValue);
		m_top = newValue;
		m_size++;
	}
}

template <typename T>
void Stack<T> :: pop() throw(PreconditionViolationException)
{
	if(isEmpty())
	{
		throw(PreconditionViolationException("error"));
	}
	else if(m_size == 1)
	{
		m_size--;
		delete m_top;
		m_top = nullptr;
	}
	else if(m_size > 1)
	{
		m_size--;
		Node<T>* newFront = nullptr;
		newFront = m_top->getNext();
		delete m_top;
		m_top = newFront;
	}
	else
	{
		std::cout << "removeFront size (negative) error.\n";
	}
}

template <typename T>
T Stack<T> :: peek() const throw(PreconditionViolationException)
{
	if(isEmpty())
	{
		throw (PreconditionViolationException("error"));
	}
	else
	{
		return(m_top->getValue());
	}
}

template <typename T>
int Stack<T> :: size() const
{
	return(m_size);
}

template <typename T>
void Stack<T> :: print() const
{
	if(isEmpty())
	{
		std::cout << "";
	}
	else
	{
		Node<T>* traverse = m_top;
		std::cout << traverse->getValue() << " ";
		while(traverse != nullptr)
		{
			traverse = traverse->getNext();
			if(traverse == nullptr)
			{
				return;
			}
			std::cout << traverse->getValue() << " ";
		}
	}
}
