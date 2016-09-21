 #Test.o
Lab08: main.o Test.o
	g++ -std=c++11 -g -Wall main.o Test.o -o Lab08
	
main.o: main.cpp Node.h Node.hpp BSTI.h BinarySearchTree.h BinarySearchTree.hpp Test.h
	g++ -std=c++11 -g -Wall -c main.cpp

Test.o: Test.h BinarySearchTree.h
	g++ -std=c++11 -g -Wall -c Test.cpp

clean:
	rm *.o Lab08
	echo clean done

