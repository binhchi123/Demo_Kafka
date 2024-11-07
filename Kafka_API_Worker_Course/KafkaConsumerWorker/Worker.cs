namespace KafkaConsumerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker>           _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceScopeFactory      _scopeFactory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _logger       = logger;
            _scopeFactory = scopeFactory;
            var consumer = new ConsumerConfig
            {
                BootstrapServers      = configuration["KafkaConfig:BootstrapServers"],
                GroupId               = configuration["KafkaConfig:GroupId"],
                AutoOffsetReset       = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true,
                EnableAutoCommit      = false
            };
            _consumer = new ConsumerBuilder<string, string>(consumer).Build();
            _consumer.Subscribe(configuration["KafkaConfig:Topic"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumerResult = _consumer.Consume(stoppingToken);
                        if (consumerResult != null)
                        {
                            LogMessageDetail(consumerResult);
                            var eventName = GetHeader(consumerResult.Message.Headers, "eventname");
                            _logger.LogInformation($"eventname: {eventName}");
                            if (!string.IsNullOrEmpty(eventName))
                            {
                                switch (eventName)
                                {
                                    case "InsertCourseRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data = DeserializeMessage<InsertCourseRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.CourseId} - {data.CourseName}");
                                            var course = new Course
                                            {
                                                CourseId    = data.CourseId,
                                                CourseName  = data.CourseName,
                                                Description = data.Description,
                                                Tuition     = data.Tuition,
                                                StartDay    = data.StartDay,
                                                EndDay      = data.EndDay
                                            };
                                            context.Add(course);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "InsertCourseDayRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data = DeserializeMessage<InsertCourseDayRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.CourseDayId} - {data.Content}");
                                            var courseDay = new CourseDay
                                            {
                                                CourseDayId = data.CourseDayId,
                                                CourseId    = data.CourseId,
                                                Content     = data.Content,
                                                Note        = data.Note,
                                            };
                                            context.Add(courseDay);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "InsertStudentRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data = DeserializeMessage<InsertStudentRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.StudentId} - {data.Name}");
                                            var student = new Student
                                            {
                                                StudentId     = data.StudentId,
                                                CourseId      = data.CourseId,
                                                Name          = data.Name,
                                                Birthday      = data.Birthday,
                                                NativeCountry = data.NativeCountry,
                                                Address       = data.Address,
                                                PhoneNumber   = data.PhoneNumber,
                                            };
                                            context.Add(student);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "UpdateCourseRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data = DeserializeMessage<UpdateCourseRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.CourseId} - {data.CourseName}");
                                            var course = new Course
                                            {
                                                CourseId    = data.CourseId,
                                                CourseName  = data.CourseName,
                                                Description = data.Description,
                                                Tuition     = data.Tuition,
                                                StartDay    = data.StartDay,
                                                EndDay      = data.EndDay
                                            };
                                            context.Update(course);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "UpdateCourseDayRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data = DeserializeMessage<UpdateCourseDayRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.CourseDayId} - {data.Content}");
                                            var courseDay = new CourseDay
                                            {
                                                CourseDayId = data.CourseDayId,
                                                CourseId    = data.CourseId,
                                                Content     = data.Content,
                                                Note        = data.Note,
                                            };
                                            context.Update(courseDay);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "UpdateStudentRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data = DeserializeMessage<UpdateStudentRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.StudentId} - {data.Name}");
                                            var student = new Student
                                            {
                                                StudentId     = data.StudentId,
                                                CourseId      = data.CourseId,
                                                Name          = data.Name,
                                                Birthday      = data.Birthday,
                                                NativeCountry = data.NativeCountry,
                                                Address       = data.Address,
                                                PhoneNumber   = data.PhoneNumber
                                            };
                                            context.Update(student);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "DeleteCourseRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data    = DeserializeMessage<DeleteCourseRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.CourseId}");
                                            var course = await context.Courses.FirstOrDefaultAsync(c => c.CourseId == data.CourseId);
                                            context.Remove(course);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "DeleteCourseDayRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data    = DeserializeMessage<DeleteCourseDayRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.CourseDayId}");
                                            var courseDay = await context.CourseDays.FirstOrDefaultAsync(c => c.CourseDayId == data.CourseDayId);
                                            context.Remove(courseDay);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    case "DeleteStudentRequest":
                                        using (var scope = _scopeFactory.CreateScope())
                                        {
                                            var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
                                            var data    = DeserializeMessage<DeleteStudentRequest>(consumerResult.Message.Value);
                                            _logger.LogInformation($"Data: {data.StudentId}");
                                            var student = await context.Students.FirstOrDefaultAsync(s => s.StudentId == data.StudentId);
                                            context.Remove(student);
                                            await context.SaveChangesAsync();
                                        }
                                        break;

                                    default:
                                        _logger.LogWarning($"Unhandle eventname {eventName}");
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error: {ex.Message}");
                    }
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
            finally
            {
                _consumer.Close();
            }
        }

        private void LogMessageDetail(ConsumeResult<string, string> consumerResult)
        {
            var message = consumerResult.Message;
            _logger.LogInformation(
                "Received message:\n" +
                "Key: {Key}\n" +
                "Value: {Value}\n" +
                "Topic: {Topic}\n" +
                "Partition: {Partition}\n" +
                "Offset: {Offset}\n" +
                "Timestamp: {Timestamp}\n" +
                "Headers: {Headers}",
                message.Key,
                message.Value,
                consumerResult.Topic,
                consumerResult.Partition,
                consumerResult.Offset,
                message.Timestamp.UtcDateTime,
                FormatHeaders(message.Headers)
                );
        }

        private static string GetHeader(Headers headers, string key)
        {
            if (headers.TryGetLastBytes(key, out var headerBytes))
            {
                return System.Text.Encoding.UTF8.GetString(headerBytes);
            }
            return null;
        }

        private string FormatHeaders(Headers headers)
        {
            if (headers == null || headers.Count == 0) return "None";
            var headerList = new List<string>();
            foreach (var header in headers)
            {
                headerList.Add($"{header.Key}: {System.Text.Encoding.UTF8.GetString(header.GetValueBytes())}");
            }
            return string.Join(";", headerList);
        }

        private T DeserializeMessage<T>(string message) where T : class
        {
            try
            {
                return JsonSerializer.Deserialize<T>(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to deserialize message for type {typeof(T).Name}: {ex.Message}");
                return null;
            }
        }
    }
}
