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

};
#include "Sorts.hpp"
#endif

