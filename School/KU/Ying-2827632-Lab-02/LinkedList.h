/**	@file : LinkedList.h
*	@author : John Ying
*	@date : 2015.9.15
*	Purpose : Creates a List of integers that can be modified on the go
*/

#include <iostream>
#ifndef LINKEDLIST_H
#define LINKEDLIST_H
#include "Node.h"

class LinkedList
{
    public:
        /**
        *	@pre none
        *	@post Creates and initializes a Node instances
        *	@return Initializes Node with zero value and nullptr
        */
        LinkedList();

        /**
        *	@pre none
        *	@post none
        *	@return destructs and cleans memory on LinkedList class
        */
        ~LinkedList();

		/**
        *	@pre none
        *	@post none
        *	@return tells if the list has ANY pointers/nodes in it
        */
        bool isEmpty() const;
        
        /**
        *	@pre none
        *	@post none
        *	@return tells the user the current integer number of nodes in the list
        */
        int size() const;
        
        /**
        *	@pre give an integer to look for in the list
        *	@post keeps all variables the same
        *	@return tells the user if it is (true) or isn't in the list
        */
        bool search(int value) const;
        
        /**
        *	@pre prints nothing if the list is empty
        *	@post prints out the list of nodes
        *	@return none
        */
        void printList() const;
        
        /**
        *	@pre integer to be placed at the end of the list
        *	@post places another node in the (previously) last node in the list
        *	@return none
        */
        void addBack(int value);
        
        /**
        *	@pre integer to be placed at the front/beginning of the list
        *	@post places a node in the front of the list and changes the front node
        *	@return none
        */
        void addFront(int value);
        
        /**
        *	@pre none
        *	@post removes the last node and changes the second to last node pointer to nullptr
        *	@return tells the user if the node was removed or false if the list is empty
        */
        bool removeBack();
        
        /**
        *	@pre none
        *	@post removes the first node and changes the second node to now be the first
        *	@return true if the front node was removed successfully or false if the list is empty to begin with
        */
        bool removeFront();

    private:
        Node* m_front; //the first node in the list of objects
        int m_size; //size of the list
};
#endif

