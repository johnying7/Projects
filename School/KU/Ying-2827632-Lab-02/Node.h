/**	@file : Node.h
*	@author : John Ying
*	@date : 2015.9.15
*	Purpose : Creates a container with which to put integers into.
*/

#include <iostream>
#ifndef NODE_H
#define NODE_H

class Node
{
    public:
        /**
        *	@pre none
        *	@post Creates and initializes a Node instances
        *   @return Initializes Node with zero value and nullptr
        */
        Node();

        /**
        *	@pre set the value to any integer
        *	@post changes the m_value private variable
        *	@return none
        */
		void setValue(int value);
		
		/**
        *	@pre none
        *	@post none
        *	@return returns m_value in the node
        */
        int getValue() const;
        
        /**
        *	@pre requires the node pointer previous in the sequence
        *	@post changes the current node to the input
        *	@return none
        */
        void setNext(Node* prev);
        
        /**
        *	@pre none
        *	@post none
        *	@return gives the next node in the list sequence
        */
        Node* getNext() const;
        

    private:
        int m_value; //the value in the node
        Node* m_next; //the pointer to another node in the list
};
#endif
