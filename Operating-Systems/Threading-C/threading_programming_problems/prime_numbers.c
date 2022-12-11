/*4.23) Write a multithreaded program that outputs prime numbers. This pro-
gram should work as follows: The user will run the program and will
enter a number_to_prime on the command line. The program will then create a
separate thread that outputs all the prime numbers less than or equal to
the number_to_prime entered by the user
*/

/*thread formula
1) create thread id
2) create thread attr struct?
3) initialize thread attr to default - pass in thread attr
4) create thread - id, attr, function, some param
*/

#include <stdio.h>
#include <pthread.h>
#include <unistd.h>

//generic function to return array of prime numbers
void* all_primes(void* param);
int number_to_prime;
int* ptr_to_arrayOfPrimes;
int numberOfPrimes = 0;


int main(int argc, char* argv[])
{
    printf("Give all primes of: ");
    scanf("%d", &number_to_prime);
    
    int arrayOfPrimes[number_to_prime];
    ptr_to_arrayOfPrimes = arrayOfPrimes;

    pthread_t tid;
    pthread_attr_t attr;

    pthread_attr_init(&attr);

    pthread_create(&tid, &attr, all_primes, NULL);

    pthread_join(tid, NULL);



    int current_element = 0;
    ptr_to_arrayOfPrimes = arrayOfPrimes;

    if(numberOfPrimes == 0)
    {
        printf("No primes found");
        return 0;
    }

    printf("The list of primes are: ");
    while(current_element < numberOfPrimes)
    {
        printf("%d ", *ptr_to_arrayOfPrimes);
        ptr_to_arrayOfPrimes++;
        current_element++;
    }




    return 0;
}

void* all_primes(void* param)
{
    int current_number = number_to_prime;
    int numbers_lessthan_current;
    int numberOfConstants = 0;
    for(current_number; 0 < current_number; --current_number)
    {
        numbers_lessthan_current = current_number - 1;
        for(numbers_lessthan_current - 1; (numbers_lessthan_current - 1) >= 1; --numbers_lessthan_current)
        {
            if(current_number % numbers_lessthan_current == 0)
            {
                numberOfConstants++;
            }

        }
            if(numberOfConstants == 0){
                *ptr_to_arrayOfPrimes = current_number;
                ptr_to_arrayOfPrimes++;
                numberOfPrimes++;
            }

            numberOfConstants = 0;
    }
    
}