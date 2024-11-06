namespace KafkaConsumerWorker.Application.Models
{
    public class CourseMessage
    {
        public string Type { get; set; }
        public Course Course { get; set; }
    }
}
