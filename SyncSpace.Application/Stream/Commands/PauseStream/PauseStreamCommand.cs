using MediatR;

namespace SyncSpace.Application.Stream.Commands.PauseStream;

public class PauseStreamCommand(string roomId):IRequest
{
    public string RoomId { get; set; } = roomId;
}
