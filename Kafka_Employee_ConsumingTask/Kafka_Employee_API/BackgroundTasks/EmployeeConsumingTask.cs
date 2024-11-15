namespace Kafka_Employee_API.BackgroundTasks
{
    public class EmployeeConsumingTask : IConsumingTask<string, string>
    {
        private readonly ILogger<EmployeeConsumingTask> _logger;
        private readonly IEmployeePersistenceService    _employeeService;

        public EmployeeConsumingTask(ILogger<EmployeeConsumingTask> logger, IEmployeePersistenceService employeeService)
        {
            _logger          = logger;
            _employeeService = employeeService;
        }
        public async Task ExecuteAsync(ConsumeResult<string, string> result)
        {
            var employeeEvent = result.Message.Headers.FirstOrDefault(h => h.Key == "eventname")?.GetValueBytes();
            if (employeeEvent == null) return;
            var eventString = Encoding.UTF8.GetString(employeeEvent);
            _logger.LogInformation($"Consuming message from topic: {result.Topic}, partition: {result.Partition}, offset: {result.Offset}, key: {result.Message.Key}");
            switch (eventString)
            {
                case "InsertEmployee":
                    var employee = JsonSerializer.Deserialize<Employee>(result.Message.Value);
                    await _employeeService.InsertEmployee(employee);
                    break;
                case "UpdateEmployee":
                    var emp = JsonSerializer.Deserialize<UpdateEmployeeDTO>(result.Message.Value);
                    await _employeeService.UpdateEmployee(new Employee
                    {
                        EmployeeId  = emp.EmployeeId,
                        Name        = emp.Name,
                        Birthday    = emp.Birthday,
                        PhoneNumber = emp.PhoneNumber,
                        Address     = emp.Address,
                        Email       = emp.Email,
                    });
                    break;
                case "DeleteEmployee":
                    var request = JsonSerializer.Deserialize<DeleteEmployeeRequest>(result.Message.Value);
                    _logger.LogInformation($"Deleting student with ID {request.EmployeeId}");
                    await _employeeService.DeleteEmployee(request.EmployeeId);
                    break;
                default:
                    _logger.LogWarning("Received unknown event: {Event}", employeeEvent);
                    break;
            }
        }
    }
}
