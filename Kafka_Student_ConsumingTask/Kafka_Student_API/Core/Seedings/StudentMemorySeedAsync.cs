namespace Kafka_Student_API.Core.Seedings
{
    public class StudentMemorySeedAsync
    {
        public async Task SeedAsync(StudentMemory memory, StudentDbContext context)
        {
            var students = await context.Students.ToListAsync();
            if (students.Count > 0)
            {
                foreach (var s in students)
                {
                    memory.StudentsMemory.Add(s.StudentId.ToString(), s);
                }
            }
        }
    }
}
