#ifndef _TComplejo_
#define _TComplejo_

#include <iostream>
#include <math.h>

using namespace std;

class TComplejo {
    public:
        //Forma Canonica
        TComplejo(void); //Contructor por defecto
        TComplejo(const double&);//Contructor sobrecargado
        TComplejo(const double&,const double&); //Constructor sobrecargado
        TComplejo(const TComplejo& c); // Constructor de copia
        TComplejo& operator= (const TComplejo&); // Operacion =
        ~TComplejo(); // Destructor

        //Operaciones Aritmeticas
        TComplejo operator+ (const TComplejo&) const; 
        TComplejo operator- (const TComplejo&) const; 
        TComplejo operator* (const TComplejo&) const; 
        TComplejo operator+ (const double&) const; 
        TComplejo operator- (const double&) const; 
        TComplejo operator* (const double&) const;
        
        //Operaciones logicas
        bool operator==(const TComplejo&) const;
        bool operator!= (const TComplejo&) const; // DESIGUALDAD de números complejos 
        double Re() const; // Devuelve PARTE REAL 
        double Im() const; // Devuelve PARTE IMAGINARIA 
        void Re(const double&); // Modifica PARTE REAL 
        void Im(const double&); // Modifica PARTE IMAGINARIA 
        double Arg(void); // Calcula el Argumento (en Radianes) 
        double Mod(void); // Calcula el Módulo
    private:
        double re; // Parte real
        double im; // Parte imaginaria
        void clone(const TComplejo &c);

    // Sobrecarga del operador SALIDA 
    friend ostream& operator<<(ostream &,const TComplejo &); 

    //Gunciones amigas Calculo Aritmetico
    friend TComplejo operator+ (const double& ,const TComplejo&); 
    friend TComplejo operator- (const double& ,const TComplejo&); 
    friend TComplejo operator* (const double& ,const TComplejo&); 
};


#endif