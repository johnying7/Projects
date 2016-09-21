/**********************************************************************
/
/    collisionsAndMomentum.cpp holds the main input functions for the
/	 user to find the projection of collisions upon an object on a
/	 plane. Also between two objects against each other. Each of these
/	 are based off of functions from the VectorClass.h file.
/
/    John Ying, Apr 2012
/    Johnson County Community College
/
/*********************************************************************/
#include <iostream>
#include <string>
#include <cmath>
#include "VectorClass.h"
using namespace::std;

void main()
{
	//float variable holders (velocities are in meters per second
	float xVelocity = 0;
	float yVelocity = 0;
	float zVelocity = 0;
	float iVelocity1 = 0;//initial velocity of object 1
	float iVelocity2 = 0;
	float mass1 = 0;//mass based on kg (kilograms)
	float mass2 = 0;
	float cRest = 0;//coefficient of restitution

	//vector variable holders
	Vector3D iVelocity;
	Vector3D v1;
	Vector3D v2;
	Vector3D vReflection;

	//asks the user for the velocity for object reflection
	cout << "Object Reflection\n";
	cout << "What is the initial velocity for x?\n";
	cin >> xVelocity;

	cout << "What is the initial velocity for y?\n";
	cin >> yVelocity;

	cout << "What is the initial velocity for z?\n";
	cin >> zVelocity;

	iVelocity.setRectGivenRect(xVelocity, yVelocity, zVelocity, 0);//sets the user input info into a vector

	//finds the coefficient and first vector plane
	cout << "What is the coefficient of restitution?\n";
	cin >> cRest;

	cout << "What is the first plane vector x?\n";
	cin >> xVelocity;

	cout << "What is the first plane vector y?\n";
	cin >> yVelocity;

	cout << "What is the first plane vector z?\n";
	cin >> zVelocity;

	v1.setRectGivenRect(xVelocity, yVelocity, zVelocity, 0);//sets the user input info into a vector

	//finds the second vector plane
	cout << "What is the second plane vector x?\n";
	cin >> xVelocity;

	cout << "What is the second plane vector y?\n";
	cin >> yVelocity;

	cout << "What is the second plane vector z?\n";
	cin >> zVelocity;

	v2.setRectGivenRect(xVelocity, yVelocity, zVelocity, 0);//sets the user input info into a vector

	vReflection.reflection3D( iVelocity, cRest, v1, v2 );//calls the reflection function calculator

	vReflection.printRect();//prints out the reflection info

	//finds the needed user input for two objects colliding
	cout << "What is the initial velocity for object 1?\n";
	cin >> iVelocity1;

	cout << "What is the initial velocity for object 2?\n";
	cin >> iVelocity2;

	cout << "What is the mass of object 1?\n";
	cin >> mass1;

	cout << "What is the mass of object 2?\n";
	cin >> mass2;

	cout << "What is the coefficient of restitution?\n";
	cin >> cRest;

	vReflection.reflection1D( iVelocity1, iVelocity2, mass1, mass2, cRest );//calls the collision function for two objects

}