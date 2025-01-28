using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncSpace.API.Controllers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SyncSpace.API.Helper;
using FluentAssertions;
using Xunit;
using System.Net;
using MediatR;
using Moq;
using Microsoft.AspNetCore.SignalR;
using SyncSpace.API.SignalR.Hubs;
using SyncSpace.Application.Room.Commands.JoinRoom;
using SyncSpace.Application.Room.Commands.LeaveRoom;

namespace SyncSpace.API.Controllers.Tests
{
    [TestClass()]
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

            _ = _factory.CreateClient();
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

        [Fact()]
        public async Task JoinRoom_Should_AddUserToGroupAndBroadcastMessage()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var hubContextMock = new Mock<IHubContext<StreamingHub>>();
            var groupManagerMock = new Mock<IGroupManager>();
            var clientsMock = new Mock<IHubClients>();
            var groupClientMock = new Mock<IClientProxy>();

            string connectionId = "test-connection-id";
            string roomId = "test-room-id";

            hubContextMock.Setup(hub => hub.Groups).Returns(groupManagerMock.Object);
            hubContextMock.Setup(hub => hub.Clients).Returns(clientsMock.Object);
            clientsMock.Setup(client => client.Group(It.IsAny<string>())).Returns(groupClientMock.Object);

            var command = new JoinRoomCommand { RoomId = roomId, ConnectionId = connectionId };
            var controller = new RoomController(mediatorMock.Object, hubContextMock.Object);

            // Act
            var result = await controller.JoinRoom(roomId, command);

            // Assert
            groupManagerMock.Verify(g => g.AddToGroupAsync(connectionId, roomId, default), Times.Once);
            groupClientMock.Verify(c => c.SendCoreAsync("UserJoined", It.IsAny<object[]>(), default), Times.Once);

        }

        [Fact()]
        public async Task LeaveRoom_Should_RemoveUserFromGroupAndBroadcastMessage()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var hubContextMock = new Mock<IHubContext<StreamingHub>>();
            var groupManagerMock = new Mock<IGroupManager>();
            var clientsMock = new Mock<IHubClients>();
            var groupClientMock = new Mock<IClientProxy>();

            string connectionId = "test-connection-id";
            string roomId = "test-room-id";

            hubContextMock.Setup(hub => hub.Groups).Returns(groupManagerMock.Object);
            hubContextMock.Setup(hub => hub.Clients).Returns(clientsMock.Object);
            clientsMock.Setup(client => client.Group(It.IsAny<string>())).Returns(groupClientMock.Object);

            var command = new LeaveRoomCommand { RoomId = roomId, ConnectionId = connectionId };
            var controller = new RoomController(mediatorMock.Object, hubContextMock.Object);

            // Act
            var result = await controller.LeaveRoom(roomId, command);

            // Assert
            groupManagerMock.Verify(g => g.RemoveFromGroupAsync(connectionId, roomId, default), Times.Once);
            groupClientMock.Verify(c => c.SendCoreAsync("UserLeft", It.IsAny<object[]>(), default), Times.Once);

        }
    }
}
