using System;

namespace Bounteous.Core.Test.Startup
{
    public interface IDependency
    {
        void RunMe();
    }

    public class DefaultDependency : IDependency
    {
        public void RunMe()
            => Console.WriteLine($"{GetType().Name} RunMe called");
    }
}