/**	@file : DoubleLinkedList.h
*	@author : John Ying
*	@date : 2015.9.23
*	Purpose : Creates a List of objects that can be modified on the go
*/

#include <iostream>
#include <string>
#include <stdexcept>
#ifndef DOUBLELINKEDLIST_H
#define DOUBLELINKEDLIST_H
#include "Node.h"

template <typename T>
class DoubleLinkedList
{
    public:
        /**
        *	@pre none
        *	@post initializes m_front and m_back to nullptr and size to 0
        *	@return none
        */
        DoubleLinkedList();
        
        /**
        *	@pre none
        *	@post removes memory objects in the list when delete is called
        *	@return none
        */
        ~DoubleLinkedList();
        
        /**
        *	@pre none
        *	@post none
        *	@return true if m_size is 0
        */
        bool isEmpty() const;
        
        /**
        *	@pre none
        *	@post none
        *	@return gives the value in m_size
        */
        int size() const;
        
        /**
        *	@pre put value of list type T
        *	@post creates and places the object in the front of the list
        *	@return none
        */
        void pushFront(T value);
        
        /**
        *	@pre put value of list type T
        *	@post creates and places the object in the back of the list
        *	@return none
        */
        void pushBack(T value);
        
        /**
        *	@pre none
        *	@post one object is removed from the back of the list
        *	@return true if an object was removed from the back of the list
        */
        bool removeBack();
        
        /**
        *	@pre none
        *	@post one object is removed from the front of the list
        *	@return true if an object was removed from the front of the list
        */
        bool removeFront();
        
        /**
        *	@pre value of type T to be removed from the list
        *	@post removes the value that matches in the list
        *	@return true if the object was removed from the list
        */
        bool remove(T value);
        
        /**
        *	@pre listValue is in the list, otherwise exception error
        *	@post size increased, new Node created named newValue, newValue is inserted in the position previous to listValue
        *	@return none
        */
        void insertAhead(T listValue, T newValue) throw(std::exception);
        
        /**
        *	@pre listValue is in the list, otherwise exception error
        *	@post size increased, new Node created named newValue, newValue is inserted in the position after listValue
        *	@return none
        */
        void insertBehind(T listValue, T newValue) throw(std::exception);
        
        /**
        *	@pre needs T type for the list search
        *	@post none
        *	@return pointer to first found value in the list or nullptr if not found
        */
        Node<T>* find(T value) const;
        
        /**
        *	@pre type T is overloaded to be printable via << operator
        *	@post if empty prints "", if not empty, print content separated by spaces (one line)
        *	@return outputs cout of the list
        */
        void printList() const;

    private:
        Node<T>* m_front; //looks at the first node in the list
        Node<T>* m_back; //looks at the last node in the list
        int m_size; //current size of the list
};
#include "DoubleLinkedList.hpp"
#endif

