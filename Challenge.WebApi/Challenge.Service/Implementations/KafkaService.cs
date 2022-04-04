using Challenge.Domain.Entities;
using Challenge.Domain.Options;
using Challenge.Service.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Implementations
{
    public class KafkaService : IMessage
    {
        private readonly IOptions<KafkaSettings> kafkasettings;
        private readonly ProducerConfig config;
        public KafkaService(IOptions<KafkaSettings> kafkasettings)
        {
            this.kafkasettings = kafkasettings;
            config = new ProducerConfig() { 
                BootstrapServers = kafkasettings.Value.BootstrapServers,
            };
        }

        public async void Send(Message message)
        {
            using (var producer =
                 new ProducerBuilder<string, string>(config).Build())
            {
                try
                {
                    await producer.ProduceAsync(kafkasettings.Value.Topic, new Message<string, string> { Value = message.Content });    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
        }
    }
}
