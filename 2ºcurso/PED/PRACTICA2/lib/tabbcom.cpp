#include "tabbcom.h"

TNodoABB::TNodoABB(): item(), iz(), de(){

}

void TNodoABB::clone(const TNodoABB& n){
    item = n.item;
    de = n.de;
    iz = n.iz;
}

TNodoABB::TNodoABB(const TNodoABB& n){
    clone(n);
}

TNodoABB::~TNodoABB(){
    item.~TComplejo();
    iz.~TABBCom();
    de.~TABBCom();
}

TNodoABB& TNodoABB::operator=(const TNodoABB& n){
    if(this == &n) return *this;
    this->~TNodoABB();
    clone(n);
    return *this;
}

//---------------------------------------------------

TABBCom::TABBCom(){
    nodo = nullptr;
}

void TABBCom::clone(const TABBCom& a){
    if(a.nodo == nullptr){
        nodo = nullptr;
    }else{
        nodo = new TNodoABB(*a.nodo);
    }
}

TABBCom::TABBCom(const TABBCom& a){
    clone(a);
}

TABBCom::~TABBCom(){
    if(nodo != nullptr){
        delete nodo;
        nodo = nullptr;
    }
}

TABBCom& TABBCom::operator=(const TABBCom& a){
    if(this == &a) return *this;
    this->~TABBCom();
    clone(a);
    return *this;
}

bool TABBCom::operator==(const TABBCom& a) const{
    if(a.nodo == nullptr && this->nodo == nullptr) return true;
    if(Nodos() != a.Nodos()) return false;
    TVectorCom v = Niveles();
    for(int i=1;i<=v.Tamano();i++){
        if(!a.Buscar(v[i])) return false;
    }
    return true;
}

bool TABBCom::EsVacio() const{
    return nodo == nullptr;
}

bool TABBCom::Insertar(const TComplejo& c){
    if(nodo == nullptr){
        nodo = new TNodoABB;
        nodo->item = c;
        return true;
    }
    if(nodo->item == c) return false;
    if(nodo->item > c) return nodo->iz.Insertar(c);
    else return nodo->de.Insertar(c);
}

TComplejo TABBCom::max() const{
    if(nodo == nullptr) return TComplejo();
    if(nodo->de.nodo != nullptr) return nodo->de.max();
    else return nodo->item;
}

bool TABBCom::Borrar(const TComplejo& c){
    if(EsVacio()) return false;
    if(this->nodo->item != c){
        if(nodo->item > c) return nodo->iz.Borrar(c);
        else return nodo->de.Borrar(c);
    }

    if(nodo->iz.nodo != nullptr && nodo->de.nodo != nullptr){
        TComplejo aux = nodo->iz.max();
        Borrar(aux);
        nodo->item = aux;
    } else if(nodo->iz.nodo == nullptr && nodo->de.nodo == nullptr) {
        delete nodo;
        nodo = nullptr;
    } else if(nodo->iz.nodo != nullptr){
        TNodoABB *aux = new TNodoABB(*(nodo->iz.nodo));
        delete nodo;
        nodo = nullptr;
        nodo = aux;
    } else if(nodo->de.nodo != nullptr){
        TNodoABB *aux = new TNodoABB(*(nodo->de.nodo));
        delete nodo;
        nodo = nullptr;
        nodo = aux;
    }

    return true;
}

bool TABBCom::Buscar(const TComplejo& c) const{
    if(EsVacio()) return false;
    if(nodo->item == c) return true;
    if(nodo->item > c) return nodo->iz.Buscar(c);
    else return nodo->de.Buscar(c);
}

TComplejo TABBCom::Raiz() const{
    if(EsVacio()) return TComplejo();
    return nodo->item;
}

int TABBCom::Altura() const{
    if(EsVacio()) return 0;
    int aD = nodo->de.Altura();
    int aI = nodo->iz.Altura();
    return 1 + (aD<aI ? aI:aD);
}

int TABBCom::Nodos() const{
    if(EsVacio()) return 0;
    return 1+nodo->iz.Nodos()+nodo->de.Nodos();
}

int TABBCom::NodosHoja() const{
    if(EsVacio()) return 0;
    if(nodo->iz.nodo == nullptr && nodo->de.nodo == nullptr) return 1;
    return nodo->iz.NodosHoja() + nodo->de.NodosHoja();
}

void TABBCom::InordenAux(TVectorCom& v,int& p) const{
    if(!EsVacio()){
        nodo->iz.InordenAux(v,p);
        v[p++] = Raiz();
        nodo->de.InordenAux(v,p);
    }
}

TVectorCom TABBCom::Inorden() const{
    TVectorCom v(Nodos());
    int posicion = 1;

    InordenAux(v,posicion);
    return v;
}

void TABBCom::PostordenAux(TVectorCom& v, int& p) const{
    if(!EsVacio()){
        nodo->iz.PostordenAux(v,p);
        nodo->de.PostordenAux(v,p);
        v[p++] = Raiz();
    }
}

TVectorCom TABBCom::Postorden() const{
    int posicion=1;
    TVectorCom v(Nodos());

    PostordenAux(v,posicion);
    return v;
}

void TABBCom::PreordenAux(TVectorCom& v,int& p) const{
    if(!EsVacio()){
        v[p++] = Raiz();
        nodo->iz.PreordenAux(v,p);
        nodo->de.PreordenAux(v,p);
    }
}

TVectorCom TABBCom::Preorden() const {
    int poscicion = 1;
    TVectorCom v(Nodos());

    PreordenAux(v,poscicion);
    return v;
}

TVectorCom TABBCom::Niveles() const{
    std::queue<TABBCom> cola;
    int posicion = 1;
    TVectorCom v(Nodos());
    if(EsVacio()) return v;
    TABBCom aux;
    cola.push(*this);
    while (!cola.empty()){
        aux = cola.front();
        v[posicion++] = aux.Raiz();
        cola.pop();
        if(!aux.nodo->iz.EsVacio()) cola.push(aux.nodo->iz);
        if(!aux.nodo->de.EsVacio()) cola.push(aux.nodo->de);
    }
    return v;
}

ostream& operator<<(ostream& o, const TABBCom& a){
    o << a.Niveles();
    return o;
}