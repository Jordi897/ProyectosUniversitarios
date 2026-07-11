// Jordi Bodí Soriano

#include <iostream>
#include <cstdlib> // Para rand() y srand()
#include <cstring> // Para strcpy(), strcat() y strcmp()

using namespace std;

const int kTEAMNAME=40; // Máximo tamaño del nombre de un equipo
const int kPLAYERNAME=50; // Máximo tamaño del nombre de un jugador
const int kPLAYERS=5; // Número de jugadores por equipo
const int kMAXTEAMS=20; // Número máximo de equipos
const int kMINTEAMS=2; // Número mínimo de equipos

// Registro para los jugadores
struct Player{
    char name[kPLAYERNAME];
    unsigned int goals;
    bool best;
};

// Registro para los equipos
struct Team{
    unsigned int id;
    char name[kTEAMNAME];
    unsigned int wins;
    unsigned int losses;
    unsigned int draws;
    unsigned int points;
    Player players[kPLAYERS];
};

// Tipos de error posibles
enum Error{
    ERR_EMPTY,
    ERR_MAX_TEAMS,
    ERR_NO_LEAGUE,
    ERR_NO_TEAMS,
    ERR_NOT_EXIST,
    ERR_NUM_TEAMS,
    ERR_OPTION
};

/* Función que muestra los mensajes de error
e: tipo de error a mostrar
return: nada
*/
void error(Error e){
    switch(e){
        case ERR_EMPTY: cout << "ERROR: empty string" << endl;
            break;
        case ERR_MAX_TEAMS: cout << "ERROR: maximum number of teams reached" << endl;
            break;
        case ERR_NO_LEAGUE: cout << "ERROR: there are no standings" << endl;
            break;
        case ERR_NO_TEAMS: cout << "ERROR: there are no teams" << endl;
            break;
        case ERR_NOT_EXIST: cout << "ERROR: team does not exist" << endl;
            break;
        case ERR_NUM_TEAMS: cout << "ERROR: wrong number of teams" << endl;
            break;
        case ERR_OPTION: cout << "ERROR: wrong option" << endl;
    }
}

/* Función que simula los goles marcados por los jugadores
team: equipo para el que vamos a simular los goles marcados
return: número de goles totales del equipo
*/
int simulateGoals(Team &team){
    int goals=0;
    
    for(int i=0;i<kPLAYERS;i++){
        int goal=rand()%2; // Genera 0 o 1 (máximo un gol por robot en cada partido)
        team.players[i].goals+=goal;
        goals+=goal;
    }

    return goals;
}

/* Función que muestra el menú de opciones
return: nada
*/
void showMenu(){
    cout << "1- Add team" << endl
         << "2- Add all teams" << endl
         << "3- Delete team" << endl
         << "4- Show teams" << endl
         << "5- Play league" << endl
         << "6- Show standings" << endl
         << "7- Show best players" << endl
         << "q- Quit" << endl
         << "Option: ";
}

//Inicializa equipos a un estado base
void startComponentes(Team equipos[]){
    for(int i = 0;i < kMAXTEAMS;i++){
        equipos[i].id = 0;
    }
}

//Comprueba que el numero maximo de equipos no se a superado
bool maxEquipos(int numEquipos){

        return numEquipos >= 20;

}

//Crea los jugadores de un equipo
void crearJugadores(Team &equipo){

    char nJugador[kPLAYERNAME]; //Variable auxiliar para crear los nombre de jugadores

    for (int i = 0; i < kPLAYERS; i++){
        equipo.players[i].goals = 0;
        equipo.players[i].best = false;
        
        strcpy(nJugador,"");
        sprintf(nJugador,"-R%d",i+1);
        strcpy(equipo.players[i].name, equipo.name);
        strcat(equipo.players[i].name,nJugador);
    }
    
}

//Genera un equipo al que el usuario no introdujo nombre
void generarEquipos(Team &equipo){
    sprintf(equipo.name,"Team_%d",equipo.id);
    equipo.draws=0;
    equipo.losses=0;
    equipo.wins=0;
    equipo.points=0;
}

