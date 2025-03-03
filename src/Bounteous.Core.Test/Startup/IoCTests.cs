using System;
using Bounteous.Core.TestSupport;
using Bounteous.Core.Validations;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bounteous.Core.Test.Startup
{
    public class IoCTests : IDisposable
    {
        public IoCTests() =>
            Environment.SetEnvironmentVariable(nameof(IApplicationConfig.ConnectionString), "connectMe"); public void Dispose() => Environment.SetEnvironmentVariable(nameof(IApplicationConfig.ConnectionString), null);

        [Fact]
        public void ApplicationConfig()
        {
            var appConfig = IoC.Resolve<IApplicationConfig>();

            Validate.Begin()
                .IsNotNull(appConfig, "has an app config")
                .Check()
                .IsEqual(appConfig.AllowedHosts, "*", "got allowedHosts")
                .IsEqual(appConfig.ConnectionString, "connectMe", nameof(IApplicationConfig.ConnectionString))
                .Check();
        }

        [Fact]
        public void UsingAnotherServiceCollection()
        {
            var privateCollection = new ServiceCollection();
            IoC.ConfigureServiceCollection(privateCollection);

            IoC.Resolve<IApplicationConfig>().Should().BeOfType<ApplicationConfig>();
            IoC.Resolve<IService>().Should().BeOfType<MyService>();
            IoC.Resolve<IAddMe>().Should().BeOfType<AddMe>();
        }

        [Fact]
        public void MyService()
        {
            var service = IoC.Resolve<IService>();
            service.Should().NotBeNull();
            IoC.Resolve<IAddMe>().Should().BeOfType<AddMe>();
        }

        [Fact]
        public void ResolveShouldReturnTheSameInstance()
        {
            var service = IoC.Resolve<IService>();
            IoC.Resolve<IService>().Should().BeSameAs(service);
        }

        [Fact]
        public void TryResolveWithFindsDefault()
        {
            var privateCollection = new ServiceCollection();
            IoC.ConfigureServiceCollection(privateCollection);
            
            IoC.Resolve<IAddMe>().Should().BeOfType<AddMe>();
            IoC.TryResolve<IDependency, DefaultDependency>()
                .Should().BeOfType<DefaultDependency>();
        }
    }
}