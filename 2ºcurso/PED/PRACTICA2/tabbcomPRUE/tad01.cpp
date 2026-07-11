/* Prueba:
     Constructor
*/
#include <iostream>

using namespace std;

#include "tabbcom.h"

int main(void) {
    TComplejo v, c(3), b(5, -5);

    TABBCom a;
    a.Insertar(v);
    a.Insertar(v);
    cout << a << endl; //![(1) (0 0)]
    a.Insertar(c);
    cout << a << endl; //! [(1) (0 0), (2) (3 0)]

    TABBCom a2(a), a3;
    a.Insertar(b);
    cout << a2 << endl; //! [(1) (0 0), (2) (3 0)]
    a3 = a;
    cout << a3 << endl; //! [(1) (0 0), (2) (3 0), (3) (5 -5)]
    a3.Insertar(TComplejo(-3));
    cout << a3 << endl;//! [(1) (0 0), (2) (3 0), (3) (-3 0), (4) (5 -5)]
   
    return 0;
}