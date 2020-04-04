using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Repositories
{
    public class UserRedisRepository
    {
        private readonly IServiceScopeFactory scopeFactory;

        public UserRedisRepository(IServiceScopeFactory _scopeFactory)
        {
            scopeFactory = _scopeFactory;
        }
        
        //AccessTest
        public void AccessTest()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var distributedCache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();
                distributedCache.Remove("testkey");
                distributedCache.SetString("testkey", "test");
                string value = distributedCache.GetString("testkey");
                if (value == null)
                {
                    throw new Exception("Redis Test Failed");
                }
            }
        }

    }
}
