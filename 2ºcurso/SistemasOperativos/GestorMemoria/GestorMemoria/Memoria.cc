#include "Memoria.h"
#include <stdexcept>

Memoria::Memoria(int tamano){
    Proceso *p = new Proceso;
    p->nombre = "Hueco";p->instanteLLegada=0;p->posMemoria=0;p->tamano=tamano;p->tiempoEjec=0;
    tamanoMemoria = 1;
    procesos = new Proceso*[1];
    procesos[0] = p;
    instanteTiempo = 1;
    siguienteHueco = 0;
}

Memoria::~Memoria(){
    for(int i=0;i<tamanoMemoria;i++){
        delete procesos[i];
    }
    delete[] procesos;
}

void Memoria::addProcesoPrimerHueco(Proceso p){
    if(p.instanteLLegada > this->instanteTiempo || p.tiempoEjec <= 0 || p.nombre == "Hueco" || p.tamano < 100) throw invalid_argument("Argumento Invalido");

    for(int i=0;i<tamanoMemoria;i++){
        if(procesos[i]->nombre == "Hueco" && procesos[i]->tamano >= p.tamano){
            Proceso *pNew = new Proceso;
            *pNew = p;
            pNew->posMemoria = procesos[i]->posMemoria;
            procesos[i]->posMemoria += pNew->tamano; 
            procesos[i]->tamano -= pNew->tamano;
            if(procesos[i]->tamano == 0){
                delete procesos[i];
                procesos[i] = pNew;
            }else{
                tamanoMemoria++;
                Proceso **pAux = new Proceso*[tamanoMemoria];
                for(int j=0;j<i;j++) pAux[j] = procesos[j]; 
                for(int j=i+1;j<tamanoMemoria;j++) pAux[j] = procesos[j-1];
                pAux[i] = pNew;
                delete[] procesos;
                procesos = pAux;
            }

            return;
        }
    }

    throw runtime_error("No hay espacio en memoria");
}

void Memoria::addProcesoSiguienteHueco(Proceso p){
    if(p.instanteLLegada > this->instanteTiempo || p.tiempoEjec <= 0 || p.nombre == "Hueco") throw invalid_argument("Argumento Invalido");

    for(int i=siguienteHueco;i<tamanoMemoria;i++){
        if(procesos[i]->nombre == "Hueco" && procesos[i]->tamano >= p.tamano){
            Proceso *pNew = new Proceso;
            *pNew = p;
            pNew->posMemoria = procesos[i]->posMemoria;
            procesos[i]->posMemoria += pNew->tamano; 
            procesos[i]->tamano -= pNew->tamano;
            if(procesos[i]->tamano == 0){
                delete procesos[i];
                procesos[i] = pNew;
            }else{
                tamanoMemoria++;
                Proceso **pAux = new Proceso*[tamanoMemoria];
                for(int j=0;j<i;j++) pAux[j] = procesos[j]; 
                for(int j=i+1;j<tamanoMemoria;j++) pAux[j] = procesos[j-1];
                pAux[i] = pNew;
                delete[] procesos;
                procesos = pAux;
            }
            siguienteHueco = i;

            return;
        }
    }

    for(int i=0;i<siguienteHueco;i++){
        if(procesos[i]->nombre == "Hueco" && procesos[i]->tamano >= p.tamano){
            Proceso *pNew = new Proceso;
            *pNew = p;
            pNew->posMemoria = procesos[i]->posMemoria;
            procesos[i]->posMemoria += pNew->tamano; 
            procesos[i]->tamano -= pNew->tamano;
            if(procesos[i]->tamano == 0){
                delete procesos[i];
                procesos[i] = pNew;
            }else{
                tamanoMemoria++;
                Proceso **pAux = new Proceso*[tamanoMemoria];
                for(int j=0;j<i;j++) pAux[j] = procesos[j]; 
                for(int j=i+1;j<tamanoMemoria;j++) pAux[j] = procesos[j-1];
                pAux[i] = pNew;
                delete[] procesos;
                procesos = pAux;
            }
            siguienteHueco = i;

            return;
        }
    }

    throw runtime_error("No hay espacio en memoria");
}

void Memoria::passInstante(){
    for(int i=0;i<tamanoMemoria;i++){
        if(procesos[i]->nombre != "Hueco")
            if(procesos[i]->tiempoEjec ==0){
                procesos[i]->nombre = "Hueco";
            }else procesos[i]->tiempoEjec--;
    }

    for(int i=0;i<tamanoMemoria;i++){
        if(procesos[i]->nombre == "Hueco" && i+1 < tamanoMemoria && procesos[i+1]->nombre == "Hueco"){
            tamanoMemoria--;
            Proceso **pAux = new Proceso*[tamanoMemoria];
            procesos[i+1]->posMemoria = procesos[i]->posMemoria;
            procesos[i+1]->tamano += procesos[i]->tamano;
            delete procesos[i];
            for(int j=i;j<tamanoMemoria;j++){
                procesos[j] = procesos[j+1];
            }
            for(int j=0;j<tamanoMemoria;j++){
                pAux[j] = procesos[j];
            }
            delete[] procesos;
            procesos = pAux;
            if(siguienteHueco == i+1) siguienteHueco--;
            i--;
        }
    }

    instanteTiempo++;
}

ostream& operator<<(ostream &o,const Memoria &m){
    o << m.instanteTiempo;
    for(int i=0;i<m.tamanoMemoria;i++){
        o << " [" << m.procesos[i]->posMemoria << " " << m.procesos[i]->nombre << " " << m.procesos[i]->tamano << "]";
    }
    
    o << endl;
    
    return o;
}