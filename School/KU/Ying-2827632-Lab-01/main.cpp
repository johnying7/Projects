/**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.8.29
*	Purpose : main operation code that controls all core classes and interactions.
*	find what pre and post conditions for every method signature in the header files is....
*/

#include <iostream>
#include <string>
#include "Colosseum.h"
#include "Pokemon.h"

int main()
{
	Pokemon p1;
	Pokemon p2;
	Colosseum c1;
	bool keepPlaying = true;
	while (keepPlaying == true)
	{
		std::cout << "Choose your pokemon!" << std::endl;
		c1.userBuild(p1);
		c1.userBuild(p2);

		c1.play(p1,p2);

		std::string playAnswer = "";
		std::cout << "Keep playing (y/n)?" << std::endl;
		std::cin >> playAnswer ;
		if(playAnswer == "n")
		{
			keepPlaying = false;
		}
		else if (playAnswer == "y")
		{
			keepPlaying = true;
		}
		else
			std::cout << "Please input either y or n.";
	}
	
	return (0);
}
