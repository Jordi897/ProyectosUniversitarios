#include <iostream>
#include "Coordinate.h"

using namespace std;

/*
 * operator<<
 * ----------
 * Sobrecarga del operador de salida para imprimir una coordenada.
 *
 * Formato:
 *   - Si la coordenada es válida (row >= 0 y column >= 0):
 *         [row,column]
 *
 *   - Si la coordenada NO es válida:
 *         [-,-]
 *
 * Esto permite mostrar fácilmente la posición de un objeto en el tablero
 * o indicar que no está colocado.
 */
ostream& operator<<(ostream &o,const Coordinate &c){
    if(c.isValid()){
        o << "[" << c.row << "," << c.column << "]"; 
    } else {
        o << "[" << "-" << "," << "-" << "]"; 
    }

    return o;
}
