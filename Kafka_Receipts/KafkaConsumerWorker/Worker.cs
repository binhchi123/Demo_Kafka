namespace KafkaConsumerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ApplicationDbContext _context;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers      = configuration["KafkaConfig:BootstrapServers"],
                GroupId               = configuration["KafkaConfig:GroupId"],
                AutoOffsetReset       = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true,
                EnableAutoCommit      = false,
            };

            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _consumer.Subscribe(configuration["KafkaConfig:Topic"]);
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Received message: {cr.Value}");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
                        var ingredientService = scope.ServiceProvider.GetRequiredService<IIngredientService>();
                        var receiptService = scope.ServiceProvider.GetRequiredService<IReceiptService>();

                        // Deserialize the message to Message
                        var message = JsonSerializer.Deserialize<Message>(cr.Value);
                        string messageType = message.Type;

                        switch (messageType)
                        {
                            case "Category":
                                var categoryMessage = JsonSerializer.Deserialize<CategoryMessage>(cr.Value);
                                var categoryDTO = new CategoryDTO
                                {
                                    CategoryId   = categoryMessage.Category.CategoryId,
                                    CategoryName = categoryMessage.Category.CategoryName,
                                    Description  = categoryMessage.Category.Description,
                                };
                                switch (categoryMessage.Type)
                                {
                                    case "Add":
                                        await categoryService.AddCategoryAsync(categoryDTO);
                                        break;
                                    case "Update":
                                        await categoryService.UpdateCategoryAsync(categoryDTO);
                                        break;
                                    case "Delete":
                                        await categoryService.DeleteCategoryAsync(categoryDTO.CategoryId);
                                        break;
                                    default:
                                        _logger.LogWarning($"Unknown action: {categoryMessage.Type}");
                                        break;
                                }
                                break;

                            case "Ingredient":
                                var ingredientMessage = JsonSerializer.Deserialize<IngredientMessage>(cr.Value);
                                var ingredientDTO = new IngredientDTO
                                {
                                    IngredientId   = ingredientMessage.Ingredient.IngredientId,
                                    CategoryId     = ingredientMessage.Ingredient.CategoryId,
                                    IngredientName = ingredientMessage.Ingredient.IngredientName,
                                    Price          = ingredientMessage.Ingredient.Price,
                                    Unit           = ingredientMessage.Ingredient.Unit,
                                    Quantity       = ingredientMessage.Ingredient.Quantity,
                                };
                                switch (ingredientMessage.Type)
                                {
                                    case "Add":
                                        await ingredientService.AddIngredientAsync(ingredientDTO);
                                        break;
                                    case "Update":
                                        await ingredientService.UpdateIngredientAsync(ingredientDTO);
                                        break;
                                    case "Delete":
                                        await ingredientService.DeleteIngredientAsync(ingredientDTO.IngredientId);
                                        break;
                                    default:
                                        _logger.LogWarning($"Unknown action: {ingredientMessage.Type}");
                                        break;
                                }
                                break;

                            case "Receipt":
                                var receiptMessage = JsonSerializer.Deserialize<ReceiptMessage>(cr.Value);
                                var receiptDTO = new ReceiptDTO
                                {
                                    ReceiptId    = receiptMessage.Receipt.ReceiptId,
                                    CreatedDate  = receiptMessage.Receipt.CreatedDate,
                                    EmployeeName = receiptMessage.Receipt.EmployeeName,
                                    Note         = receiptMessage.Receipt.Note,
                                    TotalAmout   = receiptMessage.Receipt.TotalAmout,
                                };
                                switch (receiptMessage.Type)
                                {
                                    case "Add":
                                        await receiptService.AddReceiptAsync(receiptDTO);
                                        break;
                                    case "Update":
                                        await receiptService.UpdateReceiptAsync(receiptDTO);
                                        break;
                                    case "Delete":
                                        await receiptService.DeleteReceiptAsync(receiptDTO.ReceiptId);
                                        break;
                                    default:
                                        _logger.LogWarning($"Unknown action: {receiptMessage.Type}");
                                        break;
                                }
                                break;
                        } 
                    }

                    _consumer.Commit(cr);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the message.");
                }
            }
        }
    }
}
