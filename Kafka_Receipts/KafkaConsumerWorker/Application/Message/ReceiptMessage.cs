namespace KafkaConsumerWorker.Application.Message
{
    public class ReceiptMessage
    {
        public string Type     { get; set; }
        public Receipt Receipt { get; set; }
    }
}
