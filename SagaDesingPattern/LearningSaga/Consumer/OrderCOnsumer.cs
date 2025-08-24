using MassTransit;
using SharedModel;
using System.Text.Json;

namespace Consumer
{
    public class OrderCOnsumer : IConsumer<Order>
    {
        private readonly ILogger<OrderCOnsumer> _logger;

        public OrderCOnsumer(ILogger<OrderCOnsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
            _logger.LogInformation($"Recived info :{JsonSerializer.Serialize(context.Message)}");
        }
    }
}
