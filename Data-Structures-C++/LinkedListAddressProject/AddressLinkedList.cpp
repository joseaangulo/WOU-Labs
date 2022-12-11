//----------------------------------------------------------
// CS260 Assignment 2 Starter Code
// Copyright Andrew Scholer (ascholer@chemeketa.edu)
// Neither this code, nor any works derived from it
//    may not be republished without approval.
//----------------------------------------------------------

#include "AddressLinkedList.h"
#include <iostream>
#include <sstream>
#include <exception>
#include <stdexcept>

//void AddressLinkedList::printRange(int startIndex, int endIndex) const{
//    for(int i = startIndex; i <= endIndex; i++){
//           cout << list.
//    }
//}

AddressLinkedList::AddressLinkedList(){
    head = nullptr;
    tail = nullptr;
    length = 0;
}


AddressLinkedList::AddressLinkedList(const AddressLinkedList& other){
        head = nullptr;
        tail = nullptr;
        length = 0;

        //empty lists
        if(other.length == 0){
            return;
        }

        else if(other.length == 1){
        insertStart(other.head->data);
        }

        else{
            AddressListNode* current = other.head;

            while(current != nullptr){
                insertEnd(current->data);
                current = current->next;
            }

            current = nullptr;
        }

}


//AddressLinkedList& AddressLinkedList::operator=(const AddressLinkedList& other){

//}

AddressLinkedList::~AddressLinkedList(){
    while(head != nullptr){
        AddressListNode* current = head;
        head = head->next;
        delete current;
    }

    head = nullptr;
    tail = nullptr;

    length = 0;
}

int AddressLinkedList::listSize() const{
    return length;
}


void AddressLinkedList::print() const{
    //current will point to each element in turn
    AddressListNode* current = head;

    while(current != nullptr){
        std::cout << current->data << std::endl; //print current item
        current = current->next; //advance to next
    }
}

void AddressLinkedList::printRange(int startIndex, int endIndex) const{

   //from startIndex to endIndex print
    for(int i = startIndex; i <= endIndex; i++){
        std::cout << retrieveAt(i) << std::endl;
    }

}


void AddressLinkedList::insertStart(const Address& value){
    AddressListNode* temp = new AddressListNode(value);

    //In the case of empty list
    if(length == 0){
        head = temp;
        tail = temp;
    }
    else{
        temp->next = head;
        head = temp;
    }

    temp = nullptr;
    length++;
}


void AddressLinkedList::insertEnd(const Address& value){
    if(length == 0){
            insertStart(value);
        }
        else{
        AddressListNode* temp = new AddressListNode(value);

        tail->next = temp;
        tail = temp;
        length++;

        temp = nullptr;
        }

}

Address AddressLinkedList::removeFirst(){

    //Case of empty list
    if(head == nullptr)
        throw std::out_of_range("Not gonna happen");

    //case of 1 item list
    if(length == 1){
       Address temp = head->data;
       length--;
       head = nullptr;
       tail = nullptr;
       return temp;
    }
    //all other cases, tail remains the same
    else{
        Address temp = head->data;
        head = head->next;
        length--;
        return temp;
    }


}

Address AddressLinkedList::retrieveAt(int index) const{
    if(index < 0 || index >= length)
        throw std::out_of_range("Bad index in retrieveAt");

    AddressListNode* current = head;
    for(int stepsLeft = index; stepsLeft > 0; stepsLeft--) {
        current = current->next;
    }

    return current->data;
}


void AddressLinkedList::combine(AddressLinkedList& other){
    //linking up both lists and setting new tail + length
    tail->next = other.head;
    length = length + other.length;
    tail = other.tail;

    //clearing other list
    other.head = nullptr;
    other.tail = nullptr;
    other.length = 0;


}


AddressLinkedList AddressLinkedList::extractAllMatches(const Address& itemToMatch){
    AddressLinkedList myList;
    AddressListNode* current = head->next;
    AddressListNode* previous = head;


    //empty list
    if(length == 0){
        return myList;
    }

    //first item and/or list of size 1
    if(head->data.state == itemToMatch.state){
        myList.insertEnd(removeFirst());

        if(length == 1){
            return myList;
        }
    }

    //cycle through rest of the list
    int myLength = length;
    for(int i = 0; i < (myLength - 1); i++){
        if(current->data.state == itemToMatch.state){
            previous->next = current->next; //removes listnode

            myList.insertEnd(current->data); //adds to myList by value

            delete current; //deletes listnode in heap

            current = previous->next; //set up for upcoming testing

            length --; //listnode was removed

            }
        //the case of no match... just continue cycling through
        else{
            previous = current;
            current = current->next;
        }
        }

    current = nullptr;
    previous = nullptr;


    return myList;


}



void AddressLinkedList::interleave(AddressLinkedList& other){
    AddressListNode* current_1 = head;
    AddressListNode* current_1_forward = current_1;
    AddressListNode* current_2 = other.head;
    AddressListNode* current_2_forward = current_2;

    int originalLength = length;

    //cycling through linkedlist for length of other
    for(int i = 0; i < other.length; i++){

        if(current_2->next != nullptr)
            current_2_forward = current_2->next; //keeps track of next item

        if(current_1->next != nullptr){
            current_1_forward = current_1->next; //keeps track of next item
            current_2->next = current_1_forward; //interleaving
        }

        current_1->next = current_2;//interleaving


        //reset for next iteration
        current_1 = current_1_forward;

        current_2 = current_2_forward;


        length++;
    }

    //the case they are of the same size
    if(originalLength == other.length){
        tail = other.tail; //tail would be from other
    }

    //clearing used pointers

    current_1 = nullptr;
    current_2 = nullptr;
    current_1_forward = nullptr;
    current_2_forward = nullptr;

    other.length = 0;
    other.head = nullptr;
    other.tail = nullptr;
}


AddressListNode::AddressListNode() {
    next = nullptr;
    //data will be default initialized
}

AddressListNode::AddressListNode(const Address& value)
    : data(value)
{
    next = nullptr;
}
