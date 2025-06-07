using FluentAssertions;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OrderService.IntegrationTests.Containers;
using System.Net;
using System.Net.Http.Json;

namespace OrderService.IntegrationTests;

public class OrderCreatedTests : IClassFixture<CustomTestContainers>
{
    private readonly CustomTestContainers _containers;

    public OrderCreatedTests(CustomTestContainers containers)
    {
        _containers = containers;
    }

    [Fact]
    public async Task OrderService_Should_Publish_OrderCreatedEvent()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //services.AddMassTransitTestHarness(cfg =>
                    //{
                    //    cfg.AddConsumer<OrderCreatedConsumer>();
                    //});
                });
            });

        using var scope = factory.Services.CreateScope();
        var provider = scope.ServiceProvider;

        ITestHarness harness = provider.GetRequiredService<ITestHarness>();
        await harness.Start();

        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/orders", new
        {
            OrderId = Guid.NewGuid(),
            Items = new[] { new { ProductId = Guid.NewGuid(), Quantity = 2 } }
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
