namespace Kafka_Employee_API.Core.IService
{
    public interface IEmployeePersistenceService
    {
        Task<List<Employee>> GetAllEmployee();
        Task<Employee> InsertEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int id);
    }
}
