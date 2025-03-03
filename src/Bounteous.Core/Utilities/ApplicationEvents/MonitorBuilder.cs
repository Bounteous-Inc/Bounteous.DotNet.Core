namespace Bounteous.Core.Utilities.ApplicationEvents;

public class MonitorBuilder : IMonitorBuilder
{
    private readonly IEventSink sink;

    public MonitorBuilder(IEventSink sink)
    {
        this.sink = sink;
    }

    public IEventMonitor Begin(string user, string operation, string details = null,
        int acceptableDurationMilliseconds = 2000)
    {
        return new EventMonitor(user, operation, details, acceptableDurationMilliseconds, sink);
    }
}