/**********************************************************************
/
/    closestPoints.cpp provides the main function code for providing
/	 information for the vector class as well as the final distance
/	 calculations between the closest point and the given point of
/	 relevant position
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

int main()
{
	//variable declarations for vector storage based upon asked information
	Vector3D ship;
	Vector3D point;
	Vector3D direction;
	Vector3D cPointHolder1;
	Vector3D distance1;
	Vector3D distance2;
	Vector3D cPointHolder2;
	Vector3D planeP1;
	Vector3D planeP2;
	Vector3D planeP3;

	//temporary storage holders for the information in the vector classes
	float x;
	float y;
	float z;

	//sets the vectors based upon user provided information
	cout << "Enter the components of the ship in space: {x y z}\n";
	cin >> x >> y >> z;
	ship.setRectGivenRect(x, y, z);

	cout << "Enter the point of a moving object: {x y z}\n";
	cin >> x >> y >> z;
	point.setRectGivenRect(x, y, z);

	cout << "Enter the direction of the moving object: {x y z}\n";
	cin >> x >> y >> z;
	direction.setRectGivenRect(x, y, z);

	//report the closest point of the ship using the ship as the first vector, point of obj as 2nd vector, direction as 3rd vector
	cPointHolder1.closestPointLine( ship, point, direction );

	cout << "The closest point to the ship is: ";
	cPointHolder1.printRect();

	//calculates the DISTANCE between the ship and closest point
	x = cPointHolder1.getX() - ship.getX();
	y = cPointHolder1.getY() - ship.getY();
	z = cPointHolder1.getZ() - ship.getZ();
	distance1.setRectGivenRect(x, y, z);

	cout << "The distance from that ship is: " << distance1.getMagnitude() << "km." << endl;

	//sets the plane based upon user provided information
	cout << "Enter 3 points on a surface: {x y z}\n";
	cin >> x >> y >> z;
	planeP1.setRectGivenRect(x, y, z);

	cout << "Enter the second point: {x y z}\n";
	cin >> x >> y >> z;
	planeP2.setRectGivenRect(x, y, z);

	cout << "Enter the third point: {x y z}\n";
	cin >> x >> y >> z;
	planeP3.setRectGivenRect(x, y, z);

	//reports the closest point based upon the ship, and 3 points on the given user provided plane
	cPointHolder2.closestPointPlane(ship, planeP1, planeP2, planeP3);

	cout << "The closest point on the surface to the ship is: \n";
	cPointHolder2.printRect();

	//calculates the DISTANCE between the ship and the closest point on the PLANE
	x = cPointHolder2.getX() - ship.getX();
	y = cPointHolder2.getY() - ship.getY();
	z = cPointHolder2.getZ() - ship.getZ();
	distance2.setRectGivenRect(x, y, z);

	cout << "The distance from that ship is: " << distance2.getMagnitude() << endl;
}
