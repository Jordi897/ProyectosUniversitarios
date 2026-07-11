#include <iostream>
#include <fstream> // Para usar ficheros
#include <vector> // Para usar vector
#include <cctype>
#include <cstring>
#include <sstream>

using namespace std;

const int KMAXNIF=10;
const int KMAXNAME=50;
const int KMAXTELEPHONE=14;

// Registro para los pacientes
struct Patient{
    string nif;
    string name;
    string telephone;
};

// Registro para los pacientes en fichero binario
struct PatientBin{
    char nif[KMAXNIF];
    char name[KMAXNAME];
    char telephone[KMAXTELEPHONE];
};

// Registro para las fechas
struct Date{
    int day;
    int month;
    int year;
};

// Registro para las analíticas
struct Analysis{
    unsigned int id;
    char nif[KMAXNIF];
    Date dateAnalysis ;
    float weight;
    float height;
};

// Registro para la base de datos
struct Database{
    unsigned int nextId;
    vector<Patient> patients;
    vector<Analysis> analysis;
};

// Tipos de errores posibles
enum Error {
    ERR_ARGS,
    ERR_FILE,
    ERR_OPTION,
    ERR_PATIENT_EXISTS,
    ERR_PATIENT_NOT_EXISTS,
    ERR_WRONG_DATE,
    ERR_WRONG_NAME,
    ERR_WRONG_NIF,
    ERR_WRONG_NUMBER,
    ERR_WRONG_TELEPHONE
};

/*
Función que muestra los distintos tipos de errores posibles
e: tipo de error a mostrar
return: void
*/
void error(Error e){
    switch (e){
        case ERR_ARGS: cout << "ERROR: wrong arguments" << endl;
            break;
        case ERR_FILE: cout << "ERROR: cannot open file" << endl;
            break;
        case ERR_OPTION: cout << "ERROR: wrong option" << endl;
            break;
        case ERR_PATIENT_EXISTS: cout << "ERROR: patient already exists" << endl;
            break;
        case ERR_PATIENT_NOT_EXISTS: cout << "ERROR: patient does not exist" << endl;
            break;
        case ERR_WRONG_DATE: cout << "ERROR: wrong date" << endl;
            break;
        case ERR_WRONG_NAME: cout << "ERROR: wrong name " << endl;
            break;
        case ERR_WRONG_NIF: cout << "ERROR: wrong NIF " << endl;
            break;
        case ERR_WRONG_NUMBER: cout << "ERROR: wrong number " << endl;
            break;
        case ERR_WRONG_TELEPHONE: cout << "ERROR: wrong telephone number" << endl;
    }
}

/*
Función que muestra el menú de opciones
return: void
*/
void showMenu() {
    cout << "1- Add patient" << endl
         << "2- View Patient" << endl
         << "3- Delete patient" << endl
         << "4- Save patients" << endl
         << "5- Add analysis" << endl
         << "6- Export analysis" << endl
         << "7- Import analysis" << endl
         << "8- Statistics" << endl
         << "q- Quit" << endl
         << "Option: ";
}

// Abre el fichero binario "patients.bin"
// Lee cada registro PatientBin y lo convierte a Patient
// Inserta cada paciente en el vector data.patients
// Solo se ejecuta al inicio del programa
void loadPatients(Database &data){
    ifstream lectura; // Creamos lector
    PatientBin pacienteBin; // Declaramos un tipo paciente Binario
    Patient paciente; // Declaramos un tipo paciente
    
    lectura.open("patients.bin",ios::in|ios::binary); // Abrimos archivo patients.bin

    if(lectura.is_open()){
        // Recogemos y guardamos la informacion de los pacientes en el archivo binario y lo transformamos en nuetra estructura de datos
        while(lectura.read((char *)&pacienteBin,sizeof(PatientBin))){
            paciente.name = pacienteBin.name;
            paciente.nif = pacienteBin.nif;
            paciente.telephone = pacienteBin.telephone;

            data.patients.push_back(paciente); // Guardamos el paciente en nuestro dataset
        }

        lectura.close();
    }
}

