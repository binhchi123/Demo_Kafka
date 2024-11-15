namespace Kafka_Employee_API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseOracle(configuration.GetConnectionString("OrclDb"));
            });
            return services;    
        }

        public static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            services.AddSingleton<EmployeeMemory>();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IEmployeePersistenceService, EmployeePersistenceService>();
            services.AddSingleton<IEmployeeProducer, EmployeeProducer>();

            return services;
        }

        public static IServiceCollection AddKafkaServices(this IServiceCollection services, AppSetting appSetting)
        {
            services.AddKafkaProducers(builder =>
            {
                builder.AddProducer(appSetting.GetProducerSetting("1"));
            });

            services.AddKafkaConsumers(builder =>
            {
                builder.AddConsumer<EmployeeConsumingTask>(appSetting.GetConsumerSetting("0"));
            });

            return services;
        }
    }
}
