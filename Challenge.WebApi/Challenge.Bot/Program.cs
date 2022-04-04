// See https://aka.ms/new-console-template for more information

using Challenge.Domain.Entities;
using Challenge.Service.Implementations;
using Challenge.Service.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

IConfiguration settings = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", false, true)
               .Build();

var serviceProvider = new ServiceCollection()
        .AddScoped<IAuthentication, JwtAuthentication>()
        .AddScoped<IStockService, StockService>()
        .AddSingleton(settings)
        .BuildServiceProvider();
        



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
            serviceProvider.GetService<IStockService>().RetrieveStockMessage(JsonConvert.DeserializeObject<Message>(consumer.Message.Value)) ;
        }
    }
    catch (Exception)
    {
        builder.Close();
    }
}

