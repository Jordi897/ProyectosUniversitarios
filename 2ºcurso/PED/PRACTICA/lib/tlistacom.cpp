#include "tlistacom.h"

TListaNodo::TListaNodo(){
    anterior = nullptr;
    siguiente = nullptr;
    e = TComplejo();
}

void TListaNodo::clone(const TListaNodo& nodo){
    this->e = nodo.e;
    this->siguiente = nodo.siguiente;
    this->anterior = nodo.anterior;
}

TListaNodo::TListaNodo(const TListaNodo& nodo){
    clone(nodo);
}

TListaNodo::~TListaNodo(){
    anterior = nullptr;
    siguiente = nullptr;
}

TListaNodo& TListaNodo::operator=(const TListaNodo& nodo){
    if(this == &nodo) return *this;
    this->~TListaNodo();
    clone(nodo);
    return  *this;
}

TListaPos::TListaPos(){
    p = nullptr;
}

void TListaPos::clone(const TListaPos& posicion){
    this->p = posicion.p;
}

TListaPos::TListaPos(const TListaPos& posicion){
    clone(posicion);
}

TListaPos::~TListaPos(){
    p = nullptr;
}

TListaPos& TListaPos::operator=(const TListaPos& posicion){
    if(this == &posicion) return *this;
    this->~TListaPos();
    clone(posicion);
    return *this;
}

bool TListaPos::operator==(const TListaPos& posicion) const{
    if(this == &posicion) return true;
    if(&posicion == nullptr) return false;
    if(posicion.p == this->p) return true;
    return false;
}

bool TListaPos::operator!=(const TListaPos& posicion) const{
    return !(posicion == *this);
}

TListaPos TListaPos::Anterior() const{
    TListaPos p;
    p.p = (this->p == nullptr) ? nullptr:this->p->anterior;
    return p;
}

TListaPos TListaPos::Siguiente() const{
    TListaPos p;
    p.p = (this->p == nullptr) ? nullptr:this->p->siguiente;
    return p;
}

bool TListaPos::EsVacia() const{
    return p == nullptr;
}

TListaCom::TListaCom(){
    primero = nullptr;
    ultimo = nullptr;
}

void TListaCom::clone(const TListaCom& l){
    TListaPos pos;
    TListaPos aux;
    pos.p = l.primero;
    if(pos.EsVacia()){
       primero =nullptr;
       ultimo = nullptr; 
    }else{
        primero = new TListaNodo(*(pos.p));
        if(primero == nullptr) exit(1);
        aux.p = primero;
        aux.p->siguiente = nullptr;
        pos = pos.Siguiente();
        while (!pos.EsVacia())
        {
            aux.p->siguiente = new TListaNodo(*(pos.p));
            aux.Siguiente().p->anterior = aux.p;
            aux = aux.Siguiente();
            aux.p->siguiente = nullptr;
            pos = pos.Siguiente();
        }
        ultimo = aux.p;
    }
}

TListaCom::TListaCom(const TListaCom& l){
    clone(l);
}

TListaCom::~TListaCom(){
    
    TListaPos pos;
    TListaPos aux;
    pos.p = primero;
    while (!pos.EsVacia())
    {
        aux = pos.Siguiente();
        delete (pos.p);
        pos.p = nullptr;
        pos = aux;
    }
    primero = nullptr;
    ultimo = nullptr;
}

TListaCom& TListaCom::operator=(const TListaCom& l){
    if(this == &l) return *this;
    this->~TListaCom();
    clone(l);
    return *this;
}

bool TListaCom::operator==(const TListaCom& l) const{
    if(&l == this) return true;
    TListaPos posThis;
    TListaPos posL;
    posThis.p = primero;
    posL.p = l.primero;
    while(!posThis.EsVacia() && !posL.EsVacia()){
        if(Obtener(posThis) != Obtener(posL)) return false;
        posThis = posThis.Siguiente();
        posL = posL.Siguiente();
    }
    if(posThis.p == nullptr && posL.p == nullptr) return true;
    return false;
}

bool TListaCom::operator!=(const TListaCom& l) const{
    return !(l == *this);
}

