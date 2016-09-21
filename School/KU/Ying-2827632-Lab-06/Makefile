Lab06: main.o Test.o
	g++ -std=c++11 -g -Wall main.o Test.o -o Lab06
	
main.o: main.cpp Sorts.h Sorts.hpp Test.h
	g++ -std=c++11 -g -Wall -c main.cpp
	
Test.o: Test.h Sorts.h
	g++ -std=c++11 -g -Wall -c Test.cpp
	
clean:
	rm *.o Lab06
	echo clean done
