using System.Collections.Generic;
using System.Reflection;
using Sims.CheatEngine.Services;
using WebToolkit.Contracts;
using DataServiceRegistration = Sims.CheatEngine.Data.ServiceRegistration;
namespace Sims.CheatEngine.Broker
{
    public class ServiceBroker : IServiceBroker
    {
        public IEnumerable<Assembly> GetServiceAssemblies()
        {
            return new []
            {
                Assembly.GetAssembly(typeof(DataServiceRegistration)),
                Assembly.GetAssembly(typeof(ServiceRegistration))
            };
        }
    }
}