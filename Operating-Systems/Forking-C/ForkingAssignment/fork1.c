 //fork1.c 
 #include <stdio.h> 
 #include <unistd.h> 
 #include <sys/types.h> 
   
 int main() 
 { 
      int id, ret; 
   
      ret = fork();
      id = getpid(); 
    
      printf("\n My identifier is ID = [%d] and ret value %d\n", id, ret);
    
      while (1) ; 
    
      return 0;
}