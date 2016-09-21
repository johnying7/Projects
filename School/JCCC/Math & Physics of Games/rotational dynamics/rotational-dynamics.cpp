/**********************************************************************
/
/    rotational-dynamics.cpp tries to represent the projection of
/	 two masses connected by a pole and the position based on the
/	 amount of force applied and the amount of time that force is
/	 being applied to it.
/
/    John Ying, Mar 2012
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
	//variable declarations
	float appForce = 0;
	float time = 0;
	float r = .35;
	float m1 = 1.2;
	float m2 = 4.9;
	float middle = 0.135;
	float theta = 0;
	float r1 = 0;
	float r2 = 0;

	float com1 = 0;
	float com2 = 0;
	float com = 0;
	float torqueMag = 0;
	float angAccel = 0;
	float inertia = 0;
	float angVelocity = 0;
	float angDisp = 0;

	float tStep = 1;
	
	//vector declarations
	Vector3D vR1;
	Vector3D torque;
	Vector3D force; //still needs figuring
	Vector3D comPos;
	Vector3D velocityV;
	Vector3D accelV;

	//user requested input
	cout << "What is the force applied?" << endl;
	cin >> appForce;

	cout << "What is the amount of time the force is applied?" << endl;
	cin >> time;

	//calculation of the center of mass betwee the two different masses
	middle = r/2;
	com1 = (m1*-middle) + (m2*middle);
	com2 = m1 + m2;
	com = com1/com2;
	r1 = middle + com;
	r2 = middle - com;

	
	//calculation of inertia
	inertia = (m1 * (r1*r1)) + (m2 * (r2*r2));

	
	//loop set to each time step inbetween calculations of rotation and translation
	for( int i; i < time; i++ )
	{
		vR1.setRectGivenPolar(r1, 180 + angDisp);//finds the magnitude between mass 1 and center of mass
		force.setRectGivenPolar(appForce, 180 + angDisp);//finds the magnitude of the force applied
		torque.crossProduct( vR1, force );
		torqueMag = torque.getZ();
		angAccel = torqueMag/inertia;
		angDisp += angVelocity * (i*tStep);
		angVelocity += angAccel * (i*tStep);
		velocityV.mult( i*tStep );
		comPos = comPos.add(velocityV);
		accelV.mult( i*tStep );
		velocityV = velocityV.add(accelV);
	}
}