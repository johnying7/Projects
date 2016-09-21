#include <iostream>
#include <string>
#include <cmath>
using namespace::std;

void sphereOverlap(float x1, float y1, float z1, float radius1,
	float x2, float y2, float z2, float radius2);		//Checks if the two spheres at given points overlap
void aabbOverlap(float x1, float y1, float z1, float x2, float y2, float z2,
	float bx1, float bx2, float by1, float by2, float bz1, float bz2);		//Checks if the two boxes overlap
void lineOverlap(float x1, float y1, float x2, float y2,
		float bx1, float bx2, float by1, float by2);		//Checks if the two given lines overlap
bool zCheck();		//Asks if there is a Z-coordinate in the shapes

void main()
{
	//Variable declarations for all shape uses
	float x1 = 0.0f;
	float y1 = 0.0f;
	float z1 = 0.0f;

	float x2 = 0.0f;
	float y2 = 0.0f;
	float z2 = 0.0f;

	//Variable declarations for the box and spheres
	float radius1 = 0.0f;
	float radius2 = 0.0f;

	//Variable declarations for the box(2) method
	float bx1 = 0.0f;
	float bx2 = 0.0f;
	float by1 = 0.0f;
	float by2 = 0.0f;
	float bz1 = 0.0f;
	float bz2 = 0.0f;

	string testInput;
	string sphereString = "sphere";
	string aabbString = "aabb";
	string lineString = "line";

	char anotherTest;


	cout << "Which overlap test would you like to do? {sphere, aabb, line}:\n";
	
	cin >> testInput;

	if (testInput == sphereString)
	{
		sphereOverlap(x1, y1, z1, radius1, x2, y2, z2, radius2);
	}
	else if (testInput == aabbString)
	{
		aabbOverlap( x1, y1, z1, x2, y2, z2, bx1, bx2, by1, by2, bz1, bz2);
	}
	else if (testInput == lineString)
	{
		lineOverlap( x1, y1, x2, y2, bx1, bx2, by1, by2 );
	}
	else
	{
		cout << "That is an incorrect selection.\n";
	}

	cout << "Would you like to try another test? {y, n}\n";

	cin >> anotherTest;

	if ( anotherTest == 'y' )
	{
		main();
	}
	else if ( anotherTest == 'n' )
	{

	}
	else
		cout << "Invalid answer, test will end.\n";

}

bool zCheck()
{
	char yesOrNo;

	cout << "Does this test include a Z-coordinate? {y, n}\n";
	cin >> yesOrNo;

	if (yesOrNo == 'y')
		return true;
	else if (yesOrNo == 'n')
		return false;
	else
	{
		cout << "Z-coordinate error has occured.\n";
		zCheck();		// Loops on itself if the yes or no answer was entered incorrectly
	}
}

void sphereOverlap(float x1, float y1, float z1, float radius1,
	float x2, float y2, float z2, float radius2)
{
	float rDistance;
	float tDistance;


	//Asks for user input coordinates

	cout << "Please input the center coordinates and radius of sphere 1 {x y z radius}:\n";
	cin >> x1 >> y1 >> z1 >> radius1;

	cout << "Please input the center coordinates and radius of sphere 2 {x y z radius}:\n";
	cin >> x2 >> y2 >> z2 >> radius2;


	//Does the math for finding the total distance and the combined radius' of the two spheres
	rDistance = radius1 + radius2;

	tDistance = sqrt( pow((x2-x1),2) + pow((y2-y1),2) +
		pow((z2-z1),2) );


	//Replies with the answer
	if ( rDistance < tDistance)
		cout << "No, they do not overlap.\n" << "The sum of radius' is: "
		<< rDistance << "\nThe total distance between points is: "
		<< tDistance << endl;
	else
		cout << "Yes, they do overlap.\n" << "The sum of radius' is: "
		<< rDistance << "\nThe total distance between points is: "
		<< tDistance << endl;

}


