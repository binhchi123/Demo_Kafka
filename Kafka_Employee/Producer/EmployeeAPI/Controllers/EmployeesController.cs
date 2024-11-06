using Confluent.Kafka;
using EmployeeAPI.Appllication.Models;
using EmployeeAPI.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmployeeDbContext            _context;
        public EmployeesController(ILogger<EmployeesController> logger, EmployeeDbContext context)
        {
            _logger  = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _logger.LogInformation("Requesting all employees");
            return await _context.Employees.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(string name, string surname)
        {
            var employee = new Employee(Guid.NewGuid(), name, surname);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var message = new Message<string, string>()
            {
                Key = employee.Id.ToString(),
                Value = JsonSerializer.Serialize(employee)
            };

            // client
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All
            };

            var producer = new ProducerBuilder<string, string>(config).Build();
            await producer.ProduceAsync("employee_topic", message);
            producer.Dispose();

            return Ok(employee);
        }
    }
}
