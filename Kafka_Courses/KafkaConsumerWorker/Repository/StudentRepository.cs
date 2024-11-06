namespace KafkaConsumerWorker.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CourseDbContext _context;
        public StudentRepository(CourseDbContext context) 
        {
            _context = context;
        }
        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountByCourseIdAsync(int courseId)
        {
            return await _context.Courses.CountAsync(c => c.CourseId == courseId);
        }

        public async Task DeleteStudentAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students.ToListAsync();   
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
