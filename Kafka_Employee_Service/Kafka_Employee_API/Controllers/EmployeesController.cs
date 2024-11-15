using Kafka_Employee_API.Producer;
using System.Text;
using System.Text.Json;

namespace Kafka_Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeProducer            _producer;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeProducer producer, ILogger<EmployeesController> logger)
        {
            _producer = producer;
            _logger   = logger;
        }

        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee insertEmployee)
        {
            try
            {
                var message = new Message<string, string>
                {
                    Value = JsonSerializer.Serialize(insertEmployee),
                    Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes("InsertEmployee") },
                    }
                };
                _producer.SendMessageAsync(message);
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
                var message = new Message<string, string>
                {
                    Value = JsonSerializer.Serialize(updateEmployee),
                    Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes("UpdateEmployee") },
                    }
                };
                _producer.SendMessageAsync(message);
                return Ok("UpdateEmployee message sent to Kafka topic 'employee-input'");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([FromBody] int id)
        {
            try
            {
                var message = new Message<string, string>
                {
                    Value = JsonSerializer.Serialize(new {EmployeeId = id}),
                    Headers = new Headers
                    {
                        {"eventname", Encoding.UTF8.GetBytes("DeleteEmployee") },
                    }
                };
                _producer.SendMessageAsync(message);
                return Ok("DeleteEmployee message sent to Kafka topic 'employee-input'");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
