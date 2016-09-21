/***********************************************************
/
/	Projections.cpp tries to solve for the shortest path
/	for a bee to reach a pole. It currently is not completed
/	as only the user input for the pole's angle, and the
/	magnitude, heading, and pitch of the bee. As well as
/	some attempts at the rest but most focus was upon the
/	Vector3D.h header file.
/	
/	John Ying, 2/22/12
/	Johnson County Community College
/
/**********************************************************/

#include <iostream>
#include <string>
#include <cmath>
#include "Vector3D.h"
using namespace::std;

void main()
{
	Vector3D beeTest;
	float poleAngle;
	float mag;
	float heading;
	float pitch;

	cout << "Input the angular information of the utility pole: ";
	cin >> poleAngle;

	cout << "Enter a leg of the trip in distance, heading, and pitch: ";
	cin >> mag >> heading >> pitch;
	beeTest.setRectGivenMagHeadPitch(mag, heading, pitch);

	//perform requested motion

	beeTest.printMagHeadPitch();

	beeTest.operator|()//reports the cloest point on the pole to the bee
}