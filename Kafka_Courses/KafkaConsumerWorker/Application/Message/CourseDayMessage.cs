namespace KafkaConsumerWorker.Application.Message
{
    public class CourseDayMessage
    {
        public string Type { get; set; }
        public CourseDay CourseDay { get; set; }
    }
}
