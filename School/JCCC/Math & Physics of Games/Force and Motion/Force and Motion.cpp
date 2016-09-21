/**********************************************************************
/
/    Force and Motion.cpp simulates the motion of a box with a specific
/	 amount of force in a direction. The box will have a user input
/	 initial speed and a coefficient of friction that will apply to it.
/	 It will output the final distance traveled with hidden calculated
/	 time intervals of 0.10 seconds.
/
/    John G Ying, March 2012
/    Johnson County Community College
/
/*********************************************************************/

#include <iostream>
#include <string>
#include <cmath>
using namespace::std;

void main()
{
	double iSpeed;
	double cFriction;
	double tChange = .10;
	double iPosition = 0;
	double acceleration;
	double gravity = 9.8;
	double fPosition;

	cout << "Please enter the initial speed:\n";
	cin >> iSpeed;
	cout << "Please enter the coefficient of friction:\n";
	cin >> cFriction;

	acceleration = (-1.0 * cFriction) * gravity;

	while ( iSpeed > 0 )
	{
		iPosition += iSpeed * tChange;
		iSpeed += acceleration * tChange;
	}

	cout << "The final distance traveled is: " << iPosition << " units." << endl;
	
}

