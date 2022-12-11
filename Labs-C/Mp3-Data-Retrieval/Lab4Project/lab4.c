/* CS360 Lab 4: C */

#include <stdio.h>
#include <stdlib.h>

//gloabl variable
FILE*  fp;

void finish()
{
    fclose(fp);				// close and free the file
    exit(EXIT_SUCCESS);		// or return 0;
}

void initialize(int argc, char** argv)
{
    // Open the file given on the command line
    if( argc != 2 )
    {
        printf( "Usage: %s filename.mp3\n", argv[0] );
        exit(EXIT_FAILURE);
    }

    fp = fopen(argv[1], "rb");

    if( fp == NULL )
    {
        printf( "Can't open file %s\n", argv[1] );
        exit(EXIT_FAILURE);
    }
}

int* decToBinary(int n)
{
    // array to store binary number
    int binaryNum[8];

    // counter for binary array
    int i = 0;
    while(n > 0)
    {
        binaryNum[i] = n % 2;
        n = n / 2;
        i++;
    }

    while(i < 8)
    {
        binaryNum[i] = 0;
        i++;
    }

    int k = 0;
    int* reverseBinaryNum = malloc( 8 * sizeof(int));
    // printing binary array in reverse order
    for (int j = i - 1; j >= 0; j--){
        reverseBinaryNum[k] = binaryNum[j];
        k++;
    }

    while(k < 8)
    {
        reverseBinaryNum[k] = 0;
        k++;
    }

    return reverseBinaryNum;
}

struct song{
    long size;
    unsigned char* data;
    size_t bytesRead;
};

void readFile(struct song *a)
{
    // How many bytes are there in the file?  If you know the OS you're
    // on you can use a system API call to find out.  Here we use ANSI standard
    // function calls.
    a->size = 0;
    fseek( fp, 0, SEEK_END );		// go to 0 bytes from the end
    a->size = ftell(fp);				// how far from the beginning?
    rewind(fp);						// go back to the beginning
    const int megabyte = 10485760;

    if( a->size < 1 || a->size > megabyte )
    {
        printf("File size is not within the allowed range\n");
        finish();
    }

    printf( "File size: %0.2f MB\n", (float)a->size/megabyte );

    // Allocate memory on the heap for a copy of the file
    a->data = (unsigned char *)malloc(a->size);

    // Read it into our block of memory
    a->bytesRead = fread( a->data, sizeof(unsigned char), a->size, fp );
    //free(a->data);
    if( a->bytesRead != a->size )
    {
        printf( "Error reading file. Unexpected number of bytes read: %I64d\n",a->bytesRead );
        finish();
    }

}

unsigned char* syncPosition(struct song* a)
{
    unsigned char* myPointer;
    for(int i = 0; i < (int)a->bytesRead; i++)
    {
        if(a->data[i] == 255){
         unsigned char x = a->data[i + 1];
         x = x >> 4;
         if(x == 15){
         myPointer = &a->data[i];
         return myPointer;
         }
        }
     }
    return NULL;
}
int* toFullBinary(int syncBytes[], unsigned char* syncbitPosition)
{
    for(int i = 0; i < 4; i++)
    {
        //printf("0x%02X\n", syncbitPosition[i]);
        syncBytes[i] = (int)syncbitPosition[i];
    }

    int k = 0;
    int* fullBinary = malloc( 32 * sizeof(int));
    int* myByte;

    for(int i = 0; i < 4; i++)
    {
        myByte = decToBinary(syncBytes[i]);
        for(int j = 0; j < 8; j++){
        fullBinary[k] = myByte[j];
        k++;
        }

       // printf("%d """, myByte[i]);
    }
    return fullBinary;
}
int bitRate(int* array)
{
    int brArray[4];
    int k = 0;
    int decimalValue = 0;
    for(int i = 16; i < 20; i++)
    {
        brArray[k] = array[i];
        k++;
    }

    if(brArray[0] == 1)
        decimalValue = decimalValue + 8;
    if(brArray[1] == 1)
        decimalValue = decimalValue + 4;
    if(brArray[2] == 1)
        decimalValue = decimalValue + 2;
    if(brArray[3] == 1)
        decimalValue = decimalValue + 1;

    if(decimalValue == 0)
        return 0;
    if(decimalValue == 1)
        return 32;
    else if(decimalValue == 2)
        return 40;
    else if(decimalValue == 3)
        return 48;
    else if(decimalValue == 4)
        return 56;
    else if(decimalValue == 5)
        return 64;
    else if(decimalValue == 6)
        return 80;
    else if(decimalValue == 7)
        return 96;
    else if(decimalValue == 8)
        return 112;
    else if(decimalValue == 9)
        return 128;
    else if(decimalValue == 10)
        return 160;
    else if(decimalValue == 11)
        return 192;
    else if(decimalValue == 12)
        return 224;
    else if(decimalValue == 13)
        return 256;
    else if(decimalValue == 14)
        return 320;
    else
        return -1;
}
int frequency(int* array)
{
    if(array[20] == 0 && array[21] == 0)
    {
        return 44100;
    }
    else if(array[20] == 0 && array[21] == 1)
    {
        return 4800;
    }
    else if(array[20] == 1 && array[21] == 0)
    {
        return 3200;
    }
    else
        return 1;

}
int main( int argc, char ** argv )
{
    //fp = fopen("song.mp3", "rb");

    initialize(argc, argv);



    struct song the_song;
    struct song *mySong = &the_song;
    readFile(mySong);

    // We now have a pointer to the first byte of data in a copy of the file, have fun
    // unsigned char * data    <--- this is the pointer
    unsigned char* syncbitPosition = syncPosition(mySong);
    int syncBytes[4];


    int* fullBinary = toFullBinary(syncBytes, syncbitPosition);


    for(int i = 0; i < 32; i++)
    {
        if(i % 4 == 0)
        {
            printf("\n");
        }
        printf("%d" , fullBinary[i]);

    }
    printf("\n");

    //BitRate Function
    int songBitRate = bitRate(fullBinary);
    int songFrequency = frequency(fullBinary);
    //Convert binary to decimal
    //Frequency Function

    //MPEG V1
    if(fullBinary[12] == 1)
    {
        //Layer3 Version
        if(fullBinary[13] == 0 && fullBinary[14] == 1)
        {
            //bitRate
            if(songBitRate == 0)
            {
                printf("Bitrate:Free\n");
            }
            else if(songBitRate == -1)
            {
                printf("BitRate:Bad\n");
            }
            else
                printf("BitRate:%dkbps\n", songBitRate);

            //Frequency
            if(songFrequency == 1)
            {
                printf("Song Frequency:reserv.\n");
            }
            else
                printf("Song Frequency:%0.1fkHz\n", (double)songFrequency/1000);

            //CopyRighted
             if(fullBinary[28] == 0)
                 printf("Copy Status:Not Copy-Righted\n");
             else
                 printf("Copy Status:Copy-Righted\n");
            //Original
             if(fullBinary[29] == 0)
                 printf("Original Status: Copy Of Original Media\n");
            //Copy
             else
                 printf("Original Status: Original\n");
        }
    }
    else
        printf("Not MPEG V1 Layer3");

    free(fullBinary);
    free(syncbitPosition);
    free(mySong->data);
    free(mySong);
}