//Funcion para añadir un equipo
void addTeam(Team equipos[], unsigned int &contadorEquipos,int &numEquipos){

    char nEquipo[kTEAMNAME]; //Variable auxiliar donde almacenar el nombre del equipo introducido o generado

    if(!maxEquipos(numEquipos)){ //comprobacion de que no se exceda el numero maximo de equipos
        
        cout << "Enter team name: ";
        cin.clear();
        cin.getline(nEquipo, kTEAMNAME); // Introduccion de nombre

        if(strlen(nEquipo)==0){ // Comprobacion de si se introdujo algun nombre
            generarEquipos(equipos[numEquipos]); // Generar equipo si no se introdujo nada
        } else{ // inicializacion del equipo con el nombre introducido
            strcpy(equipos[numEquipos].name, nEquipo);
            equipos[numEquipos].draws=0;
            equipos[numEquipos].losses=0;
            equipos[numEquipos].wins=0;
            equipos[numEquipos].points=0;
        }

        crearJugadores(equipos[numEquipos]); // Genera los jugadores del equipo

        contadorEquipos++; // Aumentando la id de equipo para el proximo
        numEquipos++; // Indicacion que hay un equipo mas en la lista
        
    } else {
        error(ERR_MAX_TEAMS);
    }
}

//  Genera tantos equipos como el usuario quiera de forma automatica
void addallteams(Team equipos[],unsigned int &contadorEquipos, int &numEquipos){
    
    char opcion = 'y'; //Variable donde guadar los inputs que haga el usuario

    if(!(numEquipos==0)){ // Comprobando si existen equipos para avisar de su eliminacion
        do{
            cout << "Do you want to delete existing teams (y/n)?";
            cin >> opcion;
            cin.ignore();
        } while (opcion != 'y' && opcion != 'Y' && opcion != 'n' && opcion != 'N');
    }

    if(opcion == 'Y' || opcion == 'y'){
        startComponentes(equipos);
        do{
            cout << "Enter number of teams: ";
            cin >> numEquipos;

            if(numEquipos < 2 || numEquipos > 20){
                error(ERR_NUM_TEAMS);
            }
        } while (numEquipos < 2 || numEquipos > 20);

        for(int i=0;i<numEquipos;i++){
            equipos[i].id = contadorEquipos; // Añade la id al equipo
            generarEquipos(equipos[i]); // Genera el equipo
            crearJugadores(equipos[i]); // Genera los jugadores del equipo11
            contadorEquipos++;
        }
    }
}

// Elimina un equipo seleccionado
void deleteTeam(Team equipos[],int &numEquipos){

    char nomEquipo[kTEAMNAME]; // variable auxiliar de input del usuario

    if(numEquipos <= 0){ // comprobacion de si existen equipos para eliminar
        error(ERR_NO_TEAMS);
    } else {
        cout << "Enter team name: ";
        cin.clear();
        cin.getline(nomEquipo, kTEAMNAME);

        if(strlen(nomEquipo)==0){ // Comprobacion para ver si el usuario introdujo algo o nada
            error(ERR_EMPTY);
        } else {
            for(int i=0; i<numEquipos;i++){ // Bucle para recorrer los equipos existentes
                if(!strcmp(nomEquipo,equipos[i].name)){ // Comprobacion para ver si coincide el equipo a eliminar con el equipo en esa posicion
                    if(i=19){ // Eliminacion en caso de ser el equipo en el ultimo lugar del array eliminando
                        equipos[i].id = 0;
                    } else { // eliminacion corriendo todos los demas equipos
                        for(int j=i; j<kMAXTEAMS-1;j++){
                            equipos[j] = equipos[j+1];
                        }
                    }
                    numEquipos--;
                }
            }
        }
    }
}

// Funcion que hace el print de la informacion de un equipo 
void showInfo(Team equipo){
    cout << "Name: " << equipo.name << endl
         << "Wins: " << equipo.wins << endl
         << "Losses: " << equipo.losses << endl
         << "Draws: " << equipo.draws << endl
         << "Points: " << equipo.points << endl;
    for(int i = 0;i<kPLAYERS;i++){ // Bucle que recorre todos los jugadores
        cout << equipo.players[i].name <<": "<< equipo.players[i].goals << " goals" << endl;
    }
}

