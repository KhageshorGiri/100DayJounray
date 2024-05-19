using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Producer
{
    public static class HeadersExchangeProducer
    {
        public static void Publsih(IModel channel)
        {
            var timeToLive = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare(exchange: "demo-header-exchange",
                type: ExchangeType.Headers,
                durable: true,
                autoDelete: false,
                arguments: timeToLive
                );

            int count = 0;
            while (count <= 10)
            {
                var message = new { Name = "Message Producer", Message = $"hello form producer,  Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                // create a properties to set headers 
                var propertis = channel.CreateBasicProperties();
                propertis.Headers = new Dictionary<string, object>
                    {
                        {"account", "new" },
                        {"action", "register" }
                    };

                // publish message
                channel.BasicPublish(exchange: "demo-header-exchange",
                    routingKey: string.Empty,
                    basicProperties: propertis,
                    body: body);

                count++;
                Thread.Sleep(1000);
            }
        }
    }
}

