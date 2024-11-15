
namespace Kafka_Employee_API.Service
{
    public class EmployeePersistenceService : IEmployeePersistenceService
    {
        private readonly ILogger<EmployeePersistenceService> _logger;
        private readonly IServiceScopeFactory                _scopeFactory;

        public EmployeePersistenceService(ILogger<EmployeePersistenceService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger       = logger;
            _scopeFactory = scopeFactory;
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
                try
                {
                    var employee = await dbContext.Employees.FindAsync(id);
                    if (employee == null)
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

        public Task<List<Employee>> GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> InsertEmployee(Employee insertEmployee)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<EmployeeDbContext>();
                try
                {
                    dbContext.Add(insertEmployee);
                    await dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Employee with ID {insertEmployee.EmployeeId} inserted");
                    return insertEmployee;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }       
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
                    employee.Name = updateEmployee.Name;
                    employee.Birthday = updateEmployee.Birthday;
                    employee.PhoneNumber = updateEmployee.PhoneNumber;
                    employee.Address = updateEmployee.Address;
                    employee.Email = updateEmployee.Email;
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
