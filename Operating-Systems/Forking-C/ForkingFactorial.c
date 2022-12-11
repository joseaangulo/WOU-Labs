 #include <stdio.h> 
 #include <unistd.h> 
 #include <sys/types.h>
 #include <sys/wait.h>

 void sumfact()
 {
    int number, ret;

    scanf("%d", &number);

    ret = fork();

    if(ret == 0)
    {
        int sumResult = 0;
        int i;
        for(i = 0; i <=number; ++i)
        {
            sumResult += i;   
        }
        
        printf("[ID = %d] Sum of positive integers up to %d is %d\n", getpid(), number, sumResult);
    }

    if(ret > 0)
    {
        if(fork() == 0){
            int factResult = 1;
            int j;
        if(number == 0)
            factResult = 0;
        else {
            for(j = 1; j <= number; ++j)
            {
                factResult *= j;
            }
        }
        printf("[ID = %d] Factorial of %d is %d\n", getpid(), number, factResult);
        }

        else
        {
            wait(NULL);
            printf("[ID = %d] Done\n", getpid());
        }
    }
    
 }

 int main()
 {
    sumfact();

    return 0;
 }