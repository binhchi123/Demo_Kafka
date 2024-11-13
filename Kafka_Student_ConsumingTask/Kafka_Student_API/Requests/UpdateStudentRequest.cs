namespace Kafka_Student_API.Requests
{
    public class UpdateStudentRequest
    {
        public int      StudentId { get; set; }
        public string   Name      { get; set; }
        public DateTime Birthday  { get; set; }
        public string   Address   { get; set; }
    }
}
