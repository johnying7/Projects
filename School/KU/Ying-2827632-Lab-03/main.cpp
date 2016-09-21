/**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.9.23
*	Purpose : main operation code that controls all core classes and interactions.
*/

#include <iostream>
#include <string>
#include <stdexcept>
#include "DoubleLinkedList.h"
#include "Test.h"
#include "Node.h"

void printMenu()
{
	std::cout << 	"\n\nMake choice\n"
		<<	"1) push value onto front\n"
		<<	"2) push value onto back\n"
		<<	"3) insert behind a value\n"
		<<	"4) insert ahead of a value\n"
		<<	"5) remove front value\n"
		<<	"6) remove back value\n"
		<<	"7) remove specific value\n"
		<<	"8) print list\n"
		<<	"9) Quit\n"
		<< 	"10) Run Tests\n"
		<< 	"Your choice: ";
}

int main()
{
	int selection = 0;
	DoubleLinkedList<int> list;
	
	while(selection != 9)
	{
		printMenu();
		
		std::cin >> selection;
		std::cout << "You chose: " << selection << std::endl;

		if(selection == 1)
		{
			std::cout << "Which value do you wish to add to the front of the list?\n";
			int value;
			std::cin >> value;
			list.pushFront(value);
		}
		else if(selection == 2)
		{
			std::cout << "Which value do you wish to add to the back of the list?\n";
			int value;
			std::cin >> value;
			list.pushBack(value);
		}
		else if(selection == 3)
		{
			std::cout << "What value do you want to insert?\n";
			int newValue;
			std::cin >> newValue;
			
			std::cout << "What position do you want to insert behind of?\n";
			int listValue;
			std::cin >> listValue;
			
			try
			{
				list.insertBehind(listValue, newValue);
			}
			catch(std::exception& e)
			{
				std::cout << "Value was not in the list: ";
				std::cout << e.what();
			}
		}
		else if(selection == 4)
		{
			std::cout << "What value do you want to insert?\n";
			int newValue;
			std::cin >> newValue;
			
			std::cout << "What position do you want to insert ahead of?\n";
			int listValue;
			std::cin >> listValue;
			
			try
			{
				list.insertAhead(listValue, newValue);
			}
			catch(std::exception& e)
			{
				std::cout << "Value was not in the list: ";
				std::cout << e.what();
			}
		}
		else if(selection == 5)
		{
			if(list.removeFront())
			{
				std::cout << "Front of list removed.\n";
			}
		}
		else if(selection == 6)
		{
			if(list.removeBack())
			{
				std::cout << "Back of list removed.\n";
			}
		}
		else if(selection == 7)
		{
			std::cout << "What do you wish to find and remove?\n";
			int value;
			std::cin >> value;
			list.remove(value);
		}
		else if(selection == 8)
		{
			list.printList();
		}
		else if(selection == 9)
		{
			std::cout << "Exiting...";
			return(0);
		}
		else if(selection == 10)
		{
			Test myTester(std::cout);
			myTester.runTests();
			return(0);
		}
		else
		{
			std::cout << "Invalid choice. \n";
			selection = 0;
		}
	}
		
	return(0);
}
