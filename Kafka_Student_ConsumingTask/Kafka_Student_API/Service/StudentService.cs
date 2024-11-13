namespace Kafka_Student_API.Service
{
    public class StudentService : IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly StudentMemory           _memory;
        private readonly IKafkaProducerManager   _producerManager;

        public StudentService(ILogger<StudentService> logger, StudentMemory memory, IKafkaProducerManager producerManager)
        {
            _logger          = logger;
            _memory          = memory;
            _producerManager = producerManager;
        }
        public Student DeleteStudent(int id)
        {
            try
            {
                if (_memory.StudentsMemory.TryGetValue(id.ToString(), out var student))
                {
                    _memory.StudentsMemory.Remove(id.ToString());
                    var kafkaProduce = _producerManager.GetProducer<string, string>("1");
                    var message = new Message<string, string>
                    {
                        Value = JsonSerializer.Serialize(student),
                        Headers = new Headers
                        {
                            {"eventname", Encoding.UTF8.GetBytes("DeleteStudent") },
                        }
                    };
                    kafkaProduce.Produce(message);
                    return student;
                }
                else
                {
                    _logger.LogWarning($"Student with ID {id} not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public List<Student> GetAllStudent()
        {
            List<Student> studentList = new List<Student>();
            try
            {
                studentList = _memory.StudentsMemory.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return studentList;
        }

        public Student InsertStudent(Student insertStudent)
        {
            try
            {
                _memory.StudentsMemory.Add(insertStudent.StudentId.ToString(), insertStudent);
                var kafkaProduce = _producerManager.GetProducer<string, string>("1");
                var message = new Message<string, string>
                {
                    Value = JsonSerializer.Serialize(insertStudent),
                    Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes("InsertStudent")},
                    }
                };
                kafkaProduce.Produce(message);
                return insertStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Student UpdateStudent(Student updateStudent)
        {
            try
            {
                if (_memory.StudentsMemory.TryGetValue(updateStudent.StudentId.ToString(), out var existingStudent))
                {
                    _memory.StudentsMemory[updateStudent.StudentId.ToString()] = updateStudent;
                    var kafkaProduce = _producerManager.GetProducer<string, string>("1");
                    var message = new Message<string, string>
                    {
                        Value = JsonSerializer.Serialize(updateStudent),
                        Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes("UpdateStudent") },
                    }
                    };
                    kafkaProduce.Produce(message);
                    return updateStudent;
                }
                else
                {
                    _logger.LogWarning($"Student with ID {updateStudent.StudentId} not found");
                    return null;
                }            
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
