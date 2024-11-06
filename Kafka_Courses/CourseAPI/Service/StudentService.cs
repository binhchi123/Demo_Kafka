
namespace CourseAPI.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository  _courseRepository;
        private readonly CourseProducer     _courseProducer;
        public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository, CourseProducer courseProducer)
        {
            _studentRepository = studentRepository;
            _courseRepository  = courseRepository;
            _courseProducer    = courseProducer;
        }
        public async Task AddStudentAsync(StudentDTO createStudentDTO)
        {
            var courseId = await _courseRepository.GetCourseByIdAsync(createStudentDTO.CourseId);
            if (courseId == null)
            {
                throw new ArgumentException("Khóa học không tồn tại");
            }
            var newStudent = new Student
            {
                CourseId      = createStudentDTO.CourseId,
                Name          = createStudentDTO.Name,
                Birthday      = createStudentDTO.Birthday,
                NativeCountry = createStudentDTO.NativeCountry,
                Address       = createStudentDTO.Address,
                PhoneNumber   = createStudentDTO.PhoneNumber,
            };

            await _studentRepository.AddStudentAsync(newStudent);
            var studentMessage = new StudentMessage
            {
                Type = "Add",
                Student = newStudent
            };
            await _courseProducer.ProduceStudentMessageAsync("Add", newStudent);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var existingStudent = await _studentRepository.GetStudentByIdAsync(studentId);
            if (existingStudent != null) {
                throw new ArgumentException("Học viên không tồn tại");
            }
            await _studentRepository.DeleteStudentAsync(existingStudent);
            var studentMessage = new StudentMessage
            {
                Type = "Delete",
                Student = existingStudent
            };
            await _courseProducer.ProduceStudentMessageAsync("Delete", existingStudent);
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _studentRepository.GetAllStudentAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _studentRepository.GetStudentByIdAsync(studentId);
        }

        public async Task UpdateStudentAsync(StudentDTO updateStudentDTO)
        {
            var studentId = updateStudentDTO.StudentId;
            var existingStudent = await _studentRepository.GetStudentByIdAsync(studentId);
            if (existingStudent == null)
            {
                throw new ArgumentException("Học viên không tồn tại");
            }
            existingStudent.CourseId    = updateStudentDTO.CourseId;
            existingStudent.Name        = updateStudentDTO.Name;
            existingStudent.Birthday    = updateStudentDTO.Birthday;
            existingStudent.Address     = updateStudentDTO.Address;
            existingStudent.PhoneNumber = updateStudentDTO.PhoneNumber;

            await _studentRepository.UpdateStudentAsync(existingStudent);
            var studentMessage = new StudentMessage
            {
                Type = "Update",
                Student = existingStudent
            };
            await _courseProducer.ProduceStudentMessageAsync("Update", existingStudent);
        }
    }
}
