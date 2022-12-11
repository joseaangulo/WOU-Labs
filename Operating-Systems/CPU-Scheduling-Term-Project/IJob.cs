namespace Coordinator;
public interface IJob
{
    int ProcessId { get; set;}
    int ArrivalTime {get; set;}
    int ServiceTime {get; set;}
    JobState JobState {get; set; }
}