
using Consumer;
using MassTransit;

var bus = Bus.Factory.CreateUsingRabbitMq(config =>
{
    config.ReceiveEndpoint("temp-order-queue", c =>
    {
        c.Consumer<OrderConsumer>();
    });
});

await bus.StartAsync(new CancellationToken());

try
{
    Console.WriteLine("Listing from producer...");
    await Task.Run(() => Console.ReadLine());
}
finally
{
    await bus.StopAsync();
}