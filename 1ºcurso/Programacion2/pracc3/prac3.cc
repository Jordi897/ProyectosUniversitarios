#include <iostream>
#include "AircraftCarrier.h"
#include "Board.h"

using namespace std;

/*
 * Programa principal
 * ------------------
 * Este main demuestra el funcionamiento del sistema:
 *   - Creación de un tablero.
 *   - Creación de dos portaaviones con sus flotas.
 *   - Lanzamiento de cazas al tablero.
 *   - Simulación de un combate entre cazas.
 *   - Actualización de estadísticas.
 *   - Visualización del estado final de cada portaaviones.
 *
 * srand(888):
 *   Se fija la semilla del generador aleatorio para que los combates
 *   produzcan siempre los mismos resultados (reproducibilidad).
 */
int main(){
    srand(888);
    Board board(5);  // Tablero de tamaño 5x5

    // Creación del primer portaaviones y su flota
    AircraftCarrier one("USS Acme");
    one.addFighters("3/F-35B:2/F-18:3/A6");

    // Creación del segundo portaaviones y su flota
    AircraftCarrier two("Spectra One");
    two.addFighters("4/SF-1:3/SB-3:2/SI-6B");

    // Posicionamos algunos cazas sobre el tablero
    Coordinate c1(2,3);
    Fighter *f1=one.getFirstAvailableFighter("F-18");
    board.launch(c1,f1);

    Coordinate c2(3,2);
    Fighter *f2=two.getFirstAvailableFighter("");
    board.launch(c2,f2);

    // Estado después de lanzar un caza de cada portaaviones
    cout << "After launching one fighter of each aircraft:" << endl;
    cout << one << endl;
    one.showFleet();
    cout << endl;
    cout << two << endl;
    two.showFleet();
    cout << endl;

    // Intentamos lanzar otro caza del primer portaaviones sobre la posición del segundo
    Fighter *f3=one.getFirstAvailableFighter("");
    int result=board.launch(c2,f3);

    // Si hubo combate (result != 0), actualizamos estadísticas
    if(result!=0){
        one.updateResults(result);
        two.updateResults(-result);
    }
    
    // Estado después del combate
    cout << "After a fight between two fighters:" << endl;
    cout << one << endl;
    one.showFleet();
    cout << endl;
    cout << two << endl;
    two.showFleet();
    cout << endl;

    return 0;
}
