using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace RabbitMQ_Producer
{
    public class RbbitMqHelper
    {
    }

    public class SingleProducerConsumerWithQueue
    {
        public void Publish()
        {
            var factory = new ConnectionFactory
            {
                // rabbitMq server url with usernaeme and password
                Uri = new Uri("amqp://guest:guest@localhost:5772")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("SinglePublisherConsumer_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = new { Name = "Message Producer", Message = "hello form producer" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            // publish message
            channel.BasicPublish(exchange: "", routingKey: "", basicProperties: null, body: body);
        }
    }

    // One producer produce message to mutiple consumer
    public static class QueueProducer
    {
        public static void Publish()
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

            int count = 0;
            while (count <= 10)
            {
                var message = new { Name = "Message Producer", Message = "hello form producer" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                // publish message
                channel.BasicPublish(exchange: "", routingKey: "", basicProperties: null, body: body);

                count++;
                Thread.Sleep(1000);
            }

        }
    }
}
