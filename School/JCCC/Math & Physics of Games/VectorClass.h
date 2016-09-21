/**********************************************************************
/
/    VectorClass.h sets the starting point for many different types
/	 of calculations ranging from projections, predictions, and any
/	 other type of calculations based upon vectors in 2D or 3D space.
/
/    John Ying, Mar 2012
/    Johnson County Community College
/
/*********************************************************************/

#include <iostream>
#include <string>
#include <cmath>
using namespace::std;

class Vector3D //the Vector3D class declaration and beginning
{
public:

	Vector3D(void) // constructor for the class Vector3D
	{
		x = 0.0;
		y = 0.0;
		z = 0.0;
		vPoint = 0.0;
		//executed when Vector3D is created
	}

	float getZ()
	{
		return z;
	}

	void setRectGivenRect( float setX, float setY, float setZ, float setvPoint ) //sets the x, y, and z variables in the private to the given parameters
	{
		x = setX;
		y = setY;
		z = setZ;
		vPoint = setvPoint;
	}

	void setRectGivenPolar( float mag, float degrees ) //sets the x and y based upon a given magnitude and degrees
	{
		x = mag*cos(degrees);
		y = mag*sin(degrees);
	}

	void setRectGivenMagHeadPitch( float mag, float heading, float pitch) //sets the x, y, and z based upon a given magnitude, heading, and pitch
	{
		x = mag*cos(pitch)*cos(heading);
		y = mag*cos(pitch)*sin(heading);
		z = mag*sin(pitch);
	}

	void printRect(void)	//public function for the class to print the private x, y, and z variables
	{
		cout << endl;
		cout << "x: " << x << endl;
		cout << "y: " << y << endl;
		cout << "z: " << z << endl;
		cout << "vPoint(w): " << vPoint << endl;
	}

	void printMagHeadPitch()	//public function for the class to print the magnitude, heading, and pitch
	{
		cout << endl;
		cout << "Magnitude: " << getMagnitude() << endl;
		cout << "Heading: " << getHeading() << endl;
		cout << "Pitch: " << getPitch() << endl;
	}

	void printAngles(void)	//public function for the class to print the alpha, beta, and gamma angles
	{
		cout << endl;
		cout << "The angles are\n";
		cout << "Alpha: " << getAlpha() << endl;
		cout << "Beta: " << getBeta() << endl;
		cout << "Gamma: " << getGamma() << endl;
	}

	float getMagnitude() //returns the magnitude based on the given x, y, and z coordinates in the class
	{
		return (sqrt((x*x)+(y*y)+(z*z)));
	}

