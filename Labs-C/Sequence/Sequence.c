//main.c
#include <stdio.h>
#include <stdlib.h>

void PrintArray(int* array, int size) {
   for (int j = 0; j < size; ++j) {
      printf("%d ", array[j]);
   }
}

// Return the first n Fibonacci numbers
// fibonacci(0) = 0, fibonacci(1) = 1, fibonacci(2) = 1
// Ex: n = 5, seq = 0 1 1 2 3
int* Fibonacci(int n) {
   int* seq;
   int j;

   seq = malloc(n * sizeof(j));


   for(j = 0; j < n; ++j)
   {
      if(j == 0)
      *(seq + j) = 0;

      else if(j == 1)
      *(seq + j) = 1;

      else
         *(seq + j) = *(seq + (j -1)) + *(seq + (j - 2));

   }

   return seq;
}

// Return sequence of squares for 1..n (inclusive)
// Ex: sqrn = 3, seq = 1 4 9
int* Squares(int n) {
   int* seq;

  seq = malloc(n * sizeof(int));

   int j;

   for(j = 1; j <= n; ++j)
   {
      *(seq + (j - 1)) = j * j;
   }


   return seq;
}

// Return an array that is a copy of array1 followed by
// the elements of array2
int* Concatenate(int* array1, int size1, int* array2, int size2) {
   int j;
   int* seq;
   int k;
   
   seq = malloc((size1 + size2) * sizeof(int));
   
   for(j = 0; j < size1; ++j)
      *(seq + j) = *(array1 + j);

   for(k = 0; k < size2; ++k)
      *(seq + j + k) = *(array2 + k);

   return seq;
}

int main(void) {

   int fibn;                                 // seq of first fibn Fibonacci numbers
                                             // Ex: fibn = 5, seq = 0 1 1 2 3
   int sqrn;                                 // number of squares starting with 1
                                             // Ex: sqrn = 3, seq = 1 4 9
   scanf("%d %d", &fibn, &sqrn);

   int* fibs;
   int* sqrs;
   int* conc;

   fibs = Fibonacci(fibn);
   PrintArray(fibs, fibn);
   printf("\n");

   sqrs = Squares(sqrn);
   PrintArray(sqrs, sqrn);
   printf("\n");

   conc = Concatenate(fibs, fibn, sqrs, sqrn);
   PrintArray(conc, fibn + sqrn);
   printf("\n");

   return 0;
}
