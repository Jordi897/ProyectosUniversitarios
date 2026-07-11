#ifndef _TListaCom_
#define _TListaCom_

#include <iostream>
#include "tcomplejo.h"

class TListaNodo{

    public:
        TListaNodo(void);
        TListaNodo(const TListaNodo&);
        ~TListaNodo();
        TListaNodo& operator=(const TListaNodo&);

    private:
        TComplejo e;
        TListaNodo *anterior;
        TListaNodo *siguiente;
        void clone(const TListaNodo&);

    friend class TListaPos;
    friend class TListaCom;
};

class TListaPos{

    public:
        TListaPos(void);
        TListaPos(const TListaPos&);
        ~TListaPos();
        TListaPos& operator=(const TListaPos&);
        bool operator==(const TListaPos&) const;
        bool operator!=(const TListaPos&) const;
        TListaPos Anterior() const;
        TListaPos Siguiente() const;
        bool EsVacia() const;
    private:
        // Puntero a un nodo de la lista 
        TListaNodo *p;
        void clone(const TListaPos&);
    friend class TListaCom;
};

class TListaCom {

    public:
        //Constructor por defecto
        TListaCom (); 
        // Constructor de copia 
        TListaCom (const TListaCom &); 
        // Destructor 
        ~TListaCom(); 
        // Sobrecarga del operador asignación 
        TListaCom& operator=(const TListaCom &);
        // Sobrecarga del operador igualdad 
        bool operator==(const TListaCom &) const; 
        // Sobrecarga del operador desigualdad 
        bool operator!=(const TListaCom &) const; 
        // Sobrecarga del operador suma 
        TListaCom operator+(const TListaCom &) const; 
        // Sobrecarga del operador resta 
        TListaCom operator-(const TListaCom &) const; 
        // Devuelve true si la lista está vacía, false en caso contrario 
        bool EsVacia() const; 
        // Inserta el elemento en la cabeza de la lista 
        bool InsCabeza(const TComplejo &);  
        // Inserta el elemento a la izquierda de la posición indicada 
        bool InsertarI(const TComplejo &,const TListaPos &); 
        // Inserta el elemento a la derecha de la posición indicada 
        bool InsertarD(const TComplejo &,const TListaPos &); 
        // Busca y borra la primera ocurrencia del elemento 
        bool Borrar(const TComplejo &); 
        // Busca y borra todas las ocurrencias del elemento 
        bool BorrarTodos(const TComplejo &); 
        // Borra el elemento que ocupa la posición indicada 
        bool Borrar(TListaPos &); 
        // Obtiene el elemento que ocupa la posición indicada 
        TComplejo Obtener(const TListaPos &) const; 
        // Devuelve true si el elemento está en la lista, false en caso contrario 
        bool Buscar(const TComplejo &) const; 
        // Devuelve la longitud de la lista 
        int Longitud() const; 
        // Devuelve la primera posición en la lista 
        TListaPos Primera() const; 
        // Devuelve la última posición en la lista 
        TListaPos Ultima() const;  
    private:
        // Primer elemento de la lista 
        TListaNodo *primero; 
        // Ultimo elemento de la lista 
        TListaNodo *ultimo;
        void clone(const TListaCom&);
        TListaPos Busqueda(const TComplejo&) const;
    
    // Sobrecarga del operador salida 
    friend ostream& operator<<(ostream &,const TListaCom &); 
};


#endif