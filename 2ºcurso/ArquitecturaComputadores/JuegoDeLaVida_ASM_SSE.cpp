#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <stdint.h>
#include <time.h>

/*
* Normas del juego de la vida:
*	Celula viva: 1
*	Celula Muerta: 0
*	Normas de Supervivencia (La celula esta viva y se decide si deja de estar viva):
*		Permanece viva si hay 2 o 3 celulas a su alrededor.
*		Muerte por Soledad: Hay menos de 2 celulas a su alrededor.
*		Muerte por Sobrepoblacion: Hay mas de 3 celulas vivas a su alrededor.
*	Normas de Nacimiento (La celula muerta pasa a estar viva):
*		Cuando hay exactamente 3 celulas vivas a su alrededor.
*
*	Pautas a tener en cuenta: No modificar las celulas hasta no comprobar todo el tablero,
*	revivir celulas o matarlas mientras se recorre el la matriz puede afectar en un futuro a las otras celulas.
*	Ejemplo:
*	0 1 0
*	1 0 0
*	1 0 0
*	Si esta matriz la analizamos de izquierda derecha y arriba abajo la celula centro arriba va a morir,
*	pero la celula del centro al tener 3 celulas vivas al rededor debe nacer.
*	Si vamos modificando la matriz por comprobacion llegaremos al caso (0, 1) que al solo haber una celula vecina morira.
*	0 0 0
*	1 0 0
*	1 0 0
*	Ahora en este array ya no puede revivir la celula del centro.
*/

// ============================================================================
//   SSE / SSE2 — Instrucciones para manejo de enteros de 32 bits (packed int32)
//   (Extraídas de la referencia oficial x86)  
// ============================================================================
//
//  ARITMÉTICA (4 x int32)
// ----------------------------------------------------------------------------
//  PADDD      // Suma packed de 4 enteros de 32 bits
//  PSUBD      // Resta packed de 4 enteros de 32 bits
//
//  COMPARACIONES (4 x int32)
// ----------------------------------------------------------------------------
//  PCMPEQD    // Compara igualdad (==) entre 4 enteros de 32 bits
//  PCMPGTD    // Compara mayor que (>) entre 4 enteros de 32 bits (signed)
//
//  LÓGICAS BIT A BIT (128 bits, aplican a int32 empaquetados)
// ----------------------------------------------------------------------------
//  PAND       // AND bit a bit
//  PANDN      // AND NOT bit a bit
//  POR        // OR bit a bit
//  PXOR       // XOR bit a bit
//
//  DESPLAZAMIENTOS (cada int32 se desplaza individualmente)
// ----------------------------------------------------------------------------
//  PSLLD      // Shift lógico a la izquierda de 4 enteros de 32 bits
//  PSRLD      // Shift lógico a la derecha de 4 enteros de 32 bits
//  PSRAD      // Shift aritmético a la derecha de 4 enteros de 32 bits
//
//  REORGANIZACIÓN / SHUFFLE
// ----------------------------------------------------------------------------
//  PSHUFD     // Reordena los 4 enteros de 32 bits dentro del XMM
//  PUNPCKLDQ  // Unpack: combina las partes bajas (low dwords)
//  PUNPCKHDQ  // Unpack: combina las partes altas (high dwords)
//
//  MOVIMIENTOS (carga/almacenamiento de enteros empaquetados)
// ----------------------------------------------------------------------------
//  MOVDQA     // Mueve 128 bits alineados (packed integers)
//  MOVDQU     // Mueve 128 bits no alineados (packed integers)
//  MOVD       // Mueve un entero de 32 bits entre XMM y GPR
//
// ============================================================================
//   Nota: Estas son TODAS las instrucciones SSE/SSE2 relevantes para int32.
//   No se incluyen conversiones, instrucciones para bytes/words/qwords,
//   ni extensiones posteriores (SSSE3, SSE4.x, AVX, etc.).
// ============================================================================

