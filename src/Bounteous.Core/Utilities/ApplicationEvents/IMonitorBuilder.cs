namespace Bounteous.Core.Utilities.ApplicationEvents;

public interface IMonitorBuilder
{
    IEventMonitor Begin(string user, string operation, string details = null, int acceptableDurationMilliseconds = 2);
}