namespace Kafka_Employee_API.Producer
{
    public interface IEmployeeProducer
    {
        Task ProduceInsertEmployeeAsync(InsertEmployeeRequest employee);
        Task ProduceUpdateEmployeeAsync(UpdateEmployeeRequest employee);
        Task ProduceDeleteEmployeeAsync(DeleteEmployeeRequest employee);
    }
}