void aabbOverlap(float x1, float y1, float z1, float x2, float y2, float z2,
	float bx1, float bx2, float by1, float by2, float bz1, float bz2)
{
	float center1;
	float center2;
	float radius1;
	float radius2;
	float distance;

	float absValue;


	//Calls the zCheck method if there is the Z-coordinate to input
	if (zCheck() == true)
	{
		cout << "Please input the coordinates of point 1 on box 1 {x y z}:\n";
		cin >> x1 >> y1 >> z1;

		cout << "Please input the coordinates of point 2 on box 1 {x y z}:\n";
		cin >> x2 >> y2 >> z2;

		cout << "Please input the coordinates of point 1 on box 2 {x y z}:\n";
		cin >> bx1 >> by1 >> bz1;

		cout << "Please input the coordinates of point 2 on box 2 {x y z}:\n";
		cin >> bx2 >> by2 >> bz2;

		center1 = (x2 + x1) / 2;
		radius1 = x2 - center1;

		center2 = (bx2 + bx1) / 2;
		radius2 = bx2 - center2;

		distance = center1 - center2;

		absValue = (distance * distance);
		distance = sqrt(absValue);

		if (distance <= (radius1 + radius2))
		{
			center1 = (y2 + y1) / 2;
			radius1 = y2 - center1;

			center2 = (by2 + by1) / 2;
			radius2 = by2 - center2;

			distance = center1 - center2;

			absValue = (distance * distance);
			distance = sqrt(absValue);

			if (distance <= (radius1 + radius2))
			{
				center1 = (z2 + z1) / 2;
				radius1 = z2 - center1;

				center2 = (bz2 + bz1) / 2;
				radius2 = bz2 - center2;
				 
				distance = center1 - center2;

				absValue = (distance * distance);
				distance = sqrt(absValue);

				if (distance <= (radius1 + radius2))
				{
					cout << "They DO overlap.\n";
				}
				else
					cout << "They do NOT overlap.\n";
			}
			else
				cout << "They do NOT overlap.\n";
		}
		else
			cout << "They do NOT overlap.\n";
	}
	else			// If zCheck == false
	{
		cout << "Please input the coordinates of point 1 on box 1 {x y}:\n";
		cin >> x1 >> y1;

		cout << "Please input the coordinates of point 2 on box 1 {x y}:\n";
		cin >> x2 >> y2;

		cout << "Please input the coordinates of point 1 on box 2 {x y}:\n";
		cin >> bx1 >> by1;

		cout << "Please input the coordinates of point 2 on box 2 {x y}:\n";
		cin >> bx2 >> by2;

		center1 = (x2 + x1) / 2;
		radius1 = x2 - center1;

		center2 = (bx2 + bx1) / 2;
		radius2 = bx2 - center2;

		distance = center1 - center2;

		if (distance <= (radius1 + radius2))
		{
			center1 = (y2 + y1) / 2;
			radius1 = y2 - center1;

			center2 = (by2 + by1) / 2;
			radius2 = by2 - center2;

			distance = center1 - center2;

			absValue = (distance * distance);
			distance = sqrt(absValue);

			if (distance <= (radius1 + radius2))
			{
				cout << "They DO overlap.\n";
			}
			else
				cout << "They do NOT overlap.\n";
		}
		else
			cout << "They do NOT overlap.\n";
	}
}


void lineOverlap(float x1, float y1, float x2, float y2,
	float bx1, float bx2, float by1, float by2)
{
	//In terms of standard form Ax + By = C
	float xValue1; //stands for A in line 1
	float yValue1; //stands for B in line 1
	float ans1; //stands for C in line 1

	float xValue2; //stands for A in line 2
	float yValue2; //stands for B in line 2
	float ans2; //stands for C in line 2

	float xSol; //the x intersecting coordinate of lines
	float ySol; // the y intersecting coordinate of lines

	float zeroCheck; //Used to check if the problem divides by zero

	cout << "Input the values for the first line. {x1 y1 x2 y2}\n";
	cin >> x1 >> y1 >> x2 >> y2;

	ans1 = (( x2 - x1 )*y1) - (( y2 - y1 )*x1); //solves for the C in Ax + By = C in standard form for line 1
	xValue1 = y1 - y2;
	yValue1 = x2 - x1;

	/*
	For Math Concepts
	(xValue1)x + (yValue1)y = (ans1)
	*/

	cout << "Input the values for the second line. {x1 y1 x2 y2}\n";
	cin >> bx1 >> by1 >> bx2 >> by2;

	ans2 = (( bx2 - bx1 )*by1) - (( by2 - by1 )*bx1); //solves for the C in Ax + By = C in standard form for line 2
	xValue2 = by1 - by2;
	yValue2 = bx2 - bx1;

	/*
	For Math Concepts
	(xValue2)x + (yValue2)y = (ans2)
	*/

	xSol = ( ((yValue1)*(ans2))  -  ((ans1)*(yValue2)) ) /
		( ((yValue1)*(xValue2))  -  ((xValue1)*(yValue2))  ); //solves for x coordinate

	ySol = (  ((ans1)*(xValue2))  -  ((xValue1)*(ans2))  ) /
		( ((yValue1)*(xValue2))  -  ((xValue1)*(yValue2))  ); //solves for y coordinate
	

	zeroCheck = ((yValue1)*(xValue2))  -  ((xValue1)*(yValue2));

	if (zeroCheck != 0.0)
	{
		if ( x1 <= xSol && xSol <= x2 )
		{
			if ( bx1 <= xSol && xSol <= bx2 )
			{
				if ( y1 <= ySol && ySol <= y2 )
				{
					if ( by1 <= ySol && ySol <= by2 )
						cout << "The line segments DO intersect.\n";
					else
						cout << "The line segments do NOT intersect.\n";
				}
				else
					cout << "The line segments do NOT intersect.\n";
			}
			else
				cout << "The line segments do NOT intersect.\n";
		}
		else
			cout << "The line segments do NOT intersect.\n";
	}
	else
		cout << "The line segments do NOT intersect.\n";
}