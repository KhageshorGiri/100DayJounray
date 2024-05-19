using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ_consumer
{
    public class RabbitMqHelper
    {
    }

    public class SingleProducerConsumerWithQueue
    {
        public void Consume()
        {
            var factory = new ConnectionFactory
            {
                // rabbitMq server url with usernaeme and password
                Uri = new Uri("amqp://guest:guest@localhost:5772")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue:"SinglePublisherConsumer_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Consume message
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) => {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queue: "SinglePublisherConsumer_queue", autoAck: true, consumer: consumer);

        }
    }


    // One producer produce message to mutiple consumer
    public static class QueueConsumer
    {
        public static void Consume()
        {
            var factory = new ConnectionFactory
            {
                // rabbitMq server url with usernaeme and password
                Uri = new Uri("amqp://guest:guest@localhost:5772")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("SingleProducerMultipleConsumner",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Consume message
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) => {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queue: "SingleProducerMultipleConsumner", autoAck: true, consumer: consumer);

        }
    }
}