int32_t* lecturaTablero(const char*, int*);
void guardarTablero(int32_t*, int, const char*);
void MostrarTablero(int32_t*, int);
void JuegoDeLaVida(int32_t*, int, int, int32_t*);
unsigned int pow2(unsigned int n) {
	unsigned int res = 1;
	for (unsigned int i = 0;i < n;i++) {
		res *= 2;
	}
	return res;
}
int32_t* CrearMatriz(int n);

int main(void) {
	srand(999);

	int32_t* matriz;
	int32_t* aux;
	//MostrarTablero(matriz, size);
	printf("Calculo del tiempo de ejecucion del Juego de la vida de Conway.\n");
	printf("Generaciones: 10000\t\tTamanyo: 300\n");
	for (int j = 14;j < 15;j++) {
		printf("Tiempo de ejecucion: ");
		int size = 300;
		for (int k = 0;k < 1;k++) {
			matriz = CrearMatriz(size);
			// inicial
			guardarTablero(matriz, size, "tablero_inicial.txt");
			aux = (int32_t*)malloc(size * size * sizeof(int32_t));
			if (matriz == NULL) return 1;
			auto start = clock();
			for (unsigned int i = 0;i < 10000;i++)
				JuegoDeLaVida(matriz, size * 4, size * size * 4, aux);
			guardarTablero(matriz, size, "tablero_final.txt");
			auto end = clock();
			printf("%f segundos", (double)(end - start) / CLOCKS_PER_SEC);


			free(matriz);
			free(aux);
			matriz = NULL;
			aux = NULL;
		}
	}
	//printf("\n\n\n");
	//MostrarTablero(matriz, size);

	return 0;
}

