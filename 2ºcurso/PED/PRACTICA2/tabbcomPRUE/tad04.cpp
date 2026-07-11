/* Prueba:
     Borrar
*/
#include <iostream>

using namespace std;

#include "tabbcom.h"

int main(void) {
    TComplejo v, c(3), b(5, -5),d(1,0),e(2,3),f(30),g(0,-30),h(2);

    TABBCom a, a2;
    a.Insertar(e);
    a.Insertar(c);
    a.Insertar(v);
    a.Insertar(f);
    a.Insertar(g);
    a.Insertar(b);
    a.Insertar(d);

    a2 = a;
    a2.Insertar(h);

    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (0 -30), (6) (1 0), (7) (5 -5)]
    cout << a.Borrar(d) << endl; //! 1
    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (0 -30), (6) (5 -5)]
    a.Insertar(d);
    cout << a.Borrar(b) << endl; //! 1
    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (0 -30), (6) (1 0)]

    a.Insertar(b);
    cout << a.Borrar(v) << endl; //! 1
    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (1 0), (5) (0 -30), (6) (5 -5)]

    cout << a.Borrar(g) << endl; //! 1
    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (1 0), (5) (5 -5)]
    a.Insertar(h);
    a.Insertar(v);
    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (1 0), (5) (5 -5), (6) (0 0), (7) (2 0)]
    cout << a.Borrar(d) << endl; //! 1
    cout << a.Inorden() << endl; //* [(1) (0 0), (2) (2 0), (3) (3 0), (4) (2 3), (5) (5 -5), (6) (30 0)]
    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (5 -5), (6) (2 0)]
    cout << a.Borrar(a.Raiz()) << endl; //! 1
    cout << a.Inorden() << endl; //* [(1) (0 0), (2) (2 0), (3) (3 0), (4) (5 -5), (5) (30 0)]
    cout << a << endl; //* [(1) (3 0), (2) (0 0), (3) (30 0), (4) (2 0), (5) (5 -5)]

    while (!a2.EsVacio()){
        //? 8 veces
        cout << a2.Borrar(a2.Raiz()) << endl; //! 1 
    }

    cout << a.Borrar(d) << endl; //! 0
    cout << a.Borrar(g) << endl; //! 0
    cout << a.Borrar(e) << endl; //! 0

    return 0;
}