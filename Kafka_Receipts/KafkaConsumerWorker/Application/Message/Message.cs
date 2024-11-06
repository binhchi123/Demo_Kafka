namespace KafkaConsumerWorker.Application.Message
{
    class Message
    {
        public string            Type       { get; set; }
        public CategoryMessage   Category   { get; set; }
        public IngredientMessage Ingredient { get; set; }
        public ReceiptMessage    Receipt    { get; set; }
    }
}
