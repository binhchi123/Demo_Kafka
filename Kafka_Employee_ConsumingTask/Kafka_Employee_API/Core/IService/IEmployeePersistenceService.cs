namespace Kafka_Employee_API.Core.IService
{
    public interface IEmployeePersistenceService
    {
        Task<Employee> InsertEmployee(Employee insertEmployee);
        Task<Employee> UpdateEmployee(Employee updateEmployee);
        Task<Employee> DeleteEmployee(int id);
    }
}
