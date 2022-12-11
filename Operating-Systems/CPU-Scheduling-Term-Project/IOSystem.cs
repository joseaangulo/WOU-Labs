namespace Coordinator;

public class IOSystem
{

const int CHANCE_OF_IO_REQUEST = 10;
const int CHANCE_OF_IO_COMPLETE = 4;

Random random = new Random();
IList<IJob> waitingJobs = new List<IJob>();

public void addingToWaiting(IJob newJOb)
{
    waitingJobs.Add(newJOb);
}
public void removeFromWaiting(IJob job)
{
    waitingJobs.Remove(job);
}
public int IO_Request()
{
    int rand = random.Next();
    if(rand % CHANCE_OF_IO_REQUEST == 0)
    {
        return 1;
    }
        
    else
        return 0;
}

public int IO_Complete()
{
    int rand = random.Next();
    if(rand % CHANCE_OF_IO_COMPLETE == 0)
        return 1;
    else
        return 0;
}

public int numberOfJobs()
{
    return waitingJobs.Count;
}
public IList<IJob> waitingList()
{
    return waitingJobs;
}

}