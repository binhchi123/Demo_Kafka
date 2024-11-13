namespace Kafka_Student_API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void LoadStudentMemoryData(this WebApplication app)
        {
            app.LoadDataToMemory<StudentMemory, StudentDbContext>((studentInMe, dbContext) =>
            {
                new StudentMemorySeedAsync().SeedAsync(studentInMe, dbContext).Wait();
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
