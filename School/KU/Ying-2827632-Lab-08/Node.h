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
        *	@post copy constructor
        *   @return creates a deep copy of the other node (also creates copies of any nodes being pointed to by other
        */
		Node(const Node<T>& other);
        
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
        void setLeft(Node<T>* left);
        
        /**
        *	@pre none
        *	@post none
        *   @return gives the next object in the list
        */
        Node<T>* getLeft() const;
        
        /**
        *	@pre requires a pointer of any type
        *	@post changes the m_previous pointer address
        *   @return none
        */
        void setRight(Node<T>* right);
        
        /**
        *	@pre none
        *	@post none
        *   @return gives the object of whatever is behind this object in the list
        */
        Node<T>* getRight() const;

    private:
        T m_value; //The stored object in the node container
        Node<T>* m_left; //left side of the node tree
        Node<T>* m_right; //right side of the node tree
};
#include "Node.hpp"
#endif
