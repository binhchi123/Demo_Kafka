using KafkaConsumerWorker;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService(sp =>
{
    var logger = sp.GetRequiredService<ILogger<Worker>>();
    var config = sp.GetRequiredService<IConfiguration>();
    var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new Worker(logger, config, serviceScopeFactory);
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OrclDb"));
});
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
var host = builder.Build();
host.Run();
