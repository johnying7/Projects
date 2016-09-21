/**	@file : StackInterface.h
*	@author : John Ying
*	@date : 2015.9.29
*	Purpose : pure virtual class that creates an interface for derived stack classes
*/ 

#ifndef STACKINTERFACE_H
#define STACKINTERFACE_H
#include <iostream>
#include <string>
#include <stdexcept>
#include "PreconditionViolationException.h"

template <typename T>
class StackInterface
{
	public:
		/**
		*	@pre none
		*	@post none
		*	@return none
		*/
	 	virtual ~StackInterface() {};
	 	
	 	/**
		*	@pre none
		*	@post none
		*	@return true if the size of the stack is empty
		*/
	 	virtual bool isEmpty() const = 0;
	 	
	 	/**
		*	@pre none
		*	@post adds T type object to the top of the stack
		*	@return none
		*/
	 	virtual void push(const T newEntry) = 0;
	 	
	 	/**
		*	@pre assumes stack is not empty otherwise throws exception
		*	@post removes top of stack
		*	@return none
		*/
	 	virtual void pop() throw(PreconditionViolationException) = 0;
	 	
	 	/**
		*	@pre assumes stack isn't empty otherwise throws exception
		*	@post none
		*	@return value at the top of the stack
		*/
	 	virtual T peek() const throw(PreconditionViolationException) = 0;
	 	
	 	/**
		*	@pre none
		*	@post none
		*	@return size of the stack
		*/
	 	virtual int size() const = 0;
	 	
	 	/**
		*	@pre none
		*	@post none
		*	@return prints contents of the stack
		*/
	 	virtual void print() const = 0;
};
#endif