TListaCom TListaCom::operator+(const TListaCom& l) const {
    TListaCom l1(*this);
    TListaCom l2(l);

    l1.ultimo->siguiente = l2.primero;
    l2.primero->anterior = l1.ultimo;
    l1.ultimo = l2.ultimo;

    TListaCom l3(l1);

    l1.ultimo = l2.primero->anterior;
    l2.primero->anterior = nullptr;
    l1.ultimo->siguiente = nullptr;

    return l3;
}

TListaPos TListaCom::Busqueda(const TComplejo& c) const{
    TListaPos p;
    p.p = primero;
    while (!p.EsVacia())
    {
        if(Obtener(p) == c) return p;
        p = p.Siguiente();
    }
    return p;
}    

TListaCom TListaCom::operator-(const TListaCom& l) const{
    TListaCom aux(*this);
    TListaPos p;
    p.p = l.primero;
    while (!p.EsVacia())
    {
        aux.BorrarTodos(Obtener(p));
        p = p.Siguiente();
    }

    return aux; 
}

bool TListaCom::Borrar(const TComplejo& c){
    TListaPos p;
    p = Busqueda(c);
    if(p.EsVacia()) return false; 
    Borrar(p);
    return true;
}

bool TListaCom::Borrar(TListaPos& p){
    if(p.EsVacia()) return false;
    TListaPos ant;
    TListaPos sig;
    TListaPos pri = Primera();
    TListaPos ult = Ultima();

    ant = p.Anterior();
    sig = p.Siguiente();
    if(pri == p) primero = sig.p;
    if(ult == p) ultimo = ant.p;
    if(!ant.EsVacia()) ant.p->siguiente = sig.p;
    if(!sig.EsVacia()) sig.p->anterior = ant.p;

    delete p.p;
    p.p = nullptr;

    return true;
}

bool TListaCom::BorrarTodos(const TComplejo& c){
    bool bor = false;
    while (Borrar(c)) bor = true;
    return bor;
}

bool TListaCom::EsVacia() const{
    return primero == nullptr;
}

TComplejo TListaCom::Obtener(const TListaPos& l) const{
    if(l.EsVacia()) return TComplejo();
    return l.p->e;
}

bool TListaCom::Buscar(const TComplejo& c) const{
    return !Busqueda(c).EsVacia();
}

int TListaCom::Longitud() const{
    int longi = 0;
    TListaPos p;
    p.p = primero;
    while (!p.EsVacia())
    {
        longi++;
        p = p.Siguiente();
    }
    return longi;
}

TListaPos TListaCom::Primera() const{
    TListaPos p;
    p.p = primero;
    return p;
}

TListaPos TListaCom::Ultima() const{
    TListaPos p;
    p.p = ultimo;
    return p;
}

bool TListaCom::InsCabeza(const TComplejo& c){
    TListaNodo *n = new TListaNodo();
    if(n == nullptr) return false;
    n->e = c;
    if(!this->EsVacia()){
        n->siguiente = primero;
        primero->anterior = n;
        primero = n;
    }else{
        primero = n;
        ultimo = n;
    }

    return true;
}

bool TListaCom::InsertarI(const TComplejo& c,const TListaPos& p){
    TListaNodo *n = new TListaNodo();
    if(n == nullptr) return false;
    if(p.EsVacia()) return false;
    if(p.p == primero) primero = n;
    n->e = c;
    n->anterior = p.Anterior().p;
    n->siguiente = p.p;
    if(!p.Anterior().EsVacia()) p.Anterior().p->siguiente = n;
    p.p->anterior = n; 
    return true;
}

bool TListaCom::InsertarD(const TComplejo& c, const TListaPos& p){
    TListaNodo *n = new TListaNodo();
    if(n == nullptr) return false;
    if(p.EsVacia()) return false;
    if(p.p == ultimo) ultimo = n;
    n->e = c;
    n->siguiente = p.Siguiente().p;
    n->anterior = p.p;
    if(!p.Siguiente().EsVacia()) p.Siguiente().p->anterior = n;
    p.p->siguiente = n;

    return true;
}

ostream& operator<<(ostream& o, const TListaCom& l){
    o << "{";
    TListaPos p = l.Primera();
    for(int i=0;i<l.Longitud();i++){
        o << l.Obtener(p);
        p = p.Siguiente();
        if(!p.EsVacia()) o << " ";
    }

    o << "}";
    return o;
}