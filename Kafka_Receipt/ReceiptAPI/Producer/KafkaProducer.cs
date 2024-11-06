namespace ReceiptAPI.Producer
{
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;
        private readonly string _topic;

        public KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"],
                Acks = Acks.All
            };
            _topic = configuration["KafkaConfig:Topic"];
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            _logger = logger;
        }

        public async Task ProduceAsync(string key, string value)
        {
            var message = new Message<string, string>
            {
                Key = key,
                Value = value
            };
            await _producer.ProduceAsync(_topic, message);
        }

        // Phương thức để gửi CategoryMessage
        public async Task ProduceCategoryMessageAsync(string action, Category category)
        {
            var categoryMessage = new CategoryMessage
            {
                Type = action,
                Category = category
            };

            // Chuyển đổi thành chuỗi JSON
            var messageValue = JsonSerializer.Serialize(categoryMessage);
            await ProduceAsync(category.CategoryId.ToString(), messageValue);
        }

        // Phương thức để gửi IngredientMessage
        public async Task ProduceIngredientMessageAsync(string action, Ingredient ingredient)
        {
            var ingredientMessage = new IngredientMessage
            {
                Type = action,
                Ingredient = ingredient
            };

            // Chuyển đổi thành chuỗi JSON
            var messageValue = JsonSerializer.Serialize(ingredientMessage);
            await ProduceAsync(ingredient.IngredientId.ToString(), messageValue);
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
