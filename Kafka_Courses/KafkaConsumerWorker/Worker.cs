namespace KafkaConsumerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers      = configuration["KafkaConfig:BootstrapServers"],
                GroupId               = configuration["KafkaConfig:GroupId"],
                AutoOffsetReset       = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true,
                EnableAutoCommit      = false,
            };

            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _consumer.Subscribe(configuration["KafkaConfig:Topic"]);
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Received message: {cr.Value}");
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();
                        var courseDayService = scope.ServiceProvider.GetRequiredService<ICourseDayService>();
                        var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();

                        // Deserialize the message to Message
                        var message = JsonSerializer.Deserialize<Message>(cr.Value);
                        string messageType = message.Type;

                        switch (messageType)
                        {
                            case "Course":
                                var courseMessage = JsonSerializer.Deserialize<CourseMessage>(cr.Value);
                                var courseDTO = new CourseDTO
                                {
                                    CourseId    = courseMessage.Course.CourseId,
                                    CourseName  = courseMessage.Course.CourseName,
                                    Description = courseMessage.Course.Description,
                                    Tuition     = courseMessage.Course.Tuition,
                                    StartDay    = courseMessage.Course.StartDay,
                                    EndDay      = courseMessage.Course.EndDay
                                };
                                switch (courseMessage.Type)
                                {
                                    case "Add":
                                        await courseService.AddCourseAsync(courseDTO);
                                        break;
                                    case "Update":
                                        await courseService.UpdateCourseAsync(courseDTO);
                                        break;
                                    case "Delete":
                                        await courseService.DeleteCourseAsync(courseDTO.CourseId);
                                        break;
                                    default:
                                        _logger.LogWarning($"Unknown action: {courseMessage.Type}");
                                        break;
                                }
                                break;

                            case "CourseDay":
                                var courseDayMessage = JsonSerializer.Deserialize<CourseDayMessage>(cr.Value);
                                var courseDayDTO = new CourseDayDTO
                                {
                                    CourseDayId = courseDayMessage.CourseDay.CourseDayId,
                                    CourseId    = courseDayMessage.CourseDay.CourseId,
                                    Content     = courseDayMessage.CourseDay.Content,
                                    Note        = courseDayMessage.CourseDay.Note,
                                };
                                switch (courseDayMessage.Type)
                                {
                                    case "Add":
                                        await courseDayService.AddCourseDayAsync(courseDayDTO);
                                        break;
                                    case "Update":
                                        await courseDayService.UpdateCourseDayAsync(courseDayDTO);
                                        break;
                                    case "Delete":
                                        await courseDayService.DeleteCourseDayAsync(courseDayDTO.CourseDayId);
                                        break;
                                    default:
                                        _logger.LogWarning($"Unknown action: {courseDayMessage.Type}");
                                        break;
                                }
                                break;

                            case "Student":
                                var studentMessage = JsonSerializer.Deserialize<StudentMessage>(cr.Value);
                                var studentDTO = new StudentDTO
                                {
                                    StudentId      = studentMessage.Student.StudentId,
                                    CourseId       = studentMessage.Student.CourseId,
                                    Name           = studentMessage.Student.Name,
                                    Birthday       = studentMessage.Student.Birthday,
                                    NativeCountry  = studentMessage.Student.NativeCountry,
                                    Address        = studentMessage.Student.Address,
                                    PhoneNumber    = studentMessage.Student.PhoneNumber,
                                };
                                switch (studentMessage.Type)
                                {
                                    case "Add":
                                        await studentService.AddStudentAsync(studentDTO);
                                        break;
                                    case "Update":
                                        await studentService.UpdateStudentAsync(studentDTO);
                                        break;
                                    case "Delete":
                                        await studentService.DeleteStudentAsync(studentDTO.StudentId);
                                        break;
                                    default:
                                        _logger.LogWarning($"Unknown action: {studentMessage.Type}");
                                        break;
                                }
                                break;
                        }
                    }

                    _consumer.Commit(cr); 
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the message.");
                }
            }
        }

    }
}