void JuegoDeLaVida(int32_t* m, int bytesize, int bytesizeMax, int32_t* aux) {
	__asm {
		pusha; Guradamos en la pila toda la informacion de registros que modificaremos

		mov ebx, m; Guardamos la matriz
		mov esi, aux; Guardamos la direccion de la matriz auxiliar para ir juardando los elementos

		mov edx, 0; inicializamos el indice fila

		Programa : cmp edx, bytesizeMax;  Comparamos si ya hemos terminado de recorrer la matriz
		je end_Programa;
		mov ecx, 0; inicializamos el indice columna
			interno_loop : cmp ecx, bytesize; Comparamos si se termino la fila de recorrer
			jae end_interno_loop;
		mov eax, edx;
		add eax, ecx; pasamos los indices para que lo entinda ensamblador
			movdqu xmm1, [ebx + eax]; Guardamos los elementos
			pxor xmm2, xmm2; Iniciamos el contador de celulas de los cuatro elementos
			mov eax, edx; Guardamos edx para hacer comparativas para saber si esta dentro del array los elementos a comparar
			sub eax, bytesize; Comparaos si estamos en la primera fila
			jns primera_fila;
		pxor xmm3, xmm3;
	end_primera_fila: mov eax, edx;
		add eax, bytesize;
		cmp eax, bytesizeMax; Comparamos ahora si hay fila por arriba
			jb ultima_fila;
		pxor xmm4, xmm4;
	end_ultima_fila: cmp ecx, 0; Comparamos si hay elementos a la izquierda
		je no_columna_izquierda;
	mov eax, ecx;
	sub eax, 4; Desplazamos eax para pillar los registros a la izquierda
		add eax, edx;
	movdqu xmm5, [ebx + eax];
end_no_columna_izquierda: mov eax, ecx;
	add eax, 16;
	cmp eax, bytesize; Comparamos con el elemento de mas a la derecha para ver si esta
		je no_columna_derecha;
	mov eax, ecx;
	add eax, 4;
	add eax, edx;
	movdqu xmm6, [ebx + eax];
end_no_columa_derecha: paddd xmm2, xmm3; Empezamos ha hacer el conteo para liberar registros
paddd xmm2, xmm4;
paddd xmm2, xmm5;
paddd xmm2, xmm6;
mov eax, edx; Comrpobamos si hay filas arriba
cmp eax, bytesize;
jae diagonales_arriba;
pxor xmm3, xmm3;
pxor xmm4, xmm4;
end_diagonales_arriba: mov eax, edx;
add eax, bytesize;
cmp eax, bytesizeMax; Comprobamos si hay filas abajo
jne diagonales_abajo;
pxor xmm5, xmm5;
pxor xmm6, xmm6;
end_diagonales_abajo:paddd xmm2, xmm3;
paddd xmm2, xmm4;
paddd xmm2, xmm5;
paddd xmm2, xmm6; Terminamos de contar la cantidad de unos que hay

pxor xmm7, xmm7;
mov eax, 2;
movd xmm7, eax;
pshufd xmm7, xmm7, 0; creamos un registro con sus celdas de 32 bits con 2
pcmpeqd xmm7, xmm2; Comparamos cuales celulas tienen dos celulas vivas a su alrededor

movdqa xmm3, xmm7;

mov eax, 3;
movd xmm7, eax;
pshufd xmm7, xmm7, 0; creamos un registro con sus celdas de 32 bits con 3
pcmpeqd xmm7, xmm2; Comparamos cuales celulas tienen 3 celulas vivas a su alrededor

movdqa xmm4, xmm7;

pand xmm3, xmm1; Comprobamos si la celula esta viva para saber si la mantenemos
pand xmm4, xmm1;
por xmm3, xmm4; Con que se cumpla una de las dos la celula seguira viva

pxor xmm4, xmm4;
pcmpeqd xmm4, xmm1; Compruebo cuales celdas son muertas
pcmpeqd xmm5, xmm5; Creo un registro con todos los bits a 1
pxor xmm4, xmm5; invierto el resultado para que todos los bits esten a uno cuando la celula este viva y todos los bits a 0 cuando este muerta

movd xmm7, eax;
pshufd xmm7, xmm7, 0; creamos un registro con sus celdas de 32 bits con 3
pcmpeqd xmm7, xmm2; Comparamos cuales celulas tienen 3 celulas vivas a su alrededor

pandn xmm4, xmm7; And que dejara todo a 1 las celdas muertas con 3 celulas vivas a su alrededor

mov eax, 1;
movd xmm7, eax;
pshufd xmm7, xmm7, 0; creamos un registro con las cuatro celdas a 1

pand xmm4, xmm7; aplicamos and ya que las celulas vivas que hemos obtenido estan sus celdas todo a 1 asi que aplicamos una mascara de 1 para que solo el bit de menor peso se mantenga

por xmm4, xmm3; como soy contrarios los dos casos pero con que se cumpla uno u otro la clula tendra que estar viva aplicamos or
mov eax, edx;
add eax, ecx;
movdqu[esi + eax], xmm4; guardamos el resultado en la matriz auxiliar

add ecx, 16;
jmp interno_loop;
end_interno_loop: add edx, ecx;
jmp Programa;
end_Programa: mov ecx, 0; Recorremos la matriz para poner los valores correctos
mov edx, aux; guardamos el valor del array auxiliar donde esta el nuevo tablero
matrizNueva_loop : cmp ecx, bytesizeMax;
je end_matrizNueva_loop;
movdqu xmm1, [edx + ecx]; Pillamos cuatro elementos del auxiliar para pasarlo a la matriz
movdqu[ebx + ecx], xmm1; Guardamos los elementos en el array correcto
add ecx, 16;
jmp matrizNueva_loop;
end_matrizNueva_loop: jmp fin;

primera_fila: add eax, ecx;
movdqu xmm3, [ebx + eax];
jmp end_primera_fila;
ultima_fila: add eax, ecx;
movdqu xmm4, [ebx + eax];
jmp end_ultima_fila;
no_columna_izquierda: mov eax, edx
add eax, ecx;
movdqu xmm5, [ebx + eax];
pslldq xmm5, 4; desplazo todos a la derecha;
jmp end_no_columna_izquierda;
no_columna_derecha: mov eax, edx;
add eax, ecx;
movdqu xmm6, [ebx + eax];
psrldq xmm6, 4;
jmp end_no_columa_derecha;
diagonales_arriba: sub eax, bytesize;
mov edi, ecx
cmp edi, 0;
je diagonal_desplazada_izquierda_arriba;
sub edi, 4;
add edi, eax;
movdqu xmm3, [ebx + edi];
jmp diagonal_derecha_arriba;
diagonal_desplazada_izquierda_arriba: add edi, eax;
movdqu xmm3, [ebx + edi];
pslldq xmm3, 4;
diagonal_derecha_arriba: mov edi, ecx;
add edi, 16;
cmp edi, bytesize;
je diagonal_desplazada_derecha_arriba;
sub edi, 16;
add edi, 4
add edi, eax;
movdqu xmm4, [ebx + edi];
jmp end_diagonales_arriba;
diagonal_desplazada_derecha_arriba: sub edi, 16;
add edi, eax;
movdqu xmm4, [ebx + edi];
psrldq xmm4, 4;
jmp end_diagonales_arriba;

diagonales_abajo: mov edi, ecx;
cmp edi, 0;
je diagonales_abajo_desplazamiento_izquierda;
sub edi, 4;
add edi, eax;
movdqu xmm5, [ebx + edi];
jmp diagonales_abajo_derecha;
diagonales_abajo_desplazamiento_izquierda: add edi, eax;
movdqu xmm5, [ebx + edi];
pslldq xmm5, 4;
diagonales_abajo_derecha: mov edi, ecx;
add edi, 16;
cmp edi, bytesize;
je diagonales_abajo_desplazamiento_derecha;
sub edi, 16;
add edi, 4;
add edi, eax;
movdqu xmm6, [ebx + edi];
jmp end_diagonales_abajo;
diagonales_abajo_desplazamiento_derecha: sub edi, 16;
add edi, eax;
movdqu xmm6, [ebx + edi];
psrldq xmm6, 4;
jmp end_diagonales_abajo;

fin: popa; Recuperamos los registros
	}
}

