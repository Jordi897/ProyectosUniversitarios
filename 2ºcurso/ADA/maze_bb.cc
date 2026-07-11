#include <iostream>
#include <vector>
#include <map>
#include <fstream>
#include <math.h>
#include <time.h>
#include <unistd.h>
#include <queue>
#include <stack>

#define INF 0xFFFFFFFF

using namespace std;

enum ERR{
    ERROR_OPEN_FILE,
    ERROR_ARG_NEED,
    ERROR_OPTION_NO_EXIST,
    ERROR_NO_FILE
};

struct datosNodos{
    unsigned visitados;
    unsigned explorados;
    unsigned hojas;
    unsigned nofactibles;
    unsigned noPrometedores;
    unsigned antesPrometedores;
    unsigned actualizacionSoluion;
    unsigned actualizacionPesimista;
};


unsigned int maze_bb(vector<vector<int>>&,vector<vector<unsigned int>>&,datosNodos&);

bool lectorFichero(char*, vector<vector<int>>&);
void mostrarSolucion(const vector<vector<int>>&);
void generarSolucion(const vector<vector<unsigned int>>&, vector<int>&, vector<vector<int>>&);
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
    datosNodos dn {
        0, 0, 0, 0, 0, 0, 0 ,0
    };
    
    if(!lectorFichero(argv[posFile],laberinto)){
        return 1;
    }
    vector<vector<unsigned int>> solucion;
    vector<int> secuencia;
    auto start = clock();
    unsigned int bb = maze_bb(laberinto,solucion,dn);
    auto end = clock();
    double time = double(end-start)/(double)CLOCKS_PER_SEC;
    if(bb == INF) cout << 0 << endl;
    else cout << bb << endl;
    cout << dn.visitados << " " << dn.explorados << " " << dn.hojas << " "
        << dn.nofactibles << " " << dn.noPrometedores << " " << dn.antesPrometedores << " "
        << dn.actualizacionSoluion << " " << dn.actualizacionPesimista << endl;
    cout << (double)time*1000.0 << endl;
    if(bb != INF){
        if(arguments["--p2D"] || arguments["-p"]){
            generarSolucion(solucion,secuencia,laberinto);
        }
    }
    if(arguments["--p2D"]){
        mostrarSolucion(laberinto);
    }
    if(arguments["-p"]){
        if(bb == INF){
            cout << "<0>" << endl;
        } else {
            cout<<"<";
            for(unsigned int i=0;i<secuencia.size();i++){
                cout << secuencia[i];
            }
            cout << ">" <<endl;
        }
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

void generarSolucion(const vector<vector<unsigned int>>&sol, vector<int>&secuancia, vector<vector<int>>&laberinto){
    int x = (int)laberinto.size()-1; int y = (int)laberinto[0].size()-1;
    unsigned int longitud = sol[x][y];
    laberinto[x][y] = 2;
    secuancia.resize(longitud-1);
    while (x != 0 || y != 0)
    {
        for(int i =-1;i<2;i++){
            for(int j=-1;j<2;j++){
                if((i == 0 && j ==0) || x+i < 0 || y+j < 0 || x+i > (int)laberinto.size()-1 || y+j > (int)laberinto[0].size()-1) continue;
                else if(sol[x+i][y+j] == longitud-1){
                    x = x+i;
                    y = y+j;
                    longitud--;
                    laberinto[x][y] = 2;
                    if(i == -1 && j == 0) secuancia[longitud-1] = 5;
                    else if(i == -1 && j == 1) secuancia[longitud-1] = 6;
                    else if(i == 0 && j == 1) secuancia[longitud-1] = 7;
                    else if(i == 1 && j == 1) secuancia[longitud-1] = 8;
                    else if(i == 1 && j == 0) secuancia[longitud-1] = 1;
                    else if(i == 1 && j == -1) secuancia[longitud-1] = 2;
                    else if(i == 0 && j == -1) secuancia[longitud-1] = 3;
                    else secuancia[longitud-1] = 4;
                }
            }
        }
    }
    laberinto[0][0] = 2;
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

struct  Nodo
{
    int x; int y; 
   unsigned int longitud; 
   int sizeX; int sizeY;
};

struct cmp
{
    bool operator()(const Nodo& a,const Nodo& b){
        int disA = max(abs((a.sizeX-1)-a.x),abs((a.sizeY-1)-a.y));
        int disB = max(abs((b.sizeX-1)-b.x),abs((b.sizeY-1)-b.y));

        if((unsigned int)disA+a.longitud < (unsigned int)disB+b.longitud) return false;
        if((unsigned int)disA+a.longitud > (unsigned int)disB+b.longitud) return true;
        if(disA < disB) return false;
        if(disA > disB) return true;
        if(a.x < b.x && a.y < b.y) return true;
        if(a.x < b.x) return true;
        return b.y > a.y;
   } 
};

unsigned int maze_bb(vector<vector<int>> &laberinto, vector<vector<unsigned int>>& visitado,datosNodos &dn){

    visitado.clear();
    for(unsigned int i=0;i<laberinto.size();i++){
        vector<unsigned int> aux(laberinto[0].size(),INF);
        visitado.push_back(aux);
    }
    if(laberinto[0][0] == 0) return INF;
    visitado[0][0] = 1;
    priority_queue<Nodo, vector<Nodo>, cmp > pq;
    pq.emplace(Nodo{0, 0, 1, (int)laberinto.size(), (int)laberinto[0].size()});
    while (!pq.empty())
    {
        auto n = pq.top();
        pq.pop();
        if(n.x == n.sizeX-1 && n.y == n.sizeY-1){
            dn.actualizacionSoluion++;
            dn.hojas++;
            dn.antesPrometedores += pq.size();
            return n.longitud;
        }

        dn.explorados++;
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                if(i == 0 && j == 0) continue;
                dn.visitados++;
                if(n.x+i < 0 || n.y+j < 0 || n.x+i >= n.sizeX || n.y+j >= n.sizeY || laberinto[n.x+i][n.y+j] != 1) {
                    dn.nofactibles++;
                    continue;
                }

                if(visitado[n.x+i][n.y+j] <= n.longitud+1) {
                    dn.noPrometedores++;
                    continue;
                };
                visitado[n.x+i][n.y+j] = n.longitud+1;
                pq.emplace(Nodo{n.x+i, n.y+j, n.longitud+1, n.sizeX, n.sizeY});
            }
        }
    }
    return INF;
}

