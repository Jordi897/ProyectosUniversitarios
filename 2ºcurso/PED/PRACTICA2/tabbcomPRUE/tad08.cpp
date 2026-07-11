/* Prueba:
     vectores
*/
#include <iostream>

using namespace std;

#include "tabbcom.h"

int main(void) {
    TComplejo v, c(3), b(5, -5),d(1,0),e(2,3),f(30),g(0,-30),h(2);

    TABBCom a, a2, a3;
    TVectorCom va = a.Niveles();
    TVectorCom va2 = a2.Niveles();
    TVectorCom va3 = a3.Niveles();

    cout << va << endl; //* []
    cout << va2 << endl; //* []
    cout << va3 << endl; //* []

    va = a.Inorden();
    va2 = a2.Preorden();
    va3 = a3.Postorden();

    cout << va << endl; //* []
    cout << va2 << endl; //* []
    cout << va3 << endl; //* []

    a.Insertar(e);
    a.Insertar(c);
    a.Insertar(v);
    a.Insertar(f);
    a.Insertar(g);
    a.Insertar(b);
    a.Insertar(d);

    a3.Insertar(c);
    a3.Insertar(g);
    a3.Insertar(d);
    a3.Insertar(f);
    a3.Insertar(h);
    a3.Insertar(b);
    a3.Insertar(e);
    a3.Insertar(v);

    a2 = a;
    a2.Insertar(h);

    va = a.Niveles();
    va2 = a2.Niveles();
    va3 = a3.Niveles();

    cout << a << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (0 -30), (6) (1 0), (7) (5 -5)]
    cout << va << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (0 -30), (6) (1 0), (7) (5 -5)]
    cout << va2 << endl; //* [(1) (2 3), (2) (3 0), (3) (30 0), (4) (0 0), (5) (0 -30), (6) (1 0), (7) (5 -5), (8) (2 0)]
    cout << va3 << endl; //* [(1) (3 0), (2) (1 0), (3) (0 -30), (4) (0 0), (5) (2 0), (6) (5 -5), (7) (30 0), (8) (2 3)]
    
    va = a.Inorden();
    va2 = a2.Inorden();
    va3 = a3.Inorden();

    cout << va << endl; //* [(1) (0 0), (2) (1 0), (3) (3 0), (4) (2 3), (5) (5 -5), (6) (0 -30), (7) (30 0)]
    cout << va2 << endl; //* [(1) (0 0), (2) (1 0), (3) (2 0), (4) (3 0), (5) (2 3), (6) (5 -5), (7) (0 -30), (8) (30 0)]
    cout << va3 << endl; //* [(1) (0 0), (2) (1 0), (3) (2 0), (4) (3 0), (5) (2 3), (6) (5 -5), (7) (0 -30), (8) (30 0)]

    va = a.Postorden();
    va2 = a2.Postorden();
    va3 = a3.Postorden();

    cout << va << endl; //* [(1) (1 0), (2) (0 0), (3) (3 0), (4) (5 -5), (5) (0 -30), (6) (30 0), (7) (2 3)]
    cout << va2 << endl; //* [(1) (2 0), (2) (1 0), (3) (0 0), (4) (3 0), (5) (5 -5), (6) (0 -30), (7) (30 0), (8) (2 3)]
    cout << va3 << endl; //* [(1) (0 0), (2) (2 0), (3) (1 0), (4) (2 3), (5) (5 -5), (6) (30 0), (7) (0 -30), (8) (3 0)]

    va = a.Preorden();
    va2 = a2.Preorden();
    va3 = a3.Preorden();

    cout << va << endl; //* [(1) (2 3), (2) (3 0), (3) (0 0), (4) (1 0), (5) (30 0), (6) (0 -30), (7) (5 -5)]
    cout << va2 << endl; //* [(1) (2 3), (2) (3 0), (3) (0 0), (4) (1 0), (5) (2 0), (6) (30 0), (7) (0 -30), (8) (5 -5)]
    cout << va3 << endl; //* [(1) (3 0), (2) (1 0), (3) (0 0), (4) (2 0), (5) (0 -30), (6) (5 -5), (7) (2 3), (8) (30 0)] 

    return 0;
}