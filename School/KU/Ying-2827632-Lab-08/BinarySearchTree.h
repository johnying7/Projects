/**	@file : BinarySearchTree.h
*	@author : John Ying
*	@date : 2015.11.3
*	Purpose : contains the methods for organizing and using a binary search tree
*/

#include <iostream>
#include <string>
#include <stdexcept>
#ifndef BINARYSEARCHTREE_H
#define BINARYSEARCHTREE_H
#include "Node.h"
#include "BSTI.h"

template <typename T>
class BinarySearchTree : public BSTI<T>
{
	public:
		/**
        *	@pre none
        *	@post Constructor that initializes BinarySearchTree
        *   @return sets m_root to nullptr
        */
		BinarySearchTree();
		
		/**
        *	@pre none
        *	@post Copy Constructor that creates a BinarySearchTree with given "other" values
        *   @return sets values to other's values
        */
		BinarySearchTree(const BinarySearchTree<T>& other);
		
		/**
        *	@pre none
        *	@post destructor
        *   @return none
        */
		~BinarySearchTree();
		
		/**
        *	@pre none
        *	@post none
        *   @return a vector with the contents of the tree in the specified order
        */
		std::vector<T> treeToVector(Order order) const;
		
		
		/**
		*	@pre this is in a valid state
		*	@post creates a deep copy of this
		*   @return returns a pointer to a deep copy of this
		*/
		BSTI<T>* clone();

		/**
		*	@pre none
		*	@post none
		*   @return true if no nodes in the tree, otherwise returns false.
		*/
		bool isEmpty() const;

		/**
		*	@pre value is a valid T
		*	@post a new Node<T> is created an inserted into the tree based
		*   @return false if the value couldn't be added (i.e. duplicate values not allowed)
		*/
		bool add(T value);

		/**
		*	@pre The type T is comparable by the == operator
		*	@post none
		*   @return true if the value is in the tree, false otherwise
		*/
		bool search(T value) const;

		/**
		*	@pre none
		*	@post the contents of the tree are printed to the console in the specified order
		*   @return none
		*/
		void printTree(Order order) const;
		
	private:
		Node<T>* m_root; //ptr always looking at the root of the tree
		
		/**
		*	@pre none
		*	@post adds value to the correct branch (left/right) of subtree
		*   @return true if add operation successful
		*/
		bool add(T value, Node<T>* subtree);
		
		/**
		*	@pre none
		*	@post deletes tree completely
		*   @return none
		*/
		void deleteTree(Node<T>* subTree);
		
		/**
		*	@pre none
		*	@post none
		*   @return true if value is in the tree
		*/
		bool search(T value, Node<T>* subtree) const;
		
		/**
		*	@pre none
		*	@post prints tree in PRE_ORDER or IN_ORDER or POST_ORDER using subtree and the "order"
		*   @return none
		*/
		void printTree(Node<T>* subtree, Order order) const;
		
		/**
		*	@pre none
		*	@post recursive helper function to load the vector, vec in the specified order
		*   @return none
		*/
		void treeToVector(Node<T>* subtree, Order order, std::vector<T>& vec) const;
};
#include "BinarySearchTree.hpp"
#endif
