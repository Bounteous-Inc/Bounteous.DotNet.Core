using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Bounteous.Core;

public class ApplicationConfigurationBuilder<T> where T : IApplicationConfigBase, new()
{
    private IConfiguration configuration;

    public IConfiguration Configuration
    {
        get
        {
            var stageName = Environment.GetEnvironmentVariable("stageName");
            return configuration ??= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{stageName}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }

    public T Build()
    {
        var appConfig = new T();
        Configuration.Bind(appConfig);
        return Build(configuration, appConfig);
    }

    protected virtual T Build(IConfiguration config, T appConfig) => appConfig;
}