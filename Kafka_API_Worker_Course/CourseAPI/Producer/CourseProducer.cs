namespace CourseAPI.Producer
{
    public class CourseProducer : IProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<CourseProducer> _logger;
        private readonly string _topic;

        public CourseProducer(IConfiguration configuration, ILogger<CourseProducer> logger)
        {
            var producer = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"]
            };
            _topic = configuration["KafkaConfig:Topic"];
            _producer = new ProducerBuilder<string, string>(producer).Build();
            _logger = logger;
        }

        public void Produce<TRequest>(TRequest request) where TRequest : class
        {
            try
            {
                var message = new Message<string, string>()
                {
                    Value = JsonSerializer.Serialize(request),
                    Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes(typeof(TRequest).Name)}
                    }
                };
                _producer.Produce(_topic, message, deliveryReport =>
                    _logger.LogInformation($"[CourseProducer] delivered {deliveryReport.Value} to {deliveryReport.TopicPartitionOffset}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending insert product message to Kafka. {Message}", ex.ToString());
            }
        }
    }
}
