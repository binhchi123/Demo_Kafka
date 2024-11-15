namespace Kafka_Employee_API.Core.IService
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployee();
        Employee InsertEmployee(Employee insertEmployee);
        Employee UpdateEmployee(Employee updateEmployee);
        Employee DeleteEmployee(int id);
    }
}
