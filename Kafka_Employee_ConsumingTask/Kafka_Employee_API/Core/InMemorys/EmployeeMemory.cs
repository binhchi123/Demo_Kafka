namespace Kafka_Employee_API.Core.InMemorys
{
    public class EmployeeMemory
    {
        public Dictionary<string, Employee> EmployeesMemory { get; set; }
        public EmployeeMemory()
        {
            EmployeesMemory = new Dictionary<string, Employee>();
        }
    }
}
