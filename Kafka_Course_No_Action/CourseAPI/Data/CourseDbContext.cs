namespace CourseAPI.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDay> CoursesDays { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
