using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Producer
{
    public static class FanoutExchangePublisher
    {
        public static void Publsih(IModel channel)
        {
            var timeToLive = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare(exchange: "demo-fanout-exchange",
                type: ExchangeType.Fanout,
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
                // it doesnot matter even we use routhing and headres while publishing messages to rabbitMq server.
                channel.BasicPublish(exchange: "demo-fanout-exchange",
                    routingKey: "account.validate",
                    basicProperties: propertis,
                    body: body);

                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
