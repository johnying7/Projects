/***********************************************************
/
/	Vector3D.h creates a class for other main.cpp functions
/	to access for use using vectors, and the projections for
/	those vectors to interact with each other.
/	
/	John Ying, 2/22/12
/	Johnson County Community College
/
/**********************************************************/

#include <iostream>
#include <string>
#include <cmath>
using namespace::std;


class Vector3D
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

	void setRectGivenRect( float setX, float setY, float setZ ) //sets the coordinates for the object given coordinates
	{
		x = setX;
		y = setY;
		z = setZ;
	}

	void setRectGivenPolar( float mag, float degrees ) //sets the coordinates for the object given coordinates
	{
		x = mag*cos(degrees);
		y = mag*sin(degrees);
	}

	void setRectGivenMagHeadPitch( float mag, float heading, float pitch) //produces the x, y, and z coordinates from magnitude, heading, and pitch
	{
		x = mag*cos(pitch)*cos(heading);
		y = mag*cos(pitch)*sin(heading);
		z = mag*sin(pitch);
	}

	void printRect(void)	// prints the x,y, and z coordinates
	{
		cout << endl;
		cout << "x: " << x << endl;
		cout << "y: " << y << endl;
		cout << "z: " << z << endl;
	}

	void printMagHeadPitch()	// prints the magnitude, heading, and pitch
	{
		cout << endl;
		cout << "Magnitude: " << getMagnitude() << endl;
		cout << "Heading: " << getHeading() << endl;
		cout << "Pitch: " << getPitch() << endl;
	}

	void printAngles(void)	// prints the angles
	{
		cout << endl;
		cout << "The angles are\n";
		cout << "Alpha: " << getAlpha() << endl;
		cout << "Beta: " << getBeta() << endl;
		cout << "Gamma: " << getGamma() << endl;
	}

	float getMagnitude() //solves for the magnitude of the vector
	{
		return (sqrt((x*x)+(y*y)+(z*z)));
	}

	float getPitch() //solves for the pitch of the vector
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (asin(z/getMagnitude()));
	}

	float getHeading() // solves for the heading of the vector
	{
		if (sqrt((x*x)+(y*y)) == 0)
			return 0;
		else
			return (acos(x/(sqrt((x*x)+(y*y)))));
	}

	float getAlpha() //solves for the alpha angle
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(x / getMagnitude()));
	}

	float getBeta() //solves for the beta angle
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(y / getMagnitude()));
	}

	float getGamma() //solves for the gamma angle
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(z / getMagnitude()));
	}

	float getX() //finds and returns the x value in the vector
	{
		return x;
	}

	float getY() //finds and returns the y value in the vector
	{
		return y;
	}

	float getZ() //finds and returns the z value in the vector
	{
		return z;
	}

	Vector3D operator+ (Vector3D v) //sum of two vectors
	{
		Vector3D temp;
		temp.setRectGivenRect (x+v.getX(),y+v.getY(), z+v.getZ());
		return temp;
	}

	Vector3D operator- (Vector3D v) //difference of two vectors
	{
		Vector3D temp;
		temp.setRectGivenRect (x-v.getX(),y-v.getY(), z-v.getZ());
		return temp;
	}

	Vector3D operator& (float num) //scalar multiplication of a vector
	{
		Vector3D temp;
		temp.setRectGivenRect (num*getX(), num*getY(), num*getZ());
		return temp;
	}

	Vector3D operator! () //normalization of a vector
	{
		Vector3D temp;
		temp = ( *this &(1.0f/getMagnitude()));
		return temp;
	}

	Vector3D operator* (Vector3D v) //dot product of two vectors
	{
		float answer;
		answer = (getMagnitude() * v.getMagnitude() ) * cos())
	}

	/*Vector3D operator% (Vector3D v) //angle between two vectors
	{
		Vector3D temp;
		temp.setRectGivenRect (x+v.getX(),y+v.getY(), z+v.getZ());
		return temp;
	}*/

	Vector3D operator| (Vector3D v) //perpendicular of two vectors
	{
		Vector3D temp;
		temp.setRectGivenRect (x - v.getX(operator^(v)), y - v.getY(operator^(v)), z - v.getZ(operator^(v)));
		return temp;
	}

	Vector3D operator^ (Vector3D v) //parallel of two vectors
	{
		Vector3D temp;
		float num;
		num = (operator*(v) / (v.getMagnitude()*v.getMagnitude()));
		temp.setRectGivenRect (num*v.getX(),num*v.getY(), num*v.getZ());
		return temp;
	}
private:
	float x;	//Private Variable Declarations
	float y;
	float z;
	float vPoint;

};