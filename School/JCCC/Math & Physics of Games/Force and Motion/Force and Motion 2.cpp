/**********************************************************************
/
/    Force and Motion 2.cpp simulates the motion of a rocket launched
/    at a specific position, mass, wind resistance coefficient, thrust,
/	 time length on thrust, heading, pitch, and time interval. The
/	 program produces the final destination of the rocket after launch
/	 and final landing (or crash) into the ground as well as how long
/	 it took the rocket to get there.
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
	double fTime = 0; //the current time of the program
	double PosX = 0; //current x position of the rocket
	double PosY = 0; //current y position of the rocket
	double PosZ = 0.2; //current z position of the rocket
	double tStep = 0.1; //the time step intervals between calculations

	//the variables for magnitude, heading, and pitch variable increments in vector format
	double xThrust;
	double yThrust;
	double zThrust;

	double mass = 0.0742; //mass of the rocket in kilograms (kg)

	double cResistance = 0.02; //coefficient of wind resistance (not actual wind resistance)

	double force = 10.0; //upward force of the rocket in newtons
	double thrustTime = 1.00; //
	double heading = 23;
	double pitch = 62;
	double fHeading = 0;
	double fPitch = 0;

	//cout statements giving the user the starting statistics
	cout << "The rocket begins at position (" << PosX << ", " << PosY << ", " << PosZ << ")" << endl;
	cout << "The rocket has a mass of " << mass << "kg\n";
	cout << "The coefficient of wind resistance is " << cResistance << endl;
	cout << "The rocket provides a thrust of " << force << " N for " << thrustTime << " seconds.\n";
	cout << "The heading is set at " << heading << " and the pitch at " << pitch << endl;


	//degrees to radians
	fHeading = heading / 57.29577951;
	fPitch = pitch / 57.29577951;

	//thrust force in vector format calculations
	xThrust = force*cos(fPitch)*cos(fHeading);
	yThrust = force*cos(fPitch)*sin(fHeading);
	zThrust = force*sin(fPitch);

	//zSin should equal 0.018885157 but instead equals 0.882948
	/*
	double zSin = 0;
	zSin = sin(fPitch);
	cout << "zSin " << zSin << endl;
	*/

	double gravity = -9.8; //force of gravity in meters per second square
	
	//net force coordinate direction
	double xNet;
	double yNet;
	double zNet;

	//wind resistance coordinate direction
	double wXResistance = 0;
	double wYResistance = 0;
	double wZResistance = 0;

	//acceleration coordinate direction
	double accelerationX = 0;
	double accelerationY = 0;
	double accelerationZ = 0;

	//velocity coordinate direction
	double xVelocity = 0;
	double yVelocity = 0;
	double zVelocity = 0;


	while ( PosZ >= 0 ) //while the rocket is above ground (0 coordinate)
	{
		if ( fTime > thrustTime ) //while the rocket is thrusting
		{
			//set thrust force to zero
			xThrust = 0;
			yThrust = 0;
			zThrust = 0;
		}

		//update the current position
		PosX = PosX + xVelocity * tStep;
		PosY = PosY + yVelocity * tStep;
		PosZ = PosZ + zVelocity * tStep;

		//update the current velocity
		xVelocity = accelerationX * tStep;
		yVelocity = accelerationY * tStep;
		zVelocity = accelerationZ * tStep;
		
		//update the current resistance
		wXResistance = (-1 * cResistance) * xVelocity;
		wYResistance = (-1 * cResistance) * yVelocity;
		wZResistance = (-1 * cResistance) * zVelocity;

		//update the net force
		xNet = xThrust + wXResistance;
		yNet = yThrust + wYResistance;
		zNet = zThrust + (mass * gravity) + wZResistance;

		//update the acceleration
		accelerationX = xNet/mass;
		accelerationY = yNet/mass;
		accelerationZ = zNet/mass;

		fTime += tStep; //update the current time with the increment
	}

	cout << "The final time is: " << fTime << " seconds." << endl;
	cout << "The final position is: (" << PosX << ", " << PosY << ", " << PosZ << ")\n";
}