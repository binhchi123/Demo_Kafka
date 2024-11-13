var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Load AppSettings
var appSetting = AppConfiguration.LoadAppSettings(configuration);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContextService
builder.Services.AddDbContextServices(configuration);

// Add Singleton Service
builder.Services.AddSingletonServices();

// Kafka Services
builder.Services.AddKafkaServices(appSetting);

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

// Seeding data into memory
app.LoadStudentMemoryData();

// Use kafkabus
app.UseCustomKafkaMessageBus();

app.Run();
