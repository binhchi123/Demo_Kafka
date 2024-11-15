namespace Kafka_Employee_API.Extensions
{
    public static class AppConfiguration
    {
        public static IConfiguration Configuration { get; set; }

        public static AppSetting LoadAppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
            return AppSetting.MapValues(Configuration);
        }
    }
}
