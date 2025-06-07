using InventoryService.API.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("inventory-service", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);

            e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(2)));
            e.UseCircuitBreaker(cb =>
            {
                cb.TrackingPeriod = TimeSpan.FromSeconds(30);
                //cb.TripThreshold = 0.25;
                cb.TripThreshold = 1;
                cb.ActiveThreshold = 4;
                cb.ResetInterval = TimeSpan.FromMinutes(1);
            });
        });

        //cfg.ReceiveEndpoint("inventory-service", e =>
        //{
        //    e.ConfigureConsumer<OrderCreatedConsumer>(context);
        //});
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
