#Test.o
Lab07: main.o NumberFileGenerator.o SortDriver.o NumberFileDriver.o Test.o
	g++ -std=c++11 -g -Wall main.o NumberFileGenerator.o SortDriver.o NumberFileDriver.o Test.o -o Lab07
	
main.o: main.cpp Sorts.h Sorts.hpp NumberFileDriver.h SortDriver.h
	g++ -std=c++11 -g -Wall -c main.cpp

NumberFileGenerator.o: NumberFileGenerator.h NumberFileGenerator.cpp
	g++ -std=c++11 -g -Wall -c NumberFileGenerator.cpp

SortDriver.o: SortDriver.h SortDriver.cpp
	g++ -std=c++11 -g -Wall -c SortDriver.cpp
	
NumberFileDriver.o: NumberFileDriver.h NumberFileDriver.cpp
	g++ -std=c++11 -g -Wall -c NumberFileDriver.cpp
		
Test.o: Test.h Sorts.h NumberFileGenerator.h NumberFileDriver.h SortDriver.h Test.cpp
	g++ -std=c++11 -g -Wall -c Test.cpp
	
clean:
	rm *.o Lab07
	echo clean done

