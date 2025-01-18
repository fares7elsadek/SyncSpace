//using MediatR;
//using Microsoft.AspNetCore.Authorization.Policy;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using SyncSpace.API.Helper;
//using SyncSpace.API.SignalR.Hubs;
//using SyncSpace.Application.Stream.Commands.ChangeStream;
//using SyncSpace.Application.Stream.Commands.PauseStream;
//using SyncSpace.Application.Stream.Commands.PlayStream;
//using SyncSpace.Application.Stream.Commands.StartStream;
//using SyncSpace.Application.Stream.Commands.SyncStream;
//using SyncSpace.Application.Stream.Queries.GetStreamDetails;
//using SyncSpace.Domain.Helpers;
//using Xunit;

//namespace SyncSpace.API.Controllers.Tests;

//public class StreamControllerTests : IClassFixture<WebApplicationFactory<Program>>
//{
//    private readonly WebApplicationFactory<Program> _factory;
//    private readonly Mock<IHubContext<StreamingHub>> _hubContextMock;
//    private readonly Mock<IHubClients> _clientsMock;
//    private readonly Mock<IClientProxy> _clientProxyMock;
//    private readonly Mock<IMediator> _mediatorMock;
//    private readonly StreamController _controller;
//    public StreamControllerTests(WebApplicationFactory<Program> factory)
//    {
//        _hubContextMock = new Mock<IHubContext<StreamingHub>>();
//        _clientsMock = new Mock<IHubClients>();
//        _clientProxyMock = new Mock<IClientProxy>();
//        _mediatorMock = new Mock<IMediator>();

//        _hubContextMock.Setup(x => x.Clients).Returns(_clientsMock.Object);
//        _clientsMock.Setup(x => x.Group(It.IsAny<string>())).Returns(_clientProxyMock.Object);

//        _controller = new StreamController(_mediatorMock.Object, _hubContextMock.Object);
//        _factory = factory.WithWebHostBuilder(builder =>
//        {
//            builder.ConfigureServices(services =>
//            {
//                services.AddSingleton<IPolicyEvaluator,FakePolicyEvaluator>();
//            });
//        });
//    }

//    [Fact]
//    public async Task StartStream_Should_SendStreamStartedSignal()
//    {
//        // Arrange
//        var roomId = "test-room-id";
//        var command = new StartStreamCommand { RoomId = roomId, VideoUrl = "http://video-url.com" };

//        // Act
//        var result = await _controller.StartStream(roomId, command);

//        // Assert
//        _clientProxyMock.Verify(
//            x => x.SendCoreAsync("StreamStarted", It.Is<object[]>(o => o[0].ToString() == command.VideoUrl), default),
//            Times.Once);
//    }

//    [Fact]
//    public async Task ChangeStream_Should_SendStreamChangedSignal()
//    {
//        // Arrange
//        var roomId = "test-room-id";
//        var command = new ChangeStreamCommand { RoomId = roomId, VideoUrl = "http://new-video-url.com" };

//        // Act
//        var result = await _controller.ChangeStream(roomId, command);

//        // Assert
//        _clientProxyMock.Verify(
//            x => x.SendCoreAsync("StreamChanged", It.Is<object[]>(o => o[0].ToString() == command.VideoUrl), default),
//            Times.Once);
//    }

//    [Fact]
//    public async Task PauseStream_Should_SendStreamPausedSignal()
//    {
//        // Arrange
//        var roomId = "test-room-id";
//        var command = new PauseStreamCommand(roomId);

//        // Act
//        var result = await _controller.PauseStream(roomId);

//        // Assert
//        _clientProxyMock.Verify(x => x.SendCoreAsync("StreamPaused", It.IsAny<object[]>(), default), Times.Once);
       
//    }

//    [Fact]
//    public async Task PlayStream_Should_SendStreamResumedSignal()
//    {
//        // Arrange
//        var roomId = "test-room-id";
//        var command = new PlayStreamCommand(roomId);

//        // Act
//        var result = await _controller.PlayStream(roomId);

//        // Assert
//        _clientProxyMock.Verify(x => x.SendCoreAsync("StreamResumed", It.IsAny<object[]>(), default), Times.Once);
//    }

//    [Fact]
//    public async Task SyncStream_Should_SendStreamSyncedSignal()
//    {
//        // Arrange
//        var roomId = "test-room-id";
//        var command = new SyncStreamCommand { RoomId = roomId, Time = TimeSpan.FromSeconds(500) };

//        // Act
//        var result = await _controller.SyncStream(roomId, command);

//        // Assert
//        _clientProxyMock.Verify(
//            x => x.SendCoreAsync("StreamSynced", It.Is<object[]>(o => (TimeSpan)o[0] == command.Time), default),
//            Times.Once);
//    }

    
//}