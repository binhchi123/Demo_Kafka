﻿namespace KafkaConsumerWorker.Requests
{
    public class InsertCourseDayRequest
    {
        public int    CourseDayId { get; set; }
        public int    CourseId    { get; set; }
        public string Content     { get; set; }
        public string Note        { get; set; }
    }
}