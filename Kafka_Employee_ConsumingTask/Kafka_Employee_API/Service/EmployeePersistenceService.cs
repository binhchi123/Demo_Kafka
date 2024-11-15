namespace Kafka_Employee_API.Service
{
    public class EmployeePersistenceService : IEmployeePersistenceService
    {
        private readonly IServiceScopeFactory           _scopeFactory;
        private readonly ILogger<EmployeeConsumingTask> _logger;

        public EmployeePersistenceService(IServiceScopeFactory scopeFactory, ILogger<EmployeeConsumingTask> logger)
        {
            _scopeFactory = scopeFactory;
            _logger       = logger;
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
                try
                {
                    var employee = await dbContext.Employees.FindAsync(id);
                    if(employee == null)
                    {
                        _logger.LogError($"Employee with ID {id} not found");
                        return null;
                    }
                    dbContext.Employees.Remove(employee);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Employee with ID {id} deleted");
                    return employee;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<Employee> InsertEmployee(Employee insertEmployee)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
                try
                {
                    dbContext.Employees.Add(insertEmployee);
                    await dbContext.SaveChangesAsync ();                   
                }
                catch(Exception)
                {
                    throw;
                }
            }
            return insertEmployee;
        }

        public async Task<Employee> UpdateEmployee(Employee updateEmployee)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
                try
                {
                    var employee = await dbContext.Employees.FindAsync(updateEmployee.EmployeeId);
                    if (employee == null)
                    {
                        _logger.LogError($"Employee with ID {updateEmployee.EmployeeId} not found");
                        return null;
                    }
                    employee.Name        = updateEmployee.Name;
                    employee.Birthday    = updateEmployee.Birthday;
                    employee.PhoneNumber = updateEmployee.PhoneNumber;
                    employee.Address     = updateEmployee.Address;
                    employee.Email       = updateEmployee.Email;
                    dbContext.Employees.Update(employee);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Employee with ID {updateEmployee.EmployeeId} updated");
                    return employee;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
