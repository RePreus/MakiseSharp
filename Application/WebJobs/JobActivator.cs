using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;

namespace MakiseSharp.Application.WebJobs
{
    public class JobActivator : IJobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public JobActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T CreateInstance<T>()
        {
            var instance = serviceProvider.GetRequiredService(typeof(T));
            return (T)instance;
        }
    }
}
