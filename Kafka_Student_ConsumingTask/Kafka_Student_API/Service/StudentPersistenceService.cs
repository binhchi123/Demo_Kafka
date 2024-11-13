namespace Kafka_Student_API.Service
{
    public class StudentPersistenceService : IStudentPersistenceService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<StudentConsumingTask> _logger;

        public StudentPersistenceService(IServiceScopeFactory scopeFactory, ILogger<StudentConsumingTask> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        public async Task<Student> DeleteStudent(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
                try
                {
                    var student = await dbContext.Students.FindAsync(id);
                    if (student == null)
                    {
                        _logger.LogWarning($"Student with ID {id} not found");
                        return null;
                    }
                    dbContext.Students.Remove(student);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Student with ID {id} delete");
                    return student;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<Student> InsertStudent(Student insertStudent)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
                try
                {
                    dbContext.Students.Add(insertStudent);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return insertStudent;
        }

        public async Task<Student> UpdateStudent(Student updateStudent)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
                try
                {
                    var student = await dbContext.Students.FindAsync(updateStudent.StudentId);
                    if (student == null)
                    {
                        _logger.LogWarning($"Student with ID {updateStudent.StudentId} not found");
                        return null;
                    }
                    student.Name     = updateStudent.Name;
                    student.Birthday = updateStudent.Birthday;
                    student.Address  = updateStudent.Address;
                    dbContext.Update(student);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Student with ID {updateStudent.StudentId} delete");
                    return student;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
