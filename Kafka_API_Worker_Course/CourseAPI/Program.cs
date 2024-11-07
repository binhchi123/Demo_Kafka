using CourseAPI.Extensions;
using CourseAPI.Infrastructure.Seedings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CourseDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OrclDb"));
});

builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<ICourseDayService, CourseDayService>();
builder.Services.AddSingleton<IStudentService, StudentService>();

builder.Services.AddSingleton<CourseMemory>();
builder.Services.AddSingleton<CourseDayMemory>();
builder.Services.AddSingleton<StudentMemory>();

builder.Services.AddSingleton<IProducer, CourseProducer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.LoadDataToMemory<CourseMemory, CourseDbContext>((courseInMem, dbContext) =>
{
    new CourseMemorySeedAsync().SeedAsync(courseInMem, dbContext).Wait();
});

app.LoadDataToMemory<CourseDayMemory, CourseDbContext>((courseDayInMem, dbContext) =>
{
    new CourseDayMemorySeedAsync().SeedAsync(courseDayInMem, dbContext).Wait();
});

app.LoadDataToMemory<StudentMemory, CourseDbContext>((studentInMem, dbContext) =>
{
    new StudentMemorySeedAsync().SeedAsync(studentInMem, dbContext).Wait();
});

app.Run();
