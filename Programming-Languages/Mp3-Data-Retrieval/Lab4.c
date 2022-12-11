/* CS360 Lab 4: C */

#include <stdio.h>
#include <stdlib.h>

void finish(FILE *fp)
{
        fclose(fp);				// close and free the file
        exit(EXIT_SUCCESS);	
} 

int main( int argc, char ** argv )
{
	// Open the file given on the command line
	if( argc != 2 )
	{
		printf( "Usage: %s filename.mp3\n", argv[0] );
		return(EXIT_FAILURE);
	}
	
	FILE * fp = fopen(argv[1], "rb");
	if( fp == NULL )
	{
		printf( "Can't open file %s\n", argv[1] );
		return(EXIT_FAILURE);
	}
	
	// How many bytes are there in the file?  If you know the OS you're
	// on you can use a system API call to find out.  Here we use ANSI standard
	// function calls.
	long size = 0;
	fseek( fp, 0, SEEK_END );		// go to 0 bytes from the end
	size = ftell(fp);				// how far from the beginning?
	rewind(fp);						// go back to the beginning
    const int megabyte = 10485760;
	
	if( size < 1 || size > megabyte )
	{
		printf("File size is not within the allowed range\n"); 
		finish(fp);
	}
	
	printf( "File size: %0.2f MB\n", (float)size/megabyte );
	// Allocate memory on the heap for a copy of the file
	unsigned char * data = (unsigned char *)malloc(size);
	
    // Read it into our block of memory
	size_t bytesRead = fread( data, sizeof(unsigned char), size, fp );
	if( bytesRead != size )
	{
		printf( "Error reading file. Unexpected number of bytes read: %I64d\n",bytesRead );
		finish(fp);
	}
	
	// We now have a pointer to the first byte of data in a copy of the file, have fun
	// unsigned char * data    <--- this is the pointer
	
	
}