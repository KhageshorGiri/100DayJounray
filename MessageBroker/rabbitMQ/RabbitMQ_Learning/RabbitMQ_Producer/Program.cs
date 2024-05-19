// See https://aka.ms/new-console-template for more information
using RabbitMQ_Producer;

Console.WriteLine("Hello, World!");
Console.WriteLine("Publising Message...");
var rabbitMqService = new SingleProducerConsumerWithQueue();
rabbitMqService.Publish();
Console.WriteLine("Message Published.");