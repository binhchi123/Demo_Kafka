namespace CourseAPI.Infrastructure.Seedings
{
    public class CourseDayMemorySeedAsync
    {
        public async Task SeedAsync(CourseDayMemory memory, CourseDbContext context)
        {
            var courseDays = await context.CourseDays.ToListAsync();
            if (courseDays.Count > 0)
            {
                foreach (var cd in courseDays)
                {
                    memory.CourseDaysMemory.Add(cd.CourseDayId.ToString(), cd);
                }
            }
        }
    }
}
