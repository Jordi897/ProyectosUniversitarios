#ifndef _AircraftCarrier_
#define _AircraftCarrier_

#include <iostream>
#include <vector>
#include "Fighter.h"

using namespace std;

/*
 * Clase AircraftCarrier
 * ----------------------
 * Representa un portaaviones que contiene:
 *  - Un nombre identificativo.
 *  - Estadísticas de victorias y derrotas.
 *  - Una flota de cazas (objetos Fighter almacenados como punteros).
 *
 * La clase permite:
 *  - Añadir cazas a la flota.
 *  - Actualizar resultados de combate.
 *  - Obtener el primer caza disponible según un tipo.
 *  - Purgar la flota eliminando cazas no operativos.
 *  - Mostrar la flota por pantalla.
 *  - Obtener una representación textual de la flota.
 *
 * También sobrecarga el operador << para imprimir información del portaaviones.
 */
class AircraftCarrier{

    // Sobrecarga del operador de salida para mostrar información del portaaviones
    friend ostream& operator<<(ostream &o,const AircraftCarrier &air);

    private:
        string name;               // Nombre del portaaviones
        int wins;                  // Número de victorias
        int losses;                // Número de derrotas
        vector<Fighter*> fleet;    // Flota de cazas almacenados como punteros

    public:
        /*
         * Constructor
         * -----------
         * Inicializa el portaaviones con un nombre y estadísticas a cero.
         */
        AircraftCarrier(string name);

        // Devuelve el nombre del portaaviones
        string getName() const { return name; };

        // Devuelve el número de victorias
        int getWins() const { return wins; };

        // Devuelve el número de derrotas
        int getLosses() const { return losses; };

        // Devuelve la flota completa de cazas
        vector<Fighter*> getFleet() const { return fleet; };

        /*
         * addFighters
         * -----------
         * Añade cazas a la flota a partir de un descriptor (string).
         * El formato y lógica de creación se implementan en el .cpp.
         */
        void addFighters(string fd);

        /*
         * updateResults
         * -------------
         * Actualiza las estadísticas del portaaviones.
         * r = 1 → victoria
         * r = 0 → derrota
         */
        void updateResults(int r);

        /*
         * getFirstAvailableFighter
         * -------------------------
         * Devuelve el primer caza disponible que coincida con el tipo indicado.
         * Si no existe ninguno, devuelve nullptr.
         */
        Fighter* getFirstAvailableFighter(string t) const;

        /*
         * purgeFleet
         * ----------
         * Elimina de la flota los cazas que ya no están operativos.
         * Devuelve el número de cazas eliminados.
         */
        int purgeFleet();

        /*
         * showFleet
         * ---------
         * Muestra por pantalla todos los cazas del portaaviones.
         */
        void showFleet() const;

        /*
         * myFleet
         * -------
         * Devuelve una cadena con la representación textual de la flota.
         * Útil para exportar o mostrar en interfaces externas.
         */
        string myFleet() const;
}; 

#endif
