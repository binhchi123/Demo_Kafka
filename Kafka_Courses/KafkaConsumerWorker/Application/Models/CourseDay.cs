namespace KafkaConsumerWorker.Application.Models
{
    public class CourseDay
    {
        public int    CourseDayId { get; set; }
        public int    CourseId    { get; set; }
        public string Content     { get; set; }
        public string Note        { get; set; }
    }
}
