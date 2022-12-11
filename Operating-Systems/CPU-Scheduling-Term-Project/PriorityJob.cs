
namespace Coordinator;
public class PriorityJob : IJob
{
    public int ProcessId { get; set; }
    public int ArrivalTime { get; set; }
    public int ServiceTime { get; set; }
    public int Priority { get; set; }
    public JobState JobState { get; set; }

    public override string ToString()
    {
        return $" ID: {ProcessId}; Arrival: {ArrivalTime} seconds; ServiceTime: {ServiceTime}; Priority: {Priority}";
    }
}
