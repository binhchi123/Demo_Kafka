namespace Kafka_Employee_API.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly EmployeeMemory           _memory;
        private readonly IKafkaProducerManager    _producerManager;

        public EmployeeService(ILogger<EmployeeService> logger, EmployeeMemory memory, IKafkaProducerManager producerManager)
        {          
            _logger          = logger;
            _memory          = memory;
            _producerManager = producerManager;
        }
        public Employee DeleteEmployee(int id)
        {
            try
            {
                if(_memory.EmployeesMemory.TryGetValue(id.ToString(), out var employee)){
                    _memory.EmployeesMemory.Remove(id.ToString());
                    var kafkaProduce = _producerManager.GetProducer<string, string>("1");
                    var message = new Message<string, string>
                    {
                        Value = JsonSerializer.Serialize(employee),
                        Headers = new Headers
                        {
                            {"eventname", Encoding.UTF8.GetBytes("DeleteEmployee") }
                        }
                    };
                    kafkaProduce.Produce(message);
                    return employee;
                }
                else
                {
                    _logger.LogError($"Employee with ID {id} not found");
                    return null;
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public List<Employee> GetAllEmployee()
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                employeeList = _memory.EmployeesMemory.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return employeeList;
        }

        public Employee InsertEmployee(Employee insertEmployee)
        {
            try
            {
                _memory.EmployeesMemory.Add(insertEmployee.EmployeeId.ToString(), insertEmployee);
                var kafkaProduce = _producerManager.GetProducer<string, string>("1");
                var message = new Message<string, string>
                {
                    Value= JsonSerializer.Serialize(insertEmployee),
                    Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes("InsertEmployee") }
                    }
                };
                kafkaProduce.Produce(message);
                return insertEmployee;
            }
            catch( Exception ex )
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Employee UpdateEmployee(Employee updateEmployee)
        {
            try
            {
                if (_memory.EmployeesMemory.TryGetValue(updateEmployee.EmployeeId.ToString(), out var existingEmployee))
                {
                    _memory.EmployeesMemory[updateEmployee.EmployeeId.ToString()] = updateEmployee;
                    var kafkaProduce = _producerManager.GetProducer<string, string>("1");
                    var message = new Message<string, string>
                    {
                        Value = JsonSerializer.Serialize(updateEmployee),
                        Headers = new Headers
                        {
                            {"eventname", Encoding.UTF8.GetBytes("UpdateEmployee") }
                        }
                    };
                    kafkaProduce.Produce(message);
                    return updateEmployee;
                }
                else
                {
                    _logger.LogError($"Employee with ID {updateEmployee.EmployeeId} not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
