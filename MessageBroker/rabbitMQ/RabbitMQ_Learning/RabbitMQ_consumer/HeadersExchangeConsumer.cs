using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_consumer
{
    public static class HeadersExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(exchange: "demo-header-exchange",
                type: ExchangeType.Headers,
                durable: true,
                autoDelete: false
                );

            channel.QueueDeclare(queue: "HeaderExchangeDemo",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Defining headers
            var headers = new Dictionary<string, object>
            {
                {"account", "new" },
                {"action", "register" }
            };

            //Binding queue with exchange
            channel.QueueBind(queue: "HeaderExchangeDemo",
                exchange: "demo-header-exchange",
                routingKey: string.Empty,
                arguments: headers);

            // to define prefetch count
            // by this setting, the consumer can fetch only 10 message
            channel.BasicQos(prefetchSize: 0, prefetchCount: 10, global: false);

            // Consume message
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) => {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queue: "HeaderExchangeDemo", autoAck: true, consumer: consumer);
        }
    }
}
