// See https://aka.ms/new-console-template for more information
namespace Coordinator;

public class Program
{
    public static void Main(string[] args)
    {
        int ChoiceOfScheduler = -1;
        SchedulerType schedulerChosen = SchedulerType.ShortestJobFirst;
        while(ChoiceOfScheduler != 1 && ChoiceOfScheduler != 2 && ChoiceOfScheduler != 3){
        Console.WriteLine("What Scheduler would you like? (Enter number)");
        Console.WriteLine("\t 1. FirstInFirstOut");
        Console.WriteLine("\t 2. ShortestJobFirstScheduler");
        Console.WriteLine("\t 3. Round Robin");

        Console.Write("Input: ");

        ChoiceOfScheduler = Convert.ToInt32(Console.ReadLine());

            if(ChoiceOfScheduler != 1 && ChoiceOfScheduler != 2 && ChoiceOfScheduler != 3)
            {
            Console.WriteLine("Input wrong!!!");
            }

        }
        
        switch(ChoiceOfScheduler)
        {
            case 1:
            schedulerChosen = SchedulerType.FirstInFirstOut;
            Console.WriteLine("Running FIRST IN FIRST OUT");
            break;
            case 2:
            schedulerChosen = SchedulerType.ShortestJobFirst;
            Console.WriteLine("Running SHORTEST JOB FIRST");
            break;
            case 3:
            schedulerChosen = SchedulerType.RoundRobin;
            Console.WriteLine("Running ROUND ROBIN");
            break;

            default:
            schedulerChosen = SchedulerType.FirstInFirstOut;
            Console.WriteLine("First In First Out Automatically Chosen");
            break;
        }

        var coordinatorRunner = new CoordinatorRunner(schedulerChosen);

        coordinatorRunner.Run();
    }
}


