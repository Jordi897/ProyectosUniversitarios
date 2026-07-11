#include <iostream>
#include <vector>
#include <map>
#include <fstream>

#define INF 0xFFFFFFFF

using namespace std;

enum ERR{
    ERROR_OPEN_FILE,
    ERROR_ARG_NEED,
    ERROR_OPTION_NO_EXIST,
    ERROR_NO_FILE
};

unsigned int maze_greedy(vector<vector<int>>&);

bool lectorFichero(char*, vector<vector<int>>&);
void mostrarSolucion(const vector<vector<int>>&);
void error(ERR,string="");

int main(int args, char *argv[]){
    int posFile=0;
    map<string,bool> arguments;
    arguments["-f"] = false; arguments["--p2D"] = false;

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
    
    unsigned int greedy = maze_greedy(laberinto);
    if(greedy == INF) greedy = 0;
    cout << greedy << endl;

    if(arguments["--p2D"]){
        mostrarSolucion(laberinto);
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

void mostrarSolucion(const vector<vector<int>>& laberinto){
    for(unsigned int i=0; i<laberinto.size();i++){
        for(unsigned int j=0;j<laberinto[0].size();j++){
            if(laberinto[i][j] == 2){
                cout << "*";
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

unsigned int maze_greedy(vector<vector<int>> &laberinto){
    unsigned int longitud = 0;
    unsigned int fila=0;
    unsigned int colum=0;
    while(longitud != INF){
        if(laberinto[fila][colum] == 1){
            laberinto[fila][colum] = 2;
            longitud++;
            if(fila+1 < laberinto.size() && colum+1< laberinto[0].size() && laberinto[fila+1][colum+1]==1){
                fila++; colum++;
                continue;
            }
            if(fila+1 < laberinto.size() && laberinto[fila+1][colum] == 1){
                fila++;
                continue;
            }
            if(colum+1 < laberinto[0].size() && laberinto[fila][colum+1] == 1){
                colum++;
                continue;
            }
            if((fila == laberinto.size()-1 && colum == laberinto[0].size()-1)){
                break;
            }
            longitud = INF;
        } else longitud = INF;
    }

    return longitud;
}