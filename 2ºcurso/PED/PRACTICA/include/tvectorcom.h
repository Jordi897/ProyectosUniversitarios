#ifndef _TVectorCom_
#define _TVectorCom_

#include <iostream>
#include "tcomplejo.h"
#include <sstream>

using namespace std;

class TVectorCom {
    public:
        //Forma Canonica
        // Constructor por defecto 
        TVectorCom(void); 
        // Constructor a partir de un tamaño 
        TVectorCom (int); 
        // Constructor de copia 
        TVectorCom (const TVectorCom &); 
        // Destructor 
        ~TVectorCom (); 
        // Sobrecarga del operador asignación 
        TVectorCom & operator=(const TVectorCom &);

        //Metodos
        // Sobrecarga del operador igualdad 
        bool operator==(const TVectorCom &) const; 
        // Sobrecarga del operador desigualdad 
        bool operator!=(const TVectorCom &) const; 
        // Sobrecarga del operador corchete (parte IZQUIERDA) 
        TComplejo& operator[](int); 
        // Sobrecarga del operador corchete (parte DERECHA) 
        TComplejo operator[](int) const; 
        // Tamaño del vector (posiciones TOTALES) 
        int Tamano() const { return tamano; } 
        // Cantidad de posiciones OCUPADAS (TComplejo NO VACIO) en el vector 
        int Ocupadas() const; 
        // Devuelve TRUE  si existe el TComplejo en el vector 
        bool ExisteCom(const TComplejo &) const; 
        // Mostrar por pantalla los elementos TComplejo del vector  con PARTE REAL  IGUAL O POSTERIOR al argumento 
        void MostrarComplejos(const double&) const; 
        // REDIMENSIONAR el  vector de TComplejo 
        bool Redimensionar(const int&);
    private:
        TComplejo *c;
        int tamano;

        void clone(const TVectorCom&);
        static TComplejo error;

    // Sobrecarga del operador salida 
    friend ostream & operator<<(ostream &,const TVectorCom &); 
};

#endif