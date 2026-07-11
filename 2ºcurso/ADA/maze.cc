#include <iostream>
#include <vector>
#include <map>
#include <fstream>

#define INF 0xFFFFFFFE

using namespace std;

enum ERR{
    ERROR_OPEN_FILE,
    ERROR_ARG_NEED,
    ERROR_OPTION_NO_EXIST,
    ERROR_NO_FILE
};


unsigned int maze_naive(const vector<vector<int>>&,const unsigned,const unsigned);
unsigned int maze_memo(const vector<vector<int>>&,const unsigned,const unsigned,map<unsigned int,unsigned int>&);
unsigned int maze_it_matrix(const vector<vector<int>>&,vector<vector<unsigned int>>&);
unsigned int maze_it_vector(const vector<vector<int>>&);
vector<unsigned int> maze_parser(const vector<vector<int>>&);


unsigned int minimo(unsigned,unsigned,unsigned);

bool lectorFichero(char*, vector<vector<int>>&);
void mostrarSolucion(const vector<vector<int>>&);
void mostrarTablaMemo(map<unsigned int, unsigned int>&,unsigned int, unsigned int);
void mostrarTablaIt(const vector<vector<unsigned int>>&);
void error(ERR,string="");

int main(int args, char *argv[]){
    int posFile=0;
    map<string,bool> arguments;
    arguments["-f"] = false; arguments["-t"] = false; arguments["--p2D"] = false; arguments["--ignore-naive"];

    for(int i=1;i<args;i++){
        if(!arguments.count(argv[i])){
            error(ERROR_OPTION_NO_EXIST,argv[i]);
            return 1;
        }
        arguments[argv[i]] = true;
        string ar(argv[i]);
        if(ar == "-f") posFile = ++i;
    }
    if(arguments["-f"] == false) {
        error(ERROR_ARG_NEED);
        return 1;
    }
    if(posFile >= args){
        error(ERROR_NO_FILE);
        return 1;
    }

    vector<vector<int>> laberinto;
    if(!lectorFichero(argv[posFile],laberinto)){
        return 1;
    }
    map<unsigned int, unsigned int> tablaMemo;
    vector<vector<unsigned int>> tablaIt;
    unsigned int naive = INF;
    if(!arguments["--ignore-naive"])
        naive = maze_naive(laberinto,0,0);
    unsigned int memo = maze_memo(laberinto,0,0,tablaMemo);
    unsigned int it_Matrix = maze_it_matrix(laberinto,tablaIt);
    unsigned int it_vector = maze_it_vector(laberinto);
    naive = naive != INF ? naive:0;
    memo = memo != INF ? memo:0;
    it_Matrix = it_Matrix != INF ? it_Matrix:0;
    it_vector = it_vector != INF ? it_vector:0;

    cout << (arguments["--ignore-naive"] ? "-":to_string(naive)) << " " << memo << " " << it_Matrix << " " << it_vector << endl;

    if(arguments["--p2D"]){
        mostrarSolucion(laberinto);
    }

    if(arguments["-t"]){
        cout << "Memoization table:" << endl;
        mostrarTablaMemo(tablaMemo,laberinto.size(),laberinto[0].size());
        cout << "Iterative table:" << endl;
        mostrarTablaIt(tablaIt);
    }


    return 0;
}

void error(ERR e,string addition){
    switch (e)
    {
        case ERROR_OPEN_FILE:
            cerr << "ERROR: can’t open file: "<< addition << endl;
            break;
        case ERROR_OPTION_NO_EXIST:
            cerr << "ERROR: unknown option "<< addition << endl;
            break;
        case ERROR_NO_FILE:
            cerr << "ERROR: missing filename." << endl;
            break;
        case ERROR_ARG_NEED:
            break;
    }

    cerr << "Usage:"<< endl
         << "maze [--p2D] [-t] [--ignore-naive] -f file" << endl;
}

