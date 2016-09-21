/**	@file : Node.hpp
*	@author : John Ying
*	@date : 2015.11.3
*	Purpose : Implementation of the Node class
*/

#include <iostream>
#include <string>
#include <stdexcept>

template <typename T>
Node<T> :: Node()
{
	m_left = nullptr;
	m_right = nullptr;
	m_value = T();
}
template <typename T>
Node<T> :: Node(const Node<T>& other)
{
	m_left = other.getLeft();
	m_right = other.getRight();
	m_value = other.getValue();
}

template <typename T>
T Node<T> :: getValue() const
{
	return(m_value);
}

template <typename T>
void Node<T> :: setValue(T value)
{
	m_value = value;
}

template <typename T>
void Node<T> :: setLeft(Node<T>* left)
{
	m_left = left;
}

template <typename T>
Node<T>* Node<T> :: getLeft() const
{
	return(m_left);
}

template <typename T>
void Node<T> :: setRight(Node<T>* right)
{
	m_right = right;
}

template <typename T>
Node<T>* Node<T> :: getRight() const
{
	return(m_right);
}