//Funcion para mostrar todos los o equipos o uno concreto
void showTeams(Team equipos[],int numEquipos) {
    char nomTeam[kTEAMNAME]; // donde se guardara el nombrer que ponga el usuario 
    int pos = -1; // posicion del equipo que quiere mostrar si este es -1 no se encontro y 20 es mostrar todo

    do{ // Bucle que se repite mientras se intente seleccionar un equipo que no existe 
        cout << "Enter name team: ";
        cin.clear();
        cin.getline(nomTeam, kTEAMNAME);

        if(strlen(nomTeam)==0){ // comprueba si ha introducido algun nombre
            pos = 20; // Indicacion para mostra todos los equipos
        } else{ 
            for(int i = 0; i < numEquipos && pos ==-1;i++){ // Bucle que pasa todos lo equipos hasta que se encuentre el deseado o ninguno
                if(!strcmp(nomTeam,equipos[i].name)){
                    pos = i;
                }
            }

            if(pos == -1){ // Condicional que comprueba si no se a encontrado equipo para mostrar
                error(ERR_NOT_EXIST);
            }
        }

    } while (pos == -1);

    if(pos == 20){ // cuando se quiere mostrar todos los equipos
        for (int i = 0; i < numEquipos; i++){
            showInfo(equipos[i]);
        }
    } else{ // se muestra un equipo en especifico
        showInfo(equipos[pos]);
    }
}


//Resetea los equipos que van a jugar la liga antes de jugarla
void resetLiga(Team equipos[],int numEquipos){
    for(int i=0; i < numEquipos;i++){ // Recorre los equipos introducidos para inicializar a cero
        equipos[i].draws=0;
        equipos[i].losses=0;
        equipos[i].wins=0;
        equipos[i].points=0;
        for(int j=0;j<kPLAYERS;j++){ // Recorre los jugadores de los equipos y pone sus goles a 0 y best a false
            equipos[i].players[j].best = false;
            equipos[i].players[j].goals = 0;
        }
    }
}

bool playerLeague(Team equipos[],int numEquipos){
    int golA; // Variable auxiliar que guarda los goles de un equipo
    int golB; // Variable auxiliar que guarda los goles del otro equipo
    int bestscore = 0; // variable auxiliar para guardar la posicion del mejor jugador

    resetLiga(equipos, numEquipos); // Resetea los equipos

    if(numEquipos < 2){ // comprueba que hay suficientes equipos
        error(ERR_NO_TEAMS); // Error de no equipos
    } else {
        for(int i =0;i < numEquipos-1;i++){ // bucle que recorre los equipos que juegan como equipo A
            for(int j=i+1;j < numEquipos;j++){ // bucle que recorre los que son equipo B
                golA = simulateGoals(equipos[i]); // Se guarda los goles que mete el equipo A
                golB = simulateGoals(equipos[j]); // Se guarda los goles que mete el equipo B
            
                if(golA > golB){ // Condicional que comprueba si ganó el equipo A
                    equipos[i].wins++;
                    equipos[i].points += 3;
                    equipos[j].losses++;
                } else if(golB > golA){ // Condicional que comprueba si ganó el equipo B
                    equipos[j].wins++;
                    equipos[j].points += 3;
                    equipos[i].losses++;
                } else{ // Empate entre los dos equipos
                    equipos[i].draws++;
                    equipos[j].draws++;
                    equipos[i].points++;
                    equipos[j].points++;
                    
                }
            }
        }
    }

    for(int i =0; i<numEquipos;i++){ // Recorre todos los equipos
        for(int j=0;j<kPLAYERS;j++){ // recorre todos los jugadores de cada equipo
            if(equipos[i].players[bestscore].goals < equipos[i].players[j].goals){ // Comprueba quien es el jugador con mas goles y guarda su posicion
                bestscore = j;
            }
        }

        equipos[i].players[bestscore].best = true; // pone en true al jugar con mas goles en un equipo
        bestscore = 0; // resetea al primero para la siguiente iteracion
    }

    return true; // devuelve la confirmacion de que se jugo una liga

}

// Hace el print de la informacion a enseñar en showStanding
void printShowStanding(Team equipo){
    cout << equipo.name
         << " | "
         << equipo.wins
         << " | "
         << equipo.losses
         << " | "
         << equipo.points << endl;
}

