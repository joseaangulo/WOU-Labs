/* @author Jose A
 * @brief Contains algorithm exercises for Lab1 of CS 361 Spring 2022
 */

using System;
using System.Collections.Concurrent;
using System.IO;

namespace Project
{
    class Lab1
    {
        static void Main(string[] args)
        {

            string path = "c:\\Users\\imjos" +
                "\\Documents\\CS361_Algorithms\\Labs\\Lab1_361\\lab1_data.txt";
            int[] elements = new int[10000000];
            
            void merge(int[] arr, int startIndex, int mid, int endIndex)
            {
                int[] temp = new int[arr.Length];
                for (int i = startIndex; i <= endIndex; i++)
                    temp[i] = arr[i];

                int leftHalf = startIndex;
                int rightHalf = mid + 1;
                int current = startIndex;

                while (leftHalf <= mid && rightHalf <= endIndex)
                {
                    if (temp[leftHalf] <= temp[rightHalf])
                        arr[current++] = temp[leftHalf++];
                    else
                        arr[current++] = temp[rightHalf++];
                }

                //Possible remaining items on Left Half
                while (leftHalf <= mid)
                {
                    arr[current++] = temp[leftHalf++];
                }
            }
            void auxMergeSort(int[] arr, int startIndex, int endIndex)
                {
                    if(startIndex < endIndex)
                    {
                         int mid = startIndex + (endIndex - startIndex) / 2;
                         auxMergeSort(arr, startIndex, mid);
                         auxMergeSort(arr, mid + 1, endIndex);
                         merge(arr, startIndex, mid, endIndex);

                    }
            }
           
            void mergeSort(int[] arr)
            {
                auxMergeSort(arr, 0, arr.Length - 1);
            }
            
            
            void auxQuickSort(int[] arr, int leftIndex, int rightIndex)
            {
                int index = partition(arr, leftIndex, rightIndex);
                if (leftIndex < index - 1)
                {
                    auxQuickSort(arr, leftIndex, index - 1);
                }

                if (index < rightIndex)
                {
                    auxQuickSort(arr, index, rightIndex);
                }
            }

            int partition(int[] arr, int leftIndex, int rightIndex)
            {
                int pivot = arr[leftIndex + (rightIndex - leftIndex) / 2];

                while (leftIndex <= rightIndex)
                {
                    while (arr[leftIndex] < pivot) leftIndex++;
                    while(arr[rightIndex] > pivot) rightIndex--;

                    if (leftIndex <= rightIndex)
                    {
                        (arr[leftIndex], arr[rightIndex]) = (arr[rightIndex], arr[leftIndex]);
                        leftIndex++;
                        rightIndex--;
                    }

                }
                return leftIndex;
            }

            bool flgIsSortedHelper(int[] arr, int index)
            {
                if (index > 1000)
                {
                    int temp = index - 1000;
                    for (int i = 0; i < temp; i--)
                    {
                        if (arr[index - 2] > arr[index - 1])
                            return false;
                    }

                    index = 1000;
                }

                if (index == 1)
                    return true;

                if (arr[index - 2] > arr[index - 1])
                    return false;

                return flgIsSortedHelper(arr, index - 1);
            }

            //Credit on is flgSorted to
            //https://www.geeksforgeeks.org/program-check-array-sorted-not-iterative-recursive/#:~:text=Recursive%20approach%3A&text=1%3A%20If%20size%20of%20array,to%20one%2C%20satisfying%20Step%201.
            bool flgIsSorted(int[] arr)
            {
                return flgIsSortedHelper(arr, arr.Length);
            }

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int size = 0;
                    while (sr.EndOfStream != true)
                    {
                        elements[size++] = Convert.ToInt32(sr.ReadLine());
                    }

                    sr.Close();
                }

                
                

                int[] elementss = new int[10000];

                int i;
                for (i = 0; i < 10000; i++)
                {
                    elementss[i] = elements[i];
                }

                var watch = new System.Diagnostics.Stopwatch();

                int k = 1000;
                int[] temp = new int[k];
                long avgMerge = 0;
                long avgQS = 0;
                long avgIsSorted = 0;

                while (k <= 10000000)
                {
                    
                    Console.WriteLine($"MergeSort for {k} elements");
                    //MergeSort 3x with times
                    if (k < 10000000)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            temp = new int[k];
                            for (i = 0; i < k; i++)
                            {
                                temp[i] = elements[i];
                            }

                            Console.WriteLine($"MergeSort {j}:");
                            watch.Start();

                            mergeSort(temp);

                            watch.Stop();

                            Console.WriteLine(
                                $"Execution Time of Merge Sort {j} on {k} elements: {watch.ElapsedMilliseconds * 1000000} ns");
                            avgMerge += watch.ElapsedMilliseconds * 1000000;
                            watch.Start();

                            Console.WriteLine("Is Sorted: " + flgIsSorted(temp));

                            watch.Stop();

                            avgIsSorted += watch.ElapsedMilliseconds * 1000000;

                            Console.WriteLine(
                                $"Execution Time of FlagIsSorted on Merge Sort {j} on {k} elements: {watch.ElapsedMilliseconds * 1000000} ns");
                            Console.WriteLine();

                        }
                        Console.WriteLine($"The average Merge Sort was: {avgMerge / 3} nanoseconds");
                        Console.WriteLine($"The average IsSorted on Merge Sort was: {avgIsSorted / 3} nanoseconds");

                        avgIsSorted = 0;
                        avgMerge = 0;
                    }

                    Console.WriteLine($"QuickSort for {k} elements");
                    //QuickSort 3x with times
                    for (int j = 0; j < 3; j++)
                    {
                        temp = new int[k];
                        for (i = 0; i < k; i++)
                        {
                            temp[i] = elements[i];
                        }

                        Console.WriteLine($"QuickSort {j}:");
                        watch.Start();

                        auxQuickSort(temp, 0, temp.Length - 1);

                        watch.Stop();
                        avgQS += watch.ElapsedMilliseconds * 1000000;

                        Console.WriteLine($"Execution Time of Quick Sort {j} on {k} elements: {watch.ElapsedMilliseconds * 1000000} ns");

                        watch.Start();

                        Console.WriteLine("Is Sorted: " + flgIsSorted(temp));

                        watch.Stop();

                        avgIsSorted = watch.ElapsedMilliseconds * 1000000;

                        Console.WriteLine($"Execution Time of FlagIsSorted on Quick Sort {j} on {k} elements: {watch.ElapsedMilliseconds * 1000000} ns");
                        Console.WriteLine();

                    }
                    Console.WriteLine($"The average Quick Sort was: {avgQS / 3} nanoseconds");
                    Console.WriteLine($"The average Is sorted on Quick Sort was: {avgIsSorted / 3} nanoseconds");

                    avgIsSorted = 0;
                    avgQS = 0;

                    //k incremented 1000
                    k *= 10;
                }

            }
            else
            {
                Console.WriteLine("File not found");
            }
            
            
        }

       
    }
}