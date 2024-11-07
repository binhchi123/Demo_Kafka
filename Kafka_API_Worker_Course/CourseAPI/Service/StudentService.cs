namespace CourseAPI.Service
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
                var request = new DeleteStudentRequest { StudentId = id };
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
            if (insertStudent.CourseId <= 0)
            {
                throw new ArgumentException($"Invalid CourseId: {insertStudent.CourseId}");
            }
            _memory.StudentsMemory.Add(insertStudent.StudentId.ToString(), insertStudent);
            var request = new InsertStudentRequest
            {
                StudentId     = insertStudent.StudentId,
                CourseId      = insertStudent.CourseId,
                Name          = insertStudent.Name,
                Birthday      = insertStudent.Birthday,
                NativeCountry = insertStudent.NativeCountry,
                Address       = insertStudent.Address,
                PhoneNumber   = insertStudent.PhoneNumber
            };
            _producer.Produce(request);
            return insertStudent;
        }

        public Student UpdateStudent(Student updateStudent)
        {
            if (updateStudent.CourseId <= 0)
            {
                throw new ArgumentException($"Invalid CourseId: {updateStudent.CourseId}");
            }
            if (_memory.StudentsMemory.TryGetValue(updateStudent.StudentId.ToString(), out var existingStudent))
            {
                _memory.StudentsMemory[updateStudent.StudentId.ToString()] = updateStudent;
                var request = new UpdateStudentRequest
                {
                    StudentId     = updateStudent.StudentId,
                    CourseId      = updateStudent.CourseId,
                    Name          = updateStudent.Name,
                    Birthday      = updateStudent.Birthday,
                    NativeCountry = updateStudent.NativeCountry,
                    Address       = updateStudent.Address,
                    PhoneNumber   = updateStudent.PhoneNumber
                };
                _producer.Produce(request);
                return updateStudent;
            }
            else
            {
                _logger.LogWarning($"Student with ID {updateStudent.StudentId} not found");
                return null;
            }
        }
    }
}
