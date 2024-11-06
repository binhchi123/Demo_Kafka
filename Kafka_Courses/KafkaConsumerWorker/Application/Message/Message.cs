namespace KafkaConsumerWorker.Application.Message
{
    public class Message
    {
        public string           Type      { get; set; }
        public CourseMessage    Course    { get; set; }
        public CourseDayMessage CourseDay { get; set; }
        public StudentMessage   Student   { get; set; }
    }
}
