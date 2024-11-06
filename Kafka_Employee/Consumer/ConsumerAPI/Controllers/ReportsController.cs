using ConsumerDatabase;
using ConsumerShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly EmployeeReportDbContext _context;
        public ReportsController(ILogger<ReportsController> logger, EmployeeReportDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _logger.LogInformation("Getting all consumer data");
            return await _context.Reports.Select(e => new Employee(e.EmployeeId, e.Name, e.Surname)).ToListAsync();
        }
    }
}
