namespace CourseAPI.Infrastructure.Seedings
{
    public class CourseMemorySeedAsync
    {
        public async Task SeedAsync(CourseMemory memory, CourseDbContext context)
        {
            var courses = await context.Courses.ToListAsync();
            if (courses.Count > 0)
            {
                foreach (var c in courses)
                {
                    memory.CoursesMemory.Add(c.CourseId.ToString(), c);
                }
            }
        }
    }
}
