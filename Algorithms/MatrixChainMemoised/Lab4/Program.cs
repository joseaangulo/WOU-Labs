// C# program using memoization
using System;
class GFG
{

    static int[,] dp = new int[100, 100];

    // Function for matrix chain multiplication
    static int matrixChainMemoised(int[] p, int i, int j)
    {
        if (i == j)
        {
            return 0;
        }
        if (dp[i, j] != -1)
        {
            return dp[i, j];
        }
        dp[i, j] = Int32.MaxValue;
        for (int k = i; k < j; k++)
        {
            dp[i, j] = Math.Min(
                dp[i, j], matrixChainMemoised(p, i, k)
                          + matrixChainMemoised(p, k + 1, j)
                          + p[i - 1] * p[k] * p[j]);
        }
        return dp[i, j];
    }

    static int MatrixChainOrder(int[] p, int n)
    {
        int i = 1, j = n - 1;
        return matrixChainMemoised(p, i, j);
    }

    // Driver code
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4 };
        int n = arr.Length;

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                dp[i, j] = -1;
            }
        }

        Console.WriteLine("Minimum number of multiplications is " +
                          MatrixChainOrder(arr, n));
    }
}