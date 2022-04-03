using Challenge.Domain.Entities;
using Challenge.Domain.Options;
using Challenge.Service.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
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

        public void Send(Message message)
        {
            using (var producer =
                 new ProducerBuilder<Null, Message>(config).Build())
            {
                try
                {
                    producer.Produce(kafkasettings.Value.Topic, new Message<Null, Message> { Value = message });    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
        }
    }
}
