namespace Kafka_Student_API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StudentDbContext>(options =>
            {
                options.UseOracle(configuration.GetConnectionString("OrclDb"));
            });
            return services;
        }

        public static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            services.AddSingleton<StudentMemory>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<IStudentPersistenceService, StudentPersistenceService>();
            services.AddSingleton<IStudentProducer, StudentProducer>();

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
                builder.AddConsumer<StudentConsumingTask>(appSetting.GetConsumerSetting("0"));
            });

            return services;
        }
    }
}
