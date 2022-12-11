namespace Coordinator;


public class CoordinatorRunner
{
    public CoordinatorRunner(SchedulerType schedulerType)
    {
        _jobs = new List<IJob>();
        _scheduler = SchedulerFactory.CreateScheduler(schedulerType);
    }
    

    public void ArrivedJobs()
    {
        jobsToRun = _jobs.Where(job => job.ArrivalTime == _clock 
                                                  && job.JobState == JobState.New).ToList();
        foreach(var job in jobsToRun)
        {
            job.JobState = JobState.ReadyToRun;
            _scheduler.Schedule(job);
        }
    }
    public void InitialJobStatistics()
    {
        foreach(var job in _jobs)
        {
            List<int> newList = new List<int>
            {
                job.ProcessId,
                0, //Ready to Run
                0, //Waiting
                0, //Running
                0  //Totaltime
            };

            jobStatistics.Add(newList);
        }
    }
    public void UpdateJobStatistics()
    {
        foreach(var job in _jobs)
        {
            if(job.JobState == JobState.ReadyToRun)
            {
                foreach(List<int> list in jobStatistics)
                {
                    if(list[0] == job.ProcessId)
                    {
                        list[1]++;
                        list[4]++;
                    }
                }
            }
            else if(job.JobState == JobState.Waiting)
            {
                foreach(List<int> list in jobStatistics)
                {
                    if(list[0] == job.ProcessId)
                    {
                        list[2]++;
                        list[4]++;
                    }
                }
            }
            else if(job.JobState == JobState.Running)
            {
                foreach (List<int> list in jobStatistics)
                {
                    if (list[0] == job.ProcessId)
                    {
                        list[3]++;
                        list[4]++;
                    }
                }
            }
        }

    }

    public void PrintJobStatistics()
    {
        Console.WriteLine("Total time each process is in a scheduling state");

        foreach(List<int>? list in jobStatistics)
        {
            Console.WriteLine($"ProcessID: {list[0]}; Ready To Run: {list[1]};"
            +$" Waiting for I/O: {list[2]}; Running on CPU: {list[3]}");
        }
    }

    private IList<IJob> _jobs;
    private IScheduler _scheduler;
    public List<IJob>? jobsToRun;
    public List<List<int>> jobStatistics = new List<List<int>>(); //proccessid, ready, waiting, running, totaltime
    private int _clock = 0;
    public int totaljobs = 0;

    public int throughput = 0;
    public int shorestJobID;
    public int longestJobID;

    public void Run()
    {
        string filename = "scheduling_data.txt";

        
        FileReader fileReader = new FileReader();

        fileReader.ReadFile(filename, _jobs);
        ArrivedJobs();
        //no jobs started
        _scheduler.runningJob = 0;


        IOSystem io = new IOSystem();
        InitialJobStatistics();
       
       
        while (_jobs.Any(job => job.JobState != JobState.Completed))
        {
           
            IJob? currentJob = _scheduler.GetNextJob();
            
            while (true)
            {
                if(currentJob != null)
                {
                    currentJob.JobState = JobState.Running;
                }

                UpdateJobStatistics();

                IList<IJob> waitingJobs = io.waitingList();
                for(int i = 0; i < waitingJobs.Count; ++i)
                {
                    int status = io.IO_Complete();

                    if(status == 1)
                    {
                        waitingJobs[i].JobState = JobState.ReadyToRun;
                        _scheduler.Reschedule(waitingJobs[i]);
                        io.removeFromWaiting(waitingJobs[i]);
                    }

                }

                if(currentJob != null){
                
                
                if(currentJob.ServiceTime == 1)
                {
                    currentJob.JobState = JobState.Completed;
                    totaljobs++;
                }
                else if(_scheduler.HigherPriorityJob(currentJob))
                {
                    _scheduler.Reschedule(currentJob);
                    currentJob.JobState = JobState.ReadyToRun;
                }

                else if(currentJob.ServiceTime > 1)
                {
                    int status = io.IO_Request();

                    if(status == 1)
                    {
                        currentJob.JobState = JobState.Waiting;
                        io.addingToWaiting(currentJob);
                    }

                }
                
                currentJob.ServiceTime--;

                }

                _clock++;
                _scheduler.runningJob++;

                ArrivedJobs();

                if(currentJob == null)
                {
                    _scheduler.runningJob = 0;
                    break;
                }
                if(currentJob.JobState != JobState.Running)
                {
                    _scheduler.runningJob = 0;
                    break;
                }
            }
        }
        
        int total = 0;
        int shorestJobID = jobStatistics[0][0];
        int longestJobID = jobStatistics[0][0];
        int shortestJob = jobStatistics[0][1] + jobStatistics[0][2] + jobStatistics[0][3];
        int longestJob = jobStatistics[0][1] + jobStatistics[0][2] + jobStatistics[0][3];

        foreach(List<int> list in jobStatistics)
        {
            total += list[4];
            
            int currentJobTime = list[1] + list[2] + list[3];
            if (currentJobTime < shortestJob)
            {
                shorestJobID = list[0];
                shortestJob = currentJobTime;
            }

            if (currentJobTime > longestJob)
            {
                longestJobID = list[0];
                longestJob = currentJobTime;
            }
        }

        throughput = total / totaljobs;

         PrintJobStatistics();
         Console.WriteLine($"Total job: {totaljobs}");
         Console.WriteLine($"Time Elapsed: {_clock} ticks");
         Console.WriteLine($"Total Elapsed Time per job: {_clock / totaljobs}");
         Console.WriteLine($"Throughput: {throughput} ticks");
         Console.WriteLine($"The shortest job {shorestJobID} took {shortestJob} ticks");
         Console.WriteLine($"The longest job {longestJobID} took {longestJob} ticks");
         // main loop
    }
 }

