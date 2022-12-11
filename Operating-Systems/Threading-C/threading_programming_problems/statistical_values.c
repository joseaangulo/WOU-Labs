/* 4.22) Write a multithreaded program that calculates various statistical values
for a list of numbers. This program will be passed a series of numbers
on the command line and will then create three separate worker threads.
One thread will determine the average of the numbers, the second will
determine the maximum value, and the third will determine the mini-
mum value. For example, suppose your program is passed the integers
90 81 78 95 79 72 85
The program will report
The average value is 82
The minimum value is 72
The maximum value is 95
The variables representing the average, minimum, and maximum values
will be stored globally. The worker threads will set these values, and
the parent thread will output the values once the workers have exited.
(We could obviously expand this program by creating additional threads
that determine other statistical values, such as median and standard
deviation.)
*/

#include <stdio.h>
#include <unistd.h>
#include <pthread.h>
#include <stdlib.h>

/* data shared by all threads - global */
int inputs[7];
int average_num;
int minimum_num;
//int maximum_num;

int worker_count = 0;
/* threads call this function for average*/
void* average(void* param);
/* for minimum*/
void* minimum(void* param);
// /* for maximum*/
//void* maximum(void* param);

#define NUM_THREADS 2
int main(int argc, char *argv[])
{
    /* inupt 7 variables */
    //int inputs[7];
    int i;
    for(i = 0; i < 7; ++i)
    {
        printf("Enter number: ");
        scanf("%d", &inputs[i]);
    }

    /* an array of threads to be joined upon*/
    pthread_t workers[NUM_THREADS];

    pthread_attr_t attr;

    /* set default attributes of the thread*/
    pthread_attr_init(&attr);
    
    /*first thread created to do average */
    pthread_create(&workers[worker_count++], &attr, average, NULL);

    // /*first thread created to do minimum */
    pthread_create(&workers[worker_count++], &attr, minimum, NULL);

    // /*first thread created to do maximum */
    //pthread_create(&workers[worker_count++], &attr, maximum, NULL);

    /* joining all threads*/
    for(i = 0; i < NUM_THREADS; ++i){
    pthread_join(workers[i], NULL);
    }

    printf("The average value is %d\n", average_num);
    printf("The minimum value is %d", minimum_num);

    return 0;
}

/* Threads will execute in these functions */
void* average(void* param)
{
    int i, total;
    total = 0;
    for(i = 0; i < sizeof(inputs)/sizeof(inputs[0]); ++i)
    {
        total += inputs[i];
    }

    average_num = (int)(total / (sizeof(inputs)/sizeof(inputs[0])));

    pthread_exit(0);    
}

void* minimum(void* param)
{
    int i;
    i = 0;
    minimum_num = inputs[i++];

    for(; i < sizeof(inputs)/sizeof(inputs[0]); ++i)
    {
        if(inputs[i] < minimum_num)
        {
            minimum_num = inputs[i];
        }
    }

    pthread_exit(0);
}