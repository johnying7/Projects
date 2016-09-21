/**	@file : Sorts.hpp
*	@author : John Ying
*	@date : 2015.10.20
*	Purpose : Implementation of the Sorts class
*/

#include <iostream>
#include <random>
#include <ctime>
#include <chrono>
#include <cassert>
#include <functional>

template <typename T>
void Sorts<T> :: bubbleSort(T arr[], int size)
{
	int temp = 0;
	if(size < 2)
		return;
	for( int i = 0; i < size-1; i++)
	{
		for (int j = 0; j < size-1; j++)
		{
			if(arr[j] > arr[j+1])
			{
				temp = arr[j];
				arr[j] = arr[j+1];
				arr[j+1] = temp;
			}
		}
	}
	assert( Sorts<T>::isSorted(arr, size) );
}

template <typename T>
void Sorts<T> :: bogoSort(T arr[], int size)
{
	if(size < 2)
		return;
	std::default_random_engine generator(time(nullptr));
	
	while(!isSorted(arr, size))
	{
		shuffle(arr, size, generator);
	}
	assert( Sorts<T>::isSorted(arr, size) );
}

template <typename T>
void Sorts<T> :: insertionSort(T arr[], int size)
{
	if(size < 2)
		return;
	T temp;
	for(int sorted = 0; sorted < size; sorted++)
	{
		for(int i = 0; i < size && arr[sorted-i+1] < arr[sorted-i]; i++)
		{
			temp = arr[sorted-i];
			arr[sorted-i] = arr[sorted-i+1];
			arr[sorted-i+1] = temp;
		}
	}
	assert( Sorts<T>::isSorted(arr, size) );
}

template <typename T>
void Sorts<T> :: selectionSort(T arr[], int size)
{
	int min = 0;
	if(size < 2)
		return;
	for( int i = 0; i < size; i++)
	{
		min = i;
		
		for (int j = i+1; j < size; j++)
		{
			if(arr[j] < arr[min])
			{
				min = j;
			}
		}
		T temp;
		temp = arr[i];
		arr[i] = arr[min];
		arr[min] = temp;
	}
	assert( Sorts<T>::isSorted(arr, size) );
}

template <typename T>
bool Sorts<T> :: isSorted(T arr[], int size)
{
	if(size == 1)
		return true;
	for(int i = 0; i < size-1; i++)
	{
		if(arr[i] > arr[i+1])
		{
			return false;
		}
	}
	return true;
}

template <typename T>
void Sorts<T> :: shuffle(T arr[], int size, std::default_random_engine& generator)
{
	std::uniform_int_distribution<int> distribution(0,size);
	
	T temp;
	int randomNumber = 0;
	for(int i = 0; i < size; i++)
	{
		temp = arr[i];
		randomNumber = distribution(generator);
		arr[i] = arr[randomNumber];
		arr[randomNumber] = temp;
	}
}

template <typename T>
int* Sorts<T> :: createTestArray(int size, int min, int max)
{
	std::default_random_engine generator(time(nullptr));
	std::uniform_int_distribution<int> distribution(min,max);
	
	int* array = new int [size];
	for(int i = 0; i < size; i++)
	{
		array[i] = distribution(generator);
	}
	return array;
}

template <typename T>
double Sorts<T> :: sortTimer(std::function<void(T[],int)> sort, T arr[], int size)
{
	std::chrono::system_clock::time_point start;
	std::chrono::system_clock::time_point end;
	std::chrono::duration<double> elapsed;
	
	start = std::chrono::system_clock::now();

	sort(arr, size);

	end = std::chrono::system_clock::now();
	elapsed = (end - start);
	return elapsed.count();
}

template <typename T>
void Sorts<T> :: print(T arr[], int size)
{
	char print;
	std::cout << "Would you like to print the sorted array? (y/n): \n";
	std::cin >> print;
	if(print == 'y')
	{
		std::cout << "Here is the unsorted array:\n";
		std::cout << "[" << arr[0];

		for (int i = 1; i < size; i++)
		{
			std::cout << "," << arr[i];
		}
		std::cout << "]\n";
	}
	else if(print == 'n'){}
}