// Busca un paciente por NIF dentro del vector de pacientes
// Devuelve la posición si lo encuentra, o -1 si no existe
int searchPatient(const vector<Patient> patients,const string NIF){
    int res = -1;

    for(int i=0;i<patients.size() && res == -1;i++){
        if(patients[i].nif == NIF){
            res = i;
        }
    }

    return res;
}

// Comprueba si un NIF es válido:
// - Los primeros 8 caracteres deben ser dígitos
// - El último debe ser una letra
bool correctNIF(const string NIF){
    bool res = true;

    for(unsigned int i=0;i<NIF.length() && res;i++){
        switch (i) {
            case 8:
                res = isalpha(NIF[i]);
                break;
            default:
                res = isdigit(NIF[i]);
                break;
        }
    }

    return res;
}

// Añade un nuevo paciente a la base de datos
// Valida:
//   - NIF correcto y no repetido
//   - Nombre con al menos 3 caracteres
//   - Teléfono con formato +XXXXXXXXXXX
// Si todo es correcto, inserta el paciente en data.patients
void addPatient(Database &data){
    string Options;
    bool Error;
    int posPatient;
    Patient paciente;

    do{
        cout << "Enter NIF: ";
        getline(cin,Options);
        Error = false;
        if(Options.length()){ // Comprobamos si han escrito algo
            if(Options.length() != 9){ // Comprobamos si no tiene 9 caracteres, entonces emitimos error
                error(ERR_WRONG_NIF);
                Error = true;
            } else {
                Error = !correctNIF(Options);
                if(Error){ // Comprobamos si el formato del nif es correcto
                    error(ERR_WRONG_NIF);
                } else {
                    posPatient = searchPatient(data.patients, Options); // Buscamos si existe, en casod e existir emitimos mensaje de error
                    if(posPatient != -1){
                        error(ERR_PATIENT_EXISTS);
                        Error = true;
                    }
                }
            }
        }
    }while (Error); // Seguimos mientras hayan errores

    if(Options.length()){ // Comprobamos si el DNI fue valido o descarto el añadir nuevo paciente
        paciente.nif = Options;
        do{
            cout << "Enter name: ";
            getline(cin,Options);
            if(Options.length()<3){
                error(ERR_WRONG_NAME);
            }
        } while (Options.length() < 3);
        paciente.name = Options;
        
        do{
            cout << "Enter telephone: ";
            getline(cin,Options);
            Error = false;
            //Validamos si el nuero de telefono tiene un formato valido
            if(Options.length() > 13 || Options.length() < 11){
                Error = true;
            } else {
                for(unsigned int i=0;i<Options.length() && !Error;i++){
                    switch (i){
                    case 0:
                        Error = (Options[i] != '+');
                        break;
                    default:
                        Error = !isdigit(Options[i]);
                        break;
                    }
                }
            }
            if(Error){
                error(ERR_WRONG_TELEPHONE);
            }
        } while(Error);

        paciente.telephone = Options;

        data.patients.push_back(paciente);
        
    }
    
}

// Muestra por pantalla los datos de un paciente
void printPatient(Patient patient){
    cout << "NIF: " << patient.nif << endl
         << "Name: " << patient.name << endl
         << "Telephone: " << patient.telephone << endl; 
}

// Muestra todas las analíticas asociadas a un NIF
// Imprime cabecera solo una vez
void printAnalysis(const vector<Analysis> analisis, const string NIF){
    bool found = false;
    string nifAnalisi;

    for(unsigned i=0;i<analisis.size();i++){
        nifAnalisi = analisis[i].nif;
        if(nifAnalisi == NIF){
            if(!found){
                cout <<"Id\tDate\tHeight\tWeight";
                found = true;
            }
            cout << analisis[i].id << "\t"
                 << analisis[i].dateAnalysis.day << "/"
                 << analisis[i].dateAnalysis.month << "/"
                 << analisis[i].dateAnalysis.year << "\t"
                 << analisis[i].height << "\t"
                 << analisis[i].weight << endl;
        }
    }
}

