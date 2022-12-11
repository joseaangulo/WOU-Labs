//----------------------------------------------------------
// CS260 Assignment 2 Starter Code
// Copyright Andrew Scholer (ascholer@chemeketa.edu)
// Neither this code, nor any works derived from it
//    may not be republished without approval.
//----------------------------------------------------------
#include <iostream>
#include <ctime>

#include "Address.h"
#include "ArrayList.h"
#include "AddressArrayList.h"
#include "AddressLinkedList.h"

using namespace std;


int main()
{
    int size = 0;
    cout << "Enter problem size:" << endl;
    cin >> size;

    ArrayList<Address> aListA;
    ArrayList<Address> aListB;

    AddressFactory aFactory("25000AddressesA.txt");
    AddressFactory aFactory2("25000AddressesB.txt");

    for(int i = 0; i < size / 2; i++) {
        Address a = aFactory.getNext();
        aListA.insertEnd(a);
        Address a2 = aFactory2.getNext();
        aListB.insertEnd(a2);
    }

cout << "----------------------PART 1------------------" << endl;
printListRange(aListA, 0, aListA.listSize() - 1);
cout << endl;
printListRange(aListB, 0, aListB.listSize() - 1);
cout << endl;

aListA.combine(aListB);

cout << "Number of items in aListA:" << aListA.listSize() << endl;
cout << "Number of items in aListB:" << aListB.listSize() << endl;

printListRange(aListA, (size / 2 - 2), (size / 2 + 1));

cout << "----------------------PART 2------------------" << endl;

Address exampleSearch;
exampleSearch.state = "OR";

//printListRange(aListA, 0, aListA.listSize() - 1);
//cout << aListA.listSize();

ArrayList<Address> aListC = aListA.extractAllMatches(exampleSearch);

//printListRange(aListA, 0, aListA.listSize() - 1);
cout << "The length of aListA: " << aListA.listSize() << endl;

//printListRange(aListC, 0, aListC.listSize() - 1);
cout << "The length of aListC:" << aListC.listSize() << endl;

cout << "----------------------PART 3------------------" << endl;
AddressLinkedList linkListA;
AddressLinkedList linkListB;

AddressFactory aaFactory("25000AddressesA.txt");
AddressFactory bbFactory("25000AddressesB.txt");

for(int i = 0; i < size / 2; i++) {
    Address aa = aaFactory.getNext();
    linkListA.insertEnd(aa);
    Address bb = bbFactory.getNext();
    linkListB.insertEnd(bb);
}
cout << "Print range of LinkListA: " << endl;
linkListA.printRange(2, 4);


cout << "Print range of LinkListB: " << endl;
linkListB.printRange(2, 4);


cout << "----------------------PART 4------------------" << endl;
	
AddressLinkedList linkListC(linkListA);
AddressLinkedList linkListD(linkListB);

cout << "The length of linkListC is: " << linkListC.listSize() << endl;
cout << "The length of LinkListD is: " << linkListD.listSize() << endl;

linkListC.combine(linkListD);

linkListC.printRange(size/2 - 2, size / 2 + 1);


cout << "----------------------PART 5------------------" << endl;
Address searchAddress;
searchAddress.state = "OR";
AddressLinkedList linkListE = linkListC.extractAllMatches(searchAddress);

linkListC.print();
cout << "Length of linkListC is: " << linkListC.listSize() << endl;
cout << "Length of linkListE is: " << linkListE.listSize() << endl;

cout << "Print range of LinkList E (Extracted from C): " << endl;

linkListE.printRange(0,1);


cout << "----------------------PART 6------------------" << endl;


linkListA.interleave(linkListB);

cout << "Print range of LinkList A (Interleaved with B): " << endl;
linkListA.printRange(0, 4);





    
    return 0;
}
