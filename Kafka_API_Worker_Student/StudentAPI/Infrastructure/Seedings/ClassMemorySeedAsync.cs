namespace StudentAPI.Infrastructure.Seedings
{
    public class ClassMemorySeedAsync
    {
        public async Task SeedAsync(ClassMemory memory, StudentDbContext context)
        {
            var classes = await context.Classes.ToListAsync();
            if (classes.Count > 0)
            {
                foreach (var c in classes)
                {
                    memory.ClassesMemory.Add(c.ClassId.ToString(), c);
                }
            }
        }
    }
}
