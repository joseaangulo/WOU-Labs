//BankAccount.c
#include <stdio.h>
#include <string.h>
#include "BankAccount.h"

BankAccount InitBankAccount(char* newName, double amt1, double amt2)
{
  BankAccount bankAccount;
  strcpy(bankAccount.customerName, newName);
  bankAccount.customerCheckingAccountBalance = amt1;
  bankAccount.customerSavingsAccountBalance = amt2;


  return bankAccount;
}
BankAccount SetName(char* newName, BankAccount account)
{
    strcpy(account.customerName, newName);
    return account;
}
void GetName(char* customerName, BankAccount account)
{
    strcpy(customerName, account.customerName);
}

BankAccount SetChecking(double amt, BankAccount account)
{

    account.customerCheckingAccountBalance = amt;

    return account;
}

double GetChecking(BankAccount account)
{
    return account.customerCheckingAccountBalance;
}
BankAccount SetSavings(double amt, BankAccount account)
{
    account.customerSavingsAccountBalance = amt;
    return account;
}
double GetSavings(BankAccount account)
{
    return account.customerSavingsAccountBalance;
}
BankAccount DepositChecking(double amt, BankAccount account)
{
    if(amt > 0){
    account.customerCheckingAccountBalance += amt;
    }
    return account;
}
BankAccount DepositSavings(double amt, BankAccount account)
{
    if(amt > 0){
    account.customerSavingsAccountBalance += amt;
    }
    return account;
}
BankAccount WithdrawChecking(double amt, BankAccount account)
{
    if(amt > 0){
    account.customerCheckingAccountBalance -= amt;
    }

    return account;
}
BankAccount WithdrawSavings(double amt, BankAccount account)
{
    if(amt > 0){
    account.customerSavingsAccountBalance -= amt;
    }
    return account;
}
BankAccount TransferToSavings(double amt, BankAccount account)
{
    if(amt > 0){
    account.customerCheckingAccountBalance -= amt;
    account.customerSavingsAccountBalance += amt;
    }
    return account;
}
