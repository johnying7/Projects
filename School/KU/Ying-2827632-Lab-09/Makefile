#test still needing to be added
Lab09: main.o MazeReader.o MazeCreationException.o MazeWalker.o Position.o Test.o
	g++ -std=c++11 -g -Wall main.o MazeReader.o MazeCreationException.o MazeWalker.o Position.o Test.o -o Lab09
 	
main.o: main.cpp MazeReader.h MazeWalker.h ArrayHelper.h ArrayHelper.hpp Test.h
	g++ -std=c++11 -g -Wall -c main.cpp

MazeReader.o: MazeReader.h MazeCreationException.h
	g++ -std=c++11 -g -Wall -c MazeReader.cpp

MazeCreationException.o: MazeCreationException.h
	g++ -std=c++11 -g -Wall -c MazeCreationException.cpp

MazeWalker.o: MazeWalker.h Position.h
	g++ -std=c++11 -g -Wall -c MazeWalker.cpp

Position.o: Position.h
	g++ -std=c++11 -g -Wall -c Position.cpp
	
Test.o: Test.h MazeReader.h MazeWalker.h ArrayHelper.h
	g++ -std=c++11 -g -Wall -c Test.cpp
	
clean:
	rm *.o Lab09
	echo clean done
