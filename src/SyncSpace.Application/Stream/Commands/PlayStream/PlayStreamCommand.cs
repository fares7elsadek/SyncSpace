using MediatR;

namespace SyncSpace.Application.Stream.Commands.PlayStream;

public class PlayStreamCommand(string roomId):IRequest
{
    public string RoomId { get; set; } = roomId;
}
