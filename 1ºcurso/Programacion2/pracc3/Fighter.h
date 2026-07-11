#ifndef _Fighter_
#define _Fighter_

#include <iostream>
#include "Coordinate.h"

using namespace std;

/*
 * Clase Fighter
 * -------------
 * Representa un caza perteneciente a un portaaviones.
 *
 * Atributos principales:
 *   - type: tipo de caza (ej. F16, F18…)
 *   - speed: velocidad base del caza
 *   - attack: potencia de ataque
 *   - shield: puntos de defensa (si llega a 0, el caza está destruido)
 *   - id: identificador único del caza
 *   - aircraftCarrier: nombre del portaaviones al que pertenece
 *   - nextId: contador estático para asignar IDs únicos
 *   - position: coordenada actual del caza en el tablero
 *
 * Funcionalidades:
 *   - Ajustar atributos (ataque, velocidad, escudo)
 *   - Calcular daño infligido
 *   - Combatir contra otro Fighter
 *   - Comprobar si está destruido
 *   - Gestionar su posición en el tablero
 *
 * También sobrecarga el operador << para mostrar información del caza.
 */
class Fighter{

    // Permite imprimir el caza con cout << f
    friend ostream& operator<<(ostream &o,const Fighter &f);

    private:
        string type;              // Tipo de caza
        int speed;                // Velocidad base
        int attack;               // Potencia de ataque
        int shield;               // Defensa restante
        int id;                   // Identificador único
        string aircraftCarrier;   // Portaaviones al que pertenece
        static int nextId;        // Contador estático para IDs
        Coordinate position;      // Posición actual en el tablero

    public:
        /*
         * Constructor
         * -----------
         * Inicializa un caza con:
         *   - type: tipo de caza
         *   - aircraftCarrier: nombre del portaaviones
         *   - id: asignado automáticamente mediante nextId
         *   - Atributos base definidos en el .cpp
         *   - position: inicialmente inválida
         */
        Fighter(string type, string aircraftCarrier);

        /*
         * resetNextId
         * -----------
         * Reinicia el contador de IDs.
         * Útil para reiniciar partidas o simulaciones.
         */
        static void resetNextId();

        // Getters de atributos básicos
        string getType() const { return type; };
        int getId() const { return id; };
        int getSpeed() const { return speed; };
        int getAttack() const { return attack; };
        int getShield() const { return shield; };
        Coordinate getPosition() const { return position; };
        string getAircraftCarrier() const { return aircraftCarrier; };

        /*
         * setPosition
         * -----------
         * Establece la posición del caza en el tablero.
         */
        void setPosition(Coordinate c) { position.setRow(c.getRow()); position.setColumn(c.getColumn()); };

        /*
         * resetPosition
         * -------------
         * Marca la posición como inválida (fuera del tablero).
         */
        void resetPosition() { position.reset(); };

        /*
         * addAttack / addSpeed / addShield
         * --------------------------------
         * Modifican los atributos del caza.
         * - Nunca permiten que attack o speed sean negativos.
         * - El escudo sí puede bajar por debajo de 0 (destrucción).
         */
        void addAttack(int at) { attack = (at+attack <0) ? 0:at+attack; };
        void addSpeed(int sp) { speed = (sp+speed<0) ? 0:sp+speed; };
        void addShield(int sh) { shield = sh+shield; };

        /*
         * getDamage
         * ---------
         * Calcula el daño infligido según:
         *   daño = (n * attack) / 300
         * donde n es un modificador externo.
         */
        int getDamage(int n) const { return (n*attack)/300; };

        /*
         * fight
         * -----
         * Realiza un combate contra otro Fighter.
         * Devuelve:
         *   1 → si este caza gana
         *   0 → si pierde o no hay combate válido
         *
         * La lógica completa está implementada en el .cpp.
         */
        int fight(Fighter *enemy);

        /*
         * isDestroyed
         * -----------
         * Devuelve true si el escudo del caza es <= 0.
         */
        bool isDestroyed() const { return (shield <=0); };
};

#endif
