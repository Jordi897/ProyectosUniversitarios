#include "tvectorcom.h"

TComplejo TVectorCom::error;

TVectorCom::TVectorCom(void){
    c = nullptr;
    tamano = 0;
}

TVectorCom::TVectorCom(int tamano){
    if(tamano <= 0){
        this->tamano = 0;
        c = nullptr;
    } else {
        this->tamano = tamano;
        c = new TComplejo[tamano];
    }
}

TVectorCom::~TVectorCom(){
    tamano = 0;
    if(c != nullptr)
        delete[] c;
    c = nullptr;
}

void TVectorCom::clone(const TVectorCom& v){
    this->tamano = v.tamano;
    if(v.c == nullptr){
        this->c = nullptr;
    }else{
        this->c = new TComplejo[tamano];
        for(int i=0;i<tamano;i++)
            c[i] = v.c[i];
    }
}

TVectorCom::TVectorCom(const TVectorCom& v){
    clone(v);
}

TVectorCom& TVectorCom::operator=(const TVectorCom& v){
    if(this == &v) return *this;
    this->~TVectorCom();
    clone(v);
    return *this;
}

bool TVectorCom::operator==(const TVectorCom& v) const{
    if(this == &v) return true;
    if(tamano != v.tamano) return false;
    for(int i=0;i<tamano;i++)
        if(c[i] != v.c[i]) return false;
    return true;
}

bool TVectorCom::operator!=(const TVectorCom& v) const{
    return !((*this) == v);
}



TComplejo& TVectorCom::operator[](int index){
    if(index < 1 || index > tamano) return error;
    return c[index-1];
}

TComplejo TVectorCom::operator[](int index) const{
    if(index < 1 || index > tamano) return error;
    return c[index-1];
}

void TVectorCom::MostrarComplejos(const double& re) const{
    stringstream ss;
    bool bas = false;

    ss << "[";

    for(int i=0; i<tamano;i++){
        if(c[i].Re() >= re){ 
            if(bas){
                ss << ", ";
            }
            ss << c[i];
            bas = true;
        }
    }
    ss << "]";
    cout << ss.str();
}

int TVectorCom::Ocupadas() const{
    int ocupados=0;
    TComplejo a;
    for(int i=0;i<tamano;i++){
        if(a != c[i]) ocupados++;
    }
    return ocupados;
}

bool TVectorCom::ExisteCom(const TComplejo& com) const{
    for(int i=0;i<tamano;i++)
        if(c[i] == com) return true;
    return false;
}

bool TVectorCom::Redimensionar(const int& red){
    if(red <= 0 || red == tamano) return false;
    TComplejo *array = new TComplejo[red];
    for(int i=0;i<red && i<tamano;i++)
        array[i] = c[i];
    this->~TVectorCom();
    tamano = red;
    c = array;
    return true;
}

ostream& operator<<(ostream& o, const TVectorCom& v){
    o << "[";
    for(int i=0;i<v.tamano;i++){
        if(i!=v.tamano-1) {
            o << "(" << i+1 << ") " << v.c[i] << ", ";
        } else{
            o << "(" << i+1 << ") " << v.c[i];
        }
    }
    o << "]";
    return o;
}