/************************************
*       Algoritmos Comparativos     *
*************************************/
/*
unsigned int maze_bb(vector<vector<int>> &laberinto, vector<vector<unsigned int>>& visitado,datosNodos &dn){
    visitado.clear();
    for(unsigned int i=0;i<laberinto.size();i++){
        vector<unsigned int> aux(laberinto[0].size(),INF);
        visitado.push_back(aux);
    }
    if(laberinto[0][0] == 0) return INF;
    visitado[0][0] = 1;
    queue<Nodo> pq;
    pq.emplace(Nodo{0, 0, 1, (int)laberinto.size(), (int)laberinto[0].size()});
    while (!pq.empty())
    {
        auto n = pq.front();
        pq.pop();
        if(n.x == n.sizeX-1 && n.y == n.sizeY-1){
            dn.actualizacionSoluion++;
            dn.hojas++;
            dn.antesPrometedores += pq.size();
            return n.longitud;
        }

        dn.explorados++;
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                if(i == 0 && j == 0) continue;
                dn.visitados++;
                if(n.x+i < 0 || n.y+j < 0 || n.x+i >= n.sizeX || n.y+j >= n.sizeY || laberinto[n.x+i][n.y+j] != 1) {
                    dn.nofactibles++;
                    continue;
                }

                if(visitado[n.x+i][n.y+j] <= n.longitud+1) {
                    dn.noPrometedores++;
                    continue;
                };
                visitado[n.x+i][n.y+j] = n.longitud+1;
                pq.emplace(Nodo{n.x+i, n.y+j, n.longitud+1, n.sizeX, n.sizeY});
            }
        }
    }
    return INF;
}
*/
/*
struct cmp
{
    bool operator()(const Nodo& a,const Nodo& b){
        unsigned int dA = max(abs(a.sizeX-a.x),abs(a.sizeY-a.y));
        unsigned int dB = max(abs(b.sizeX-b.x),abs(b.sizeY-b.y));

        return dB < dA;
   } 
};

unsigned int maze_bb(vector<vector<int>> &laberinto, vector<vector<unsigned int>>& visitado,datosNodos &dn){

    visitado.clear();
    for(unsigned int i=0;i<laberinto.size();i++){
        vector<unsigned int> aux(laberinto[0].size(),INF);
        visitado.push_back(aux);
    }
    unsigned int best_solution = INF;
    visitado[0][0] = 1;
    if(laberinto[0][0] == 0) return INF;
    priority_queue<Nodo, vector<Nodo>, cmp > pq;
    pq.emplace(Nodo{0, 0, 1, (int)laberinto.size(), (int)laberinto[0].size()});
    while (!pq.empty())
    {
        auto n = pq.top();
        pq.pop();
        
        if(n.x == n.sizeX-1 && n.y == n.sizeY-1){
            best_solution = min(best_solution, n.longitud);
            if(best_solution == n.longitud) dn.actualizacionSoluion++;
            dn.hojas++;
            continue;
        }

        int dis = max(abs((n.sizeX-1)-(n.x)),abs((n.sizeY-1)-(n.y)));
        if(!(n.longitud+(unsigned int)dis < best_solution)){
            dn.antesPrometedores++;
            continue;
        }
        dn.explorados++;
        for(int i=-1;i<2;i++){
            for(int j=-1;j<2;j++){
                if(i == 0 && j == 0) continue;
                dn.visitados++;
                if(n.x+i < 0 || n.y+j < 0 || n.x+i >= n.sizeX || n.y+j >= n.sizeY || laberinto[n.x+i][n.y+j] != 1) {
                    dn.nofactibles++;
                    continue;
                }

                if(visitado[n.x+i][n.y+j] <= n.longitud+1) {
                    dn.noPrometedores++;
                    continue;
                };
                visitado[n.x+i][n.y+j] = n.longitud+1;

                int dis = max(abs((n.sizeX-1)-(n.x+i)),abs((n.sizeY-1)-(n.y+j)));
                if(n.longitud+1+(unsigned int)dis < best_solution){
                    pq.emplace(Nodo{n.x+i, n.y+j, n.longitud+1, n.sizeX, n.sizeY});
                }else dn.noPrometedores++;
            }
        }
    }
    return best_solution;
}
*/