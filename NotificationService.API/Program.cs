using MassTransit;
using NotificationService.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MassTransit + RabbitMQ
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

        cfg.ReceiveEndpoint("notification-service", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);

            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            e.UseCircuitBreaker(cb =>
            {
                cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                //cb.TripThreshold = 0.2;
                cb.TripThreshold = 1;
                cb.ActiveThreshold = 5;
                cb.ResetInterval = TimeSpan.FromMinutes(3);
            });
        });
    });

    //x.UsingRabbitMq((context, cfg) =>
    //{
    //    cfg.Host("rabbitmq", "/", h =>
    //    {
    //        h.Username("guest");
    //        h.Password("guest");
    //    });

    //    cfg.ReceiveEndpoint("your-service-name", e =>
    //    {
    //        e.ConfigureConsumer<OrderCreatedConsumer>(context);

    //        // ?? Retry Policy
    //        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

    //        // ?? Circuit Breaker
    //        e.UseCircuitBreaker(cb =>
    //        {
    //            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
    //            cb.TripThreshold = 0.15; // 15% failures
    //            cb.ActiveThreshold = 10; // at least 10 messages
    //            cb.ResetInterval = TimeSpan.FromMinutes(5);
    //        });

    //        // ?? Fault Observer (Optional)
    //        e.UseInMemoryOutbox(); // ensures exactly-once publishing
    //    });
    //});

    //x.UsingRabbitMq((context, cfg) =>
    //{
    //    cfg.Host("rabbitmq", "/", h =>
    //    {
    //        h.Username("guest");
    //        h.Password("guest");
    //    });

    //    cfg.ReceiveEndpoint("notification-service", e =>
    //    {
    //        e.ConfigureConsumer<OrderCreatedConsumer>(context);
    //    });
    //});
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
