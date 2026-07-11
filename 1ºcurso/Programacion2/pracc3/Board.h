#ifndef _Board_
#define _Board_

#include <iostream>
#include <vector>
#include "Fighter.h"

using namespace std;

/*
 * Clase Board
 * -----------
 * Representa un tablero cuadrado donde se pueden colocar objetos Fighter.
 *
 * Atributos:
 *   - size: tamaño del tablero (size x size).
 *   - board: matriz de punteros a Fighter, donde cada celda puede contener
 *            un caza o estar vacía (nullptr).
 *
 * Funcionalidades:
 *   - Comprobar si una coordenada está dentro del tablero.
 *   - Obtener el Fighter situado en una coordenada concreta.
 *   - Lanzar un Fighter en una posición del tablero.
 */
class Board{
    private:
        int size;                               // Tamaño del tablero
        vector<vector<Fighter*>> board;         // Matriz de punteros a Fighter

    public:
        /*
         * Constructor
         * -----------
         * Inicializa el tablero con el tamaño indicado.
         * Crea una matriz size x size donde todas las posiciones comienzan en nullptr.
         */
        Board(int size);

        // Devuelve el tamaño del tablero
        int getSize() const { return size; };

        /*
         * getFighter
         * ----------
         * Devuelve el Fighter situado en la coordenada c.
         * Si la coordenada está fuera del tablero, el comportamiento depende del .cpp.
         */
        Fighter* getFighter(Coordinate c) const;

        /*
         * inside
         * ------
         * Comprueba si una coordenada está dentro de los límites del tablero.
         * Devuelve true si:
         *   - fila entre 0 y size-1
         *   - columna entre 0 y size-1
         */
        bool inside(Coordinate c) const { return (this->getSize() > c.getRow() && c.getRow() >= 0 && c.getColumn() < this->getSize() && c.getColumn()>=0); };

        /*
         * launch
         * ------
         * Intenta colocar un Fighter en la coordenada c.
         * El valor devuelto depende de la lógica implementada en el .cpp:
         *   - Puede indicar éxito, fallo, colisión, etc.
         */
        int launch(Coordinate c, Fighter *f);
};

#endif
