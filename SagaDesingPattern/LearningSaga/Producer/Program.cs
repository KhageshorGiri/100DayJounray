

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Config from scratch
//var bus = Bus.Factory.CreateUsingRabbitMq(config =>
//{
//    config.Host("amqp://guest:guest@localhost:5672");
//    config.ReceiveEndpoint("temp-order-queue", c =>
//    {
//        c.Handler<Order>(ctx =>
//        {
//            return Console.Out.WriteAsync(ctx.Message.Name);
//        });
//    });
//});

//await bus.StartAsync();

//await bus.Publish(new Order { Id = 1, Name = "Test Order Name" });

app.Run();
