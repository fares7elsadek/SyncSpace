using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using SyncSpace.API.SignalR.Hubs;
using MediatR;
using SyncSpace.Application.ChatRoom.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization.Policy;
using SyncSpace.API.Helper;
using Microsoft.Extensions.DependencyInjection;
using SyncSpace.Application.ChatRoom.Dtos;

namespace SyncSpace.API.Controllers.Tests;

[TestClass()]
public class ChatControllerTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public ChatControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            });
        });
    }
    [Fact()]
    public async Task SendMessage_Should_SendMessageAndBroadcastToGroup()
    {
        // Arrange
        var hubContextMock = new Mock<IHubContext<StreamingHub>>();
        var clientsMock = new Mock<IHubClients>();
        var groupClientMock = new Mock<IClientProxy>();
        var groupManagerMock = new Mock<IGroupManager>();
        var mediatorMock = new Mock<IMediator>();

        string roomId = "test-room-id";
        string message = "test-message";

        // Mock setup
        mediatorMock
            .Setup(x => x.Send(It.IsAny<SendMessageCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MessageDto {
                MessageId = "test-message-id",
                Content = message,
                RoomId = roomId,
                SentAt = DateTime.UtcNow,
                UserId = "test-user-id"
            });

        hubContextMock.Setup(x => x.Groups).Returns(groupManagerMock.Object);
        hubContextMock.Setup(x => x.Clients).Returns(clientsMock.Object);
        clientsMock.Setup(client => client.Group(It.IsAny<string>())).Returns(groupClientMock.Object);

        var command = new SendMessageCommand
        {
            RoomId = roomId,
            Content = message
        };

        var controller = new ChatController(mediatorMock.Object, hubContextMock.Object);

        // Act
        var result = await controller.SendMessage(command);

        // Assert
        groupClientMock.Verify(c => c.SendCoreAsync("ReceiveMessage", It.IsAny<object[]>(), default), Times.Once);
    }


}