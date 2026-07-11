#ifndef _Coordinate_
#define _Coordinate_
 
#include <iostream>

using namespace std;

/*
 * Clase Coordinate
 * ----------------
 * Representa una posición dentro de un tablero mediante dos enteros:
 *   - row    → fila
 *   - column → columna
 *
 * Una coordenada es válida cuando ambos valores son >= 0.
 * Si no es válida, se considera que el objeto no está colocado en el tablero.
 *
 * También incluye:
 *   - Constructor por defecto (coordenada inválida).
 *   - Constructor parametrizado.
 *   - Métodos getters y setters.
 *   - Método reset() para volver a estado inválido.
 *   - Sobrecarga del operador << para imprimir la coordenada.
 */
class Coordinate{

    // Permite imprimir la coordenada con cout << coor
    friend ostream& operator<<(ostream &o,const Coordinate &coor);

    private:
        int row;       // Fila de la coordenada
        int column;    // Columna de la coordenada

    public:
        /*
         * Constructor por defecto
         * -----------------------
         * Inicializa la coordenada como inválida:
         *   row = -1
         *   column = -1
         */
        Coordinate() { row=-1; column = -1; };

        /*
         * Constructor parametrizado
         * -------------------------
         * Inicializa la coordenada con los valores dados.
         */
        Coordinate(int row, int column) {this->row=row; this->column = column;};

        // Devuelve la fila
        int getRow() const { return row; };

        // Devuelve la columna
        int getColumn() const { return column; };

        // Establece la fila
        void setRow(int row) { this->row = row; };

        // Establece la columna
        void setColumn(int column) { this->column = column; };

        /*
         * isValid
         * -------
         * Devuelve true si la coordenada es válida:
         *   row >= 0 y column >= 0
         */
        bool isValid() const { return (row >= 0 && column >= 0); };

        /*
         * reset
         * -----
         * Marca la coordenada como inválida:
         *   row = -1
         *   column = -1
         */
        void reset() { row = -1; column = -1; };
};

#endif