void MostrarTablero(int32_t* v, int n) {
	for (int i = 0;i < n;i++) {
		printf("|");
		for (int j = 0;j < n;j++) {
			printf(" %d |", v[i * n + j]);
		}
		printf("\n");
	}
}

int32_t* CrearMatriz(int n) {
	int size = n;
	int32_t* matriz = (int32_t*)malloc(size * size * sizeof(int32_t));
	if (matriz == NULL) return NULL;
	for (int i = 0;i < size;i++) {
		for (int j = 0;j < size;j++) {
			matriz[i * size + j] = rand() % 2;
		}
	}
	return matriz;
}

int32_t* lecturaTablero(const char* nombre, int* n) {
	FILE* f = NULL;
	if (fopen_s(&f, nombre, "rb") != 0) {
		f = NULL;
		*n = (500) * 4;
		int size = *n;
		int32_t* matriz = (int32_t*)malloc(size * size * sizeof(int32_t));
		if (matriz == NULL) return NULL;
		for (int i = 0;i < size;i++) {
			for (int j = 0;j < size;j++) {
				matriz[i * size + j] = rand() % 2;
			}
		}
		return matriz;
	}

	fread(n, sizeof(int), 1, f);
	int32_t* matriz = (int32_t*)malloc((*n) * (*n) * sizeof(int32_t));
	if (matriz == NULL) return NULL;
	fread(matriz, sizeof(int32_t), (*n) * (*n), f);

	fclose(f);
	return matriz;
}

void guardarTablero(int32_t* v, int n, const char* nombre) {
	FILE* f = NULL;
	fopen_s(&f, nombre, "w");
	if (f == NULL) {
		printf("Error al crear el archivo\n");
		return;
	}

	fprintf(f, "%d\n", n);

	for (int i = 0; i < n * n; i++) {
		fprintf(f, "%d ", v[i]);
		if ((i + 1) % n == 0)
			fprintf(f, "\n");
	}

	fclose(f);
}