#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>

int CrearMemoriaCompartida(int num){
    int shmid;
    if((shmid = shmget(IPC_PRIVATE,sizeof(int)*num,IPC_CREAT | 0666)) == -1){
        printf("Error al reservar memoria");
    }
    return shmid;
}

int main(int args, char *argv[]){

    if(args < 3) perror("Error Faltan argumentos\n");
    int x = atoi(argv[1]);
    int y = atoi(argv[2]);
    int tubo[2];
    int i;

    int shmidX = CrearMemoriaCompartida(x+1);
    int shmidY = CrearMemoriaCompartida(y);
    
    for(i=0;i<x;i++)
        if(fork() > 0) break;

    int *pidX = (int *)shmat(shmidX,0,0);
    pidX[i] = getpid();
    shmdt(pidX);

    if(i==0){
        wait(NULL);
        int *pids = (int *)shmat(shmidY,0,0);
        printf("Soy el superPadre (%d) : mid hijos finales son: ",getpid());
        for(int j=0;j<y-1;j++)  
            printf("%dx%d, ",pids[j],j+1);
        printf("%dx%d\n",pids[y-1],y);
        shmdt(pids);
        if(shmctl(shmidY,IPC_RMID,0)==-1) perror("Error\n");
        if(shmctl(shmidX,IPC_RMID,0)==-1) perror("Error\n");
        exit(0);
    } else if(i==x){
        int j;
        for(j=0;j<y;j++)
            if(fork()==0) break;

        int *pidY = (int *)shmat(shmidY,0,0);
        pidY[j] = getpid();
        shmdt(pidY);

        if(!(y == j)){
            sleep(j);
            int *pids = (int *)shmat(shmidX,0,0);
            printf("Soy el subhijo %dx%d, mi padres son: ",getpid(),j+1);
            for(int k=0;k<x;k++) 
                printf("%d, ",pids[k]);
            printf("%d\n",pids[x]);
            shmdt(pids);
        }
    }
    
    while(wait(NULL) > 0);
    exit(0);
}