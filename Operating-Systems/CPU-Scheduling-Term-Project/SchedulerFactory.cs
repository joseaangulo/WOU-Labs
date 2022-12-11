namespace Coordinator;
public static class SchedulerFactory
{
    public static IScheduler CreateScheduler(SchedulerType schedulerType)
    {
        switch (schedulerType)
        {
            case SchedulerType.ShortestJobFirst:
                return new ShortestJobFirstScheduler();

            case SchedulerType.FirstInFirstOut:
                return new FirstInFirstOutScheduler();
            
            case SchedulerType.RoundRobin:
                return new RoundRobinScheduler();
            default:
                throw new ArgumentOutOfRangeException(nameof(schedulerType), 
                schedulerType, 
                null);
        }
    }

}