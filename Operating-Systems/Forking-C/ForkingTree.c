#include <stdio.h>
#include <unistd.h>
#include <stdlib.h>


void tree()
{
    int ret;

    ret = fork();

    if(ret > 0)
    {
        printf("[ID = %d] I am the root parent\n", getpid());
        if(fork() == 0)
        {
            printf("[ID = %d] My parent is [%d]\n", getpid(), getppid());
            if(fork() == 0)
            {
                printf("[ID = %d] My parent is [%d]\n", getpid(), getppid());
        
            }
        }
        
    }
    else
    {
        printf("[ID = %d] My parent is [%d]\n", getpid(), getppid());
        if(fork() == 0)
        {
            printf("[ID = %d] My parent is [%d]\n", getpid(), getppid());
        
        }
    }


}

int main()
{
    tree();

    return 0;
}