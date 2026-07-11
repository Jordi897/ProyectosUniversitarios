#include <iostream>
#include <vector>
#include <map>
#include <fstream>
#include <math.h>
#include <time.h>
#include <unistd.h>

#define INF 0xFFFFFFFF

using namespace std;

enum ERR{
    ERROR_OPEN_FILE,
    ERROR_ARG_NEED,
    ERROR_OPTION_NO_EXIST,
    ERROR_NO_FILE
};

string camino(vector<vector<int>>);

unsigned int backtraking(vector<vector<int>>&, int, int,unsigned int,unsigned int,vector<unsigned int>&,vector<vector<unsigned int>>&,vector<vector<int>>&);
unsigned int maze_bt(vector<vector<int>>&,vector<unsigned int>&);

bool lectorFichero(char*, vector<vector<int>>&);
void mostrarSolucion(const vector<vector<int>>&);
void error(ERR,string="");

int main(int args, char *argv[]){
    int posFile=0;
    map<string,bool> arguments;
    arguments["-f"] = false; arguments["--p2D"] = false; arguments["-p"] = false;

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
    vector<unsigned int> stats;
    if(!lectorFichero(argv[posFile],laberinto)){
        return 1;
    }
    auto start = clock();
    unsigned int bt = maze_bt(laberinto,stats);
    auto end = clock();
    double time = double(end-start)/(double)CLOCKS_PER_SEC;
    if(bt == INF) cout << 0 << endl;
    else cout << bt << endl;
    for(unsigned int i=0;i<stats.size();i++){
        cout << stats[i] << " ";
    }
    cout << endl << (double)time*1000.0 << endl;

    if(arguments["--p2D"]){
        mostrarSolucion(laberinto);
    }

    if(arguments["-p"]){
        if(bt != INF){
            cout << "<" << camino(laberinto) << ">" << endl;
        } else cout << "<0>" << endl;
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
            if(laberinto[i][j] != 1 && laberinto[i][j] != 0){
                cout << "*";
            }else{
                cout << laberinto[i][j];
            }
        }
        cout << endl;
    }
}

string camino(vector<vector<int>> solu){
    string cam = "";
    int i=0;
    int j=0;

    while (!(i == (int)(solu.size()-1) && j == (int)(solu[0].size()-1))){
        bool found = false;
        for(int ni=-1;ni<=1 && !found;ni++){
            for(int nj=-1;nj<=1 && !found;nj++){
                if((ni == 0 && nj == 0) || ni+i < 0 || nj+j < 0 || ni+i >= (int)solu.size() || nj+j >= (int)solu[0].size()) continue;
                if(solu[i+ni][j+nj] != 1 && solu[i+ni][j+nj] != 0){
                    found = true;
                    solu[i][j] = 1;
                    if(ni == -1 && nj == 0) cam = cam + "1";
                    else if(ni == -1 && nj == 1) cam = cam + "2";
                    else if(ni == 0 && nj == 1) cam = cam + "3";
                    else if(ni == 1 && nj == 1) cam = cam + "4";
                    else if(ni == 1 && nj == 0) cam = cam + "5";
                    else if(ni == 1 && nj == -1) cam = cam + "6";
                    else if(ni == 0 && nj == -1) cam = cam + "7";
                    else if(ni == -1 && nj == -1) cam = cam + "8";
                    i = i+ni;
                    j = j+nj;
                }
            }
        }
    }
    return cam;
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

unsigned int backtraking(vector<vector<int>> &laberinto, int ni, int nj,unsigned int caminoMasCorto,unsigned int longitud,vector<unsigned int>&stats,vector<vector<unsigned int>>& memo,vector<vector<int>>&solucion){
    if(ni < 0 || nj < 0 || ni >= (int)laberinto.size() || nj >= (int)laberinto[0].size() || laberinto[ni][nj] != 1){
        stats[2]++;
        stats[3]++;
        return INF;
    }
    unsigned int distancia = max((int)abs((int)laberinto.size()-1-ni),(int)abs((int)laberinto[0].size()-nj-1));
    if(longitud+distancia >= caminoMasCorto || memo[ni][nj] <= longitud){
        stats[4]++;
        return INF;
    }
    stats[1]++;
    memo[ni][nj] = longitud;
    if(ni == (int)laberinto.size()-1 && nj == (int)laberinto[0].size()-1){
        stats[2]++;
        solucion = laberinto;
        return longitud;
    }
    laberinto[ni][nj] = longitud;
    for(int i=-1;i<=1;i++){
        for(int j=-1;j<=1;j++){
            if(i == 0 && j == 0) continue;
            stats[0]++;
            caminoMasCorto = min(caminoMasCorto, backtraking(laberinto,ni-i,nj-j,caminoMasCorto,longitud+1,stats,memo,solucion));
        }
    }
    laberinto[ni][nj] = 1;

    return caminoMasCorto;

}

unsigned int maze_bt(vector<vector<int>> &laberinto,vector<unsigned int>&stats){

    stats.clear();
    for(int i=0;i<5;i++)
        stats.push_back(0);
    if(laberinto[0][0] == 0){
        stats[3]++;
        return INF;
    }
    vector<vector<unsigned int>> memo;
    for(unsigned int i=0;i<laberinto.size();i++){
        vector<unsigned int> aux;
        for(unsigned int j=0;j<laberinto[0].size();j++){
            aux.push_back(INF);
        }
        memo.push_back(aux);
    }

    unsigned int minimo = INF;
    vector<vector<int>> sol;
    minimo = backtraking(laberinto,0,0,minimo,2,stats,memo,sol);
    if(minimo == INF) return minimo;
    sol[laberinto.size()-1][laberinto[0].size()-1] = 2;
    laberinto = sol;
    if(minimo == INF) return INF;
    return minimo-1;
}