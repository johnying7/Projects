 /**	@file : main.cpp
*	@author : John Ying
*	@date : 2015.10.6
*	Purpose : main operation code that controls all core classes and interactions.
*/
 
#include <iostream>
#include <string>
#include <stdexcept>
#include "Node.h"
#include "Stack.h"
#include "StackInterface.h"
#include "AnimalPen.h"
#include "Cow.h"
#include "Chicken.h"
#include "CyberChicken.h"

void goodbyeMessage(const FarmAnimal& animal)
{
	std::cout << "Upon release the " << animal.getName() << " said " << animal.getSound() << std::endl;
}

int main()
{
	AnimalPen* animalPen = new AnimalPen();
	
	char createAnimal = 'n';
	
	while(createAnimal == 'n')
	{
		switch(createAnimal)
		{
			case 'n':
			{
				int animalType;
				
				std::cout << "What kind of animal would you like to add?\n"
					<< "1) Cow (produced milk)\n"
					<< "2) Chicken (cannot lay eggs)\n"
					<< "3) Cyber Chicken (can lay eggs)\n";
				std::cin >> animalType;

				switch(animalType)
				{
					case 1:
					{
						FarmAnimal* cow = new Cow();
						std::cout << "How much milk can the cow produce?: ";
						double milk;
						std::cin >> milk;
						static_cast<Cow*>(cow)->setMilkProduced(milk);
						animalPen->addAnimal(cow);                                                
						break;
					}
					case 2:
					{
						FarmAnimal* chicken = new Chicken();
						animalPen->addAnimal(chicken);
						break;
					}
					case 3:
					{
						FarmAnimal* cyberChicken = new CyberChicken();
						std::cout << "How many eggs can it produce?: ";
						int eggs;
						std::cin >> eggs;
						static_cast<CyberChicken*>(cyberChicken)->setCyberEggs(eggs);
						animalPen->addAnimal(cyberChicken);
						break;
					}
					default:
						std::cout << "Invalid input.\n";
					break;
				}
				
				std::cout << "Are you finished creating an animal? (y/n)\n";
				std::cin >> createAnimal;
			break;
			}
			case 'y':
				return 0;
			break;
			default:
			{
				std::cout << "Invalid input.\n";
				createAnimal = 'n';
				break;
			}
		}
	}
	
	double totalMilkProduced = 0.0;
	int totalEggsProduced = 0;
	while(!animalPen->isPenEmpty())
	{
		if(animalPen->peakAtNextAnimal()->getName() == "Cow" )
		{
			Cow* cowPtr = static_cast<Cow*>(animalPen->peakAtNextAnimal());
			std::cout << "The Cow produced " << cowPtr->getMilkProduced() << " gallons of milk.\n";
			totalMilkProduced += cowPtr->getMilkProduced();
		}
		else if(animalPen->peakAtNextAnimal()->getName() == "Chicken")
		{
			std::cout << "Chicken is unable to lay eggs. Perhaps cybornetic implants will help?\n";
		}
		else if(animalPen->peakAtNextAnimal()->getName() == "Cyber Chicken")
		{
			CyberChicken* cyberChickenPtr = static_cast<CyberChicken*>(animalPen->peakAtNextAnimal());
			std::cout << "The Cyber Chicken laid " << cyberChickenPtr->getCyberEggs() << " cyber eggs. Humanity is in trouble.\n";
			totalEggsProduced += cyberChickenPtr->getCyberEggs();
		}
		goodbyeMessage(*animalPen->peakAtNextAnimal());
		animalPen->releaseAnimal();
	}
	std::cout << "Your farm produced " << totalMilkProduced << " gallons of milk and "
		<< totalEggsProduced << " eggs.\n";
	delete animalPen;
	return 0;	
}

