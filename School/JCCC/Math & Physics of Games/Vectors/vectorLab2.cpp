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

	void setRectGivenRect( float setX, float setY, float setZ )
	{
		x = setX;
		y = setY;
		z = setZ;
	}

	void setRectGivenPolar( float mag, float degrees )
	{
		x = mag*cos(degrees);
		y = mag*sin(degrees);
	}

	void setRectGivenMagHeadPitch( float mag, float heading, float pitch)
	{
		x = mag*cos(pitch)*cos(heading);
		y = mag*cos(pitch)*sin(heading);
		z = mag*sin(pitch);
	}

	void printRect(void)	//public function for the class
	{
		cout << endl;
		cout << "x: " << x << endl;
		cout << "y: " << y << endl;
		cout << "z: " << z << endl;
	}

	void printMagHeadPitch()	//public function for the class
	{
		cout << endl;
		cout << "Magnitude: " << getMagnitude() << endl;
		cout << "Heading: " << getHeading() << endl;
		cout << "Pitch: " << getPitch() << endl;
	}

	void printAngles(void)	//public function for the class
	{
		cout << endl;
		cout << "The angles are\n";
		cout << "Alpha: " << getAlpha() << endl;
		cout << "Beta: " << getBeta() << endl;
		cout << "Gamma: " << getGamma() << endl;
	}

	float getMagnitude()
	{
		return (sqrt((x*x)+(y*y)+(z*z)));
	}

	float getPitch()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (asin(z/getMagnitude()));
	}

	float getHeading()
	{
		if (sqrt((x*x)+(y*y)) == 0)
			return 0;
		else
			return (acos(x/(sqrt((x*x)+(y*y)))));
	}

	float getAlpha()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(x / getMagnitude()));
	}

	float getBeta()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(y / getMagnitude()));
	}

	float getGamma()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(z / getMagnitude()));
	}

	float getX()
	{
		return x;
	}

	float getY()
	{
		return y;
	}

	float getZ()
	{
		return z;
	}


private:
	float x;	//Private Variable Declarations
	float y;
	float z;
	float vPoint;

};

void main()
{
	float mag;
	float heading;
	float pitch;

	cout << "Input the magnitude, heading, and pitch. {magnitude heading pitch}\n";

	//user inputs the information to be processed
	cin >> mag >> heading >> pitch;

	Vector3D test; // creates an instance of the class
	test.setRectGivenMagHeadPitch( mag, heading, pitch);

	//prints out all the processed information
	test.printRect();
	test.printMagHeadPitch();
	test.printAngles();
}