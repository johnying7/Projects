/**	@file : BSTI.h
*	@author : John Ying
*	@date : 2015.11.3
*	Purpose : interface class for binary search tree
*/
#ifndef BSTI_H
#define BSTI_H
#include <vector>

enum Order {PRE_ORDER, IN_ORDER, POST_ORDER};

template <typename T>
class BSTI
{
	public:
	/**
    *	@pre A BSTI exists
    *	@post deletes the entire tree
    *   @return none
    */
    virtual ~BSTI(){};
	
	/**
    *	@pre this is in a valid state
    *	@post creates a deep copy of this
    *   @return returns a pointer to a deep copy of this
    */
    virtual BSTI<T>* clone() = 0;
    
    /**
    *	@pre none
    *	@post none
    *   @return true if no nodes in the tree, otherwise returns false.
    */
    virtual bool isEmpty() const = 0;
    
    /**
    *	@pre value is a valid T
    *	@post a new Node<T> is created an inserted into the tree based
    *   @return false if the value couldn't be added (i.e. duplicate values not allowed)
    */
    virtual bool add(T value) = 0;
    
    /**
    *	@pre The type T is comparable by the == operator
    *	@post none
    *   @return true if the value is in the tree, false otherwise
    */
    virtual bool search(T value) const = 0;
    
    /**
    *	@pre none
    *	@post the contents of the tree are printed to the console in the specified order
    *   @return none
    */
    virtual void printTree(Order order) const = 0;
    
    /**
    *	@pre none
    *	@post none
    *   @return a vector with the contents of the tree in the specified order is returned
    */
    virtual std::vector<T> treeToVector(Order order) const = 0;
};
#endif
