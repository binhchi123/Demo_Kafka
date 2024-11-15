namespace Kafka_Employee_API.Setting
{
    public class AppSetting
    {
        public string BootstrapServers { get; set; }
        public ConsumerSetting[] ConsumerSettings { get; set; }
        public ProducerSetting[] ProducerSettings { get; set; }

        public static AppSetting MapValues(IConfiguration configuration)
        {
            var bootstrapServers = configuration[nameof(BootstrapServers)];

            var consumerConfigurations = configuration.GetSection(nameof(ConsumerSettings)).GetChildren();
            var consumerSettings = new List<ConsumerSetting>();

            var producerConfigurations = configuration.GetSection(nameof(ProducerSettings)).GetChildren();
            var producerSettings = new List<ProducerSetting>();

            foreach (var producerConfiguration in producerConfigurations)
            {
                var producerSetting = ProducerSetting.MapValue(producerConfiguration, bootstrapServers);
                if (!producerSettings.Contains(producerSetting)) producerSettings.Add(producerSetting);
            }

            foreach (var consumerConfiguration in consumerConfigurations)
            {
                var consumerSetting= ConsumerSetting.MapValue(consumerConfiguration, bootstrapServers);
                if (!consumerSettings.Contains(consumerSetting)) consumerSettings.Add(consumerSetting);
            }

            var setting = new AppSetting
            {
                BootstrapServers = bootstrapServers,
                ConsumerSettings = consumerSettings.ToArray(),
                ProducerSettings = producerSettings.ToArray()
            };
            return setting;
        }

        public ConsumerSetting GetConsumerSetting(string id)
            => ConsumerSettings.FirstOrDefault(ConsumerSetting => ConsumerSetting.Id.Equals(id));

        public ProducerSetting GetProducerSetting(string id)
           => ProducerSettings.FirstOrDefault(ProducerSetting => ProducerSetting.Id.Equals(id));
    }
}
