using StudentAPI.Infrastructure.Models;

namespace StudentAPI.Infrastructure.Database
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Class>   Classes  { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
