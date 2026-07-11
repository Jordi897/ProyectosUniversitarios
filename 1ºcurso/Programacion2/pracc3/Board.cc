#include <iostream>
#include <stdexcept>
#include "Board.h"

using namespace std;

/*
 * Constructor Board(int size)
 * ---------------------------
 * Crea un tablero cuadrado de dimensiones size x size.
 *
 * Validaciones:
 *   - Si size <= 0, lanza invalid_argument.
 *
 * Inicialización:
 *   - Crea un vector temporal 'f' con size elementos inicializados a nullptr.
 *   - Inserta 'size' copias de ese vector en 'board', formando una matriz.
 *
 * Resultado:
 *   - board[i][j] = nullptr para toda celda del tablero.
 */
Board::Board(int size){
    if(size <= 0){
        throw invalid_argument("Wrong size");
    }

    this->size = size;
    vector<Fighter*> f;
    for(int j=0;j<size;j++){
        f.push_back(nullptr);
    }
    for(int i=0;i<size;i++){
        board.push_back(f);
    }
}

/*
 * getFighter(Coordinate c)
 * ------------------------
 * Devuelve el Fighter situado en la coordenada c.
 *
 * Condiciones para devolver un Fighter:
 *   - La fila y columna deben estar dentro del tablero.
 *   - La coordenada debe ser válida (c.isValid()).
 *   - La celda no debe ser nullptr.
 *
 * Si alguna condición falla:
 *   - Devuelve nullptr.
 */
Fighter* Board::getFighter(Coordinate c) const{
    Fighter *f=nullptr;

    if((c.getRow() < size && c.getColumn() < size && c.isValid()) && !(board[c.getRow()][c.getColumn()] == nullptr)){
        f = board[c.getRow()][c.getColumn()];
    }

    return f;
}

/*
 * launch(Coordinate c, Fighter *f)
 * --------------------------------
 * Intenta colocar el Fighter f en la coordenada c.
 *
 * Reglas:
 *   1. El Fighter debe no tener posición válida (no estar ya en el tablero).
 *   2. La coordenada debe estar dentro del tablero (inside(c)).
 *
 * Caso A: La celda está vacía
 *   - Se coloca el Fighter en board[c.row][c.col].
 *   - Se actualiza su posición con setPosition(c).
 *
 * Caso B: La celda está ocupada
 *   - Si el Fighter atacante pertenece a un portaaviones distinto:
 *         res = f->fight(ocupante)
 *   - Si res == 1 (victoria del atacante):
 *         * Se resetea la posición del derrotado.
 *         * Se coloca el atacante en la celda.
 *
 * Valor devuelto:
 *   - 0 si no hubo combate o el atacante perdió.
 *   - 1 si el atacante ganó el combate.
 */
int Board::launch(Coordinate c, Fighter *f){
    int res=0;

    if(!(f->getPosition().isValid()) && this->inside(c)){
        if(this->getFighter(c)==nullptr){
            board[c.getRow()][c.getColumn()] = f;
            f->setPosition(c);
        } else {
            if(f->getAircraftCarrier() != this->getFighter(c)->getAircraftCarrier()){
                res = f->fight(this->getFighter(c));
            }
            if(res==1){
                this->getFighter(c)->resetPosition();
                board[c.getRow()][c.getColumn()] = f;
                f->setPosition(c);

            }
        }
    }

    return res;
}
