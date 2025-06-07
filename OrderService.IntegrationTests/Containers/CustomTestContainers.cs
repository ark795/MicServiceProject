using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.RabbitMq;

namespace OrderService.IntegrationTests.Containers;

public class CustomTestContainers : IAsyncLifetime
{
    public RabbitMqContainer RabbitMq { get; private set; }

    public async Task InitializeAsync()
    {
        RabbitMq = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithName("test-rabbitmq")
            .WithPortBinding(5672, true)
            .WithPortBinding(15672, true)
            .WithUsername("guest")
            .WithPassword("guest")
            .Build();

        await RabbitMq.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await RabbitMq.DisposeAsync();
    }
}

//public class CustomTestContainers : IAsyncLifetime
//{
//    public RabbitMqContainer RabbitMq { get; private set; }

//    public async Task InitializeAsync()
//    {
//        RabbitMq = new RabbitMqBuilder()
//            .WithImage("rabbitmq:3-management")
//            .WithPortBinding(5672, true)
//            .WithPortBinding(15672, true)
//            .WithCleanUp(true)
//            .Build();

//        await RabbitMq.StartAsync();
//    }

//    public async Task DisposeAsync()
//    {
//        await RabbitMq.StopAsync();
//    }
//}
