using System.Threading;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;
using System.Diagnostics;
class Program
{
    static BlockingCollection<Customer> customers = new BlockingCollection<Customer>();
    static int numberOfWorkers = 3;
    static int averageArrivalTime, averageServiceTime, simulationDuration;
    static Stopwatch stopWatch = new Stopwatch();
    static int numberOfServedCustomers = 0;
    static int numberOfCurrentCustomers = 0;
    static double averageWaitTimeInLine = 0;
    static int numberOfTotalCustomerToday = 0;
    static Semaphore workerSemaphore = new Semaphore(numberOfWorkers, numberOfWorkers);
    static Mutex totalCustomersMutex = new Mutex();
    static Mutex timeInLineMutex = new Mutex();
    static Mutex numberOfServedCustomersMutex = new Mutex();


    static void Service(object? arg)
    {
        var timeStartInLine = stopWatch.ElapsedMilliseconds;
        Console.WriteLine($"At time   {stopWatch.ElapsedMilliseconds / 1000} seconds Customer {Thread.CurrentThread.ManagedThreadId} arrives in line");


        workerSemaphore.WaitOne();
        var timeEndInLine = stopWatch.ElapsedMilliseconds;
          
        timeInLineMutex.WaitOne();
        averageWaitTimeInLine += (timeEndInLine - timeStartInLine) / 1000;
        timeInLineMutex.ReleaseMutex();

        var timeStartOfService = stopWatch.ElapsedMilliseconds;
        Console.WriteLine($"At time   {stopWatch.ElapsedMilliseconds / 1000} seconds Customer {Thread.CurrentThread.ManagedThreadId} starts being served");
        Thread.Sleep(averageServiceTime);
        var timeEndOfService = stopWatch.ElapsedMilliseconds;
        Console.WriteLine($"At time   {stopWatch.ElapsedMilliseconds / 1000} seconds Customer {Thread.CurrentThread.ManagedThreadId} leaves the food cart");
        
        numberOfServedCustomersMutex.WaitOne();
        numberOfServedCustomers++;
        numberOfServedCustomersMutex.ReleaseMutex();

        totalCustomersMutex.WaitOne();
        numberOfCurrentCustomers--;
        totalCustomersMutex.ReleaseMutex();
        
        workerSemaphore.Release();
        
        
    }

    static void QueueCustomers(object? arg)
    {  
        
        while(stopWatch.ElapsedMilliseconds < simulationDuration)
        {
            
            if(numberOfTotalCustomerToday > 2){
                Thread.Sleep(averageArrivalTime);
            }

            if(stopWatch.ElapsedMilliseconds < simulationDuration)
            {
                totalCustomersMutex.WaitOne();
            numberOfCurrentCustomers++;
            numberOfTotalCustomerToday++;
            totalCustomersMutex.ReleaseMutex();
            
            var success = customers.TryAdd(new Customer { CustomerThread = new Thread(new ParameterizedThreadStart(Service))});
            }
            
        }
    }

    public static void Main(string[] args)
    {

        Console.Write("What is the average arrival time in seconds as a multiple of 10? ");
        averageArrivalTime = Convert.ToInt32(Console.ReadLine()) * 1000;

        Console.Write("What is the average service time in seconds as a multiple of 10? ");
        averageServiceTime = Convert.ToInt32(Console.ReadLine()) * 1000;

        Console.Write("How long will this simulation run in seconds as a multiple of 10? ");
        simulationDuration = Convert.ToInt32(Console.ReadLine()) * 1000;
        
        stopWatch.Start();

        var queueThread = new Thread(new ParameterizedThreadStart(QueueCustomers));
        queueThread.Start();

        while(stopWatch.ElapsedMilliseconds < simulationDuration)
        {
            if(customers.TryTake(out var customer)){
                customer?.CustomerThread?.Start(null);
            }
        }

        while(customers.TryTake(out var customer) || numberOfCurrentCustomers > 0)
        {
            customer?.CustomerThread?.Join();
        }

        queueThread.Join();

    
            Console.WriteLine($"Simulation terminated after {numberOfServedCustomers} customers served");
            Console.WriteLine($"Average waiting time = {(averageWaitTimeInLine / numberOfServedCustomers)} seconds");
    }

}