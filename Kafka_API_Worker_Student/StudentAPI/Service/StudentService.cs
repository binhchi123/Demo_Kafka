namespace StudentAPI.Service
{
    public class StudentService : IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly StudentMemory           _memory;
        private readonly IProducer               _producer;

        public StudentService(ILogger<StudentService> logger, StudentMemory memory, IProducer producer)
        {
            _logger   = logger;
            _memory   = memory;
            _producer = producer;
        }
        public Student DeleteStudent(int id)
        {
            if (_memory.StudentsMemory.TryGetValue(id.ToString(), out var student))
            {
                _memory.StudentsMemory.Remove(id.ToString());
                var request = new DeleteStudentRequest { StudentId = student.StudentId };
                _producer.Produce(request);
                return student;
            }
            else
            {
                _logger.LogWarning($"Student with ID {id} not found");
                return null;
            }
        }

        public IEnumerable<Student> GetAllStudent()
            => _memory.StudentsMemory.Values.ToList();

        public Student InsertStudent(Student insertStudent)
        {
            _memory.StudentsMemory.Add(insertStudent.StudentId.ToString(), insertStudent);
            var request = new InsertStudentRequest
            {
                StudentId = insertStudent.StudentId,
                ClassId   = insertStudent.ClassId,
                FullName  = insertStudent.FullName,
                Birthday  = insertStudent.Birthday,
                Address   = insertStudent.Address,
            };
            _producer.Produce(request);
            return insertStudent;
        }

        public Student UpdateStudent(Student updateStudent)
        {
            if (_memory.StudentsMemory.TryGetValue(updateStudent.StudentId.ToString(), out var existingStudent))
            {
                _memory.StudentsMemory[updateStudent.StudentId.ToString()] = updateStudent;
                var request = new UpdateStudentRequest
                {
                    StudentId = updateStudent.StudentId,
                    ClassId   = updateStudent.ClassId,
                    FullName  = updateStudent.FullName,
                    Birthday  = updateStudent.Birthday,
                    Address   = updateStudent.Address,
                };
                _producer.Produce(request);
            }
            else
            {
                _logger.LogWarning($"Student with ID {updateStudent.StudentId} not found");
                return null;
            }
            return updateStudent;
        }
    }
}
