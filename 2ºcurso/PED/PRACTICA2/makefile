.PHONY= doc clean

CC=g++
OPTIONS= -g 
DEBUG= #-D DEBUG
LIBDIR=lib
INCLUDEDIR=include
_OBJ= tcomplejo.o tvectorcom.o tabbcom.o 
OBJ = $(patsubst %,$(LIBDIR)/%,$(_OBJ))

all:	tad
	valgrind  --tool=memcheck ./tad

tad:    src/tad.cpp $(OBJ)
	$(CC) $(OPTIONS) $(DEBUG) -I$(INCLUDEDIR) src/tad.cpp $(OBJ) -o tad

$(LIBDIR)/%.o : $(LIBDIR)/%.cpp $(INCLUDEDIR)/%.h
	$(CC) $(OPTIONS) $(DEBUG) -c -I$(INCLUDEDIR) -o $@ $<

com: lib src include ./nombres.txt clean
	tar -czvf PRACTICA2.tgz lib src include nombres.txt 

clean:
	rm -f $(OBJ) 