// Pide un NIF al usuario
// Si existe, muestra el paciente y sus analíticas
// Si no existe, muestra error
void viewPatient(Database data){
    string NIF;
    int posPatient;
    bool correcto;
    do{
        correcto = true;
        cout << "Enter NIF: ";
        getline(cin,NIF);
    
        if(NIF.length()){
            if(NIF.length()==9 && correctNIF(NIF)){
                posPatient = searchPatient(data.patients, NIF);
                if(posPatient<0){
                    error(ERR_PATIENT_NOT_EXISTS);
                    correcto = false;
                } else {
                    printPatient(data.patients[posPatient]);
                    printAnalysis(data.analysis, NIF);
                }
            } else {
                correcto = false;
                error(ERR_WRONG_NIF);
            }
        }
    } while(!correcto);
}

// Elimina todas las analíticas asociadas a un NIF
// Se usa cuando se borra un paciente
void deleteAnalysis(Database &data, const string NIF){

    string nifAnalysis;

    for(unsigned int i=0;i < data.analysis.size();i++){
        nifAnalysis = data.analysis[i].nif;
        if(nifAnalysis == NIF){
            data.analysis.erase(data.analysis.begin()+i);
        }
    }
}

// Pide un NIF
// Si existe, elimina el paciente y sus analíticas asociadas
void deletePatient(Database &data){
    string NIF;
    int posPatient;
    bool correcto;
    do{
        correcto = true;
        cout << "Enter NIF: ";
        getline(cin,NIF);
    
        if(NIF.length()){
            if(NIF.length()==9 && correctNIF(NIF)){
                    posPatient = searchPatient(data.patients, NIF);
                if(posPatient<0){
                    error(ERR_PATIENT_NOT_EXISTS);
                    correcto = false;
                } else {
                    data.patients.erase(data.patients.begin()+posPatient);
                    deleteAnalysis(data,NIF);
                }
            } else {
                correcto = false;
                error(ERR_WRONG_NIF);
            }
        }
    } while(!correcto);
}

// Guarda todos los pacientes en el fichero binario "patients.bin"
// Convierte cada Patient a PatientBin antes de escribirlo
void savePatients(const Database data){
    ofstream escribir("patients.bin",ios::binary);
    PatientBin patienBin;

    if(escribir.is_open()){
        for(unsigned int i=0;i<data.patients.size();i++){

            strncpy(patienBin.name,data.patients[i].name.c_str(),KMAXNAME);
            patienBin.name[KMAXNAME-1] = '\0';
            strcpy(patienBin.nif,data.patients[i].nif.c_str());
            strcpy(patienBin.telephone,data.patients[i].telephone.c_str());
            
            escribir.write((const char *)&patienBin,sizeof(PatientBin));
        }
        escribir.close();
    }

}

// Añade una analítica a un paciente existente
// Valida:
//   - NIF existente
//   - Fecha válida
//   - Peso y altura positivos
// Asigna ID incremental y guarda la analítica en data.analysis
void addAnalysis(Database &data){
    string NIF;
    string fechaStr;
    Date fecha;
    char separator;
    float weight;
    float height;
    bool correcto;
    Analysis analysi;

    do{
        correcto = true;
        cout << "Enter NIF: ";
        getline(cin,NIF);
    
        if(NIF.length()){
            if(NIF.length()==9 && correctNIF(NIF)){
                if(searchPatient(data.patients,NIF) == -1){
                    error(ERR_PATIENT_NOT_EXISTS);
                    correcto = false;
                }
            } else {
                correcto = false;
                error(ERR_WRONG_NIF);
            }
        }
    } while(!correcto);

    if(NIF.length()){
        do{
            correcto = true;
            cout << "Enter date (day/month/year): ";
            cin >> fechaStr;
    
            stringstream ss(fechaStr);
    
            ss >> fecha.day >> separator >> fecha.month >> separator >> fecha.year;
            if(fecha.day < 1 || fecha.day > 31 || fecha.month < 1 || fecha.month > 12 || fecha.year<2025 || fecha.year>2050){
                correcto = false;
                error(ERR_WRONG_DATE);
            }
        } while (!correcto);
    
        do{
            cout << "Enter weight: ";
            cin >> weight;
            cin.ignore();
    
            if(weight < 0){
                error(ERR_WRONG_NUMBER);
            }
        } while (weight < 0);
    
        do{
            cout << "Enter height: ";
            cin >> height;
            cin.ignore();
    
            if(height < 0){
                error(ERR_WRONG_NUMBER);
            }
        } while (height < 0);

        analysi.dateAnalysis = fecha;
        strcpy(analysi.nif,NIF.c_str());
        analysi.height = height;
        analysi.weight = weight;
        analysi.id = data.nextId;
        data.nextId++;

        data.analysis.push_back(analysi);
    }
    
}

