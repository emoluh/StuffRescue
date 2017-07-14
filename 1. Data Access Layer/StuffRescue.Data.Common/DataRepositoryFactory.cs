using System;
using Core.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StuffRescue.Business.Bootstrapper;

namespace StuffRescue.Data.Common
{
    /// <summary>
    /// The responsibility is to go and find me a DataRepository
    /// as long as what I am sending in T is IDataRepository 
    /// implementer
    /// </summary>
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public T GetDataRepository<T>() where T : IDataRepository
        {

            IServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder();

            IConfigurationRoot Configuration = builder.Build(); 

            services = services.Init(Configuration);

            // create service provider
            IServiceProvider provider = services.BuildServiceProvider();

            return provider.GetService<T>();
        }
    }
}