	float getPitch() //returns the pitch based on the given x, y, and z coordinates in the class
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (asin(z/getMagnitude()));
	}

	float getHeading() //returns the heading based on the given x, y, and z coordinates in the class
	{
		if (sqrt((x*x)+(y*y)) == 0)
			return 0;
		else
			return (acos(x/(sqrt((x*x)+(y*y)))));
	}

	float getAlpha()//returns the alpha angle based on the given x, y, and z coordinates in the class
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(x / getMagnitude()));
	}

	float getBeta()//returns the beta angle based on the given x, y, and z coordinates in the class
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(y / getMagnitude()));
	}

	float getGamma()//returns the gamma angle based on the given x, y, and z coordinates in the class
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(z / getMagnitude()));
	}

	float getX() //returns the x value in the class
	{
		return x;
	}

	float getY() //returns the y value in the class
	{
		return y;
	}

	float getZ() //returns the z value in the class
	{
		return z;
	}

	Vector3D projection(Vector3D v, Vector3D u) //this finds the projection of v onto u.
	{
		//variable declarations
		float topAdd;
		float botAdd;
		float ans;
		Vector3D proj;

		//function math
		topAdd = (u.x * v.x) + (u.y * v.y) + (u.z * v.z);
		botAdd = (v.x * v.x) + (v.y * v.y) + (v.z * v.z);
		ans = topAdd / botAdd;
		proj.x = ans * v.x;
		proj.y = ans * v.y;
		proj.z = ans * v.z;
		
		return proj; //returns the vector answer
	}

	void crossProduct( Vector3D v2, Vector3D v3 ) //finds the cross product of the given parameter vectors into the current vector
	{
		x = (v2.y * v3.z) - (v2.z * v3.y);
		y = (v2.z * v3.x) - (v2.x * v3.z);
		z = (v2.x * v3.y) - (v2.y * v3.x);
	}

	void closestPointLine ( const Vector3D ship, const Vector3D point, const Vector3D direction ) //ship is the random point outside, point and direction are the two points of the line(PQ)
	{
		//x,y, z is point aka toms ship
		//sighted at position point in the direction of (direction)
		
		//variable declarations
		Vector3D pq;
		Vector3D proj;

		//solves for PQ by doing Q - P(point) in vector projections
		pq.x = ship.x - point.x;
		pq.y = ship.y - point.y;
		pq.z = ship.z - point.z;

		proj = projection(direction, pq); //solves and stores the projection of the given parameters (v, u)

		x = point.x + proj.x;
		y = point.y + proj.y;
		z = point.z + proj.z;
	}

	void closestPointPlane (Vector3D ship, Vector3D planeP1, Vector3D planeP2, Vector3D planeP3) //solves for the closest point by a given plane and Q(ship) coordinate
	{
		//variable declarations
		Vector3D proj; //vector projection storage
		Vector3D normal; //normal vector storage
		Vector3D v1; //vector 1
		Vector3D v2; //vector 2
		Vector3D point; //solving for P(point)

		//solves for the needed cross product vectors based upon the 3 given points
		v1.x = planeP1.x - planeP2.x;
		v1.y = planeP1.y - planeP2.y;
		v1.z = planeP1.z - planeP2.z;

		v2.x = planeP1.x - planeP3.x;
		v2.y = planeP1.y - planeP3.y;
		v2.z = planeP1.z - planeP3.z;

		normal.crossProduct (v1, v2); //finds the cross product of two vectors

		//solves for and finds the P(point) by a randomly given point and Q(ship)
		point.x = ship.x - planeP1.x;
		point.y = ship.y - planeP1.y;
		point.z = ship.z - planeP1.z;

		proj = projection(normal, point); //solves for and stores the projection of the normal vector onto point

		//solves for and stores the variables for the S in the projection of the closest point from a plane
		x = ship.x - proj.x;
		y = ship.y - proj.y;
		z = ship.z - proj.z;
	}

	void translation(float xT, float yT, float zT, float wT) //translates the current vector by the given parameter points
	{
		x += xT;
		y += yT;
		z += zT;
		vPoint = wT;
	}

	void rawScaling(float xS, float yS, float zS, float wS) //scales the current vector by the given paramter points
	{
		x *= xS;
		y *= yS;
		z *= zS;
		vPoint *= wS;
	}

	void scalingCenter(Vector3D concat, float xS, float yS, float zS, float wS) //multiplies the concatenation and the current vector to find the scaling about a center
	{
		x = (x * xS) + (concat.x * wS);
		y = (y * yS) + (concat.y * wS);
		z = (z * zS) + (concat.z * wS);
		vPoint = wS;
	}

	void crossProduct4( float xS, float yS, float zS, float wS, float xC, float yC, float zC ) //finds the concatenation of the two points
	{
		x = xC * (1 - xS);
		y = yC * (1 - yS);
		z = zC * (1 - zS);
		vPoint = wS;
	}

	float getPE( float s)
	{
		float mass = 100;
		float gravity = 9.8f;

		return mass * gravity * ((-.3219f * s) + 10.0f);
	}

	void reflection3D( Vector3D iVelocity, float cRest, Vector3D v1, Vector3D v2 )//reflection of an object off of a plane based on two vectors
	{
		Vector3D n;//the normal vector
		Vector3D nNormal;//the normalized vector or n with a ^ on it
		Vector3D fVelocity;//the final velocity of the function in meters per second

		//normal vector magnitude
		float nMag = 0;
		nMag = n.getMagnitude();

		n.crossProduct(v1, v2);//cross product of the two given vectors of the plane
		
		//figuring the normalized vector by diving the normal by normal magnitude
		nNormal.x = n.x / nMag;
		nNormal.y = n.y / nMag;
		nNormal.z = n.z / nMag;

		//solves for final velocity of the object based upon all factors
		fVelocity.x = iVelocity.x - ((( 1 + cRest ) * ( (iVelocity.x * nNormal.x) + (iVelocity.y * nNormal.y) + (iVelocity.z * nNormal.z) ) ) * nNormal.x);
		fVelocity.y = iVelocity.y - ((( 1 + cRest ) * ( (iVelocity.x * nNormal.x) + (iVelocity.y * nNormal.y) + (iVelocity.z * nNormal.z) ) ) * nNormal.y);
		fVelocity.z = iVelocity.z - ((( 1 + cRest ) * ( (iVelocity.x * nNormal.x) + (iVelocity.y * nNormal.y) + (iVelocity.z * nNormal.z) ) ) * nNormal.z);

		//transfers the final velocity into the 3D vector class holder
		x = fVelocity.x;
		y = fVelocity.y;
		z = fVelocity.z;
	}

	void reflection1D( float iVelocity1, float iVelocity2, float mass1, float mass2, float cRest )//solves for the velocities of two objects that collide
	{
		//holder for the final velocities of the two givin objects in meters per second
		float fVelocity1 = 0;
		float fVelocity2 = 0;

		//mathematically solves for the final velocities of two objects based on given parameters
		fVelocity1 = (((mass1 - (cRest*mass2)) * iVelocity1) + ((1 + cRest) * mass2 * iVelocity2))/ (mass1 + mass2);
		fVelocity2 = (cRest * ( iVelocity1 - iVelocity2)) + fVelocity1;

		//outputs the two velocities to the screen.
		cout << "The final velocity of object 1 is: " << fVelocity1 << "m/s." << endl;
		cout << "The final velocity of object 2 is: " << fVelocity2 << "m/s." << endl;
	}

	void xRotation ( float degrees )//rotates the points on the x axis
	{
		//provides old coordinate points for non-changing use
		float oldx = x;
		float oldy = y;
		float oldz = z;

		//rotational math on the x axis
		x = oldx;
		y = (oldy * cos(degrees)) + (-sin(degrees) * oldz);
		z = (sin(degrees) * oldy) + (cos(degrees) * oldz);
	}

	void yRotation ( float degrees )//rotates the points on the y axis
	{
		//provides old coordinate points for non-changing use
		float oldx = x;
		float oldy = y;
		float oldz = z;

		//rotational math on the y axis
		x = (cos(degrees) * oldx) + (sin(degrees) * oldz);
		y = oldy;
		z = (-sin(degrees) * oldx) + (cos(degrees) * oldz);
	}

	void zRotation ( float degrees )//rotates the points on the z axis
	{
		//provides old coordinate points for non-changing use
		float oldx = x;
		float oldy = y;
		float oldz = z;

		//rotational math on the z axis
		x = (cos(degrees) * oldx) + (-sin(degrees) * oldy);
		y = (sin(degrees) * oldx) + (cos(degrees) * oldy);
		z = oldz;
	}

	Vector3D add( Vector3D velocity)
	{
		float oldX = x;
		float oldY = y;
		float oldZ = z;

		x = x + velocity.x;
		y = y + velocity.y;
		z = z + velocity.z;
	}

	void mult( float time )
	{
		x = x * time;
		y = y * time;
		z = z * time;
	}

private:
	float x;	//Private Variable Declarations
	float y;
	float z;
	float vPoint;

};