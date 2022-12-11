namespace Coordinator;

public class ShortestJobFirstScheduler : IScheduler
{
    public void Sort(IList<IJob> jobs)
    {
        for (int i = 0; i < jobs.Count - 1; ++i)
        {
            for(int j = 0; j < jobs.Count - i - 1; ++j){
                if (jobs[j].ServiceTime > jobs[j + 1].ServiceTime)
                {
                    IJob temp = jobs[j];
                    jobs[j] = jobs[j + 1];
                    jobs[j + 1] = temp;
                }
                else if (jobs[j].ServiceTime == jobs[j + 1].ServiceTime && jobs[j].ProcessId > jobs[j + 1].ProcessId)
                {
                    IJob temp = jobs[j];
                    jobs[j] = jobs[j + 1];
                    jobs[j + 1] = temp;
                }
            }
        }

    }
    public IList<IJob> SchedulerJobs = new List<IJob>();
    public int runningJob {get; set;}
    
    public IJob? GetNextJob()
    {
        if(SchedulerJobs.Count > 0)
        {
            Sort(SchedulerJobs);
            IJob nextJob = SchedulerJobs[0];
            SchedulerJobs.RemoveAt(0);
            return nextJob;
        }
       
        return null;
    }

    public void Reschedule(IJob job)
    {
        SchedulerJobs.Add(job);
    }

    public void Schedule(IJob job)
    {
        SchedulerJobs.Add(job);
    }

    public bool HigherPriorityJob(IJob currentJob)
    {
        Sort(SchedulerJobs);
        if(SchedulerJobs.Count > 0){
        if((currentJob.ServiceTime - 1) > SchedulerJobs[0].ServiceTime)
        {
            return true;
        }

        else if((currentJob.ServiceTime - 1) == SchedulerJobs[0].ServiceTime && currentJob.ProcessId > SchedulerJobs[0].ProcessId)
        {
            return true;
        }
        else
        {
            return false;
        }
        }

        return false;


    }
}
