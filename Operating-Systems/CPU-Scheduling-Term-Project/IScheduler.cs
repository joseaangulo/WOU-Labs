namespace Coordinator;
public interface IScheduler
{
    //IList<IJob> schedulerJobs;
    int runningJob {get; set;}
    void Schedule(IJob job);
    void Reschedule(IJob job);
    IJob? GetNextJob();
    bool HigherPriorityJob(IJob job);
}