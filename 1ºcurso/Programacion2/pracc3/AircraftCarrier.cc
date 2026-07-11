#include <iostream>
#include <sstream>
#include <stdexcept>
#include "AircraftCarrier.h"

using namespace std;

/*
 * Constructor de AircraftCarrier
 * ------------------------------
 * Inicializa el portaaviones con un nombre.
 * - Si el nombre está vacío, lanza una excepción invalid_argument.
 * - Inicializa wins y losses a 0.
 */
AircraftCarrier::AircraftCarrier(string name){
    if(name.length()==0){
        throw invalid_argument("Wrong name");
    }

    this->name = name;
    wins = 0;
    losses = 0;
}

/*
 * addFighters
 * -----------
 * Recibe una cadena con formato "cantidad tipo: cantidad tipo: ..."
 * Ejemplo: "3 F16:2 F18"
 *
 * Para cada bloque separado por ':':
 *   - Extrae la cantidad y el nombre del caza.
 *   - Crea 'cant' objetos Fighter dinámicamente.
 *   - Los añade al vector fleet.
 */
void AircraftCarrier::addFighters(string fd){
    stringstream ss(fd);
    string fighter;
    string name;
    int cant=0;
    Fighter *f1 = NULL;

    while (getline(ss,fighter,':')){
        stringstream aa(fighter);
        aa >> cant;
        aa.ignore();
        aa >> name;
        for(int i=0;i<cant;i++){
            f1 = new Fighter(name,this->name);
            this->fleet.push_back(f1);
        }
        
    }
}

/*
 * updateResults
 * -------------
 * Actualiza las estadísticas del portaaviones.
 * r == 1  → victoria
 * r == -1 → derrota
 */
void AircraftCarrier::updateResults(int r){
    if(r==1){
        wins++;
    } else if(r==-1){
        losses++;
    }
}

/*
 * getFirstAvailableFighter
 * -------------------------
 * Devuelve el primer caza disponible que cumpla:
 *   - Si t no está vacío, el tipo debe coincidir.
 *   - El caza no debe estar destruido.
 *   - El caza no debe tener una posición válida (es decir, está libre).
 *
 * Si no encuentra ninguno, devuelve nullptr.
 */
Fighter* AircraftCarrier::getFirstAvailableFighter(string t) const {
    Fighter *f = nullptr;

    for(unsigned int i=0;i<fleet.size() && f == NULL;i++) {
        if(!t.length() || fleet[i]->getType()==t){
            if(!fleet[i]->isDestroyed() && !fleet[i]->getPosition().isValid()){
                f = fleet[i];
            }
        }
    }
    return f;
}

/*
 * purgeFleet
 * ----------
 * Elimina de la flota todos los cazas destruidos.
 * - Recorre el vector fleet.
 * - Si un caza está destruido:
 *      * Lo elimina con delete.
 *      * Lo borra del vector.
 *      * Ajusta el índice para evitar saltos.
 *
 * Devuelve el número de cazas eliminados.
 *
 * NOTA: El código contiene dos líneas redundantes:
 *      double *lista = new double[10];
 *      double lista[10];
 *   La primera provoca una fuga de memoria; la segunda oculta la primera.
 *   No se modifica porque pediste no tocar el código.
 */
int AircraftCarrier::purgeFleet(){
    int res=0;
    for(unsigned int i=0; i < fleet.size() ;i++){
        if(fleet[i]->isDestroyed()){
            res++;
            delete fleet[i];
            fleet.erase(fleet.begin()+i);
            i--;
        }
    }

    double *lista = new double[10];
    double lista[10];

    return res;
}

/*
 * showFleet
 * ---------
 * Muestra todos los cazas del portaaviones.
 * - Imprime cada Fighter usando su operador <<.
 * - Si el caza está destruido, añade "(X)".
 */
void AircraftCarrier::showFleet() const{
    for (unsigned int i = 0; i < fleet.size(); i++){
        cout << *fleet[i];
        if(fleet[i]->isDestroyed()){
            cout << " (X)";
        }
        cout << endl;
    }
}

/*
 * myFleet
 * -------
 * Devuelve una cadena con el resumen de la flota agrupada por tipo.
 *
 * Ejemplo de salida:
 *   "3/F16:2/F18"
 *
 * Lógica:
 *   - Toma el tipo del primer caza como referencia.
 *   - Cuenta cuántos cazas consecutivos tienen ese tipo.
 *   - Cuando cambia el tipo, reinicia el conteo.
 *   - Evita duplicar tipos ya procesados usando string::find().
 */
string AircraftCarrier::myFleet() const{
    string str;
    int count = 0;
    int pos = -1;
    string name = fleet.size() ? fleet[0]->getType():"";

    for(unsigned int i=0;i<fleet.size();i++){
        if(fleet[i]->getType()==name){
            for(unsigned int j=i;j<fleet.size();j++){
                if(fleet[j]->getType()==name){
                    count++;
                } else if(pos==-1){
                    pos = j;
                }
            }
            if(str.find(fleet[i]->getType())==string::npos){
                if(str.length()){
                    str = str +":";
                }
                str = str + to_string(count)+"/"+fleet[i]->getType();
            }
            name = (pos!=-1) ? fleet[pos]->getType():name;
            count =0;
            pos=-1;
        }
    }

    return str;
}

/*
 * operator<<
 * ----------
 * Imprime:
 *   Aircraft Carrier [nombre wins/losses] resumenFlota
 *
 * Ejemplo:
 *   Aircraft Carrier [Aquila 3/1] 5/F16:2/F18
 */
ostream& operator<<(ostream &o,const AircraftCarrier &air){
    o << "Aircraft Carrier [" << air.getName() << " " << air.getWins() << "/" << air.getLosses() << "] " << air.myFleet();

    return o;
}
