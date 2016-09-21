#add Test.o
Lab04: main.o PreconditionViolationException.o Test.o
	g++ -std=c++11 -Wall -g main.o PreconditionViolationException.o Test.o -o Lab04
	
main.o: main.cpp Node.h Node.hpp Stack.h Stack.hpp StackInterface.h
	g++ -std=c++11 -Wall -g -c main.cpp
	
Test.o: Test.h
	g++ -std=c++11 -Wall -g -c Test.cpp
	
PreconditionViolationException.o: PreconditionViolationException.h PreconditionViolationException.cpp
	g++ -std=c++11 -Wall -g -c PreconditionViolationException.cpp
	
clean:
	rm *.o Lab04
	echo clean done
