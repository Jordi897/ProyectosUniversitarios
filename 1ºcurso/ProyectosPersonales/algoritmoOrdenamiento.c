#include<stdio.h>
#include<stdlib.h>

#define KARRAY 10000

/*
 * ordenar
 * -------
 * Implementa la fase de partición del algoritmo QuickSort.
 *
 * Parámetros:
 *   - arr[] : array a ordenar
 *   - low   : índice inicial del segmento
 *   - high  : índice final del segmento (no incluido)
 *
 * Funcionamiento:
 *   - Usa arr[high-1] como pivote.
 *   - Recorre el segmento [low, high-2].
 *   - Si arr[i] < pivote, intercambia arr[i] con la posición siguiente al pivote virtual.
 *   - Al final coloca el pivote en su posición definitiva.
 *
 * Devuelve:
 *   - La posición final del pivote.
 */
int ordenar(int arr[],int low, int high){
    int pivote = low-1;
    int com = arr[high-1];

    for(int i=low;i<high-1;i++){
        if(arr[i] < com){
            int temp=arr[pivote+1];
            arr[pivote+1] = arr[i];
            arr[i] = temp;
            pivote++;
        }
    }

    int temp = arr[pivote+1];
    arr[pivote+1] = com;
    arr[high-1] = temp;

    return pivote+1;
}

/*
 * Quirk_Short
 * -----------
 * Implementación recursiva de QuickSort.
 *
 * Parámetros:
 *   - arr[] : array a ordenar
 *   - low   : índice inicial
 *   - high  : índice final (no incluido)
 *
 * Funcionamiento:
 *   - Si el segmento tiene más de un elemento:
 *       * Se particiona con ordenar()
 *       * Se ordena recursivamente la parte izquierda
 *       * Se ordena recursivamente la parte derecha
 */
void Quirk_Short(int arr[],int low, int high){
    if(low < high-1){
        int pi = ordenar(arr, low, high);

        Quirk_Short(arr,low,pi);
        Quirk_Short(arr,pi+1,high);
    }
}

/*
 * counting_short
 * --------------
 * Implementa Counting Sort para un dígito concreto (exp).
 *
 * Parámetros:
 *   - arr[] : array a ordenar
 *   - exp   : potencia de 10 que indica el dígito a ordenar
 *
 * Funcionamiento:
 *   - Cuenta cuántos elementos tienen cada dígito (0-9).
 *   - Construye el array ordenado según ese dígito.
 *   - Copia el resultado de vuelta a arr[].
 *
 * NOTA:
 *   - Está preparado para arrays de tamaño KARRAY.
 */
void counting_short(int arr[],int exp){
    int output[KARRAY];
    int count[10];

    for(int i=0;i<10;i++){
        count[i]=0;
    }

    for(int i=0;i<KARRAY;i++){
        int index = (arr[i]/exp)%10;
        count[index]++;
    }

    for(int i=1;i<10;i++){
        count[i] += count[i-1];
    }

    for(int i=KARRAY-1;i>=0;i--){
        int index = (arr[i]/exp)%10;
        output[count[index]-1] = arr[i];
        count[index]--;
    }

    for(int i=0;i<KARRAY;i++){
        arr[i] = output[i];
    }
}

/*
 * relox_short
 * -----------
 * Implementa Radix Sort usando Counting Sort por dígitos.
 *
 * Funcionamiento:
 *   - Encuentra el valor máximo del array.
 *   - Aplica counting_short para cada dígito (exp = 1, 10, 100, ...)
 *   - Continúa hasta que max/exp == 0.
 */
void relox_short(int arr[]){
    int max=-1;
    int exp = 1;
    for(int i=0;i<KARRAY;i++){
        if(max < arr[i]){
            max = arr[i];
        }
    }
    while(max/exp != 0){
        counting_short(arr,exp);
        exp = exp*10;
    }
}

/*
 * bubble_short
 * ------------
 * Implementación clásica de Bubble Sort.
 *
 * Funcionamiento:
 *   - Recorre el array repetidamente.
 *   - Intercambia elementos adyacentes si están desordenados.
 *
 * NOTA:
 *   - Es extremadamente lento para KARRAY = 10000.
 *   - Se usa aquí como demostración.
 */
void bubble_short(int array[]){
    for(int i=0;i<KARRAY;i++)
        for(int j=0;j<KARRAY-i;j++){
            if(array[j] > array[j+1]){
                int aux = array[j];
                array[j] = array[j+1];
                array[j+1] = aux;
            }
        }
}

/*
 * main
 * ----
 * Demostración de los algoritmos de ordenación.
 *
 * Pasos:
 *   1. Inicializa dos arrays con valores 0..9999.
 *   2. Mezcla aleatoriamente el primero.
 *   3. Aplica Bubble Sort al array mezclado.
 *   4. Muestra el resultado.
 *
 * NOTA:
 *   - QuickSort y Radix Sort están comentados, pero listos para usar.
 */
int main(){

    srand(939);

    int array[KARRAY];
    int array2[KARRAY];
    printf("[");
    for(int i=0;i<KARRAY;i++){
        array[i] = i;
        array2[i] = i;
    }
    for(int i=0;i<KARRAY;i++){
        int num = rand()%KARRAY;
        int aux = array[i];
        array[i] = array[num];
        array[num] = aux;
    }
    printf("]\n");

    //Quirk_Short(array,0,KARRAY);
    bubble_short(array);
    //relox_short(array2);

    printf("[");
    for(int i=0;i<KARRAY;i++){
        printf("%d ,",array[i]);
    }
    printf("]\n");
}
