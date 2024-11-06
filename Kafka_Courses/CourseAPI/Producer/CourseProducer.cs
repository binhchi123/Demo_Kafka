namespace CourseAPI.Producer
{
    public class CourseProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<CourseProducer> _logger;
        private readonly string _topic;

        public CourseProducer(IConfiguration configuration, ILogger<CourseProducer> logger)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"],
                Acks = Acks.All
            };
            _topic = configuration["KafkaConfig:Topic"];
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
            _logger = logger;
        }

        public async Task ProduceAsync(string key, string value)
        {
            var message = new Message<string, string>
            {
                Key = key,
                Value = value
            };
            await _producer.ProduceAsync(_topic, message);
        }

        // Phương thức để gửi Message
        public async Task ProduceCourseMessageAsync(string action, Course course)
        {
            var courseMessage = new CourseMessage
            {
                Type = action,
                Course = course
            };

            // Chuyển đổi thành chuỗi JSON
            var messageValue = JsonSerializer.Serialize(courseMessage);
            await ProduceAsync(course.CourseId.ToString(), messageValue);
        }

        public async Task ProduceCourseDayMessageAsync(string action, CourseDay courseDay)
        {
            var courseDayMessage = new CourseDayMessage
            {
                Type = action,
                CourseDay = courseDay
            };

            // Chuyển đổi thành chuỗi JSON
            var messageValue = JsonSerializer.Serialize(courseDayMessage);
            await ProduceAsync(courseDay.CourseDayId.ToString(), messageValue);
        }

        public async Task ProduceStudentMessageAsync(string action, Student student)
        {
            var studentMessage = new StudentMessage
            {
                Type = action,
                Student = student
            };

            // Chuyển đổi thành chuỗi JSON
            var messageValue = JsonSerializer.Serialize(studentMessage);
            await ProduceAsync(student.StudentId.ToString(), messageValue);
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