void mostrarTablaMemo(map<unsigned int, unsigned int>& tabla,unsigned int Fila, unsigned int Colum){
    for(unsigned int i=0;i<Fila;i++){
        for(unsigned int j=0;j<Colum;j++){
            if(tabla.count(Colum*i+j)){
                cout << " " << (tabla[Colum*i+j] == INF ? "X":to_string(tabla[Colum*i+j]));
            } else{
                cout << " -";
            }
        }
        cout << endl;
    }
}

void mostrarTablaIt(const vector<vector<unsigned int>>&  tabla){
    for(unsigned int i = 0; i < tabla.size();i++){
        for(unsigned int j=0; j < tabla[0].size();j++){
            if(tabla[i][j] == INF){
                cout << " X";
            }else{
                cout << " " << tabla[i][j];
            }
        }
        cout << endl;
    }
}

void mostrarSolucion(const vector<vector<int>>& laberinto){
    vector<unsigned int> camino = maze_parser(laberinto);
    unsigned int cam = camino.size()-1;
    if(camino.empty()){
        cout << 0 << endl;
        return;
    }
    for(unsigned int i=0; i<laberinto.size();i++){
        for(unsigned int j=0;j<laberinto[0].size();j++){
            if(camino[cam] == (laberinto[0].size()*i+j)){
                cout << "*";
                cam--;
            }else{
                cout << laberinto[i][j];
            }
        }
        cout << endl;
    }
}

bool lectorFichero(char* fileName, vector<vector<int>>& laberinto){
    fstream read(fileName, ios::in);

    if(read.is_open()){
        unsigned int sizeN;
        unsigned int sizeM;
        laberinto.clear();

        read >> sizeN >> sizeM;
        for(unsigned int i=0;i<sizeN;i++){
            vector<int> aux;
            for(unsigned int j=0;j<sizeM;j++){
                int number;
                read >> number;
                aux.push_back(number);
            }
            laberinto.push_back(aux);
        }

        read.close();
        return true;
    } else error(ERROR_OPEN_FILE,fileName);

    return false;
}

unsigned int minimo(unsigned n1, unsigned n2, unsigned n3){
    if(n1 < n2 && n1 < n3) return n1;
    if(n2 < n3) return n2;
    return n3;
}

vector<unsigned int> maze_parser(const vector<vector<int>>& laberinto){
    vector<vector<unsigned int>> solucion;
    unsigned int longitud = maze_it_matrix(laberinto,solucion);

    if(longitud == INF) return vector<unsigned int>();
    vector<unsigned int> camino;
    int i=solucion.size()-1;
    int j=solucion[0].size()-1;
    while (longitud > 0)
    {
        if(longitud == solucion[i][j]){
            camino.push_back(solucion[0].size()*i+j);
            longitud--;
        }

        if(i-1 >= 0 && solucion[i-1][j] == longitud){
            i--;
        }
        if(j-1 >= 0 && solucion[i][j-1] == longitud){
            j--;
        }
        if(i-1 >= 0 && j-1 >= 0 && solucion[i-1][j-1] == longitud){
            i--; j--;
        }
    }

    return camino;
}

unsigned int maze_naive(const vector<vector<int>> &laberinto,const unsigned Fila,const unsigned Colum){
    if(laberinto[Fila][Colum] == 0) return INF;
    if(Fila+1 == laberinto.size() && Colum+1 == laberinto[0].size()) return 1;

    unsigned int derecha = ((laberinto[0].size() != Colum+1)) ? maze_naive(laberinto,Fila,Colum+1):INF;
    unsigned int abajo = ((laberinto.size() != Fila+1)) ? maze_naive(laberinto,Fila+1,Colum):INF;
    unsigned int derechaAbajo = (Fila+1 != laberinto.size() && Colum+1 != laberinto[0].size()) ? maze_naive(laberinto,Fila+1,Colum+1):INF;

    unsigned int min = minimo(derecha,abajo,derechaAbajo);

    if(min == INF) return min;
    return min+1;
}

