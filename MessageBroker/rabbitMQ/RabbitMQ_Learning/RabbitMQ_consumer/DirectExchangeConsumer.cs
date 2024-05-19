using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ_consumer
{
    public class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(exchange: "demo-direct-exchange",
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false
                );
            channel.QueueDeclare(queue:"DirectExchangeDemo",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null) ;

            //Binding queue with exchange
            channel.QueueBind(queue: "DirectExchangeDemo",
                exchange: "demo-direct-exchange", 
                routingKey: "account.init");

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

            channel.BasicConsume(queue: "DirectExchangeDemo", autoAck: true, consumer: consumer);

        }
    }
}
