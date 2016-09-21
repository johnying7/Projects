/**********************************************************************
/
/    part1DMotion.cpp proprojects the motion of a cart moving down a
/	 ramp given by equations and user provided input.
/	 The program calculates the different type of energies and outputs
/	 them to the screen.
/
/    John Ying, Apr 2012
/    Johnson County Community College
/
/*********************************************************************/

#include <iostream>
#include <string>
#include "VectorClass.h"
#include <cmath>


using namespace::std;

int main()
{
	Vector3D calc;
	float cartMass = 100; //kg
	float iVelocity = 0.0; //meters per second
	float keLoss = 0.0; //epsilon
	float timeStep = 0.05f; //seconds
	//float pEnergy = 0f; //potential energy joules (newton * meter)
	float gravity = 9.8f; //meters per second
	float rHeight = 1.0;
	float pEnergy = 0.0;
	float kEnergy = 0.0; //kinetic energy joules (newton * meter)
	float s = 0; //distance
	float netForce = 0.0;
	float fVelocity = 0.0;
	float acceleration = 0.0;
	float epsilon = 0.001f;
	float velocity = 0.0;

	cout << "What is the Initial Velocity?\n";
	cin >> iVelocity;

	cout << "What is the % Kinetic Energy loss per step?\n";
	cin >> keLoss;

	for (int i = 0; rHeight > 0; i++)
	{
		s += iVelocity * timeStep;
		rHeight = (-.3219f * s) + 10.0f;
		pEnergy = calc.getPE(s);
		cout << "Potential Energy: " << pEnergy << " Joules." << endl;

		netForce = ( (calc.getPE(s + .001f) - calc.getPE(s - .001f) ) / (2 * .001f) ) * -1;
		acceleration = netForce / cartMass;
		velocity += acceleration * .05f;
		velocity *= sqrt(100-keLoss);
		kEnergy = .5f * cartMass * (velocity * velocity);
		cout << "Kinetic Energy: " << kEnergy << " Joules." << endl;
		cout << "Total Energy: " << kEnergy + pEnergy << " Joules." << endl;
	}
	

}