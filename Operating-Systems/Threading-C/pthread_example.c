#include <pthread.h> //for threads
#include <stdio.h> // to print

#include <stdlib.h> // idk??

int sum; /* this data is shared by the thread(s)*/
void *runner(void *param); /* threads call this function*/

int main(int argc, char *argv[])
{
    pthread_t tid; /* the thread identifier // for thread we'll create*/
    pthread_attr_t attr; /* set of thread attributes // will be default unless set*/

    /* set the default attributes of the thread // because we didnt set it*/
    pthread_attr_init(&attr);
    
    /* create the thread*/
    /*thread identifier, attributes, function call to pass into, and input*/
    pthread_create(&tid, &attr, runner, argv[1]);

    
    /*wait for the thread to exit*/
    /*tid only - since only 1 thread*/
    pthread_join(tid, NULL);
    
    printf("sum = %d\n", sum);

}

/* The thread will execute in this function */
void *runner(void *param)
{
    int i, upper = atoi(param);
    sum = 0;

    for(i = 1; i <= upper; i++)
        sum += i;

    pthread_exit(0);
}