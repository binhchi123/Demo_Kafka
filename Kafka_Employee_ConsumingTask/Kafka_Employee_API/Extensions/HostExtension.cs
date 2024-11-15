﻿namespace Kafka_Employee_API.Extensions
{
    public static class HostExtension
    {
        public static IHost LoadDataToMemory<TInMemoryContext, TDbContext>(this IHost host, Action<TInMemoryContext, TDbContext> seeder)
            where TDbContext : DbContext
        {
            using( var scope = host.Services.CreateScope() )
            {
                var service = scope.ServiceProvider;
                var context = service.GetService<TInMemoryContext>();
                var dbContext = service.GetService<TDbContext>();
                seeder(context, dbContext);
            }
            return host;
        }
    }
}