unsigned int maze_memo(const vector<vector<int>>& laberinto,const unsigned Fila,const unsigned Colum,map<unsigned int,unsigned int>& memo){
    if(laberinto[Fila][Colum] == 0) {
        memo[laberinto[0].size()*Fila+Colum] = INF;
        return  memo[laberinto[0].size()*Fila+Colum];
    };
    if(Fila+1 == laberinto.size() && Colum+1 == laberinto[0].size()) {
        memo[laberinto[0].size()*Fila+Colum] = 1;
        return  memo[laberinto[0].size()*Fila+Colum];
    }
    if(memo.count(Fila*laberinto[0].size()+Colum)) return memo[Fila*laberinto[0].size()+Colum];

    unsigned int derechaAbajo = (Fila+1 != laberinto.size() && Colum+1 != laberinto[0].size()) ? maze_memo(laberinto,Fila+1,Colum+1,memo):INF;
    unsigned int derecha = ((laberinto[0].size() != Colum+1)) ? maze_memo(laberinto,Fila,Colum+1,memo):INF;
    unsigned int abajo = ((laberinto.size() != Fila+1)) ? maze_memo(laberinto,Fila+1,Colum,memo):INF;

    unsigned int min = minimo(derecha,abajo,derechaAbajo);

    if(min == INF) {
        memo[Fila*laberinto[0].size()+Colum] = min;
    } else memo[Fila*laberinto[0].size()+Colum] = min + 1;

    return memo[Fila*laberinto[0].size()+Colum];
}

unsigned int maze_it_matrix(const vector<vector<int>>& laberinto,vector<vector<unsigned int>> &tabla){
    tabla.clear();
    for(unsigned int i=0;i<laberinto.size();i++){
        vector<unsigned int> copy;
        for(unsigned int j=0;j<laberinto[0].size();j++)
            copy.push_back(laberinto[i][j]);
        tabla.push_back(copy);
    }

    for(unsigned int i=0;i<laberinto.size();i++){
        for(unsigned int j=0;j<laberinto[0].size();j++){
            unsigned int min=INF;
            if(laberinto[i][j] == 1){
                if(i>0) min = minimo(min,tabla[i-1][j] != 0 ? tabla[i-1][j]:INF,INF);
                if(j>0) min = minimo(min, tabla[i][j-1] != 0 ? tabla[i][j-1]:INF, INF);
                if(j > 0 && i > 0) min = minimo(min,tabla[i-1][j-1] != 0 ? tabla[i-1][j-1]:INF, INF);
                tabla[i][j] = min == INF ? min:min+1;
                if(i == 0 && j == 0) tabla[i][j] = 1;
            } else tabla[i][j] = INF;
        }
    }
    
        return tabla[laberinto.size()-1][laberinto[0].size()-1];
}

unsigned int maze_it_vector(const vector<vector<int>>& laberinto){
    vector<unsigned int> tabla;
    for(unsigned int i=0;i<laberinto[0].size();i++)
        tabla.push_back(INF);
    
    unsigned int diagonal = INF;
    for(unsigned int i=0;i<laberinto.size();i++) {
        for(unsigned int j=0;j<laberinto[0].size();j++){
            unsigned int min=INF;
            if(laberinto[i][j] == 1){
                if(i > 0 && laberinto[i-1][j] != 0){
                    min = minimo(min,tabla[j],INF);
                }
                if(j > 0 && laberinto[i][j-1] != 0){
                    min = minimo(min,tabla[j-1],INF);
                }
                if(j > 0 && i >0 && laberinto[i-1][j-1] != 0){
                    min = minimo(min,diagonal,INF);
                }
                
            }
            diagonal = tabla[j];
            tabla[j] = (min == INF ? min:min+1);
            if(laberinto[i][j] == 1 && j==0 && i == 0) tabla[0] = 1;
        }
    }
    
    return tabla[laberinto[0].size()-1];
}