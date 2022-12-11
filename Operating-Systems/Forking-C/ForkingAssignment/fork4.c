// fork4.c 
 #include <stdio.h> 
 #include <unistd.h> 
 #include <sys/types.h> 
   
 void fork4() 
 { 
      printf("\n [%d] L0 and my parent is [%d]\n", getpid(), getppid()); 
      fork();   
      printf("\n [%d] L1 and my parent is [%d] \n", getpid(), getppid()); 
      fork();   
      printf("\n [%d] and my parent is [%d] Bye \n", getpid(), getppid()); 
  } 
    
  int main () 
  { 
      fork4();  
      return 0; 
  }