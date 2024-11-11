namespace KafkaConsumerWorker.Requests
{
    public class InsertClassRequest
    {
        public int    ClassId         { get; set; }
        public string ClassName       { get; set; }
        public int    NumberOfStudent { get; set; }
    }
}
