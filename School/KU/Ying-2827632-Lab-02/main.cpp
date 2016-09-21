/**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.9.15
*	Purpose : main operation code that controls all core classes and interactions.
*/

#include <iostream>
#include "LinkedList.h"
#include "Test.h"

int main()
{
	LinkedList list1;
	int selection = 0;
	
	while(selection != 7)
	{
		std::cout << "\n\nSelect from the following menu:\n"
			<< "1) Add to the front of the list\n"
			<< "2) Add to the end of the list\n"
			<< "3) Remove from front of the list\n"
			<< "4) Remove from back of the list\n"
			<< "5) Print the list\n"
			<< "6) Search for value\n"
			<< "7) Exit\n"
			<< "8) Run tests\n"
			<< "Enter your choice: ";
			
		std::cin >> selection;
		std::cout << "You chose: " << selection << std::endl;
			
		if(selection == 1)
		{
			std::cout << "What number do you wish to put in the front of the list?\n";
			int num;
			std::cin >> num;

			list1.addFront(num);
			std::cout << "Adding: " << num << std::endl;
		}
		else if(selection == 2)
		{
			std::cout << "What number do you wish to put in the back of the list?\n";
			int num;
			std::cin >> num;
			std::cout << "Adding: " << num << std::endl;
			list1.addBack(num);
		}
		else if(selection == 3)
		{
			if(list1.removeFront())
			{
				std::cout << "Removed from the list.\n";
			}
			else
			{
				std::cout << "Removal unsuccessful.\n";
			}
			
		}
		else if(selection == 4)
		{
			if(list1.removeBack())
			{
				std::cout << "Removed from the list.\n";
			}
			else
			{
				std::cout << "Removal unsuccessful.\n";
			}
			
		}
		else if(selection == 5)
		{
			list1.printList();
		}
		else if(selection == 6)
		{
			std::cout << "What number do you wish to search for in the list?\n";
			int num;
			std::cin >> num;
			if(list1.search(num))
			{
				std::cout << num << " is in the list.\n";
			}
			else
			{
				std::cout << num << " is not the list.\n";
			}
		}
		else if(selection == 7)
		{
			std::cout << "Exiting.....";
			return(0);
		}
		else if(selection == 8)
		{
			Test myTester(std::cout); //declares a Test instance
			myTester.runTests(); //runs a series of tests
			return(0);
		}
		else
		{
			std::cout << "Invalid choice.\n";
			selection = 0;
		}
	}
	return (0);
}
