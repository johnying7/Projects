#add Test.o to 2nd and 3rd line
Lab03: main.o Test.o
	g++ -std=c++11 -Wall -g main.o Test.o -o Lab03

#main object	
main.o: main.cpp Node.h Node.hpp DoubleLinkedList.h DoubleLinkedList.hpp Test.h Test.cpp
	g++ -std=c++11 -Wall -g -c main.cpp
	
#Node.o: Node.h
#	g++ -std=c++11 -Wall -g -c Node.hpp
	
#Test object
Test.o: Test.h DoubleLinkedList.h
	g++ -std=c++11 -Wall -g -c Test.cpp
	
clean:
	rm *.o Lab03
	echo clean done
