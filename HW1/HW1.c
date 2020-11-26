#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>

int main() {

		for (int i=0;i<10;i++){
			if(fork()==0){
				printf("I'm the child number %d (pid %d)\n",i,getpid());
				exit(0);
			}
			if (i==9){
				printf("Parent terminates (pid %d)\n",getppid());
			}
		}
		    for(int i=0;i<10;i++) wait(NULL) ;
}
