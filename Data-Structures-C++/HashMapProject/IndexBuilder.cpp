#include "IndexRecord.h"
#include "IndexMap.h"

#include <iostream>
#include <sstream>
#include <fstream>
#include <iomanip>
#include <vector>
#include <ctime>
#include <algorithm>
#include <string>

using namespace std;

int main() {
    ifstream inFile("GreatExpectations.txt");
    if( !inFile.is_open()) {
        cout << "Error opening File" << endl;
    }

    cout << "-------------------------Part3---------------------" << endl;

    string word;
    inFile >> word;
    IndexMap text(10);
    int page = 1;
    int wordNumber = 1;
        clock_t startTime = clock();
        while(inFile){

        if(word != "----------------------------------------"){
            //convert any uppercase letters to lowercase
        transform(word.begin(), word.end(), word.begin(), ::tolower);
        //add in word to text hash table
        text.add(word, page, wordNumber);
        }
        inFile >> word;
        wordNumber++;
        if(word == "----------------------------------------"){
            page++;
            wordNumber = 1;
            inFile >> word;
        }

       }
        clock_t endTime = clock();

        double seconds = static_cast<double>(endTime - startTime) / CLOCKS_PER_SEC;
        cout << "Number of keys: " << text.numKeys() << endl;

        cout << "Seconds took: " << fixed << setprecision(15) <<  seconds << endl;

       //NEED TO ADD TIME TO BUILD THE INDEX

       IndexRecord fathers = text.get("fathers");
       cout << fathers << endl;
       cout << endl;

cout << "--------------------Part4------------------------" << endl;
cout << "All Locations: ";
text.findWordPairs("great", "expectations");
cout << endl;

cout << "-------------------Part5-----------------------" << endl;
cout << "First word on page 100: " << text.firstWordOnPage(100) << endl;

//text.print();




}
