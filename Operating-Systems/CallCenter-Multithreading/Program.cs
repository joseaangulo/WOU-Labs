

using System.Runtime.CompilerServices;

public class Program
{
    private static Random number = new Random();

    private static int totalCallTime = 
    0;
    private static double callWaitTime = 0;

    private static int numberOfCustomers = 20;
    private static int numberOfWorkers = 20;

    private static Mutex totalTimeMutex = new Mutex();
    static Mutex workerMutex = new Mutex();
    static Semaphore workerSemaphore = new Semaphore(numberOfWorkers, numberOfWorkers);

    static void TakeCall(object arg)
    {
        var arrivalTime = DateTime.Now;
        int callInTime = number.Next(2000, 5000);
        Thread.Sleep(callInTime);

        Console.WriteLine($"Caller {Thread.CurrentThread.ManagedThreadId} is calling");

        int callTime = number.Next(2000, 5000);
        // Wait for a worker to become available

        workerSemaphore.WaitOne();

        var callStart = DateTime.Now;

        totalTimeMutex.WaitOne();
        callWaitTime += (callStart - arrivalTime).Seconds;
        totalTimeMutex.ReleaseMutex();

        Console.WriteLine("Worker {0} is taking a call", Convert.ToInt32(arg));
        
        
        Thread.Sleep(callTime);

        // Release the worker
        workerSemaphore.Release();

        totalCallTime = (DateTime.Now - callStart).Seconds;


        Console.WriteLine($"Caller {Thread.CurrentThread.ManagedThreadId} call over");

    }

    public static void Main(string[] args)
    {
        var callers = new List<Caller>();
        for (var i = 0; i < numberOfCustomers; i++)
        {
            callers.Add(
                new Caller
                {
                    CallerThread = new Thread(new ParameterizedThreadStart(TakeCall))
                });
        }


        foreach (var caller in callers)
        {
            int callInTime = number.Next(2000, 5000);
            Thread.Sleep(callInTime);

            caller?.CallerThread?.Start(null);
        }

        foreach (var caller in callers)
        {
            caller.CallerThread?.Join();
        }

        Console.WriteLine($"Average time for callers to reach workers: {callWaitTime / callers.Count} seconds\n" +
                          $"for workers {numberOfWorkers} and customers {numberOfCustomers}");

    }
}




