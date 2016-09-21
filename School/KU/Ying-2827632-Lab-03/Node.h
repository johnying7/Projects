/**	@file : Node.h
*	@author : John Ying
*	@date : 2015.9.23
*	Purpose : Creates a container with which to put objects into.
*/

#include <iostream>
#include <string>
#include <stdexcept>
#ifndef NODE_H
#define NODE_H

template <typename T>
class Node
{
    public:
        /**
        *	@pre none
        *	@post Creates and initializes a Node instances
        *   @return sets m_next and m_previous to nullptr and m_value to T()
        */
		Node();
        
        /**
        *	@pre none
        *	@post none
        *   @return gives the T type object of whatever is stored inside
        */
        T getValue() const;
        
        /**
        *	@pre requires an object of any type or class
        *	@post sets the m_value to what is input in the object
        *   @return none
        */
		void setValue(T value);
		
		/**
        *	@pre requires a pointer of any type
        *	@post changes the m_next pointer address
        *   @return none
        */
        void setNext(Node<T>* next);
        
        /**
        *	@pre none
        *	@post none
        *   @return gives the next object in the list
        */
        Node<T>* getNext() const;
        
        /**
        *	@pre requires a pointer of any type
        *	@post changes the m_previous pointer address
        *   @return none
        */
        void setPrevious(Node<T>* previous);
        
        /**
        *	@pre none
        *	@post none
        *   @return gives the object of whatever is behind this object in the list
        */
        Node<T>* getPrevious() const;

    private:
        T m_value; //The stored object in the node container
        Node<T>* m_previous; //previous object in the list
        Node<T>* m_next; //next object in the list
};
#include "Node.hpp"
#endif
