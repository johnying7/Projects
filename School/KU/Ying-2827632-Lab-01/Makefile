Lab01: Colosseum.o main.o Pokemon.o Dice.o
	g++ -std=c++11 -Wall -g Colosseum.o Pokemon.o Dice.o main.o -o Lab01
#main object
main.o: Pokemon.cpp Colosseum.cpp main.cpp
	g++ -std=c++11 -Wall -g -c main.cpp
#colosseum object
Colosseum.o: Colosseum.h Pokemon.cpp Colosseum.cpp
	g++ -std=c++11 -Wall -g -c Colosseum.cpp
#pokemon object
Pokemon.o: Pokemon.h Pokemon.cpp
	g++ -std=c++11 -Wall -g -c Pokemon.cpp
#dice object
Dice.o: Dice.h Dice.cpp
	g++ -std=c++11 -Wall -g -c Dice.cpp

clean:
	rm *.o Lab01
	echo clean done
