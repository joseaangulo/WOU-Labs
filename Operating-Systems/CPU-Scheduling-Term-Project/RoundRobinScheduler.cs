namespace Coordinator;

public class RoundRobinScheduler : IScheduler
{
    public IList<IJob> SchedulerJobs = new List<IJob>();
    public int runningJob {get; set;}

    public void Schedule(IJob job)
    {
        //job.JobState = JobState.Waiting;
        SchedulerJobs.Add(job);
    }

    //After out of I/O queue
    public void Reschedule(IJob job)
    {
        //begin where it left off
        //Give it IO chance
        //Change bits
        SchedulerJobs.Add(job);
    }
    
    //Deque list
    public IJob? GetNextJob()
    {
       if(SchedulerJobs.Count > 0)
       {
        IJob nextJob = SchedulerJobs[0];
        SchedulerJobs.RemoveAt(0);
        return nextJob;
       }
       
       return null;
       
    }

    public bool HigherPriorityJob(IJob currentJob)
    {
        if(runningJob < 2){
        return false;
        }
        else
        {
        return true;
        }
    }

}
