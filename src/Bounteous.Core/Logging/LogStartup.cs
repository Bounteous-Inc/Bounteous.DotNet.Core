using Bounteous.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Bounteous.Core.Logging;

public static class LogStartup
{
    private static readonly object Mutex = new();
    private static bool initialized;

    public static void Initialize(IConfiguration configuration)
    {
        if (initialized) return;
        lock (Mutex)
        {
            if (initialized) return;

            if (configuration == null) return;
            var serilog = configuration.GetSection("Serilog");

            if (serilog.IsNotEmpty())
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            else
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console(outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();

            initialized = true;
        }
    }
}