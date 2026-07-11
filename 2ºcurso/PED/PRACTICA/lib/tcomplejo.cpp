#include "tcomplejo.h"

/*
*Constructor por defecto
*/
TComplejo::TComplejo(void){
    re = 0;
    im = 0;
}

/*
*Contructor sobrecargado
*re = parte real
*/
TComplejo::TComplejo(const double &re){
    this->re = re;
    im = 0;
}

/*
*Contructor con parametros
*re = parte real, im = parte imaginaria
*/
TComplejo::TComplejo(const double &re, const double &im){
    this->re = re;
    this->im = im;
}

/*
*Contructor Copia
*/
TComplejo::TComplejo(const TComplejo& c){
    clone(c);
}

/*
*Destructor
*/
TComplejo::~TComplejo(){
    re = im = 0;
}

/*
*getter de Numero imaginario
*/
double TComplejo::Im() const{
    return im;
}

/*
*getter de Numero real
*/
double TComplejo::Re() const{
    return re;
}

/*
*Setter que modifica la parte real
*/
void TComplejo::Re(const double &re){
    this->re = re;
}

/*
*Setter que modifica la parte imaginaria
*/
void TComplejo::Im(const double &im){
    this->im = im;
}

/*
*Operacion suma de numeros Complejos
*/
TComplejo TComplejo::operator+(const TComplejo& c) const{
    double re = this->re + c.re;
    double im = this->im + c.im;

    return TComplejo(re,im);
}

/*
*Operacion resta de numeros Complejos
*/
TComplejo TComplejo::operator-(const TComplejo& c) const{
    double re = this->re - c.re;
    double im = this->im - c.im;

    return TComplejo(re,im);
}

/*
*Operacion multiplicacion de numeros Complejos
*/
TComplejo TComplejo::operator*(const TComplejo& c) const{
    double re = this->re * c.re + (this->im*c.im)*-1.;
    double im = this->im * c.re + this->re * c.im;

    return TComplejo(re,im);
}

/*
*Operacion suma de numero Complejo mas Real
*/
TComplejo TComplejo::operator+(const double &real) const{
    TComplejo c(real);

    return (*this) + c;
}

/*
*Operacion resta de numero Complejo mas Real
*/
TComplejo TComplejo::operator-(const double &real) const{
    TComplejo c(real);
    return (*this) - c;
}

/*
*Operacion multiplicacion de numero Complejo mas Real
*/
TComplejo TComplejo::operator*(const double &real) const{
    TComplejo c(real);

    return (*this)*c;
}
 /*
 *Operacion de asigancion
 */
TComplejo& TComplejo::operator=(const TComplejo& c) {
    if(this != &c){
        this->~TComplejo();
        clone(c);
    }
    return *this;
}

/*
*Operacion logica de igualdad
*Para que dos numeros complejos sean iguales tanto su parte real como imaginaria han de ser la misma
*/
bool TComplejo::operator==(const TComplejo& c) const{
    return this->im == c.im && this->re == c.re;
}

/*
*Operaion logica de desigualdad
*Para que un numero complejo no sea igual a otro con que una de sus partes difiera ya es verdad
*/
bool TComplejo::operator!=(const TComplejo& c) const{
    return !((*this) == c);
}

/*
*Calcula el argumento del numero complejo
*/
double TComplejo::Arg(){
    double arg = atan2(im,re);
    return arg;
}

/*
*Calcula el modula del numero complejo
*/
double TComplejo::Mod(){
    double mod = sqrt(pow(re,2)+pow(im,2));
    return mod;
}

/*
*Operacion << para mostrar con fromato (re,im)
*/
ostream& operator<<(ostream& o,const TComplejo& c){
    o << "(" << c.Re() << " " << c.Im() << ")";
    return o;
}

/*
*Operacion suma de numero Complejo mas Real
*/
TComplejo operator+(const double &re,const TComplejo& c){
    return c+re;
}

/*
*Operacion resta de numero Complejo mas Real
*/
TComplejo operator-(const double &re,const TComplejo& c){
    TComplejo first(re);
    
    return first - c;
}

/*
*Operacion multiplicacion de numero Complejo mas Real
*/
TComplejo operator*(const double &re,const TComplejo& c){
    return c*re;
}


/*
*Copiar toda la informacion de un objeto a otro
*/
void TComplejo::clone(const TComplejo &c){
    re = c.re;
    im = c.im;
}