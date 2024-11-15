namespace Kafka_Employee_API.Producer
{
    public class EmployeeProducer : IEmployeeProducer, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public EmployeeProducer(IConfiguration configuration)
        {
            var producer = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"],
                AllowAutoCreateTopics = true
            };
            _topic = configuration["KafkaConfig:Topic"];
            _producer = new ProducerBuilder<string, string>(producer).Build();
        }

        public async Task SendMessageAsync(Message<string, string> message)
        {
            try
            {
                var deliveryResult = await _producer.ProduceAsync(_topic, message);
                await Console.Out.WriteLineAsync($"[Producer] Deliveredd '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch(ProduceException<string, string> ex)
            {
                await Console.Out.WriteLineAsync($"Error producing message: {ex.Error.Reason}");
            }
        }

        public void Dispose()
        {
            _producer.Dispose();    
        }     
    }
}
