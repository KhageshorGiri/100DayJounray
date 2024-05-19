using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Producer
{
    public static class DirectExchangePublisher
    {
        public static void Publsih(IModel channel)
        {
            var timeToLive = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare(exchange:"demo-direct-exchange",
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                arguments: timeToLive
                );

            int count = 0;
            while (count <= 10)
            {
                var message = new { Name = "Message Producer", Message = $"hello form producer,  Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                // publish message
                channel.BasicPublish(exchange: "demo-direct-exchange",
                    routingKey: "account.init", 
                    basicProperties: null, 
                    body: body);

                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
