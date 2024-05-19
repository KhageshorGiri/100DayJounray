using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_consumer
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(exchange: "demo-topic-exchange",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false
                );
            channel.QueueDeclare(queue: "TopictExchangeDemo",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            //Binding queue with exchange
            channel.QueueBind(queue: "TopictExchangeDemo",
                exchange: "demo-topic-exchange",
                routingKey: "account.*"); // for routing key, we can define pattern to match publisher routhing key

            // to define prefetch count
            // by this setting, the consumer can fetch only 10 message
            channel.BasicQos(prefetchSize: 0,
                prefetchCount: 10,
                global: false);

            // Consume message
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) => {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queue: "TopictExchangeDemo", autoAck: true, consumer: consumer);
        }
    }
}
