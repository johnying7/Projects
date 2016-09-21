/**********************************************************************
/
/    rotations.cpp allows for the user of scaling, translations,
/	 scaling about a center, and most importantly, rotations based
/	 on all user provided input information.
/
/    John Ying, May 2012
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
	float x, y, z, vPoint; //provides place holders for the user provided input
	Vector3D shape1[4]; //starts the necessary vector array that makes up a shape
	char repeatCheck = 'y'; //place holder check to see if the user wishes to do another transformation

	//asks the user for all of the coordinate points of the shape
	for (int i = 0; i < 4; i++)
	{
		cout << "Please input the coordinates for point " << i << ": {x, y, z, w}" << endl;
		cin >> x >> y >> z >> vPoint;
		shape1[i].setRectGivenRect(x, y, z, vPoint); 
	}

	while( repeatCheck == 'y')//while the program should be done again
	{
		string trans;//place holder for what type of transformation is to be done

		//scaling or translating variables
		float x = 1;
		float y = 1;
		float z = 1;
		float w = 1;

		//scaling about the center (center points)
		float xC = 1;
		float yC = 1;
		float zC = 1;

		float degrees = 0;
		string axis;


		cout << "What kind of transformation would you like to do?\n";
		cout << "t = translation" << endl << "rs = raw scaling" << endl << "sc = scaling about a center" << endl << "r = rotation" << endl;
		cin >> trans;

		if ( trans == "t" || trans == "rs" || trans == "sc" || trans == "r" )//checks if the input transformation is a valid input
		{
			if ( trans == "t")//if translation is chosen
			{
				cout << "What translations would you like to do? {x y z w}" << endl;
				cin >> x >> y >> z >> w;

				for (int count = 0; count < 4; count++)//changes the points of the shape based on translation
				{
					shape1[count].translation(x, y, z, w);
				}
			}
			else if (trans == "rs")//if raw scaling is chosen
			{
				cout << "What scaling would you like to do? {x y z w}" << endl;
				cin >> x >> y >> z >> w;

				for (int count = 0; count < 4; count++)//changes the points of the shape based on scaling
				{
					shape1[count].rawScaling(x, y, z, w);
				}
			}
			else if (trans == "sc")//if scaling about a center is chosen
			{
				Vector3D concat;//holder for the cross product to find the concatenation

				cout << "What scaling do you wish to do? {x y z w}" << endl;
				cin >> x >> y >> z >> w;

				cout << "What is the center of the object? {x y z}" << endl;
				cin >> xC >> yC >> zC;

				concat.crossProduct4(x, y, z, w, xC, yC, zC);//finds the concatenation based on the scalar and center points

				for (int count = 0; count < 4; count++)//changes the points of the shape based on given concatenation and scaling points
				{
					shape1[count].scalingCenter(concat, x, y, z, w);
				}
			}
			else if (trans == "r")//if rotation is chosen
			{
				cout << "What would you like to rotate? {degrees axis}" << endl;
				cin >> degrees >> axis;
				
				degrees = degrees * (3.14159f/180.0f);

				if ( axis == "x" )//calls each coordinate point if x axis rotation was chosen
				{
					for (int count = 0; count < 4; count++)//rotates the points of the shape based on scaling
					{
						shape1[count].xRotation(degrees);
					}
				}
				else if ( axis == "y" )//calls each coordinate point if y axis rotation was chosen
				{
					for (int count = 0; count < 4; count++)//rotates the points of the shape based on scaling
					{
						shape1[count].yRotation(degrees);
					}
				}
				else if ( axis == "z" )//calls each coordinate point if z axis rotation was chosen
				{
					for (int count = 0; count < 4; count++)//rotates the points of the shape based on scaling
					{
						shape1[count].zRotation(degrees);
					}
				}

			}
		}
		else //if the user input an incorrect transformation answer
		{
			cout << "Incorrect transformation input.\n";
		}

		//provides the final transformation coordinates by looping through each point in the shape by array
		cout << "The transformation output is:\n";
		for (int c = 0; c < 4; c++)
		{
			shape1[c].printRect();
		}

		//asks the user if they would like another transformation done on the newly transformed coordinates
		cout << "Would you like to do another transformation? y = yes; n = no" << endl;
		cin >> repeatCheck;

		//tells the user that the program will end since they input no
		if (repeatCheck == 'n')
		{
			cout << "Closing program....\n";
		}

	}

	return 0;//returning an int allows for use of executable programs requiring a return on main
}