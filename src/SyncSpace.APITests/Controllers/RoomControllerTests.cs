using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SyncSpace.API.Helper;
using FluentAssertions;
using Xunit;
using System.Net;

namespace SyncSpace.API.Controllers.Tests
{
    public class RoomControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RoomControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            });
        }

        [Fact]
        public async Task GetAllRoomsTest_ForValidationRequest_Returns200Ok()
        {
            // arrange 
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/api/room");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
