/* Prueba:
     esVacio
*/
#include <iostream>

using namespace std;

#include "tabbcom.h"

int main(void) {
    TComplejo v, c(3), b(5, -5),d(1,0);

    TABBCom a, a2;

    cout << a.EsVacio() << endl; //! 1

    cout << a.Insertar(v) << endl; //! 1 

    cout << a.EsVacio() << endl; //! 0
    cout << a2.EsVacio() << endl; //! 1

    a2.Insertar(c);
    a2.Insertar(b);

    cout << a2.EsVacio() << endl; //! 0
   
    return 0;
}