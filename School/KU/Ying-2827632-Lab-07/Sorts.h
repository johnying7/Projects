/**	@file : Sorts.h
*	@author : John Ying
*	@date : 2015.9.23
*	Purpose : Creates, times, and sorts (method depends on user) user arrays
*/

#include <iostream>
#include <random>
#include <ctime>
#include <chrono>
#include <cassert>
#include <functional>
#ifndef SORTS_H
#define SORTS_H

template <typename T>
class Sorts
{
    public:
		/**
		*	@pre array to be sorted and the size of that array
		*	@post sorts array using bubble sort algorithm
		*   @return none
		*/
		static void bubbleSort(T arr[], int size);
		
		/**
		*	@pre array to be sorted and the size of that array
		*	@post sorts array using bogo sort algorithm
		*   @return none
		*/
		static void bogoSort(T arr[], int size);
		
		/**
		*	@pre array to be sorted and the size of that array
		*	@post sorts array using insertion sort algorithm
		*   @return none
		*/
		static void insertionSort(T arr[], int size);
		
		/**
		*	@pre array to be sorted and the size of that array
		*	@post sorts array using selection sort algorithm
		*   @return none
		*/
		static void selectionSort(T arr[], int size);
		
		/**
		*	@pre array to be checked for correct ascending order and the size of that array
		*	@post none
		*   @return true if the array is sorted in ascending order, otherwise false
		*/
		static bool isSorted(T arr[], int size);
		
		/**
		*	@pre none
		*	@post randomly changes the array objects by swapping each index with another one
		*   @return none
		*/
		static void shuffle(T arr[], int size, std::default_random_engine& generator);
		
		/**
		*	@pre desired array size with values between min and max amount
		*	@post none
		*   @return an int array with given parameters
		*/
		static int* createTestArray(int size, int min, int max);
		
		/**
		*	@pre arr is a valid array of T of size elements and operator < is overloaded for T. sort must be capable of sorting arr in ascending order, or assert ends the program
		*	@post arr is sorted in ascending order
		*   @return time in seconds the sort required to sort arr
		*/
		static double sortTimer(std::function<void(T[],int)> sort, T arr[], int size);
		
		/**
		*	@pre assumes the array is filled
		*	@post prints out the sorted array
		*   @return none
		*/
		static void print(T arr[], int size);
		
		//-----Lab07 Methods-----//
		
		/**
		*	@pre array to be sorted and the size of that array
		*	@post sorts array using Merge Sort algorithm
		*   @return none
		*/
		static void mergeSort(T arr[], int size);
		
		/**
		*	@pre array to be sorted and the size of that array
		*	@post sorts array using Quick Sort algorithm
		*   @return none
		*/
		static void quickSort(T arr[], int size);
		
		/**
		*	@pre median flag set to false
		*	@post calls quickSortRec with median flag
		*   @return none
		*/
		static void quickSortWithMedian(T arr[], int size);
		
		private:
		
		/**
		*	@pre a1 and a2 must already be correctly sorted
		*	@post combines the two arrays in the correct order
		*   @return none
		*/
		static void merge(T* a1, T* a2, int size1, int size2);
		
		/**
		*	@pre array must be filled
		*	@post quick sorts to the left and right of the pivot value
		*   @return none
		*/
		static void quickSortRec(T arr[], int first, int last, bool median);
		
		/**
		*	@pre assumes the array is filled
		*	@post puts median value of the array in the last position of the array
		*   @return none
		*/
		static void setMedianPivot(T arr[], int first, int last);
		
		/**
		*	@pre assumes array is filled
		*	@post chooses pivot based on median flag, if true, uses setMedianPivot, else uses the last element in the array
		*   @return index of the pivot
		*/
		static int partition(T arr[], int first, int last, bool median);
		
		/**
		*	@pre assumes the array is filled
		*	@post swaps random indexes in the array for each index in the array
		*   @return none
		*/
		static void shuffle(T arr[], int size);	

};
#include "Sorts.hpp"
#endif

