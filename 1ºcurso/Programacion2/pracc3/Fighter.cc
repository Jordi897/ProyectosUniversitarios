#include <iostream>
#include <stdexcept>
#include <cstdlib>
#include "Fighter.h"

using namespace std;

const unsigned int KMAXRND = 100;

/*
 * getRandomNumber
 * ----------------
 * Devuelve un número aleatorio entre 0 y maxrnd-1.
 * Se usa en el sistema de combate para determinar probabilidades.
 */
int getRandomNumber(unsigned int maxrnd){
    return rand() % maxrnd;
}

int Fighter::nextId = 1;

/*
 * Constructor Fighter
 * -------------------
 * Inicializa un caza con:
 *   - type: tipo de caza (debe tener longitud > 0)
 *   - aircraftCarrier: nombre del portaaviones al que pertenece
 *   - position: coordenada inválida por defecto
 *   - speed = 100
 *   - attack = 80
 *   - shield = 80
 *   - id: asignado automáticamente mediante nextId
 *
 * Si el tipo está vacío, lanza invalid_argument.
 */
Fighter::Fighter(string type, string aircraftCarrier){
    if(type.length()==0){
        throw invalid_argument("Wrong type");
    }
    this->type = type;
    this->aircraftCarrier = aircraftCarrier;
    position = Coordinate();
    speed = 100;
    attack = 80;
    shield = 80;
    id = nextId;
    nextId++;
}

/*
 * resetNextId
 * -----------
 * Reinicia el contador de IDs.
 * Útil para reiniciar simulaciones o partidas.
 */
void Fighter::resetNextId(){
    nextId = 1;
}

/*
 * fight(Fighter *enemy)
 * ---------------------
 * Simula un combate entre este caza y otro enemigo.
 *
 * Mecánica del combate:
 *   - Cada ronda se calcula la probabilidad de que este caza ataque primero:
 *         u = (100 * speed) / (speed + enemy->speed)
 *
 *   - Se genera un número aleatorio n entre 0 y 99.
 *
 *   - Si u >= n:
 *         Este caza ataca:
 *             damage = this->getDamage(n)
 *             enemy->addShield(-damage)
 *     Si no:
 *         El enemigo ataca:
 *             damage = enemy->getDamage(100 - n)
 *             this->addShield(-damage)
 *
 *   - El combate continúa hasta que uno de los dos quede destruido (shield <= 0).
 *
 * Resultado devuelto:
 *   -  1 → este caza gana
 *   - -1 → este caza pierde
 *   -  0 → no debería ocurrir, pero indica empate o error lógico
 */
int Fighter::fight(Fighter *enemy){

    int res=0, n, u, damage;

    while (!this->isDestroyed() && !enemy->isDestroyed()){
        u = (100*this->speed)/(this->speed + enemy->getSpeed());
        n = getRandomNumber(KMAXRND);

        if(u >= n){
            damage = this->getDamage(n);
            enemy->addShield(-damage);
        } else {
            damage = enemy->getDamage(100-n);
            this->addShield(-damage);
        }

        if(this->isDestroyed()){
            res = -1;
        } else if(enemy->isDestroyed()){
            res=1;
        }
    }

    return res;
    
}

/*
 * operator<<
 * ----------
 * Imprime la información del caza en formato:
 *
 *   (type id [row,column] {speed,attack,shield})
 *
 * Si la posición es inválida, se mostrará [-,-].
 */
ostream& operator<<(ostream &o,const Fighter &f){
    o << "(" << f.type << " " << f.id << " " << f.position << " {" << f.speed << "," << f.attack << "," << f.shield << "})";
    return o;
}
