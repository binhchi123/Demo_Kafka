namespace KafkaConsumerWorker.Requests
{
    public class InsertStudentRequest
    {
        public int      StudentId     { get; set; }
        public int      CourseId      { get; set; }
        public string   Name          { get; set; }
        public DateTime Birthday      { get; set; }
        public string   NativeCountry { get; set; }
        public string   Address       { get; set; }
        public string   PhoneNumber   { get; set; }
    }
}
