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
                AllowAutoCreateTopics = true,
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
            catch (ProduceException<string, string> ex)
            {
                await Console.Out.WriteLineAsync($"Error producing message: {ex.Error.Reason}");
            }
        }
        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task ProduceDeleteEmployeeAsync(DeleteEmployeeRequest employee)
        {
            var message = new Message<string, string>
            {
                Key = string.Empty,
                Value = JsonSerializer.Serialize(employee),
                Headers = new Headers
                {
                    {"eventname", Encoding.UTF8.GetBytes("DeleteEmployee") }
                }
            };
            await SendMessageAsync(message);
        }

        public async Task ProduceInsertEmployeeAsync(InsertEmployeeRequest employee)
        {
            var message = new Message<string, string>
            {
                Key = string.Empty,
                Value = JsonSerializer.Serialize(employee),
                Headers = new Headers
                {
                    {"eventname", Encoding.UTF8.GetBytes("InsertEmployee") }
                }
            };
            await SendMessageAsync(message);
        }

        public async Task ProduceUpdateEmployeeAsync(UpdateEmployeeRequest employee)
        {
            var value = new UpdateEmployeeDTO
            {
                EmployeeId  = employee.EmployeeId,
                Name        = employee.Name,
                Birthday    = employee.Birthday,
                PhoneNumber = employee.PhoneNumber,
                Address     = employee.Address,
                Email       = employee.Email
            };
            var message = new Message<string, string>
            {
                Key = string.Empty,
                Value = JsonSerializer.Serialize(employee),
                Headers = new Headers
                {
                    {"eventname", Encoding.UTF8.GetBytes("UpdateEmployee") }
                }
            };
            await SendMessageAsync(message);
        }
    }
}
