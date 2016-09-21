#include <iostream>
#include <string>
#include <stdexcept>

/**
*	@pre none
*	@post Constructor that initializes BinarySearchTree
*   @return sets m_root to nullptr
*/
template <typename T>
BinarySearchTree<T> :: BinarySearchTree()
{
	m_root = nullptr;
}

/**
*	@pre none
*	@post Copy Constructor that creates a BinarySearchTree with given "other" values
*   @return sets values to other's values
*/
template <typename T>
BinarySearchTree<T> :: BinarySearchTree(const BinarySearchTree<T>& other)
{
	m_root = other.m_root;
}

/**
*	@pre none
*	@post destructor
*   @return none
*/
template <typename T>
BinarySearchTree<T> :: ~BinarySearchTree()
{
	if(m_root != nullptr)
		deleteTree(this->m_root);
}

/**
*	@pre none
*	@post none
*   @return a vector with the contents of the tree in the specified order
*/
template <typename T>
std::vector<T> BinarySearchTree<T> :: treeToVector(Order order) const
{
	std::vector<T> vec;
	treeToVector(m_root,order,vec);
	return vec;
}


/**
*	@pre this is in a valid state
*	@post creates a deep copy of this
*   @return returns a pointer to a deep copy of this
*/
template <typename T>
BSTI<T>* BinarySearchTree<T> :: clone()
{
	if(this->m_root == nullptr)
	{
		BSTI<T>* newBST = new BinarySearchTree<T>();
		return newBST;
	}
	else
	{
		BSTI<T>* newBST = new BinarySearchTree<T>(*this);
		return newBST;
	}
}

/**
*	@pre none
*	@post none
*   @return true if no nodes in the tree, otherwise returns false.
*/
template <typename T>
bool BinarySearchTree<T> :: isEmpty() const
{
	if(m_root == nullptr)
		return true;
	else
		return false;
}

/**
*	@pre value is a valid T
*	@post a new Node<T> is created an inserted into the tree based
*   @return false if the value couldn't be added (i.e. duplicate values not allowed)
*/
template <typename T>
bool BinarySearchTree<T> :: add(T value)
{
	if(this->m_root == nullptr)
	{
		m_root = new Node<T>();
		m_root->setValue(value);
		return true;
	} else
		return add(value,this->m_root);
}

/**
*	@pre The type T is comparable by the == operator
*	@post none
*   @return true if the value is in the tree, false otherwise
*/
template <typename T>
bool BinarySearchTree<T> :: search(T value) const
{
	if(m_root == nullptr)
		return false;
	else 
		return search(value, this->m_root);
}

/**
*	@pre none
*	@post the contents of the tree are printed to the console in the specified order
*   @return none
*/
template <typename T>
void BinarySearchTree<T> :: printTree(Order order) const
{
	printTree(m_root, order);
}

/**
*	@pre none
*	@post adds value to the correct branch (left/right) of subtree
*   @return true if add operation successful
*/
template <typename T>
bool BinarySearchTree<T> :: add(T value, Node<T>* subtree)
{
	if(value < subtree->getValue())
	{
		if (subtree->getLeft() == nullptr) {
			Node<T>* temp = new Node<T>();
			temp->setValue(value);
			subtree->setLeft(temp);
			return true;
		} else
			return add(value,subtree->getLeft());
	}
	else if(value > subtree->getValue())
	{
		if (subtree->getRight() == nullptr) {
			Node<T>* temp = new Node<T>();
			temp->setValue(value);
			subtree->setRight(temp);
			return true;
		} else
			return add(value,subtree->getRight());
	}
	return false;
}

/**
*	@pre none
*	@post deletes tree completely
*   @return none
*/
template <typename T>
void BinarySearchTree<T> :: deleteTree(Node<T>* subTree)
{
	if(subTree->getLeft() != nullptr)
	{
		deleteTree(subTree->getLeft());
	}
	if(subTree->getRight() != nullptr)
	{
		deleteTree(subTree->getRight());
	}
	delete subTree;

}

/**
*	@pre none
*	@post none
*   @return true if value is in the tree
*/
template <typename T>
bool BinarySearchTree<T> :: search(T value, Node<T>* subtree) const
{
	//std::cout << "\nTree Value: " << subtree->getValue() << " | search value: " << value << std::endl;
	if(value == subtree->getValue())
	{
		return true;
	}
	else if(value < subtree->getValue()) 
	{
		if (subtree->getLeft() == nullptr)
			return false;
		return search(value, subtree->getLeft());
	}
	else if(value > subtree->getValue())
	{
		if (subtree->getRight() == nullptr)
			return false;
		return search(value, subtree->getRight());
	}
}

/**
*	@pre none
*	@post prints tree in PRE_ORDER or IN_ORDER or POST_ORDER using subtree and the "order"
*   @return none
*/
template <typename T>
void BinarySearchTree<T> :: printTree(Node<T>* subtree, Order order) const
{
	if(subtree != nullptr)
	{
		if(order == PRE_ORDER)
		{
			std::cout << subtree->getValue() << " ";
			if(subtree->getLeft() != nullptr)
				printTree(subtree->getLeft(), order);
			if(subtree->getRight() != nullptr)
				printTree(subtree->getRight(), order);
		}
		else if(order == IN_ORDER)
		{
			if(subtree->getLeft() != nullptr)
				printTree(subtree->getLeft(), order);
			std::cout << subtree->getValue() << " ";
			if(subtree->getRight() != nullptr)
				printTree(subtree->getRight(), order);		
		}
		else if(order == POST_ORDER)
		{
			if(subtree->getLeft() != nullptr)
				printTree(subtree->getLeft(), order);
			if(subtree->getRight() != nullptr)
				printTree(subtree->getRight(), order);
			std::cout << subtree->getValue() << " ";
		}
		else
		{
			std::cout << "\nprint tree ORDER error.\n";
		}
	}
}

/**
*	@pre none
*	@post recursive helper function to load the vector, vec in the specified order
*   @return none
*/
template <typename T>
void BinarySearchTree<T> :: treeToVector(Node<T>* subtree, Order order, std::vector<T>& vec) const
{
	if(subtree == nullptr)
		return;
	
	switch(order)
	{
		case Order :: IN_ORDER:
		{
			treeToVector(subtree->getLeft(),order, vec);
			vec.push_back(subtree->getValue());
			treeToVector(subtree->getRight(), order, vec);
			break;
		}
		case Order :: PRE_ORDER:
		{
			vec.push_back(subtree->getValue());
			treeToVector(subtree->getLeft(), order,vec);
			treeToVector(subtree->getRight(), order, vec);
			break;
		}
		case Order :: POST_ORDER:
		{
			treeToVector(subtree->getLeft(), order,vec);
			treeToVector(subtree->getRight(), order, vec);
			vec.push_back(subtree->getValue());
			break;
		}
		
	}
}
