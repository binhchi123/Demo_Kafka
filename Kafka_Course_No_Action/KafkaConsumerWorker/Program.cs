var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService(sp =>
{
    var logger = sp.GetRequiredService<ILogger<Worker>>();
    var config = sp.GetRequiredService<IConfiguration>();
    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new Worker(logger, config, serviceScopeFactory);
});

builder.Services.AddDbContext<CourseDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OrclDb"));
});
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseDayRepository, CourseDayRepository>();
builder.Services.AddScoped<ICourseDayService, CourseDayService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
var host = builder.Build();
host.Run();
