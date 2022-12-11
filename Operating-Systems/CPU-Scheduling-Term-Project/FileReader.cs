
namespace Coordinator;

class FileReader
{
    public void ReadFile(string file, IList<IJob> myJobs)
    {
       // List<Job> myJobs = new List<Job>();
        if(File.Exists(file)){
        var lines = File.ReadAllLines(file);
        foreach(var line in lines)
        {
            var data = line.Split(':');
            myJobs.Add(new PriorityJob()
            {
                ProcessId = Convert.ToInt32(String.Concat(data[0].Where(c => !Char.IsWhiteSpace(c)))),
                ArrivalTime = Convert.ToInt32(String.Concat(data[1].Where(c => !Char.IsWhiteSpace(c)))),
                ServiceTime = Convert.ToInt32(String.Concat(data[2].Where(c => !Char.IsWhiteSpace(c)))),
                Priority = Convert.ToInt32(String.Concat(data[3].Where(c => !Char.IsWhiteSpace(c)))),
                JobState = JobState.New
            });
        }

       
        }
     else
        {
            Console.WriteLine("Error. File Does Not Exist");
        }

    }    
}