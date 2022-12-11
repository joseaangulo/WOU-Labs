#include <stdio.h>
//program.c
#include <string.h>

#include "BankAccount.h"

int main() {
   BankAccount account = InitBankAccount("Mickey", 500.00, 1000.00);
   char name[20];
   
   account = SetChecking(500, account);
   account = SetSavings(500, account);
   account = WithdrawSavings(100, account);
   account = WithdrawChecking(100, account);
   account = TransferToSavings(300, account);
   
   GetName(name, account);
   printf("%s\n", name);
   printf("$%.2f\n", GetChecking(account));
   printf("$%.2f\n", GetSavings(account));

   return 0;
}