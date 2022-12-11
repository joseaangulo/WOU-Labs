//----------------------------------------------------------
// CS260 Assignment Starter Code
// Copyright Andrew Scholer (ascholer@chemeketa.edu)
// Neither this code, nor any works derived from it
//    may not be republished without approval.
//----------------------------------------------------------

#include "IndexMap.h"

#include <cassert>
#include <algorithm>
#include <stdexcept>


using namespace std;

IndexMap::IndexMap(int startingBuckets){
    numBuckets = startingBuckets;
    keyCount = 0;
    buckets = new IndexRecord[startingBuckets];
}

void IndexMap::grow() {
    //keep track of old data
    IndexRecord* oldRecords = buckets;
    int oldNumBuckets = numBuckets;

    //new data
    keyCount = 0;
    numBuckets = 2 * numBuckets + 1;
    buckets = new IndexRecord[numBuckets];

    //adding old data to new buckets
    for(int i = 0; i < oldNumBuckets; i++){
        if(oldRecords[i].word != EMPTY_CELL){
            //multiple locations added per word
            for(int j = 0; j < oldRecords[i].locations.size(); j++)
                add(oldRecords[i].word, oldRecords[i].locations[j].pageNum, oldRecords[i].locations[j].wordNum);

        }
    }

    delete [] oldRecords;

}

IndexMap::~IndexMap(){
    delete [] buckets;
}

unsigned int IndexMap::getLocationFor(const std::string& key) const {
    std::hash<string> hasher; //hash algorithm

    unsigned int hashValue = static_cast<unsigned int>(hasher(key));

    return hashValue % numBuckets; //to fit records
}

bool IndexMap::contains(const std::string &key) const{

    //hashed value
    int bucketNumber = getLocationFor(key);
    //iterate until hits empty cell
    while(buckets[bucketNumber].word != EMPTY_CELL){
        //when key is found
        if(buckets[bucketNumber].word == key)
            return true;

        //advancing through array
        bucketNumber++;

        //wrapping back to 0
        if(bucketNumber == numBuckets)
            bucketNumber = 0;
    }

    return false; //not found
}

void IndexMap::add(const std::string &key, int pageNumber, int wordNumber) {
    //grow if needed
    if(keyCount > MAX_LOAD * numBuckets)
        grow();

    //hashed value
    int bucketNumber = getLocationFor(key);

    while(buckets[bucketNumber].word != "?" && buckets[bucketNumber].word != key){
        //advancing through array
        bucketNumber++;

        //wrapping back to 0
        if(bucketNumber == numBuckets)
            bucketNumber = 0;
    }
    //new location created
    IndexLocation myLocation(pageNumber, wordNumber);


    //adding new word
    if(buckets[bucketNumber].word != key){
        IndexRecord r1(key);
        r1.addLocation(myLocation);

        buckets[bucketNumber] = r1;

        //new key
        keyCount++;
    }
    //word created
    else
        buckets[bucketNumber].addLocation(myLocation);

}

void IndexMap::print() const {
    for(int i = 0; i < numBuckets; i++){
        cout << i << ":" << buckets[i].word << endl;
        cout << "  Locations:";
        for (int j = 0; j < buckets[i].locations.size(); j++){
            cout << " " << buckets[i].locations[j].pageNum << "," << buckets[i].locations[j].wordNum << " ";
        }
        cout << endl;
        cout << endl;
    }
}

int IndexMap::numKeys() const {
   return keyCount;
}

IndexRecord IndexMap::get(const std::string &word) const{
    //word not in records
    if(!contains(word))
        throw invalid_argument("Not in set");

    //hashed value
    int bucketNumber = getLocationFor(word);

    //iterate until hits word input
    while(buckets[bucketNumber].word != word){
        //advancing through array
        bucketNumber++;

        //wrapping back to 0
        if(bucketNumber == numBuckets)
            bucketNumber = 0;
    }

    return buckets[bucketNumber];

}

void IndexMap::findWordPairs(const std::string &key1, const std::string &key2) const {
    //check if words are in records
    if(contains(key1) != true || contains(key2) != true)
        return;

    //hashed value for key1
    int key1Number = getLocationFor(key1);
    while(buckets[key1Number].word != key1){
        //advance through array
        key1Number++;

        //wrapping back to 0
        if(key1Number == numBuckets)
            key1Number = 0;
    }

    //hashed value for key2
    int key2Number = getLocationFor(key2);
    while(buckets[key2Number].word != key2){
        //advance through array
        key2Number++;

        //wrapping back to 0
        if(key2Number == numBuckets)
            key2Number = 0;
    }
    auto key1BucketLocations = buckets[key1Number].locations;
    auto key2BucketLocations = buckets[key2Number].locations;

    cout << key1 << " " << key2 << " are found: ";
    for(int i = 0; i < buckets[key1Number].locations.size(); i++){
        for(int j = 0; j < buckets[key2Number].locations.size(); j++){
            if(key1BucketLocations[i].pageNum == key2BucketLocations[j].pageNum && (key1BucketLocations[i].wordNum + 1) == key2BucketLocations[j].wordNum){
                cout << key1BucketLocations[i].pageNum << "-" << key1BucketLocations[i].wordNum << " ";
            }
        }
    }

    cout << endl;
}

std::string IndexMap::firstWordOnPage(int pageNumber) const {

    int bucketCount = 0;
    while(bucketCount < numBuckets){
        if(buckets[bucketCount].word != "?"){
        for(int i = 0; i < buckets[bucketCount].locations.size(); i++){
               if(buckets[bucketCount].locations[i].pageNum >= pageNumber && buckets[bucketCount].locations[i].wordNum > 1)
                   break;
               if(buckets[bucketCount].locations[i].pageNum == pageNumber && buckets[bucketCount].locations[i].wordNum == 1)
                       return buckets[bucketCount].word;

        }

        }
        bucketCount++;
    }
    return "error";
}
