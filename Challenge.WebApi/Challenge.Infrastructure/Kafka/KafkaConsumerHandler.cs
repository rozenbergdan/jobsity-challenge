using Challenge.Domain.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Kafka
{
    public class KafkaConsumerHandler : BackgroundService
    {
        private readonly IOptions<KafkaSettings> kafkasettings;

        public KafkaConsumerHandler(IOptions<KafkaSettings> kafkasettings)
        {
            this.kafkasettings = kafkasettings;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = kafkasettings.Value.GroupId,
                BootstrapServers = kafkasettings.Value.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var builder = new ConsumerBuilder<Ignore,
                string>(config).Build())
            {
                builder.Subscribe(kafkasettings.Value.Topic);
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
            return Task.CompletedTask;
        }
    }
}

