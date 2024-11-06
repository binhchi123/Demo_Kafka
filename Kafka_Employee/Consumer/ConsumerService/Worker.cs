using Confluent.Kafka;
using ConsumerDatabase;
using ConsumerDatabase.Entities;
using ConsumerShared;
using System.Text.Json;

namespace ConsumerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly EmployeeReportDbContext _employeeReportDbContext;

        public Worker(ILogger<Worker> logger, EmployeeReportDbContext employeeReportDbContext)
        {
            _logger = logger;
            _employeeReportDbContext = employeeReportDbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var config = new ConsumerConfig()
                {
                    BootstrapServers = "localhost:9092",
                    ClientId = "myconsumerclient",
                    GroupId = "employeeConsumerGroup",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using (var consumer = new ConsumerBuilder<string, string>(config).Build())
                {
                    consumer.Subscribe("employee_topic");
                    var consumerData = consumer.Consume(TimeSpan.FromSeconds(5));
                    if (consumerData != null)
                    {
                        var employee = JsonSerializer.Deserialize<Employee>(consumerData.Message.Value);
                        _logger.LogInformation($"Consuming {employee}", employee);
                        var employeeReport = new EmployeeReport
                        (
                            Guid.NewGuid(),
                            employee.Id,
                            employee.Name,
                            employee.Surname
                        );
                        _employeeReportDbContext.Reports.Add(employeeReport);
                        await _employeeReportDbContext.SaveChangesAsync();
                    }
                    else
                        _logger.LogInformation("Nothing to consumer");
                }
            }
        }
    }
}
