using MassTransit;
using SharedModel;
using System.Text.Json;

namespace Consumer;

public class OrderConsumer : IConsumer<Order>
{
    public async Task Consume(ConsumeContext<Order> context)
    {
        var jsonMessage = JsonSerializer.Serialize(context);
        Console.WriteLine(jsonMessage);

        await Task.CompletedTask;
    }
}
