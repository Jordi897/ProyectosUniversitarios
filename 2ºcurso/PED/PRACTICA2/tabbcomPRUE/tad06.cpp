/* Prueba:
     Raiz
*/
#include <iostream>

using namespace std;

#include "tabbcom.h"

int main(void) {
    TComplejo v, c(3), b(5, -5),d(1,0),e(2,3),f(30),g(0,-30),h(2);

    TABBCom a, a2, a3;
    a.Insertar(e);
    a.Insertar(c);
    a.Insertar(v);
    a.Insertar(f);
    a.Insertar(g);
    a.Insertar(b);
    a.Insertar(d);

    a2.Insertar(h);
    cout << a2.Insertar(h) << endl; //! 0

    cout << a.Raiz() << endl; //* (2 3)
    a.Borrar(d);
    cout << a.Raiz() << endl; //* (2 3)
    a.Insertar(d);
    a.Borrar(b);
    cout << a.Raiz() << endl; //* (2 3)
    TComplejo aux(a.Raiz());
    cout << a.Borrar(a.Raiz()) << endl; //* 1
    cout << (aux != a.Raiz() )<< endl; //* 1
    cout << a2.Raiz() << endl; //* (2 0)
    
    cout << a3.Raiz() << endl; //* (0 0)

    return 0;
}