namespace Kafka_Student_API.Producer
{
    public class StudentProducer : IStudentProducer, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public StudentProducer(IConfiguration configuration)
        {
            var producer = new ProducerConfig
            {
                BootstrapServers      = configuration["KafkaConfig:BootstrapServers"],
                AllowAutoCreateTopics = true
            };
            _topic = configuration["KafkaConfig:Topic"];
            _producer = new ProducerBuilder<string, string>(producer).Build();
        }

        public async Task SendmessageAsync(Message<string, string> message)
        {
            try
            {
                var deliveryResult = await _producer.ProduceAsync(_topic, message);
                await Console.Out.WriteLineAsync($"[Producer] Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch (ProduceException<string, string> ex)
            {
                await Console.Out.WriteLineAsync($"Error producing message: {ex.Error.Reason}");
            }
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task ProduceDeleteStudentAsync(DeleteStudentRequest student)
        {
            var message = new Message<string, string>
            {
                Key     = string.Empty,
                Value   = JsonSerializer.Serialize(student),
                Headers = new Headers
                {
                    {"eventname", Encoding.UTF8.GetBytes("DeleteStudent") }
                }
            };
            await SendmessageAsync(message);
        }

        public async Task ProduceInsertStudentAsync(InsertStudentRequest student)
        {
            var message = new Message<string, string>
            {
                Key     = string.Empty,
                Value   = JsonSerializer.Serialize(student),
                Headers = new Headers
                {
                    {"eventname", Encoding.UTF8.GetBytes("InsertStudent") }
                }
            };
            await SendmessageAsync(message);
        }

        public async Task ProduceUpdateStudentAsync(UpdateStudentRequest student)
        {
            var value = new UpdateStudentDTO
            {
                StudentId = student.StudentId,
                Name      = student.Name,
                Birthday  = student.Birthday,
                Address   = student.Address
            };
            var message = new Message<string, string>
            {
                Key     = string.Empty,
                Value   = JsonSerializer.Serialize(student),
                Headers = new Headers
                {
                    {"eventname", Encoding.UTF8.GetBytes("UpdateStudent") }
                }
            };
            await SendmessageAsync(message);
        }
    }
}
