#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

void* routine()
{  
    printf("Test from threads\n");
    sleep(3);
    printf("Ending thread\n");
}

int main(int argc, char* argv[])
{   
    //basically a struct with info
    pthread_t t1, t2;

    //pthread functions return int ( 0 or something else) which represent error
    //-number, if not 0 error occurred
    //if statement will allow for error checking
    //thread creation can be supresssed in linux environment or could be out of
    //-resouces that causes thread creation to fail
    if(pthread_create(&t1, NULL, &routine, NULL) != 0)
    {
        printf("Couldnt create thread");
    }
    if(pthread_create(&t2, NULL, &routine, NULL) != 0)
    {
        printf("Couldnt create thread");
    }

    //wait but for threads
    //passs in struct
    if(pthread_join(t1, NULL) != 0)
    {
        printf("Couldnt create thread");
    }
    if(pthread_join(t2, NULL) != 0)
    {
        printf("Couldnt create thread");
    }

    return 0;

}