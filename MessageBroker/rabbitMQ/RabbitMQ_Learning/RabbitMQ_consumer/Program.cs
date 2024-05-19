// See https://aka.ms/new-console-template for more information
using RabbitMQ_consumer;

Console.WriteLine("Hello, World!");
Console.WriteLine("Consuming Message...");
var rabbitMqService = new SingleProducerConsumerWithQueue();
rabbitMqService.Consume();
Console.WriteLine("Message Consumed.");

Console.ReadLine();