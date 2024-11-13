namespace Kafka_Student_API.Requests
{
    public class InsertStudentRequest
    {
        public int      StudentId { get; set; }
        public string   Name      { get; set; }
        public DateTime Birthday  { get; set; }
        public string   Address   { get; set; }
    }
}
