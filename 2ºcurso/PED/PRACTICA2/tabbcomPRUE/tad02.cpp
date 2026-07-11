/* Prueba:
     operador ==
*/
#include <iostream>

using namespace std;

#include "tabbcom.h"

int main(void) {
    TComplejo v, c(3), b(5, -5),d(1,0);

    TABBCom a, a2;

    cout << (a==a2) << endl; //! 1
    a.Insertar(v);

    cout <<( a == a2 )<< endl; //! 0

    cout <<( a2 == a )<< endl; //! 0

    a2.Insertar(v);

    cout << (a==a2) << endl; //! 1

    a.Insertar(b);

    cout << (a==a2) << endl; //! 0

    a2.Insertar(b);

    cout << (a==a2) << endl; //! 1

    cout << (a2 == a) << endl; //! 1

    a2.Insertar(c);

    a.Insertar(d);

    a2.Insertar(d);

    cout << (a == a2) << endl; //! 0
   
    return 0;
}