namespace Kafka_Employee_API.Extensions
{
    public static class ApplicationBuilderExtensions
    { 
        public static void UseCustomKafkaMessageBus(this WebApplication app)
        {
            app.UseKafkaMessageBus(mess =>
            {
                mess.RunConsumerAsync("0");
            });
        }
    }
}
