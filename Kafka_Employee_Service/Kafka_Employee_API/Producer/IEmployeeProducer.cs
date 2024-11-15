namespace Kafka_Employee_API.Producer
{
    public interface IEmployeeProducer
    {
        Task SendMessageAsync(Message<string, string> message);
    }
}
