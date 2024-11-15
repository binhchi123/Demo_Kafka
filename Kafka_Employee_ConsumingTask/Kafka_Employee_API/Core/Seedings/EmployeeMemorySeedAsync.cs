namespace Kafka_Employee_API.Core.Seedings
{
    public class EmployeeMemorySeedAsync
    {
        public async Task SeedAsync(EmployeeMemory memory, EmployeeDbContext context)
        {
            var employees = await context.Employees.ToListAsync();
            if (employees.Count > 0)
            {
                foreach (var e in employees)
                {
                    memory.EmployeesMemory.Add(e.EmployeeId.ToString(), e);
                }
            }
        }
    }
}
