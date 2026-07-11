#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>
#include <signal.h>
#include <sys/types.h>
#include <fcntl.h>

int main(int args,char *argv[]){
    int fd[2];
    pipe(fd);
    int archivo;
    char buffer;

    if(args < 3) perror("Falta de argumentos:\n");
    if(fork() > 0){
        close(fd[0]);
        if((archivo = open(argv[1],O_RDONLY)) == -1) perror("No existe el archivo\n");
        while (read(archivo,&buffer,1) > 0) 
            write(fd[1],&buffer,1);
        close(fd[1]);
    } else {
        close(fd[1]);
        if((archivo = creat(argv[2],0777))==-1) perror("No se pudo crear\n");
        while(read(fd[0],&buffer,1))
            write(archivo,&buffer,1);
        close(fd[0]);
    }

    close(archivo);
    exit(0);
}