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

//-----Lab07 Methods-----//

/**
*	@pre array to be sorted and the size of that array
*	@post sorts array using Merge Sort algorithm
*   @return none
*/
template <typename T>
void Sorts<T> :: mergeSort(T arr[], int size)
{
	if(size == 1)
	{
		//std::cout << "returning 1\n";
		return;
	}
	int newSize = size/2;
	T* a1 = arr;
	T* a2 = (arr + newSize);

	if(newSize != 1)
		mergeSort(a1, newSize);
	if(size-newSize != 1)
		mergeSort(a2, size-newSize);

	merge(a1, a2, newSize, size-newSize);

}

/**
*	@pre array to be sorted and the size of that array
*	@post sorts array using Quick Sort algorithm
*   @return none
*/
template <typename T>
void Sorts<T> :: quickSort(T arr[], int size)
{
	if(size == 1)
		return;
		
	int pivotIndex = partition(arr, 0, size-1, false);

	
	if(pivotIndex != size-1)
	{
		quickSort(arr, pivotIndex+1);
		T* arr1 = (arr+pivotIndex+1);
		quickSort(arr1,size-pivotIndex-1);
		
	}
	else if(pivotIndex == size-1)
	{
		quickSort(arr, pivotIndex);
	}
	else if(pivotIndex == 0)
	{
		T* arr1 = (arr+1);
		quickSort(arr1,pivotIndex);
	}

}

/**
*	@pre array to be sorted and the size of that array
*	@post calls quickSort with median flag set to false
*   @return none
*/
template <typename T>
void Sorts<T> :: quickSortWithMedian(T arr[], int size)
{
	if(size == 1)
		return;
	quickSortRec(arr, 0, size, true);
}

//private:

/**
*	@pre a1 and a2 must already be correctly sorted
*	@post combines the two arrays in the correct order
*   @return none
*/
template <typename T>
void Sorts<T> :: merge(T* a1, T* a2, int size1, int size2)
{
	int newSize = size1 + size2;
	if(size1+size2 == 2)
	{
		if(*a1 <= *a2)
		{
			return;
		}
		else if(*a2 < *a1)
		{
			T temp;
			temp = *a1;
			*a1 = *a2;
			*a2 = temp;
			return;
		}		
	}
	T* temp = new T[newSize];
	int x = 0;
	int y = 0;	
	
	for(int i = 0; i < size1+size2; i++)
	{
		if(a1[x] < a2[y] && x < size1)
		{
			temp[i] = a1[x];
			x= x+1;
		}
		else if(a1[x] > a2[y] && y < size2)
		{
			temp[i] = a2[y];
			y= y+1;
		}
		else if(x >= size1) //a1 exhausted
		{
			int count = 0;
			while(y < size2)
			{
				temp[i+count] = a2[y];
				y++;
				count++;
			}
			i = size1+size2;
		}
		else if( y >= size2)
		{
			int count = 0;
			while( x < size1)
			{
				temp[i+count] = a1[x];
				x++;
				count++;
			}
			i = size1+size2;
		}
		else if(a1[x] == a2[y])
		{
			temp[i] = a1[x];
			x +=1;
		}
		else
		{
			std::cout << "something bad happened\n";
		}
	}
	
	for(int j = 0; j < newSize; j++)
	{
		a1[j] = temp[j];
	}

	delete[] temp;
}

/**
*	@pre array must be filled
*	@post quick sorts to the left and right of the pivot value
*   @return none
*/
template <typename T>
void Sorts<T> :: quickSortRec(T arr[], int first, int last, bool median)
{
	if(last-first == 1)
		return;
	
	int pivotIndex = partition(arr, 0, last-first+1, median);
	if(pivotIndex+1 > 1)
		quickSort(arr,pivotIndex+1);
	
	if(last-pivotIndex > 1)
		quickSort(arr+pivotIndex+1, last-pivotIndex);

}

/**
*	@pre assumes the array is filled
*	@post puts median value of the array in the last position of the array
*   @return none
*/
template <typename T>
void Sorts<T> :: setMedianPivot(T arr[], int first, int last)
{
	int median = (last-first)/2;
	T newLast = arr[median];
	for(int i = median; i < last; i++)
	{
		arr[i] = arr[i+1];
	}
	arr[last] = newLast;
}

/**
*	@pre assumes array is filled
*	@post chooses pivot based on median flag, if true, uses setMedianPivot, else uses the last element in the array
*   @return index of the pivot
*/
template <typename T>
int Sorts<T> :: partition(T arr[], int first, int last, bool median)
{
	if(median)
	{
		setMedianPivot(arr,first,last);
	}
	
	T temp;
	int leftIndex = first;
	int rightIndex = last;

	while(leftIndex < rightIndex)
	{

		while(leftIndex <rightIndex && arr[leftIndex] <= arr[last])
		{
			leftIndex++;
		}
		while( rightIndex > leftIndex && arr[rightIndex-1] >= arr[last])
		{
			rightIndex--;
		}
		
		if(leftIndex < rightIndex)
		{
			temp = arr[leftIndex];
			arr[leftIndex] = arr[rightIndex-1];
			arr[rightIndex-1] = temp;
		}
	}
	
	if(rightIndex != last)
	{
		temp = arr[leftIndex];
		arr[leftIndex] = arr[last];
		arr[last] = temp;
	}
	return rightIndex;
}

/**
*	@pre assumes the array is filled
*	@post swaps random indexes in the array for each index in the array
*   @return none
*/
template <typename T>
void Sorts<T> :: shuffle(T arr[], int size)
{
	std::default_random_engine generator(time(nullptr));
	shuffle(arr, size, generator);
}
