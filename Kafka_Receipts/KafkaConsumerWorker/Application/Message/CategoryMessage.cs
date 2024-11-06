namespace KafkaConsumerWorker.Application.Message
{
    public class CategoryMessage
    {
        public string   Type     { get; set; }
        public Category Category { get; set; }
    }
}
