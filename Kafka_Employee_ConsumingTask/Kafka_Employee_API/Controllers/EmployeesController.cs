namespace Kafka_Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeProducer _producer;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeProducer producer, IEmployeeService employeeService)
        {
            _producer = producer;
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetAllEmployee()
        {
            return _employeeService.GetAllEmployee();
        }

        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee insertEmployee)
        {
            try
            {
                _employeeService.InsertEmployee(insertEmployee);
                return Ok("InsertEmployee message sent to Kafka topic 'employee-input'");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Employee updateEmployee)
        {
            try
            {
                _employeeService.UpdateEmployee(updateEmployee);
                return Ok("UpdateEmployee message sent to Kafka topic 'employee-input'");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([FromBody] int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return Ok("DeleteEmployee message sent to Kafka topic 'employee-input'");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
