var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StudentDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OrclDb"));
});

builder.Services.AddSingleton<IClassService, ClassService>();
builder.Services.AddSingleton<IStudentService, StudentService>();

builder.Services.AddSingleton<ClassMemory>();
builder.Services.AddSingleton<StudentMemory>();

builder.Services.AddSingleton<IProducer, StudentProducer>();
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

app.LoadDataToMemory<ClassMemory, StudentDbContext>((classInMem, dbContext) =>
{
    new ClassMemorySeedAsync().SeedAsync(classInMem, dbContext).Wait();
});

app.LoadDataToMemory<StudentMemory, StudentDbContext>((studentInMem, dbContext) =>
{
    new StudentMemorySeedAsync().SeedAsync(studentInMem, dbContext).Wait();
});

app.Run();