// Exporta todas las analíticas al fichero binario "analysis.bin"
void exportAnalysis(const vector<Analysis> analysis){
    ofstream escribirAnalysis("analysis.bin",ios::binary);

    if(escribirAnalysis.is_open()){
        for(unsigned int i=0;i<analysis.size();i++){
            escribirAnalysis.write((const char *)&analysis[i],sizeof(Analysis));
        }
        escribirAnalysis.close();
    }
}

// Importa analíticas desde "analysis.bin"
// Si el NIF no existe en pacientes:
//   - Lo escribe en "wrong_patients.txt"
// Si existe:
//   - Inserta la analítica y asigna nuevo ID
void importAnalysis(Database &data){
    ifstream leerAnalisis("analysis.bin",ios::binary);
    ofstream escribirWrong("wrong_patients.txt",ios::app);
    Analysis analysi;
    string NIF;

    if(!leerAnalisis.is_open() || !escribirWrong.is_open()){
        error(ERR_FILE);
    } else {
        while (leerAnalisis.read((char *)&analysi,sizeof(Analysis))){
            NIF = analysi.nif;
            if(searchPatient(data.patients,NIF) == -1){
                escribirWrong << analysi.nif << endl;
            } else {
                analysi.id = data.nextId;
                data.nextId++;

                data.analysis.push_back(analysi);
            }
        }
        leerAnalisis.close();
        escribirWrong.close();
    }
}

