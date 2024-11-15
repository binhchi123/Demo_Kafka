namespace Kafka_Employee_API.Extensions
{
    public static class ApplicationBuilderExtensions
    { 
        public static void LoadEmployeeMemoryData(this WebApplication app)
        {
            app.LoadDataToMemory<EmployeeMemory, EmployeeDbContext>((employeeInMe, dbContext) =>
            {
                new EmployeeMemorySeedAsync().SeedAsync(employeeInMe, dbContext).Wait();
            });
        }

        public static void UseCustomKafkaMessageBus(this WebApplication app)
        {
            app.UseKafkaMessageBus(mess =>
            {
                mess.RunConsumerAsync("0");
            });
        }
    }
}
