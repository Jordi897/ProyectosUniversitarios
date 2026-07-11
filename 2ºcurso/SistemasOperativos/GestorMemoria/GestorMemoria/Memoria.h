#ifndef _Memoria_H_
#define _Memoria_H_

#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

struct Proceso{
    string nombre;
    unsigned int instanteLLegada;
    unsigned int tamano;
    unsigned int tiempoEjec;
    unsigned int posMemoria;
};

class Memoria{

    friend ostream& operator<<(ostream &o, const Memoria &m);

    private:
        unsigned int instanteTiempo;
        unsigned int tamanoMemoria;
        unsigned int siguienteHueco;
        Proceso **procesos;
    public:
    Memoria(int tamano);
    ~Memoria();
    void addProcesoPrimerHueco(Proceso p);
    void addProcesoSiguienteHueco(Proceso p);
    unsigned int getInstante_Tiempo() const { return instanteTiempo; };
    void passInstante();
    bool isEmpty() const { return tamanoMemoria == 1 && procesos[0]->nombre=="Hueco"; };
};

#endif