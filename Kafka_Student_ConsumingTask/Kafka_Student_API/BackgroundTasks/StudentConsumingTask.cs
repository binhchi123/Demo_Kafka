namespace Kafka_Student_API.BackgroundTasks
{
    public class StudentConsumingTask : IConsumingTask<string, string>
    {
        private readonly ILogger<StudentConsumingTask> _logger;
        private readonly IStudentPersistenceService _studentService;

        public StudentConsumingTask(ILogger<StudentConsumingTask> logger, IStudentPersistenceService studentService)
        {
            _logger         = logger;
            _studentService = studentService;
        }
        public async Task ExecuteAsync(ConsumeResult<string, string> result)
        {
            var studentEvent = result.Message.Headers.FirstOrDefault(h => h.Key == "eventname")?.GetValueBytes();
            if (studentEvent == null) return;
            var eventString = Encoding.UTF8.GetString(studentEvent);
            _logger.LogInformation($"Consuming message from topic: {result.Topic}, partition: {result.Partition}, offset: {result.Offset}, key: {result.Message.Key}");
            switch (eventString) {
                case "InsertStudent":
                    var student = JsonSerializer.Deserialize<Student>(result.Message.Value);
                    await _studentService.InsertStudent(student);
                    break;
                case "UpdateStudent":
                    var stu = JsonSerializer.Deserialize<UpdateStudentDTO>(result.Message.Value);
                    _logger.LogInformation("Updating student for studentId: {StudentId}, name: {Name}, birthday: {Birthday}, address: {Address}", 
                        stu.StudentId, stu.Name, stu.Birthday, stu.Address);
                    await _studentService.UpdateStudent(new Student
                    {
                        StudentId = stu.StudentId,
                        Name      = stu.Name,
                        Birthday  = stu.Birthday,
                        Address   = stu.Address,
                    });
                    break;
                case "DeleteStudent":
                    var request = JsonSerializer.Deserialize<DeleteStudentRequest>(result.Message.Value);
                    _logger.LogInformation($"Deleting student with ID {request.StudentId}");
                    await _studentService.DeleteStudent(request.StudentId);
                    break;
                default:
                    _logger.LogWarning("Received unknown event: {Event}", studentEvent);
                    break;
            }
        }
    }
}
