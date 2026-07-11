/* Prueba:
     Altura, nodos, nodosHojas
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

    a.Insertar(e);
    a.Insertar(c);
    a.Insertar(v);
    a.Insertar(f);
    a.Insertar(g);
    a.Insertar(b);
    a.Insertar(d);
    a2 = a;
    a2.Insertar(h);
    cout << a2.Insertar(h) << endl; //! 0

    cout << a.Nodos() << endl; //* 7
    cout << a3.Nodos() << endl; //* 0
    cout << a2.Nodos() << endl; //* 8
    cout << a3.NodosHoja() << endl; //* 0
    cout << a3.Altura() << endl; //* 0
    a2 = a3;
    cout << a2.Nodos() << endl; //* 0
    cout << a.Altura() << endl; //* 4
    cout << a.NodosHoja() << endl; //* 2

    for(int i=0;i<100;i++){
        a3.Insertar(TComplejo((double)i));
    }

    cout << a3.Altura() << endl; //* 100

    return 0;
}