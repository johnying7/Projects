Lab02: main.o Node.o LinkedList.o Test.o
	g++ -std=c++11 -g -Wall main.o Node.o LinkedList.o Test.o -o Lab02

#main object
main.o: main.cpp LinkedList.cpp Test.cpp Test.h
	g++ -std=c++11 -g -Wall -c main.cpp

#Node object
Node.o: Node.h
	g++ -std=c++11 -g -Wall -c Node.cpp

#LinkedList object
LinkedList.o: LinkedList.h LinkedList.cpp Node.h
	g++ -std=c++11 -g -Wall -c LinkedList.cpp

#Test object
Test.o: Test.h LinkedList.h
	g++ -std=c++11 -g -Wall -c Test.cpp

clean:
	rm *.o Lab02
	echo clean done
