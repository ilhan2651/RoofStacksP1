using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using System.Configuration;


namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            var configuration = serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var redisConnectionString = configuration.GetSection("Redis:ConnectionString").Value;
            serviceCollection.AddSingleton<ICacheManager>(new RedisCacheManager(redisConnectionString));
        }
    }
}
