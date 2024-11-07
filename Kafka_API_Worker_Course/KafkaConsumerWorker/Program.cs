var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<CourseDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OrclDb"));
});
var host = builder.Build();
host.Run();
