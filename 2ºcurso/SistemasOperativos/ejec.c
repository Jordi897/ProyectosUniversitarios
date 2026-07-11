#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>
#include <string.h>
#include <signal.h>
#include <sys/types.h>

void nada(){}

void arbolB(){
    printf("Soy el proceso B con pid %d, he recibido la señal\n",getpid());
    if(fork() == 0){
        execlp("pstree","pstree",NULL);
    } else{
        wait(NULL);
    }
}

void arbolA(){
    printf("Soy el proceso A con pid %d, he recibido la señal\n",getpid());
    if(fork() == 0){
        execlp("pstree","pstree",NULL);
    } else {
        wait(NULL);
    }
}

void lsX(){
    printf("Soy el proceso X con pid %d, he recibido la señal\n",getpid());
    if(fork() == 0){
        execlp("ls", "ls", NULL);
    } else{
        wait(NULL);
    }
}

void lsY(){
    printf("Soy el proceso Y con pid %d, he recibido la señal\n",getpid());
    if(fork() == 0){
        execlp("ls", "ls", NULL);
    } else{
        wait(NULL);
    }
}

void ejecutarAccion(const char c,const pid_t XY[3],const pid_t A,const pid_t B){
    
    switch (c)
    {
    case 'A':
        kill(A,SIGUSR1);
        kill(XY[1],SIGUSR2);
        kill(XY[0],SIGUSR2);
        break;
    
    case 'B':
        kill(B,SIGUSR1);
        kill(XY[1],SIGUSR2);
        kill(XY[0],SIGUSR2);
        break;
    case 'X':
        kill(XY[0],SIGUSR1);
        kill(XY[1],SIGUSR2);
        break;
    case 'Y':
        kill(XY[1],SIGUSR1);
        kill(XY[0],SIGUSR2);
        break;

    }
}

void deadX(){
    printf("Soy X(%d) y muero\n",getpid());
    exit(0);
}

void deadY(){
    printf("Soy Y(%d) y muero\n",getpid());
    exit(0);
}

int main(int args,char *argv[]){
    int i;
    int j;
    pid_t abuelo;
    pid_t bisabuelo;
    pid_t XY[3];
    int time=1;

    bisabuelo = getpid();
    for (i = 0; i < 2; i++){
        if(fork() > 0){
            break;
        }

        if(i==1){
            abuelo = getppid();
            for (j = 0; j < 3; j++)
                if((XY[j] = fork()) == 0) break;
        }
    }

    
    switch (i){
    case 0:
        printf("Soy el proceso ejec: Mi pid es %d\n",getpid());
        wait(NULL);
        printf("Soy ejec(%d) y muero\n",getpid());
        break;
    case 1:
        printf("Soy el proceso A: Mi pid es %d. Mi padre es %d\n",getpid(),getppid());
        signal(SIGUSR1,arbolA);
        wait(NULL);
        printf("Soy A(%d) y muero\n",getpid());
        break;
    case 2:
       switch (j){
            case 0:
                sleep(1);
                printf("Soy el proceso X: Mi pid es %d. Mi padre es %d. Mi abuelo es %d.  Mi bisabuelo es %d\n",getpid(),getppid(),abuelo,bisabuelo);
                signal(SIGUSR1,lsX);
                signal(SIGUSR2,deadX);
                pause();
                break;
            case 1:
                sleep(1);
                printf("Soy el proceso Y: Mi pid es %d. Mi padre es %d. Mi abuelo es %d. Mi bisabuelo es %d\n",getpid(),getppid(),abuelo,bisabuelo);
                signal(SIGUSR1,lsY);
                signal(SIGUSR2,deadY);
                pause();
                break;
            case 2:
                sleep(1);
                printf("Soy el proceso Z: Mi pid es %d. Mi padre es %d. Mi abuelo es %d. Mi bisabuelo es %d\n",getpid(),getppid(),abuelo,bisabuelo);
                if(args > 1) {
                    time = atoi(argv[2]);
                } else {
                    ejecutarAccion('A',XY,abuelo,getppid());
                    exit(0);
                }
                signal(SIGALRM,nada);
                alarm(time);
                pause();
                char c = argv[1][0];
                ejecutarAccion(c,XY,abuelo,getppid());
                printf("Soy Z(%d) y muero\n",getpid());
                
                break;
    
            default:
                printf("Soy el proceso B: Mi pid es %d. Mi padre es %d. Mi abuelo es %d\n",getpid(),getppid(),bisabuelo);
                signal(SIGUSR1,arbolB);
                while (wait(NULL) > 0);
                printf("Soy B(%d) y muero\n",getpid());
                
                break;
        }
        break;
    }
    exit(0);
}