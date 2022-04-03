// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

var settings = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", false, true)
               .Build();

var config = new ConsumerConfig
{
    GroupId = settings["KafkaSettings:GroupId"],
    BootstrapServers = settings["KafkaSettings:BootstrapServers"],
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var builder = new ConsumerBuilder<string, string>(config).Build())
{
    builder.Subscribe(settings["KafkaSettings:Topic"]);
    var cancelToken = new CancellationTokenSource();
    try
    {
        while (true)
        {
            var consumer = builder.Consume(cancelToken.Token);
            Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
        }
    }
    catch (Exception)
    {
        builder.Close();
    }
}

