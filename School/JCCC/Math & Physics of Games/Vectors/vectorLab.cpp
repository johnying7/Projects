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
		return (sqrt((getX()*getX())+(getY()*getY())+(getZ()*getZ())));
	}

	float getPitch()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (asin(getZ()/getMagnitude()));
	}

	float getHeading()
	{
		if (sqrt((getX()*getX())+(getY()*getY())) == 0)
			return 0;
		else
			return (acos(getX()/(sqrt((getX()*getX())+(getX()*getX())))));
	}

	float getAlpha()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(getX() / getMagnitude()));
	}

	float getBeta()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(getY() / getMagnitude()));
	}

	float getGamma()
	{
		if (getMagnitude() == 0)
			return 0;
		else
			return (acos(getZ() / getMagnitude()));
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

	float setX;
	float setY;
	float setZ;

	cout << "Please input your vector.{x y z}\n";

	cin >> setX >> setY >> setZ;

	Vector3D test;// creates an instance of the class

	test.setRectGivenRect( setX, setY, setZ );

	//prints out all the processed information
	test.printRect();
	test.printMagHeadPitch();
	test.printAngles();

}