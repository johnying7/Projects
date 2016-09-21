/**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.9.29
*	Purpose : main operation code that controls all core classes and interactions.
*/
 
#include <iostream>
#include <string>
#include <stdexcept>
#include "Node.h"
#include "Stack.h"
#include "StackInterface.h"
#include "Test.h"

void printMenu()
{
	std::cout 	<< "\n\nSelect an action:\n"
			<< "1) Add to stack 1\n"
			<< "2) See what is at the top of stack\n"
			<< "3) Print all stack\n"
			<< "4) Pop stack\n"
			<< "5) Quit\n"
 			<< "6) Run Tests\n"
			<< "Enter choice: ";
}

int main()
{
	int selection = 0;
    StackInterface<int>* stack = new Stack<int>();
	
	while(selection != 5)
	{
		printMenu();
		std::cin >> selection;
		std::cout << "You chose: " << selection << std::endl;
		
		if(selection == 1)
		{
			int num;
			std::cout << "What kind of int would you like to add to the stack? ";
			std::cin >> num;
			
            stack->push(num);
			
			std::cout << "Added " << num << std::endl;
		}
		else if(selection == 2)
		{
            std::cout << stack->peek() << " is at the top of the list.\n";
		}
		else if(selection == 3)
		{
            stack->print();
		}
		else if(selection == 4)
		{
            stack->pop();
			std::cout << "The stack has been popped.\n";
		}
		else if(selection == 5)
		{
			delete stack;
			return(0);
		}
		else if(selection == 6)
		{
			Test myTester(std::cout);
			myTester.runTests();
			delete stack;
			return(0);
		}
		else
		{
			std::cout << "Invalid choice. Choose again.";
			selection = 0;
		}
	}
	delete stack;
	return(0);
}
