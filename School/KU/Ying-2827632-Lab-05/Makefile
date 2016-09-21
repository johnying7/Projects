#add Test.o
Lab04: main.o PreconditionViolationException.o AnimalPen.o FarmAnimal.o Cow.o Chicken.o CyberChicken.o
	g++ -std=c++11 -Wall -g main.o PreconditionViolationException.o AnimalPen.o FarmAnimal.o Cow.o Chicken.o CyberChicken.o -o Lab05
	
main.o: main.cpp Node.h Node.hpp Stack.h Stack.hpp StackInterface.h
	g++ -std=c++11 -Wall -g -c main.cpp
	
AnimalPen.o: AnimalPen.h AnimalPen.cpp
	g++ -std=c++11 -Wall -g -c AnimalPen.cpp
	
FarmAnimal.o: FarmAnimal.h FarmAnimal.cpp
	g++ -std=c++11 -Wall -g -c FarmAnimal.cpp

Cow.o: Cow.h Cow.cpp
	g++ -std=c++11 -Wall -g -c Cow.cpp

Chicken.o: Chicken.h Chicken.cpp
	g++ -std=c++11 -Wall -g -c Chicken.cpp

CyberChicken.o: CyberChicken.h CyberChicken.cpp
	g++ -std=c++11 -Wall -g -c CyberChicken.cpp

PreconditionViolationException.o: PreconditionViolationException.h PreconditionViolationException.cpp
	g++ -std=c++11 -Wall -g -c PreconditionViolationException.cpp
	
clean:
	rm *.o Lab05
	echo clean done
