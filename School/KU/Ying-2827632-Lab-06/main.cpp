 /**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.10.20
*	Purpose : main operation code that controls all core classes and interactions.
*/
 
#include <iostream>
#include "Sorts.h"
#include "Test.h"

void printMenu()
{
	std::cout 	<< "\n\nSelect a sort:\n"
			<< "1) Bubble Sort\n"
			<< "2) Insertion Sort\n"
			<< "3) Selection Sort\n"
			<< "4) Bogo Sort (use only with very small arrays!)\n"
			<< "5) Test\n"
			<< "Enter choice: ";
}

int main()
{
	char quit = 'n';
	while(quit == 'n')
	{
		printMenu();
		int choice;
		std::cin >> choice;
		if(choice == 5)
		{
			Test myTester(std::cout); //declares a Test instance
			myTester.runTests(); //runs a series of tests
			return(0);
		}
		int size = 0;
		//size min max
		std::cout << "What size of array would you like to make?\n";
		std::cin >> size;
	
		int min = 0;
		std::cout << "What is the lower bound of the array would you like to set?\n";
		std::cin >> min; 
	
		int max = 0;
		std::cout << "What is the upper bound of the array would you like to set?\n";
		std::cin >> max;
	
		int* arr = Sorts<int>::createTestArray(size,min,max);
		std::cout << "Would you like to print the unsorted array? (y/n): \n";
		char print;
		std::cin >> print;
		if(print == 'y')
		{
			std::cout << "Here is the unsorted array:\n";
			std::cout << "[" << arr[0];
		
			for (int i = 1; i < size; i++)
			{
				std::cout << "," << arr[i];
			}
			std::cout << "]\n\n";
		}
		else if(print == 'n'){}
	
		double elapsed;
		switch(choice)
		{
			case 1:
			{
				elapsed = Sorts<int>::sortTimer(Sorts<int>::bubbleSort, arr, size);
				Sorts<int>::print(arr,size);
				std::cout << size << " numbers were sorted in " << elapsed << " seconds executing Bubble Sort.\n";
				break;
			}
			case 2:
			{
				elapsed = Sorts<int>::sortTimer(Sorts<int>::insertionSort, arr, size);
				Sorts<int>::print(arr,size);
				std::cout << size << " numbers were sorted in " << elapsed << " seconds executing Insertion Sort.\n";
				break;
			}
			case 3:
			{
				elapsed = Sorts<int>::sortTimer(Sorts<int>::selectionSort, arr, size);
				Sorts<int>::print(arr,size);
				std::cout << size << " numbers were sorted in " << elapsed << " seconds executing Selection Sort.\n";
				break;
			}
			case 4:
			{
				elapsed = Sorts<int>::sortTimer(Sorts<int>::bogoSort, arr, size);
				Sorts<int>::print(arr,size);
				std::cout << size << " numbers were sorted in " << elapsed << " seconds executing Bogo Sort.\n";
				break;
			}
			case 5:
			{
				Test myTester(std::cout); //declares a Test instance
				myTester.runTests(); //runs a series of tests
				return(0);
				break;
			}
			default:
			{
				std::cout << "Invalid input.\n";
				break;
			}
		}
		std::cout << "Do you want to quit? (y/n):\n";
		std::cin >> quit;
		delete arr;
	}
	
	return 0;	
}