// Imprime una línea con:
//   NIF;fecha;peso;altura;categoría corporal
// También devuelve esa misma línea como string para guardarla en fichero
// Formatea la fecha con ceros si es necesario
string printStatistics(const Analysis analysis,const string CompCroporal){
    
    string res;
    res = analysis.nif;

    if(analysis.dateAnalysis.day < 10 && analysis.dateAnalysis.month <10){
        cout << analysis.nif << ";" 
             << "0" << analysis.dateAnalysis.day << "/"
             << "0" << analysis.dateAnalysis.month << "/"
             << analysis.dateAnalysis.year << ";"
             << analysis.weight << ";"
             << analysis.height << ";" 
             << CompCroporal << endl;

            res = res + ";0" + to_string(analysis.dateAnalysis.day)
                  + "/0" + to_string(analysis.dateAnalysis.month)
                  + "/" + to_string(analysis.dateAnalysis.year)
                  + ";" +  to_string(analysis.weight)
                  + ";" + to_string(analysis.height)
                  + ";" + CompCroporal + "\n";
    } else if (analysis.dateAnalysis.day < 10){
        cout << analysis.nif << ";" 
             << "0" << analysis.dateAnalysis.day << "/"
             << analysis.dateAnalysis.month << "/"
             << analysis.dateAnalysis.year << ";"
             << analysis.weight << ";"
             << analysis.height << ";" 
             << CompCroporal << endl;

             res = res + ";0" + to_string(analysis.dateAnalysis.day)
                  + "/" + to_string(analysis.dateAnalysis.month)
                  + "/" + to_string(analysis.dateAnalysis.year)
                  + ";" +  to_string(analysis.weight)
                  + ";" + to_string(analysis.height)
                  + ";" + CompCroporal + "\n";

    } else if (analysis.dateAnalysis.month <10){
        cout << analysis.nif << ";" 
             << analysis.dateAnalysis.day << "/"
             << "0" << analysis.dateAnalysis.month << "/"
             << analysis.dateAnalysis.year << ";"
             << analysis.weight << ";"
             << analysis.height << ";"
             << CompCroporal << endl;

             res = res + ";" + to_string(analysis.dateAnalysis.day)
                  + "/0" + to_string(analysis.dateAnalysis.month)
                  + "/" + to_string(analysis.dateAnalysis.year)
                  + ";" +  to_string(analysis.weight)
                  + ";" + to_string(analysis.height)
                  + ";" + CompCroporal + "\n";
    } else {
        cout << analysis.nif << ";" 
             << analysis.dateAnalysis.day << "/"
             << analysis.dateAnalysis.month << "/"
             << analysis.dateAnalysis.year << ";"
             << analysis.weight << ";"
             << analysis.height << ";"
             << CompCroporal << endl;
             res = res + ";" + to_string(analysis.dateAnalysis.day)
                  + "/" + to_string(analysis.dateAnalysis.month)
                  + "/" + to_string(analysis.dateAnalysis.year)
                  + ";" +  to_string(analysis.weight)
                  + ";" + to_string(analysis.height)
                  + ";" + CompCroporal + "\n";
    }

    return res;
}

// Calcula el índice de masa corporal (IMC) para cada analítica
// Clasifica en:
//   Underweight, Healthy, Overweight, Obesity
// Guarda todas las estadísticas en "statistics.txt"
void statistics(Database data){

    string NIF;
    float InMasaCorporal;
    string CompCorporal;
    string guardar;

    ofstream escribirStatics("statistics.txt",ios::out);

    for(unsigned int i=0;i<data.patients.size();i++){
        for(unsigned int j=0;j<data.analysis.size();j++){
            NIF = data.analysis[j].nif;
            if(NIF == data.patients[i].nif){
                InMasaCorporal = data.analysis[j].weight/((data.analysis[j].height/100)*(data.analysis[j].height/100));
                if(InMasaCorporal < 18.5){
                    CompCorporal = "Underweight";
                } else if(InMasaCorporal <= 24.9){
                    CompCorporal = "Healthy";
                } else if(InMasaCorporal <= 29.9){
                    CompCorporal = "Overweight";
                } else {
                    CompCorporal = "Obesity";
                }

                guardar = printStatistics(data.analysis[j],CompCorporal);

                if(escribirStatics.is_open()){
                    escribirStatics << guardar.c_str();
                }
            }
        }
    }

    if(escribirStatics.is_open()){
        escribirStatics.close();
    }
}

