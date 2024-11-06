using ConsumerDatabase;
using ConsumerService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<EmployeeReportDbContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("OrclDb")), ServiceLifetime.Singleton);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
