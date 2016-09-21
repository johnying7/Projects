/**	@file : DoubleLinkedList.cpp
*	@author : John Ying
*	@date : 2015.9.23
*	Purpose : Implementation of the LinkedList class
*/

#include <iostream>
#include <string>
#include <stdexcept>

template <typename T>
DoubleLinkedList<T> :: DoubleLinkedList()
{
	m_front = nullptr;
	m_back = nullptr;
	m_size = 0;
}

template <typename T>
DoubleLinkedList<T> :: ~DoubleLinkedList()
{
	while(!isEmpty())
	{
		removeBack();
	}
}

template <typename T>
bool DoubleLinkedList<T> :: isEmpty() const
{
	if(m_size == 0)
	{
		return(true);
	}
	else
	{
		return(false);
	}
}

template <typename T>
int DoubleLinkedList<T> :: size() const
{
	return(m_size);
}

template <typename T>
void DoubleLinkedList<T> :: pushFront(T value)
{
	if(isEmpty())
	{
		m_front = new Node<T>();
		m_front->setValue(value);
		m_back = m_front;
		m_size++;
	}
	else if(!isEmpty())
	{
		Node<T>* newValue = new Node<T>();
		newValue->setValue(value);
		newValue->setNext(m_front);
		m_front->setPrevious(newValue);
		m_front = newValue;
		m_size++;
	}
	//std::cout << "You added " << value << " to the front of the list.\n";
}

template <typename T>
void DoubleLinkedList<T> :: pushBack(T value)
{
	if(isEmpty())
	{
		m_front = new Node<T>();
		m_front->setValue(value);
		m_back = m_front;
		m_size++;
	}
	else if(!isEmpty())
	{
		Node<T>* newValue = new Node<T>();
		newValue->setValue(value);
		newValue->setPrevious(m_back);
		m_back->setNext(newValue);
		m_back = newValue;
		m_size++;
	}
	//std::cout << "You added " << value << " to the back of the list.\n";
}

template <typename T>
bool DoubleLinkedList<T> :: removeBack()
{
	if(isEmpty())
	{
		return(false);
	}
	else if(m_size == 1)
	{
		m_size--;
		delete m_front;
		m_front = nullptr;
		m_back = nullptr;
		return(true);
	}
	else if(m_size > 1)
	{
		m_size--;
		Node<T>* newBack = m_back->getPrevious();
		newBack->setNext(nullptr);
		delete m_back;
		m_back = newBack;
		return(true);
	}
	else
	{
		std::cout << "removeBack size (negative) error.\n";
		return(false);
	}
}

template <typename T>
bool DoubleLinkedList<T> :: removeFront()
{
	if(isEmpty())
	{
		return(false);
	}
	else if(m_size == 1)
	{
		m_size--;
		delete m_front;
		m_front = nullptr;
		m_back = nullptr;
		return(true);
	}
	else if(m_size > 1)
	{
		m_size--;
		Node<T>* newFront = nullptr;
		newFront = m_front->getNext();
		delete m_front;
		m_front = newFront;
		return(true);
	}
	else
	{
		std::cout << "removeFront size (negative) error.\n";
		return(false);
	}
}

template <typename T>
bool DoubleLinkedList<T> :: remove(T value)
{
	//std::cout << "You gave " << value << std::endl;
	
	if(isEmpty())
	{
		return(false);
	}
	else if(m_size == 1 && m_front->getValue() == value)
	{
		m_size--;
		delete m_front;
		m_front = nullptr;
		m_back = nullptr;
		std::cout << value << " removed from the list.\n";
		return(true);
	}
	else if(m_size > 1)
	{
		Node<T>* traverse = m_front;
		while(traverse->getValue() != value)
		{
			traverse = traverse->getNext();
			if(traverse == nullptr)
			{
				std::cout << value << " was not found.\n";
				return(false);
			}
		}

		
		if(traverse == m_front)
		{
			removeFront();
			return(true);
		}
		else if(traverse == m_back)
		{
			removeBack();
			return(true);
		}
		else
		{
			traverse->getPrevious()->setNext(traverse->getNext());
			traverse->getNext()->setPrevious(traverse->getPrevious());
			delete traverse;
			m_size--;
			return(true);
		}
	}
	else
	{
		return(false);
	}
}

template <typename T>
void DoubleLinkedList<T> :: insertAhead(T listValue, T newValue) throw (std::exception)
{
	if(find(listValue) == nullptr)
	{
		throw (std::exception());
	}
	Node<T>* value = find(listValue);
	if(value == m_front || m_size == 1)
	{
		pushFront(newValue);
	}
	else if(value != nullptr)
	{
		Node<T>* newNode = new Node<T>();
		newNode->setValue(newValue);
		newNode->setPrevious(value->getPrevious());
		newNode->getPrevious()->setNext(newNode);
		value->setPrevious(newNode);
		newNode->setNext(value);
		m_size++;
	}
}

template <typename T>
void DoubleLinkedList<T> :: insertBehind(T listValue, T newValue) throw (std::exception)
{
	if(find(listValue) == nullptr)
	{
		throw (std::exception());
	}
	Node<T>* value = find(listValue);
	if(value == m_back || m_size == 1)
	{
		std::cout << "calling pushBack\n";
		pushBack(newValue);
	}
	else if(value != nullptr)
	{
		Node<T>* newNode = new Node<T>();
		newNode->setPrevious(value);
		newNode->setNext(value->getNext());
		newNode->getPrevious()->setNext(newNode);
		newNode->getNext()->setPrevious(newNode);
		newNode->setValue(newValue);
		m_size++;
	}
}

template <typename T>
Node<T>* DoubleLinkedList<T> :: find(T value) const
{
	if(!isEmpty())
	{
		Node<T>* traverse = m_front;
		while(traverse->getValue() != value)
		{
			traverse = traverse->getNext();
			if(traverse == nullptr)
			{
				return(nullptr);
			}
		}
		
		if(traverse->getValue() == value)
		{
			return(traverse);
		}
	}
	return(nullptr);
}

template <typename T>
void DoubleLinkedList<T> :: printList() const
{
	if(isEmpty())
	{
		std::cout << "";
	}
	else
	{
		Node<T>* traverse = m_front;
		std::cout << traverse->getValue() << " ";
		while(traverse != nullptr)
		{
			traverse = traverse->getNext();
			if(traverse == nullptr)
			{
				return;
			}
			std::cout << traverse->getValue() << " ";
		}
	}
}
