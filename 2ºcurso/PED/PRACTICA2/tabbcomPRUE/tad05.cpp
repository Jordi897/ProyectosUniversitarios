/* Prueba:
     Buscar
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

    cout << a.Buscar(d) << endl; //! 1
    cout << a.Buscar(h) << endl; //! 0
    cout << a.Buscar(e) << endl; //! 1
    cout << a.Buscar(TComplejo(7.,9.)) << endl; //! 0

    return 0;
}