// Funcion que muestra como quedaron los equipos de la liga en orden de mejor a peor
void showStanding(Team equipo[],int numEquipos, bool playliga){
    
    bool printEquipo[20]; // Array auxiliar para saber que equipos se han mostrado y cuales no
    int posBest; // variable auxiliar que guarda la posicion que se mostrara
    
    if (!playliga){ // comprobacion de si se jugo la liga
        error(ERR_NO_LEAGUE); // Error no se a jugado liga
    } else{
        for(int i=0;i<numEquipos;i++){ // Inicializacion de los equipos que hay que mostrar
            printEquipo[numEquipos] = true;
        }

        for (int i = 0; i < numEquipos; i++){ //Bucle que recorre todos los equipos a mostrar
            posBest = 0; // Iniciamos dando por hecho que el mejor equipo sera el primero
            for(int j = 1; j< numEquipos;i++){ // Bucle que recorre los equipos para encontrar el mejor

                if(!printEquipo[posBest] && printEquipo[j]){ // Comprobacion si el equipo que queremos mostrar ya se mostro pasarla a uno que no
                    posBest = j;
                } else if (printEquipo[j] && equipo[j].points > equipo[posBest].points) { // Condicional que detecta si hay un equipo con mejor puntuacion
                        posBest = j;
                }
            }

            printEquipo[posBest] = false; // Elimina la opcion de mostrar el equipo en esa posicion para siguiente iteracion
            printShowStanding(equipo[posBest]); // Muestra el equipo seleccionado
        }
        
    }
    
}

// Muestra el mejor jugador de cada equipo
void showBestPlayer(Team equipos[],int numEquipos, bool playLeague){
   if(!playLeague){ // Comprueba que se haya jugado la liga
        error(ERR_NO_LEAGUE); // Error no se a jugado liga
    } else {
        for(int i=0;i<numEquipos;i++){ // Recorre todos los equipos
            for(int j=0;j<kPLAYERS;i++){ // Reccore todos los jugadores
                if(equipos[i].players[j].best){ // condicional para mostrar solo al jugador que es mejor
                    cout << equipos[i].name
                         << " | "
                         << equipos[i].players[j].name
                         << " | "
                         << equipos[i].players[j].goals << endl;
                }
            }
        }
    }
}

// Función principal. Tendrás que añadir más código tuyo
int main(){
    char option;
    bool playLiga = false; // Variable que indica si se a jugado la liga o no
    unsigned int contadorEquipos = 0; // Asigna la id del proximo equipo
    int numEquipos=0; // Variable para almacenar el numero de equipos que tiene mi programa
    Team equipos[kMAXTEAMS]; // Variable que guarda todos los equipos

    srand(888); // Fija la semilla del generador de números aleatorios. ¡NO TOCAR!
    
    do{
        showMenu();
        cin >> option;
        cin.ignore(); // Para evitar que el salto de línea se quede en el buffer de teclado y luego pueda dar problemas si usas "getline"
        
        switch(option){
            case '1': // Llamar a la función "addTeam" para añadir un nuevo equipo
                addTeam(equipos, contadorEquipos, numEquipos);
                break;
            case '2': // Llamar a la función "addAllTeams" para añadir todos los equipos de una vez
                addallteams(equipos,contadorEquipos, numEquipos);
                break;
            case '3': // Llamar a la función "deleteTeam" para borrar un equipo
                deleteTeam(equipos, numEquipos);
                break;
            case '4': // Llamar a la función "showTeams" para mostrar los datos de los equipos
                showTeams(equipos, numEquipos);
                break;
            case '5': // Llamar a la función "playLeague" para simular los resultados de la competición
                playLiga = playerLeague(equipos, numEquipos);
                break;
            case '6': // Llamar a la función "showStandings" para mostrar la clasificación final
                showStanding(equipos, numEquipos, playLiga);
                break;
            case '7': // Llamar a la función "showBestPlayers" para mostrar los mejores jugadores de cada equipo
                showBestPlayer(equipos, numEquipos,playLiga);
                break;
            case 'q': break;
            default: error(ERR_OPTION);
        }
    }while(option!='q');
}