// Importa analíticas desde un fichero de texto con formato:
// NIF;dia/mes/año;peso;altura;categoria
// Lee carácter a carácter y reconstruye los campos
// Si ArgumentS es true, también genera estadísticas
void importAnalysisText(Database &data, const char nameImport[], bool ArgumentS){

    Analysis analisis;
    char c;
    string str;
    int fase=0;
    int recorrido=0;

    ifstream leerFichero(nameImport);

    if(!ArgumentS){
        if(!leerFichero.is_open()){
            error(ERR_FILE);
        } else {
            while (leerFichero >> c){
                if(fase == 0){
                    if(c != ';'){
                        analisis.nif[recorrido]=c;
                        recorrido++;
                    } else {
                        fase++;
                    }
                } else if(fase == 1){
                    if(c == '/'){
                        analisis.dateAnalysis.day = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase==2){
                    if(c == '/'){
                        analisis.dateAnalysis.month = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase == 3){
                    if(c == ';'){
                        analisis.dateAnalysis.year = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if (fase == 4){
                    if(c == ';'){
                        analisis.weight = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase == 4){
                    if(c == ';'){
                        analisis.height = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase == 5){
                    if(c = '\n'){
                        fase = 0;
                        analisis.id = data.nextId;
                        data.nextId++;
                        data.analysis.push_back(analisis);
                    }
                }
            }
        }
    } else {
        if(!leerFichero.is_open()){
            error(ERR_FILE);
        } else {
            while (leerFichero >> c){
                if(fase == 0){
                    if(c != ';'){
                        analisis.nif[recorrido]=c;
                        recorrido++;
                    } else {
                        fase++;
                    }
                } else if(fase == 1){
                    if(c == '/'){
                        analisis.dateAnalysis.day = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase==2){
                    if(c == '/'){
                        analisis.dateAnalysis.month = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase == 3){
                    if(c == ';'){
                        analisis.dateAnalysis.year = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if (fase == 4){
                    if(c == ';'){
                        analisis.weight = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase == 4){
                    if(c == ';'){
                        analisis.height = stoi(str);
                        str = "";
                        fase++;
                    } else {
                        str = str + c;
                    }
                } else if(fase == 5){
                    if(c = '\n'){
                        fase = 0;
                        analisis.id = data.nextId;
                        data.nextId++;
                        data.analysis.push_back(analisis);
                    }
                }
            }
            statistics(data);
        }
    }
}

// Inicializa la base de datos
// Carga pacientes desde fichero
// Procesa argumentos -f y -s para importar analíticas
// Si no hay salida directa, muestra el menú y ejecuta las opciones
int main(int argc, char *argv[]){
    Database data;
    data.nextId=1;
    char option;
    string arg;
    bool NosalidaDirecta = true;

    loadPatients(data);

    switch (argc){
        case 2:
            arg = argv[1];
            if(arg == "-f"){
                importAnalysisText(data, argv[2],false);
            } else {
                error(ERR_ARGS);
                NosalidaDirecta = false;
            }
            break;
        case 3:
            int posText = -1;
            for(int i=1;i<argc;i++){
                if(!strcmp(argv[i],"-f") || !strcmp(argv[i],"-s")){
                    posText = (!strcmp(argv[i],"-f") && posText==-1) ? i:posText;
                }
            }
            if((!strcmp(argv[1],"-f") && !strcmp(argv[3],"-s")) || (!strcmp(argv[1],"-s") && !strcmp(argv[2],"-f"))){
                importAnalysisText(data,argv[posText+1],true);
                NosalidaDirecta = false;
            } else {
                error(ERR_ARGS);
                NosalidaDirecta = false;
            }
        case 0:
            break;
        default:
            error(ERR_ARGS);
            NosalidaDirecta = false;
            break;
    }
    
    if(NosalidaDirecta){
        do{
            showMenu();
            cin >> option;
            cin.ignore();
        
            switch(option){
                case '1': addPatient(data);// Llamar a la función "addPatient" para añadir una nueva ficha de paciente
                    break;
                case '2': viewPatient(data); // Llamar a la función "viewPatient" para ver la información de un paciente
                    break;
                case '3': deletePatient(data); // Llamar a la función "deletePatient" para eliminar una ficha de paciente
                    break;
                case '4': savePatients(data); // Llamar a la función "savePatients" para guardar las fichas de pacientes en fichero binario
                    break;
                case '5': addAnalysis(data); // Llamar a la función "addAnalysis" para anadir una analítica
                    break;
                case '6': exportAnalysis(data.analysis); // Llamar a la función "exportAnalysis" para exportar las analiticas realizadas a fichero binario                
                    break;
                case '7': importAnalysis(data); // Llamar a la función "importAnalysis" para importar las analiticas en fichero binario
                    break;
                case '8': statistics(data); // Llamar a la función "statistics" para guardar las preguntas en fichero
                    break;
                case 'q': // Salir del programa 
                    break;
                default: error(ERR_OPTION);
            }
        }while(option!='q');
    }
   
    return 0;
}