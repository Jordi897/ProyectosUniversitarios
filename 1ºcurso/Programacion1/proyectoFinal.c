#include<stdio.h>
#include<stdbool.h>
#include<stdlib.h>
#include<string.h>
#include<unistd.h>

#define MAXP 509
#define MAXT 100
//Creamos una estructura
typedef struct {
    int hora;
    int min;
    int dia;
    int mes;
    int ano;
} Ttime;
//Creamos la estructura del producto
typedef struct {
    char prod[5];
    char descripcion[30];
    int stock;
    int stockMin;
    float precio;
    float des;
} Tproducto;
//Creamos una estructura para cada linea de los tickets
typedef struct {
    char prod[5];
    char descripcion[30];
    int unidadesVendidas;
    float precioUnidad;
    float descuento;
}Tlinea;
//Creamos un tipo de dato que sea un array de Tlinea
typedef Tlinea Tlineas[10];
//Estructura del ticket
typedef struct {
    int cTicket;
    Ttime fecha;
    float importeT;
    Tlineas lTicket;
    int lineas;
} Tticket;

//Limpia la pantalla
void clear(){
    system("clear");
}
//Muestra el menu inicial
void menu(){
    clear();
    printf("*********************************************\n");
    printf("*             Comercial S.L.           *\n");
    printf("*               @alu.ua.es             *\n");
    printf("*********************************************\n");
    printf("---------------------------------------------\n");
    printf("1- Alta nuevo producto                       \n");
    printf("2- Baja de producto                          \n");
    printf("3- Modificacion de producto                  \n");
    printf("4- Busqueda de producto                      \n");
    printf("---------------------------------------------\n");
    printf("5- Crear ticket                              \n");
    printf("6- Buscar/Eliminar ticket                    \n");
    printf("7- Comprobar Stock                           \n");
    printf("----------------------------------------Extra\n");
    printf("9- Salir\n");
    printf("---------------------------------------------\n");
}
//Inicializa todos los productos con la id 0000
void inicializarP(Tproducto productos[]){
    //pone todos los codigos del producto a un caso base 0000
    for(int i;i<MAXP;i++){
        strcpy(productos[i].prod,"0000");
        productos[i].des = -1;
    }
}
//Inicializa todos los tikets a 0
void inicializarT(Tticket tickets[]){
    //pone todos los valores del codigo de ticket a un caso base que es 0
    for(int i=0; i<MAXT;i++){
        tickets[i].cTicket = 0;
    }
}
//Comprueba que la Id del producto es valida
bool Pcorrecto(char codigo[]){
    //Inicializamos el resultado como si la ID no es correcta en una primera instancia
    bool res = false;

    //Condicional que comprueba que la id es de la longitud permitida
    if(strlen(codigo)==4){
        //Una vez comprovado su longitud damos por hecho que es correcto en el momento que sea falso el bucle se detendra
        res = true;
        //Recorre cada caracter de la ID menos el \0
        for(int pos=0;pos<4 && res;pos++){
            switch (pos){
                //Caso especial en el que el dato introducido es un numero
                case 3:
                    res = ('0'<=codigo[pos] && codigo[pos]<='9');
                    break;
                //Caso particular en el que el dato introducido es un caracter contemplando tanto mayus como minus
                default:
                    res = ('a'<=codigo[pos] && codigo[pos] <='z') || ('A'<=codigo[pos] && codigo[pos]<='Z');
                    break;
            }
        }
    }

    //devolvemos el resultado
    return res;
}
//Mostramos el producto seleccionado
void mostrar(Tproducto producto){
    //muestra los datos del producto introducido
    printf("Codigo del producto: %s\n", producto.prod);
    printf("Descripcion del producto: %s\n", producto.descripcion);
    printf("Stock del producto: %d\n", producto.stock);
    printf("Stock minimo del producto: %d\n", producto.stockMin);
    printf("Precio del producto: %.2f\n", producto.precio);
    printf("Ojerta del producto: %.2f\n", producto.des*100);

    getchar();
}
//Pide los datos de los productos
void pedirDatosP(Tproducto productos[], int pos){
    //array auxiliar para despues comprobar que el codigo es correcto
    char id[100];
    //variable que dice si el codigo es correcto, se supone desde el inicio que no es asi
    bool fCorrecto = false;

    do{
        printf("Introduzca el codigo del producto: ");
        scanf("%s",id);
        //comprueba que el formato producto es correcto en la posicion elegida 
        fCorrecto = Pcorrecto(id);
        //Recorre todo el array de productos mirando que no se repita
        for(int i=0; i < MAXP && fCorrecto;i++){
            //Condicional por si al modificar producto la ID la quiere mantener
            if(pos != i){
                //Compara todos los casos y mientras no son iguales sera true
                fCorrecto = strcmp(productos[i].prod, id) ? true:false;
            }
        }
        if(!fCorrecto){
            //Salida de error
            printf("Error el codigo introducido no es correcto o ya existe\n");
        }
    }while(!fCorrecto);

    //Una vez todo esta correcto ponemos la id en la variableS
    strcpy(productos[pos].prod, id);

    //Descripcion
    printf("Introduce la descripcion maximo 30 caracteres: ");
    scanf(" %[^\n]s", productos[pos].descripcion);

    do{
        //Stock
        printf("Introduce cuanto stock del producto hay: ");
        scanf("%d", &productos[pos].stock);
        //El numero de Stock es imposible
        if(productos[pos].stock < 0){
                //Salida de error
                printf("Error el numero introducido no es posible\n");
        }
    } while (productos[pos].stock < 0);

    do{
        //StockMinimo
        printf("Introduce cuanto stock minimo del producto hay: ");
        scanf("%d", &productos[pos].stockMin);
        if(productos[pos].stockMin < 0){
            //Salida de error
            printf("Error el numero introducido no es posible\n");
        }
    } while (productos[pos].stockMin < 0);

    do{
        //precio
        printf("Introduce el precio del producto hay: ");
        scanf("%f", &productos[pos].precio);
        if(productos[pos].precio < 0){
            //Salida de error
            printf("Error el numero introducido no es posible\n");
        }
    } while (productos[pos].precio < 0);

    do{
        //Descuento
        printf("Introduce cuanto el descuento del producto hay: ");
        scanf("%f", &productos[pos].des);
        if(productos[pos].des < 0 || productos[pos].des > 1){
            //Salida de error
            printf("Error el numero introducido no es posible\n");
        }
    } while (productos[pos].des < 0 || productos[pos].des > 1);
        
}
//Crea un nuevo producto
void altaProducto(Tproducto productos[]){
    //Inicializamos la posicion en una imposible para que si no se encuentra  posicion enviar un error
    int pos =-1;
    //Busca en todas las posiciones si encuentra una id de producto vacia comparando con el estado inicial 0000
    for(int i=0; i<MAXP && pos == -1;i++){
        if(!(strcmp(productos[i].prod,"0000"))){
            pos = i;
        }
    }
    if(pos>-1){
        //pide y modifica las notas en la posicion seleccionada
        pedirDatosP(productos, pos);
    } else {
        printf("Error hay 50/50 espacios ocupado presiona espacio para continuar\n");
        getchar();
    }
}
//Busca un codigo entre los productos
int posicionProducto(Tproducto productos[], const char mensaje[]){
    //Variable para almacenar el codigo que introduce el usuario
    char codigo[5];
    //variable que almacena la posicion del producto
    int pos = -1;
    //Pide el codigo
    printf("%s", mensaje);
    scanf("%s", codigo);
    //Busca en todo el array que encuentra un codigo que coincida hasta que encuentra un codigo 0000
    for(int i = 0; i < MAXP && strcmp(productos[i].prod,"0000"); i++){
        pos = !(strcmp(productos[i].prod,codigo)) ? i:pos;
    }

    //caso de que el usuario introduce un 0 se devuelve un codigo de error distinto
    pos = !(strcmp(codigo, "0")) ? -2:pos; 
    //devuelve la posicion o el error
    return pos;
}
//Elimina un producto de la lista
void bajaProducto(Tproducto productos[]){
    //pides el codigo y buscas la posicion del codigo
    int pos = posicionProducto(productos, "Introduzca el codigo del producto a eliminar: ");
    //Variable que sirve para comprobar si esta seguro de eliminar el producto
    char confirmar;
    
    if(pos>-1){
        //Confirmacion de que quiere hacerlo
        printf("Desea eliminar el producto: %s? S/N\n", productos[pos].prod);
        scanf("%s", &confirmar);
        //Comprueba que afirma que si
        if(confirmar=='S' || confirmar == 's'){
            //Translada todos los productos a uno menos desde el procuto a eliminar
            for(int i = pos; i < MAXP-1 && strcmp(productos[i].prod,"0000");i++){
                productos[i] = productos[i+1];
            }
            //caso excepcional que es cuando el producto a elimnar es el que esta al final se modifica el codigo a 0000 para liberar la memoria del sitio
            if(pos == 49){
                strcpy(productos[pos].prod, "0000");
            }

            printf("Producto eliminado\n");
        }
    } else{
        printf("Error el producto no se a encontrado\n");
    }
    getchar();
}
//Modifica un producto
void modificarProducto(Tproducto productos[]){
    //pide el codigo a buscar y encuentra la posicion del codigo
    int pos = posicionProducto(productos, "Introduzca el producto a modificar: ");
    
    if(pos>-1){
        //pide los datos y los modifica en la posicion deseada
        pedirDatosP(productos, pos);
    } else{
        printf("Error no se a encontrado el producto");
        getchar();
    }
}
//Busca y muestra los datos de un producto
void buscarProducto(Tproducto productos[]){
    //pide un codigo y busca la posicion de este
    int pos = posicionProducto(productos, "Introduzco codigo de producto a buscar: ");

    if(pos>-1){
        //Muestra los datos del producto
        mostrar(productos[pos]);
    } else {
        printf("Error no se a encontrado el producto\n");
        getchar();
    }
}
//Imprime el ticket
void imprimirTicket(Tticket ticket){
    printf("               Comercial Bodi S.L.             \n");
    printf("-------------------------------------------------------\n");
    printf("Ticket: %3d                                     \n",ticket.cTicket);
    printf("Fecha: %d/%d/%d                                \n",ticket.fecha.dia, ticket.fecha.mes, ticket.fecha.ano);
    printf("Hora: %d:%d                                    \n", ticket.fecha.hora, ticket.fecha.min);
    printf("-------------------------------------------------------\n");
    printf("Productos       Unidades              Descuento          Precios\n");
    //bucle que recorre todas las lineas de ticket para poner su informacion
   for (int i = 0; i < ticket.lineas; i++){
     printf(" %s              %d                    %0.2f             %.2f  \n", ticket.lTicket[i].prod,ticket.lTicket[i].unidadesVendidas ,ticket.lTicket[i].descuento*100,ticket.lTicket[i].precioUnidad*ticket.lTicket[i].unidadesVendidas*(1-ticket.lTicket[i].descuento));
   }
   printf("------------------------------------------------\n");
   printf("Importe Total: %.2f\n", ticket.importeT);

   getchar();
   
}
//pide los datos encontrados dentros de la lista del ticket
void pedirDatosT(Tticket tickets[],Tproducto productos[], int pos){
    //posicion del producto que se introducira en el ticket
    int posP;
    //Cuantas lineas de productos tendra el ticket
    int linea=0;
    //Las unidades que se venderan del producto
    int unidades;
    //Variable que almacena seleccion del usuario
    char input;
    //Variable que sirve para saber si el usuario quiere seguir haciendo lineas de tickets o quiere parar
    bool seguir = true;
    //Variable auxiliar para pasar de un numero a entero a flotante
    float UnidadesVendidas;

    while(linea < 10 && seguir){
        //optiene la posicion del producto
        posP = posicionProducto(productos, "Dime la identificacion de producto: ");
        //comprueba que el producto existe
        if(posP>-1){
            //muestra el producto y si informacion
            mostrar(productos[posP]);
            //pide las unidades del producto que se van a vender
            printf("Cuantas unidades quiere vender(para cancelar la linea introduzca 0): ");
            scanf("%d", &unidades);
            //Comprueba que las unidades son positivos y diferente a 0 ademas de que no sean mayor al stock que hay del producto
            if(unidades <= productos[posP].stock && unidades != 0 && unidades > -1){
                //se almacena toda la informacion del producto
                strcpy(tickets[pos].lTicket[linea].prod,productos[posP].prod);
                strcpy(tickets[pos].lTicket[linea].descripcion, productos[posP].descripcion);
                tickets[pos].lTicket[linea].precioUnidad = productos[posP].precio;
                tickets[pos].lTicket[linea].descuento = productos[posP].des;
                tickets[pos].lTicket[linea].unidadesVendidas = unidades;
                if(linea != 9){
                    //pide si quiere otra linea si no quiere termina el blucle
                    printf("Desea añadir otra linea? S/N ");
                    scanf(" %c", &input);
                    if(!(input == 'S' || input == 's')){
                        seguir = false;
                    }
                }
                ++linea;
                //caso de que quiera cancelar el producto y poner otro
            } else if(unidades == 0){
                posP = -2;
            } else {
                printf("Error no hay suficiente stock del producto\n");
            }
        } else {
            if(!(posP == -2)){
                printf("Error no se a encontrado el producto vuelve a intentarlo\n");
            }
        }
    };
    //Se guarda cuantas lineas de condigo se han introducido
    tickets[pos].lineas = linea;
    //aseguramos que en el espacio de memoria inicie a 0
    tickets[pos].importeT = 0;

    //bucle que suma todos los precios con el descuento ya aplicado al importe total
    for (int i = 0; i < linea; i++){
        UnidadesVendidas = tickets[pos].lTicket[i].unidadesVendidas;
        tickets[pos].importeT += (tickets[pos].lTicket[i].precioUnidad*UnidadesVendidas)*(1-tickets[pos].lTicket[i].descuento);
    }
}
//Genera un ticket
void crearTicket(Tticket tickets[], Tproducto productos[], int *codigoT){
    //posicion de memoria donde estara el registro en el array
    int pos = -1;
    //Variable de confrimacion de querer crear el ticket
    char input;
    //Variable que recoje datos inservibles que no se usaran
    char basura;

    int posP;

    //bucle for que recorre el array de tickets en busca de posicion libre y que se para si lo encuentra
    for(int i=0; i < MAXT && pos == -1;i++){
        if(tickets[i].cTicket == 0){
            pos = i;
            tickets[i].cTicket = *codigoT;
        }
    }
    //Condicional que comprueba que se encontro el espacio en el array
    if(pos > -1){
        //Pide y registra los datos de los productos del ticket
        pedirDatosT(tickets, productos, pos);

        printf("Esta seguro que quiere hacer el ticket? S ");
        scanf(" %c", &input);

        //Comprueba que el usuario quiere generarlo, si no es asi pone la posicion del codigo ticket a 0
        if(input == 'S' || input == 's'){
            printf("Procesando...\n");
            //Pide los ultimos datos necesarios y elimnar el del stock las unidades vendidas
            printf("Fecha (dd/mm/aaaa): ");
            scanf("%d%c%d%c%d",&tickets[pos].fecha.dia,&basura,&tickets[pos].fecha.mes,&basura,&tickets[pos].fecha.ano);
            printf("Hora (hh:mm): ");
            scanf("%d%c%d", &tickets[pos].fecha.hora,&basura,&tickets[pos].fecha.min);
            //recorre todas las lineas de tickets
            for (int i = 0; i < tickets[pos].lineas; i++){
                posP=-1;
                //recorre el array para buscar el producto y modificar su stock
                for (int j = 0; j < MAXP && strcmp(productos[j].prod,"0000") && posP == -1; j++){
                    posP = !(strcmp(productos[j].prod, tickets[pos].lTicket[i].prod)) ? j:posP;
                }
                productos[posP].stock =productos[posP].stock - tickets[pos].lTicket[i].unidadesVendidas;
            }
            
            //Añade 1 a los numeros de codigo de tickets
            ++*codigoT;
            printf("ticket %d generado con exito\n", tickets[pos].cTicket);
            getchar();
        } else {
            tickets[pos].cTicket = 0;
        }
    } else {
        printf("Error no hay espacio para mas tickets");
    }

}
//Busca un ticket y lo muetra para despues dar la opcion de no hacer nada o eliminarlo
void buscarEliminarT(Tticket tickets[]){
    //Codigo a buscar del ticket
    int codigo;
    //la posicion en la que esta ese codigo por si hay que eliminarlo
    int pos = -1;
    //m:Menu, e:Eliminar. Variable para almacenar la decision de que hacer con el ticket del usuario
    char me;
    
    printf("Que ticket deseas Buscar/Eliminar: ");
    scanf("%d", &codigo);
    //Bucle que busca si el codigo se encuentra dentro del array
    for (int i = 0; i < MAXT && tickets[i].cTicket != 0 && pos ==-1; i++){
        pos = codigo==tickets[i].cTicket ? i:pos;
    }
    //Condicional que comprueba si existe en el array el ticket
    if(pos>-1){
        //Muestra el ticket
        imprimirTicket(tickets[pos]);
        do{
            //Pide que quiere hacer el usuario si volver al menu o eliminar el ticket y volver al menu
            printf("M - Volver al menu | E - Eliminar ticket: ");
            scanf(" %c",&me);
            if(me == 'E' || me == 'e'){
                //bucle for que mueve desde la posicion que se quiere eliminar hasta el maximo menos 1
                for (int i = pos; i < MAXT-1; i++){
                    tickets[i] = tickets[i+1];
                }
                //condicional que mira el caso que no contempla el for anterior para poner el codigo a 0 liberando asi la posicion
                if(pos == MAXT-1){
                    tickets[pos].cTicket = 0;
                }
                //comprueba si no ha sido ninguna de las opciones contempladas
            } else if(!(me == 'M' || me=='m')){
                printf("Esa condicion no esta contemplada\n");
            }
            //se repite el proceso hasta que introduzca una opcion valida
        } while (!(me == 'E' || me == 'e') && !(me == 'M' || me=='m'));
        
    } else {
        printf("Error no se ha encontrado el ticket\n");
        getchar();
    }
    
};
//Muestra un listado con el stock bajo o igual al minimo
void comprobarStock(Tproducto productos[]){
    printf("                                                   Comercial Bodi S.L.                                                       \n");
   printf("-------------------------------------------------------------------------------------------------------------------------------------\n");
    printf("%-20s |  %-30s |  %-10s |  %-10s |  %-10s |  %-10s |  %-10s\n","Productos","Descripcion","Stock","StockMin","Descuento","Precio","PrecioConDescuento");
    //Recorre el array de productos hasta que llega a los arrays sin codigo asignado
    for (int i=0; i < MAXP && strcmp(productos[i].prod,"0000"); i++){
        //Condicion que comprueba que el stock es menor o igual al minimo
        if(productos[i].stock <= productos[i].stockMin){
            //mostrar la linea de informacion del producto
            printf("--------------------------------------------------------------------------------------------------------------------------------------\n");
            printf("%-20s |  %-30s |  %-10d |  %-10d |  %-10.2f |  %-10.2f |  %-10.2f\n",
            productos[i].prod,
            productos[i].descripcion,
            productos[i].stock,
            productos[i].stockMin,
            productos[i].des*100,
            productos[i].precio,
            productos[i].precio*(1-productos[i].des));
        }
    }
   printf("-------------------------------------------------------------------------------------------------------------------------------------\n");
    getchar();
}
//Funcion que se ejecuta a inicializar
int main(){
    int input=0;
    int codigoT=1;

    //Creamos las listas de productos y tikets con los maximos que se pueden generar de estos
    Tproducto productos[MAXP];
    Tticket tickets[MAXT];

    inicializarP(productos);
    inicializarT(tickets);
    do{
        //Se ejecuta la opcion elegida
        menu();
        scanf("%d",&input);
        switch(input){
            case 1:
                altaProducto(productos);
                break;
            case 2:
                bajaProducto(productos);
                break;
            case 3:
                modificarProducto(productos);
                break;
            case 4:
                buscarProducto(productos);
                break;
            case 5:
                crearTicket(tickets,productos, &codigoT);
                break;
            case 6:
                buscarEliminarT(tickets);
                break;
            case 7:
                comprobarStock(productos);
                break;
            case 9:
                printf("Saliendo...");
                break;
            default:
                printf("Error no se contempla esa opcion\n");
                getchar();
                break;
        }
        getchar();
    }while (input != 9);
}
