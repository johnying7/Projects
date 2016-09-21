/**	@file : Stack.h
*	@author : John Ying
*	@date : 2015.9.29
*	Purpose : class that inherits the pure virtual stack class and defines its methods
*/ 

#ifndef STACK_H
#define STACK_H
#include <iostream>
#include <string>
#include <stdexcept>
#include "Node.h"
#include "StackInterface.h"
#include "PreconditionViolationException.h"

template <typename T>
class Stack : public StackInterface<T>
{
 	public:
 		/**
        *	@pre none
        *	@post Creates and initializes a Stack instance
        *	@return sets m_top to nullptr and m_size to 0
        */
	 	Stack();
 	
	 	/**
        *	@pre none
        *	@post destroys Stack instance
        *	@return none
        */
	 	~Stack();
	 	
	 	/**
        *	@pre none
        *	@post none
        *	@return true if m_size is 0 otherwise false
        */
	 	bool isEmpty() const;
	 	
	 	/**
        *	@pre newEntry of any type
        *	@post adds newEntry to the top of the stack list
        *	@return none
        */
	 	void push(const T newEntry);
	 	
	 	/**
        *	@pre assumes stack isn't empty otherwise throws preconditionviolationexception error
        *	@post removes top object from the stack
        *	@return none
        */
	 	void pop() throw(PreconditionViolationException);
	 	
	 	/**
        *	@pre assumes stack isn't empty, otherwise throws preconditionviolationexception error
        *	@post none
        *	@return the value at the top of the stack
        */
	 	T peek() const throw(PreconditionViolationException);
	 	
	 	/**
        *	@pre none
        *	@post none
        *	@return gives m_size
        */
	 	int size() const;
	 	
	 	/**
        *	@pre none
        *	@post if empty, prints empty string
        *	@return output prints the contents of the stack, last pushed first
        */
	 	void print() const;
	 	
	 private:
	 	Node<T>* m_top;
	 	int m_size;
};
#include "Stack.hpp"
#endif
