namespace KafkaConsumerWorker.Requests
{
    public class InsertStudentRequest
    {
        public int      StudentId { get; set; }
        public int      ClassId   { get; set; }
        public string   FullName  { get; set; }
        public DateTime Birthday  { get; set; }
        public string   Address   { get; set; }
    }
}
